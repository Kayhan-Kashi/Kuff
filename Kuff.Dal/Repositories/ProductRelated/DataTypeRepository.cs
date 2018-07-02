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
    public class DataTypeRepository : IRepository<DataTypeDto>
    {
        #region Constructors 
        public DataTypeRepository()
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

        #region IRepository<DataTypeViewModel> methods

        public void Insert(DataTypeDto item, bool save = true)
        {
            item.Id = Guid.NewGuid();
            Context.DataTypes.Add(MapDtoToModel(item));
            if (save)
            {
                Context.SaveChanges();
            }
        }

        public IQueryable<DataTypeDto> Get()
        {
            // Unlike the insert method we couldn't use AutoMapper here cause putting Mappers method in the Select method of the context causes the Expression tree to use the method for providing
            // the query and since Expression tree is not aware of .Net methods it tries to find the method in the database and hence doesn't find it and throws an error. 
            return Context.DataTypes.Select(d => new DataTypeDto
            {
                Id = d.Id,
                Name = d.Name,
                ControlToRender = d.ControlToRender
            });
        }

        public void Update(DataTypeDto item, bool save = true)
        {
            // Get existing object from database
            DataType oldItem = GetFromModel().FirstOrDefault(x => x.Id.Equals(item.Id));

            // Set the new values for the fetched object
            if (oldItem != null)
            {
                oldItem.Name = item.Name;
                oldItem.ControlToRender = item.ControlToRender;
            }

            if (save)
            {
                Context.SaveChanges();
            }

        }

        public void Delete(DataTypeDto item, bool save = true)
        {
            // Get existing model object from database
            DataType oldItem = GetFromModel().FirstOrDefault(c => c.Id.Equals(item.Id));

            if (oldItem != null)
            {
                Context.DataTypes.Remove(oldItem);
            }

            if (save)
            {
                Context.SaveChanges();
            }
        }

        #endregion

        #region Methods

        private IQueryable<DataType> GetFromModel()
        {
            return Context.DataTypes;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
        #endregion

        #region Mappers
        public DataType MapDtoToModel(DataTypeDto viewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DataTypeDto, DataType>());
            var mapper = config.CreateMapper();
            return mapper.Map<DataType>(viewModel);
        }

        public DataTypeDto MapModelToDto(DataType model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DataType, DataTypeDto>());
            var mapper = config.CreateMapper();
            return mapper.Map<DataTypeDto>(model);
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
