using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.Concretes.Products;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Common.Interfaces.Service;
using Kuff.Common.Interfaces.UnitOfWork;
using Kuff.Dal;
using Kuff.Dal.Repositories.ProductRelated;
using Kuff.Service.Interfaces.ProductRelated;
using System.IO;
using System.Xml.Linq;
using Kuff.Common.Concretes.UnitOfWork;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Kuff.Service.Services.ProductRelated
{
    public class ProductService : IProductService
    {
        #region Constructors
        public ProductService()
        {
            this.Repository = new ProductRepository();
        }


        #endregion

        #region Fields

        #endregion

        #region Properties
        public ProductRepository Repository { get; set; }
        IRepository IService.Repository
        {
            get
            {
                return Repository;
            }

            set { Repository = value as ProductRepository; }
        }
        public IUnitOfWork Unit { get; set; }
        public enum SortPredicates { MostDiscount, MostExpensive, Cheapest }
        #endregion

        #region IProductService methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="virtualPath"></param>
        /// <param name="physicalPath"></param>
        /// <param name="pictureTransfers"></param>
        public void Insert(ProductDto item, string virtualPath, string physicalPath, ICollection<PictureTransfer> pictureTransfers)
        {

            InitProductPicture(item, virtualPath, pictureTransfers);

            // if Unit is not null means that our service is called after another service.            
            if (Unit != null)
            {
                this.Repository.Insert(item, false);  // call data access insert
            }
            // if Unit is null means that our service is the first service that is calling repository.
            else
            {
                this.Repository.Insert(item, false);
            }

            //saving the images in the storage.
            for (int i = 0; i < item.ProductPictures.Count(); i++)
            {
                string pathToSave = physicalPath + item.ProductPictures.ElementAt(i).Id;
                Save(pathToSave, pictureTransfers.ElementAt(i));
            }

            Repository.Save();

        }

        public List<ProductDto> Get(Expression<Func<ProductDto, bool>> filter = null, Func<IQueryable<ProductDto>, IOrderedQueryable<ProductDto>> orderBy = null, int? count = default(int?))
        {
            var query = Repository.Get();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.ToList();
        }

        public void Update(ProductDto item, string virtualPath, string physicalPath, ICollection<PictureTransfer> pictureTransfers)
        {
            InitProductPicture(item, virtualPath, pictureTransfers);

            Unit = new UnitOfWork(this);

            Repository.Update(item, false);

            IProductPictureService pictureService = GetService<IProductPictureService>();

            int counter = 0;
            foreach (var pic in pictureTransfers)
            {
                switch (pic.State)
                {
                    case PictureTransfer.States.Added:
                        {
                            pictureService.Insert(item.ProductPictures.ElementAt(counter));
                            break;
                        }
                    case PictureTransfer.States.Updated:
                        {
                            pictureService.Update(item.ProductPictures.ElementAt(counter));
                            break;
                        }
                    case PictureTransfer.States.Deleted:
                        {
                            pictureService.Delete(item.ProductPictures.ElementAt(counter));
                            break;
                        }
                }

                counter++;
            }

            StorePictures(item, physicalPath, pictureTransfers);

            Unit.Save();
        }

        public void Delete(ProductDto item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<ProductDto, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDto> FindProductsByTerm(string term, string categoryId = "")
        {
            if (categoryId.Equals(string.Empty))
            {
                return Get(p => p.Summary.Contains(term));
            }
            else
            {
                return Get(p => p.Summary.Contains(term)).Where(p => p.CategoryId.ToString().Equals(categoryId));
            }           
        }

        //public IEnumerable<ProductDto> SortProductsByPredicate(IEnumerable<ProductDto> products, SortPredicates predicate )
        //{
        //    if (predicate == SortPredicates.MostExpensive)
        //    {
        //        products = products.OrderByDescending(pr => pr.IsAvailable).OrderByDescending(p => p.DiscountInPercent);
        //    }
        //    else if (predicate == SortPredicates.MostDiscount)
        //    {
        //        products = products.OrderByDescending(pr => pr.IsAvailable).OrderByDescending(p => p.LastPriceWithDiscount);
        //    }
        //    else if (predicate == SortPredicates.Cheapest)
        //    {
        //        products = products.OrderByDescending(pr => pr.IsAvailable).OrderBy(p => p.LastPriceWithDiscount);
        //    }

        //    return products;
        //}
        #endregion

        #region Methods
        private void StorePictures(ProductDto item, string physicalPath, ICollection<PictureTransfer> pictureTransfers)
        {
            int i = 0;
            foreach (var pic in pictureTransfers)
            {
                string pathToSave = physicalPath + item.ProductPictures.ElementAt(i).Id;

                switch (pic.State)
                {
                    case PictureTransfer.States.Added:
                        {
                            Save(pathToSave, pictureTransfers.ElementAt(i));
                            break;
                        }
                    case PictureTransfer.States.Updated:
                        {
                            System.IO.File.Delete(pathToSave);
                            Save(pathToSave, pictureTransfers.ElementAt(i));
                            break;
                        }
                    case PictureTransfer.States.Deleted:
                        {
                            System.IO.File.Delete(pathToSave);
                            break;
                        }
                }

                i++;
            }
        }
        private void InitProductPicture(ProductDto product, string virtualPath, ICollection<PictureTransfer> picsTransfers)
        {
            if (product.ProductPictures == null)
            {
                product.ProductPictures = new List<ProductPictureDto>();
            }

            var productPictures = new List<ProductPictureDto>();


            for (int i = 0; i < picsTransfers.Count; i++)
            {
                var productPicture = product.ProductPictures.ElementAt(i);

                if (picsTransfers.ElementAt(i).ContentLength > 0)
                {
                    switch (picsTransfers.ElementAt(i).State)
                    {
                        case PictureTransfer.States.Added:
                            {
                                string fileName = picsTransfers.ElementAt(i).FileName;
                                string[] detached = fileName.Split('.');

                                Guid pictureId = Guid.NewGuid();
                                string path = virtualPath + pictureId;

                                productPicture.IsMainPicture = true;
                                productPicture.ProductId = product.Id;
                                productPicture.FileName = detached[0];
                                productPicture.FilePath = path;
                                productPicture.FileExtension = detached[detached.Length - 1];
                                productPicture.ContentType = picsTransfers.ElementAt(i).ContentType;
                                productPicture.Id = pictureId;
                                productPicture.IsForSummaryPreview = (i == 0);
                                productPicture.PictureOrder = i;

                                break;
                            }

                        case PictureTransfer.States.Updated:
                            {
                                string fileName = picsTransfers.ElementAt(i).FileName;
                                string[] detached = fileName.Split('.');

                                productPicture.ContentType = picsTransfers.ElementAt(i).ContentType;
                                productPicture.FileExtension = detached[detached.Length - 1];
                                productPicture.ContentType = picsTransfers.ElementAt(i).ContentType;
                                break;
                            }


                        default:
                            {
                                break;
                            }

                    }
                }
            }

            //product.ProductPictures = productPictures;
        }
        private void Save(string path, PictureTransfer picTransfer)
        {
            FileStream fileStream = File.Create(path, picTransfer.ContentLength);

            // Initialize the bytes array with the stream length and then fill it with data
            byte[] bytesInStream = new byte[picTransfer.ContentLength];
            picTransfer.Stream.Read(bytesInStream, 0, bytesInStream.Length);

            // Use write method to write to the file specified above
            fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            fileStream.Close();
        }
        private T GetService<T>() where T : IService
        {
            T service = EnterpriseLibraryContainer.Current.GetInstance<T>();
            if (Unit != null)
            {
                this.Unit.AddService(service);
            }

            return service;
        }



        #endregion
    }
}
