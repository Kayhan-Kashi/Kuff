using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Repositories;
using AutoMapper;
using Kuff.Dal.DataModel.ProductRelated;

namespace Kuff.Dal.Repositories.ProductRelated
{
    public class ProductRepository : IRepository<ProductDto>
    {
        #region Constructors
        public ProductRepository()
        {

        }
        #endregion

        #region Fields
        private KuffEntities _context;
        #endregion

        #region Properties
        public KuffEntities Context
        {
            get { return _context ?? (_context = new KuffEntities()); } // if context is null it will be initialized
            set
            {
                _context = value;
            }
        }

        DbContext IRepository.Context
        {
            get { return Context; }

            set
            {
                Context = value as KuffEntities;
            }
        }
        #endregion

        #region IRepository<Product> Methods
        public void Insert(ProductDto item, bool save = true)
        {
            //item.Id = Guid.NewGuid();
            item.DateOfAdding = PersianDateTime.Now.ToString();

            foreach (ProductPropertyValueDto prodValue in item.ProductPropertyValues)
            {
                prodValue.Id = Guid.NewGuid();
                prodValue.ProductId = item.Id;
            }

            var prod = MapDtoToModel(item);
            Context.Products.Add(MapDtoToModel(item));

            if (save)
            {
                Context.SaveChanges();
            }

        }

        public IQueryable<ProductDto> Get()
        {
            return Context.Products.Select(p => new ProductDto
            {
                Id = p.Id,
                Comments = p.Comments,
                DateOfAdding = p.DateOfAdding,
                ProductTypeCode = p.ProductType.Code,
                ProductTypeId = p.ProductTypeId,
                Summary = p.Summary,
                ProductTypeDateOfCreation = p.ProductType.DateOfCreation,
                ProductTypeComments = p.ProductType.Comments,
                ProductTypeTitle = p.ProductType.Title,
                ProductPropertyValues = p.ProductPropertyValues.Select(pr => new ProductPropertyValueDto
                {
                    Id = pr.Id,
                    Value = pr.Value,
                    ProductId = p.Id,
                    ProductTypePropertyId = pr.ProductTypePropertyId,
                    ProductComments = p.Comments,
                    ProductDateOfAdding = p.DateOfAdding,
                    ProductSummary = pr.Product.Summary,
                    ProductTypePropertyIsUserDecision = pr.ProductTypeProperty.IsUserDecision,
                    ProductTypePropertyOrderNumber = pr.ProductTypeProperty.OrderNumber,
                    ProductTypePropertyTitle = pr.ProductTypeProperty.Title
                }),
                ProductPictures = p.ProductPictures.Select(pp => new ProductPictureDto
                {
                    Id = pp.Id,
                    ProductId = p.Id,
                    ProductComments = p.Comments,
                    ProductDateOfAdding = p.DateOfAdding,
                    ProductSummary = p.Summary,
                    ContentType = pp.ContentType,
                    FileExtension = pp.FileExtension,
                    FileName = pp.FileName,
                    FilePath = pp.FilePath,
                    IsForSummaryPreview = pp.IsForSummaryPreview,
                    IsMainPicture = pp.IsMainPicture,
                    PictureOrder = pp.PictureOrder
                }),
                ProductPrices = p.ProductPrices.Select(pr => new ProductPriceDto
                {
                    Id = pr.Id,
                    PriceWithoutDiscount = pr.PriceWithoutDiscount,
                    Date = pr.Date,
                    PriceWithDiscount = pr.PriceWithDiscount,
                    ProductComments = p.Comments,
                    ProductDateOfAdding = p.DateOfAdding,
                    ProductId = p.Id,
                    ProductSummary = p.Summary
                }),
                ProductAvailabilities = p.ProductAvailabilities.Select(pa => new ProductAvailabilityDto
                {
                    Date = pa.Date,
                    ProductComments = p.Comments,
                    ProductId = p.Id,
                    Id = pa.Id,
                    IsAvailable = pa.IsAvailable,
                    ProductDateOfAdding = p.DateOfAdding,
                    ProductSummary = p.Summary              
                }),
                CategoryId = p.ProductType.CategoryId,
                CategoryDescription = p.ProductType.Category.Description,
                CategoryName = p.ProductType.Category.Name
                          
                //Discount = p.ProductPrices.OrderByDescending(prodprice => prodprice.Date).FirstOrDefault().Discount,
                //HasDiscount = p.ProductPrices.OrderByDescending(prodprice => prodprice.Date).FirstOrDefault().PriceWithoutDiscount > p.ProductPrices.OrderByDescending(prodprice => prodprice.Date).FirstOrDefault().PriceWithDiscount,
                //IsAvailable = p.ProductAvailabilities.OrderByDescending(pa => pa.Date).FirstOrDefault().IsAvailable,
                //DiscountInPercent = p.ProductPrices.OrderByDescending(prodPrice => prodPrice.Date).FirstOrDefault().DiscountInPercent,
                //LastPriceWithDiscount = p.ProductPrices.OrderByDescending(prodPrice => prodPrice.Date).FirstOrDefault().PriceWithDiscount,
                //LastPriceWithoutDiscount = p.ProductPrices.OrderByDescending(prodPrice => prodPrice.Date).FirstOrDefault().PriceWithoutDiscount,
                //ProductPriceId = p.ProductPrices.OrderByDescending(prodPrice => prodPrice.Date).FirstOrDefault().Id
            });

        }

        public void Update(ProductDto item, bool save = true)
        {
            var oldItem = GetFromModel().FirstOrDefault(p => p.Id.Equals(item.Id));

            if (oldItem != null)
            {
                oldItem.Comments = item.Comments;
                oldItem.DateOfAdding = PersianDateTime.Now.ToString();
                oldItem.Summary = item.Summary;
            }

            if (save)
            {
                Context.SaveChanges();
            }
        }

        public void Delete(ProductDto item, bool save = true)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods
        private IQueryable<Product> GetFromModel()
        {
            return Context.Products;
        }
        public void Save()
        {
            Context.SaveChanges();
        }
        #endregion

        #region Mappers
        public Product MapDtoToModel(ProductDto viewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDto, Product>();
                cfg.CreateMap<ProductPropertyValueDto, ProductPropertyValue>();
                cfg.CreateMap<ProductPictureDto, ProductPicture>();
                cfg.CreateMap<ProductTypeDto, ProductType>();
                cfg.CreateMap<ProductAvailabilityDto, ProductAvailability>();
                cfg.CreateMap<ProductPriceDto, ProductPrice>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<Product>(viewModel);
        }

        public ProductDto MapModelToDto(ProductType model)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<ProductPropertyValue, ProductPropertyValueDto>();
                cfg.CreateMap<ProductPicture, ProductPictureDto>();
                cfg.CreateMap<ProductType , ProductTypeDto>();
                cfg.CreateMap<ProductAvailability, ProductAvailabilityDto>();
                cfg.CreateMap<ProductPrice, ProductPriceDto>();
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
