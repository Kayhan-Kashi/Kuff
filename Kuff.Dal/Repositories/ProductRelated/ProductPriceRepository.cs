using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Common.DTOs.ProductRelated;
using System.Data.Entity;
using AutoMapper;
using Kuff.Dal.DataModel.ProductRelated;

namespace Kuff.Dal.Repositories.ProductRelated
{
    public class ProductPriceRepository : IRepository<ProductPriceDto>
    {
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
            get
            {
                return Context;
            }

            set
            {
                Context = value as KuffEntities;
            }
        }

        #endregion

        #region IRepository methods
        public void Insert(ProductPriceDto item, bool save = true)
        {
            item.Id = Guid.NewGuid();
            item.Date = PersianDateTime.Now.ToString();

            Context.ProductPrices.Add(MapDtoToModel(item));

            if (save)
            {
                Context.SaveChanges();
            }
        }

        public IQueryable<ProductPriceDto> Get()
        {
            try
            {
                // Unlike the insert method we couldn't use AutoMapper here cause putting Mappers method in the Select method of the context causes the Expression tree to use the method for providing
                // the query and since Expression tree is not aware of .Net methods it tries to find the method in the database and hence doesn't find it and throws an error. 
                return Context.ProductPrices.Select(p => new ProductPriceDto
                {
                    Id = p.Id,
                    ProductId = p.ProductId,
                    ProductComments = p.Product.Comments,
                    ProductDateOfAdding = p.Product.DateOfAdding,
                    ProductSummary = p.Product.Summary,
                    PriceWithoutDiscount = p.PriceWithoutDiscount,
                    PriceWithDiscount = p.PriceWithDiscount,
                    Date = p.Date
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(ProductPriceDto item, bool save = true)
        {
            try
            {
                // Get existing Category object from database
                ProductPrice oldItem = GetFromModel().FirstOrDefault(x => x.Id.Equals(item.Id));

                // Set the new values for the fetched Category object
                if (oldItem != null)
                {
                    oldItem.Date = PersianDateTime.Now.ToString();
                    oldItem.PriceWithDiscount = item.PriceWithDiscount;
                    oldItem.PriceWithoutDiscount = item.PriceWithoutDiscount;
                    oldItem.ProductId = item.ProductId;

                    if (save)
                    {
                        this.Context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(ProductPriceDto item, bool save = true)
        {
            try
            {
                // Get existing model object from database
                ProductPrice oldItem = GetFromModel().FirstOrDefault(p => p.Id.Equals(item.Id));

                if (oldItem != null)
                {
                    Context.ProductPrices.Remove(oldItem);
                }

                if (save)
                {
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Methods
        public IQueryable<ProductPrice> GetFromModel()
        {
            return Context.ProductPrices;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
        #endregion

        #region IDisposable methods
        public void Dispose()
        {
            Context.Dispose();
        }
        #endregion

        #region Mappers
        public ProductPrice MapDtoToModel(ProductPriceDto viewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductPriceDto, ProductPrice>());
            var mapper = config.CreateMapper();
            return mapper.Map<ProductPrice>(viewModel);
        }

        public ProductPriceDto MapModelToDto(ProductPrice model)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductPrice, ProductPriceDto>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<ProductPriceDto>(model);
        }
        #endregion
    }
}
