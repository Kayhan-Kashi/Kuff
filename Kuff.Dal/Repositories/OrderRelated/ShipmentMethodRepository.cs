using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kuff.Common.DTOs.OrderRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Dal.DataModel.OrderRelated;

namespace Kuff.Dal.Repositories.OrderRelated
{
    public class ShipmentMethodRepository : IRepository<ShipmentMethodDto>
    {
        #region Fields
        private KuffEntities _context;
        #endregion

        #region Constructors 
        public ShipmentMethodRepository()
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
        public void Insert(ShipmentMethodDto item, bool save = true)
        {
            item.Id = Guid.NewGuid();
            Context.ShipmentMethods.Add(MapDtoToModel(item));
            if (save)
            {
                Context.SaveChanges();
            }
        }

        public IQueryable<ShipmentMethodDto> Get()
        {
            return Context.ShipmentMethods.Select(s => new ShipmentMethodDto
            {
                Id = s.Id,
                Description = s.Description,
                ShipmentCosts = s.ShipmentCosts.Select(ss => new ShipmentCostDto
                {
                    Id = s.Id,
                    DestinationCity = ss.DestinationCity,
                    DateOfAddedShipmentCost = ss.DateOfAddedShipmentCost,
                    ShipmentMethodId = ss.ShipmentMethodId,
                    Cost = ss.Cost,
                    DepartureCity = ss.DepartureCity,
                    ShipmentMethodDescription = ss.ShipmentMethod.Description
                })               
            });
        }

        public void Update(ShipmentMethodDto item, bool save = true)
        {
            try
            {
                // Get existing Category object from database
                ShipmentMethod oldItem = GetFromModel().FirstOrDefault(x => x.Id.Equals(item.Id));

                // Set the new values for the fetched Category object
                if (oldItem != null)
                {
                    oldItem.Description = item.Description;

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
        public void Delete(ShipmentMethodDto item, bool save = true)
        {
            try
            {
                // Get existing model object from database
                ShipmentMethod oldItem = GetFromModel().FirstOrDefault(c => c.Id.Equals(item.Id));

                if (oldItem != null)
                {
                    Context.ShipmentMethods.Remove(oldItem);
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
        private IQueryable<ShipmentMethod> GetFromModel()
        {
            var query = Context.ShipmentMethods;
            return query;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
        #endregion

        #region Mappers
        public ShipmentMethod MapDtoToModel(ShipmentMethodDto viewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ShipmentMethodDto, ShipmentMethod>();
                cfg.CreateMap<ShipmentCostDto, ShipmentCost>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<ShipmentMethod>(viewModel);
        }

        public ShipmentCostDto MapModelToDto(ShipmentCost model)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ShipmentMethod , ShipmentMethodDto>();
                cfg.CreateMap<ShipmentCost, ShipmentCostDto>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<ShipmentCostDto>(model);
        }
        #endregion
    }
}
