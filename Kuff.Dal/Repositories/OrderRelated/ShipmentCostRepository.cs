using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kuff.Common.DTOs.OrderRelated;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Dal.DataModel.OrderRelated;
using Kuff.Dal.DataModel.ProductRelated;

namespace Kuff.Dal.Repositories.OrderRelated
{
    public class ShipmentCostRepository : IRepository<ShipmentCostDto>

    {
        #region Fields
        private KuffEntities _context;
        #endregion

        #region Constructors 
        public ShipmentCostRepository()
        {

        }
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
        public void Insert(ShipmentCostDto item, bool save = true)
        {
            item.Id = Guid.NewGuid();
            item.DateOfAddedShipmentCost = PersianDateTime.Now.ToString();
            Context.ShipmentCosts.Add(MapDtoToModel(item));
            if (save)
            {
                Context.SaveChanges();
            }
        }

        public IQueryable<ShipmentCostDto> Get()
        {
            return Context.ShipmentCosts.Select(s => new ShipmentCostDto
            {
                Id = s.Id,
                DestinationCity = s.DestinationCity,
                DateOfAddedShipmentCost = s.DateOfAddedShipmentCost,
                ShipmentMethodId = s.ShipmentMethodId,
                Cost = s.Cost,
                DepartureCity = s.DepartureCity,
                ShipmentMethodDescription = s.ShipmentMethod.Description
            });
        }

        public void Update(ShipmentCostDto item, bool save = true)
        {
            try
            {
                // Get existing Category object from database
                ShipmentCost oldItem = GetFromModel().FirstOrDefault(x => x.Id.Equals(item.Id));

                // Set the new values for the fetched Category object
                if (oldItem != null)
                {
                    oldItem.Cost = item.Cost;
                    oldItem.DepartureCity = item.DepartureCity;
                    oldItem.DestinationCity = item.DestinationCity;
                    oldItem.DateOfAddedShipmentCost = PersianDateTime.Now.ToString();

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
        public void Delete(ShipmentCostDto item, bool save = true)
        {
            try
            {
                // Get existing model object from database
                ShipmentCost oldItem = GetFromModel().FirstOrDefault(c => c.Id.Equals(item.Id));

                if (oldItem != null)
                {
                    Context.ShipmentCosts.Remove(oldItem);
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

        #region IDisposable Methods
        public void Dispose()
        {
            Context.Dispose();
        }
        #endregion

        #region Methods 
        private IQueryable<ShipmentCost> GetFromModel()
        {
            var query = Context.ShipmentCosts;
            return query;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
        #endregion

        #region Mappers
        public ShipmentCost MapDtoToModel(ShipmentCostDto viewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ShipmentCostDto, ShipmentCost>();
                cfg.CreateMap<ShipmentMethodDto, ShipmentMethod>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<ShipmentCost>(viewModel);
        }

        public ShipmentCost MapModelToDto(ShipmentCost model)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ShipmentCost, ShipmentCostDto>();
                cfg.CreateMap<ShipmentMethod, ShipmentMethodDto>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<ShipmentCost>(model);
        }
        #endregion
    }
}
