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
    public class PaymentMethodRepository : IRepository<PaymentMethodDto>
    {
        #region Fields
        private KuffEntities _context;
        #endregion

        #region Constructors 
        public PaymentMethodRepository()
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
        public void Insert(PaymentMethodDto item, bool save = true)
        {
            item.Id = Guid.NewGuid();
            Context.PaymentMethods.Add(MapDtoToModel(item));
            if (save)
            {
                Context.SaveChanges();
            }
        }

        public IQueryable<PaymentMethodDto> Get()
        {
            return Context.PaymentMethods.Select(p => new PaymentMethodDto
            {
                Id = p.Id,
                Description = p.Description
            });
        }

        public void Update(PaymentMethodDto item, bool save = true)
        {
            try
            {
                // Get existing Category object from database
                PaymentMethod olditem = GetFromModel().FirstOrDefault(x => x.Id.Equals(item.Id));

                // Set the new values for the fetched Category object
                if (olditem != null)
                {
                    olditem.Description = item.Description;

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
        public void Delete(PaymentMethodDto item, bool save = true)
        {
            try
            {
                // Get existing model object from database
                PaymentMethod oldItem = GetFromModel().FirstOrDefault(c => c.Id.Equals(item.Id));

                if (oldItem != null)
                {
                    Context.PaymentMethods.Remove(oldItem);
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
        private IQueryable<PaymentMethod> GetFromModel()
        {
            var query = Context.PaymentMethods;
            return query;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
        #endregion

        #region Mappers
        public PaymentMethod MapDtoToModel(PaymentMethodDto viewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PaymentMethodDto, PaymentMethod>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<PaymentMethod>(viewModel);
        }

        public PaymentMethodDto MapModelToDto(PaymentMethod model)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PaymentMethod, PaymentMethodDto>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<PaymentMethodDto>(model);
        }
        #endregion
    }
}
