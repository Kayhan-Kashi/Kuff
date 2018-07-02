using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Dal.DataModel.ProductRelated;
using AutoMapper;

namespace Kuff.Dal.Repositories.ProductRelated
{
    public class ProductTypeRepository : IRepository<ProductTypeDto>
    {
        #region Constructors
        public ProductTypeRepository()
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

        #region IRepository<ProductTypeDto>
        public void Insert(ProductTypeDto item, bool save = true)
        {
            item.Id = Guid.NewGuid();
            item.DateOfCreation = PersianDateTime.Now.ToString();

            foreach (ProductTypePropertyDto prodProperty in item.ProductTypeProperties)
            {
                prodProperty.Id = Guid.NewGuid();
                prodProperty.ProductTypeId = item.Id;
            }

            var m = MapDtoToModel(item);
            Context.ProductTypes.Add(MapDtoToModel((item)));
            
            if (save)
            {
                Context.SaveChanges();
            }
        }

        public IQueryable<ProductTypeDto> Get()
        {
            return Context.ProductTypes.Select(p => new ProductTypeDto
            {
                Id = p.Id,
                Title = p.Title,
                CategoryDescription = p.Category.Description,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                Code = p.Code,
                Comments = p.Comments,
                DateOfCreation = p.DateOfCreation,
                ProductTypeProperties = p.ProductTypeProperties.Select(pr => new ProductTypePropertyDto
                {
                    Id = pr.Id,
                    Title = pr.Title,
                    DataTypeId = pr.DataTypeId,
                    ProductTypeId = pr.ProductTypeId,
                    DataTypeControlToRender = pr.DataType.ControlToRender,
                    DataTypeName = pr.DataType.Name,
                    IsUserDecision = pr.IsUserDecision,
                    OrderNumber = pr.OrderNumber,
                    ProductTypeCode = pr.ProductType.Code,
                    ProductTypeComments = pr.ProductType.Comments,
                    ProductTypeDateOfCreation = pr.ProductType.DateOfCreation,
                    ProductTypeTitle = pr.ProductType.Title
                })
            });
        }

        public void Update(ProductTypeDto item, bool save = true)
        {
            bool isExisted = GetFromModel().Any(p => p.Id.Equals(item.Id));

            if (isExisted)
            {
                ProductType modifiedItem = MapDtoToModel(item);
                int counter = 0;
                foreach (ProductTypeProperty property in modifiedItem.ProductTypeProperties)
                {
                    if (property.Id != Guid.Empty)
                    {
                        Context.Entry(property).State = EntityState.Modified;
                    }
                    else
                    {
                        property.Id = Guid.NewGuid();
                        property.ProductTypeId = modifiedItem.Id;
                        property.OrderNumber = counter;
                        Context.Entry(property).State = System.Data.Entity.EntityState.Added;
                    }
                    counter++;
                }

                Context.Entry(modifiedItem).State = EntityState.Modified;

                if (save)
                {
                    Context.SaveChanges();
                }
            }
        }

        public void Delete(ProductTypeDto item, bool save = true)
        {
            // Get existing model object from database
            ProductType oldItem = GetFromModel().FirstOrDefault(c => c.Id.Equals(item.Id));

            if (oldItem != null)
            {
                Context.ProductTypes.Remove(oldItem);
            }

            if (save)
            {
                Context.SaveChanges();
            }

        }
        #endregion

        #region Mappers
        public ProductType MapDtoToModel(ProductTypeDto viewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductTypeDto, ProductType>();
                cfg.CreateMap<ProductTypePropertyDto, ProductTypeProperty>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<ProductType>(viewModel);
        }

        public ProductTypeDto MapModelToDto(ProductType model)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductType, ProductTypeDto>();
                cfg.CreateMap<ProductTypeProperty, ProductTypePropertyDto>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<ProductTypeDto>(model);
        }
        #endregion

        #region Methods
        public IQueryable<ProductType> GetFromModel()
        {
            return Context.ProductTypes;
        }

        public void Save()
        {
            Context.SaveChanges();
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
