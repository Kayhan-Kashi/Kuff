using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Dal.DataModel.ProductRelated;
using Kuff.Common.DTOs.ProductRelated;
using System.Data.Entity;
using System.Windows.Markup;
using AutoMapper;

namespace Kuff.Dal.Repositories.ProductRelated
{
    public class ProductAvailabilityRepository : IRepository<ProductAvailabilityDto>
    {
        public ProductAvailabilityRepository()
        {

        }

        private KuffEntities _context;

        public KuffEntities Context
        {
            get { return _context ?? (_context = new KuffEntities()); } // if context is null it will be initialized
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

        public void Delete(ProductAvailabilityDto item, bool save = true)
        {
            Context.ProductAvailabilities.Remove(MapDtoToModel(item));
            if (save)
            {
                Context.SaveChanges();
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public IQueryable<ProductAvailabilityDto> Get()
        {
            return Context.ProductAvailabilities.Select(p => new ProductAvailabilityDto
            {
                Id = p.Id,
                ProductId = p.ProductId,
                ProductSummary = p.Product.Summary,
                ProductComments = p.Product.Comments,
                ProductDateOfAdding = p.Product.DateOfAdding,
                IsAvailable = p.IsAvailable,
                Date = p.Date
            });
        }

        public void Insert(ProductAvailabilityDto item, bool save = true)
        {
            item.Id = Guid.NewGuid();
            item.Date = PersianDateTime.Now.ToString();
            //var prod = MapDtoToModel(item);
            //Context.ProductAvailabilities.Add(prod);

            if (save)
            {
                Context.ProductAvailabilities.Add(new ProductAvailability
                {
                    Id = Guid.NewGuid(),
                    Date = PersianDateTime.Now.ToString(),
                    ProductId = item.ProductId,
                    IsAvailable = item.IsAvailable
                });
                Context.SaveChanges();
            }
        }

        public void Update(ProductAvailabilityDto item, bool save = true)
        {
            // Get existing object from database
            ProductAvailability oldItem = GetFromModel().FirstOrDefault(x => x.Id.Equals(item.Id));

            // Set the new values for the fetched object
            if (oldItem != null)
            {
                oldItem.IsAvailable = item.IsAvailable;
                oldItem.Date = item.Date;
                oldItem.ProductId = item.ProductId;
            }

            if (save)
            {
                Context.SaveChanges();
            }
        }

        private IQueryable<ProductAvailability> GetFromModel()
        {
            return Context.ProductAvailabilities;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        #region Mappers
        public ProductAvailability MapDtoToModel(ProductAvailabilityDto viewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductAvailabilityDto, ProductAvailability>();
                cfg.CreateMap<ProductDto, Product>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<ProductAvailability>(viewModel);
        }

        public ProductAvailabilityDto MapModelToDto(ProductAvailability model)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductAvailability, ProductAvailabilityDto>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<ProductAvailabilityDto>(model);
        }
        #endregion
    }
}
