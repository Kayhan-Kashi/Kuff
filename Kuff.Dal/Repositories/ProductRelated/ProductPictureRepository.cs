using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Dal.DataModel.ProductRelated;

namespace Kuff.Dal.Repositories.ProductRelated
{
    public class ProductPictureRepository : IRepository<ProductPictureDto>
    {

        public ProductPictureRepository()
        {

        }

        private KuffEntities _context;

        public KuffEntities Context
        {
            get { return _context ?? new KuffEntities(); }
            set { _context = value; }
        }

        DbContext IRepository.Context
        {
            get { return Context; }

            set
            {
                Context = value as KuffEntities;
            }
        }

        public void Delete(ProductPictureDto item, bool save = true)
        {
            // Get existing model object from database
            ProductPicture oldItem = GetFromModel().FirstOrDefault(p => p.Id.Equals(item.Id));

            if (oldItem != null)
            {
                Context.ProductPictures.Remove(oldItem);
            }

            if (save)
            {
                Context.SaveChanges();
            }
        }

        public IQueryable<ProductPictureDto> Get()
        {
            return Context.ProductPictures.Select(p => new ProductPictureDto
            {
                Id = p.Id,
                ProductId = p.ProductId,
                ProductComments = p.Product.Comments,
                ProductDateOfAdding = p.Product.DateOfAdding,
                ProductSummary = p.Product.Summary,
                FileName = p.FileName,
                FileExtension = p.FileExtension,
                ContentType = p.ContentType,
                IsMainPicture = p.IsMainPicture,
                FilePath = p.FilePath,
                PictureOrder = p.PictureOrder,
                IsForSummaryPreview = p.IsForSummaryPreview
            });
        }

        public void Insert(ProductPictureDto item, bool save = true)
        {
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
            }
            
            Context.ProductPictures.Add(MapDtoToModel(item));

            if (save)
            {
                Context.SaveChanges();
            }

        }

        public void Update(ProductPictureDto item, bool save = true)
        {
            // Get existing object from database
            ProductPicture oldItem = GetFromModel().FirstOrDefault(x => x.Id.Equals(item.Id));

            // Set the new values for the fetched object
            if (oldItem != null)
            {
                oldItem.ContentType = item.ContentType;
                oldItem.FileExtension = item.FileExtension;
                oldItem.FileName = item.FileName;
                oldItem.FilePath = item.FilePath;
                oldItem.IsForSummaryPreview = item.IsForSummaryPreview;
                oldItem.IsMainPicture = item.IsMainPicture;
                oldItem.PictureOrder = item.PictureOrder;
                oldItem.ProductId = item.ProductId;
            }

            if (save)
            {
                Context.SaveChanges();
            }

        }
    
        private IQueryable<ProductPicture> GetFromModel()
        {
            return Context.ProductPictures;
        }


        #region Mappers
        public ProductPicture MapDtoToModel(ProductPictureDto viewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductPictureDto, ProductPicture>();
                cfg.CreateMap<ProductDto, Product>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<ProductPicture>(viewModel);
        }


        public ProductDto MapModelToDto(ProductPicture model)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductPicture, ProductPictureDto>();
                cfg.CreateMap<Product, ProductDto>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<ProductDto>(model);
        }
        #endregion

        #region IDisposable Methods
        public void Dispose()
        {
            Context.Dispose();
        }
        #endregion
    }
}
