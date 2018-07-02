using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Common.DTOs;
using Kuff.Dal.DataModel.ProductRelated;
using Kuff.Common.DTOs.ProductRelated;
using System.Linq.Expressions;
using AutoMapper;

namespace Kuff.Dal.Repositories.ProductRelated
{
    public class CategoryRepository : IRepository<CategoryDto>
    {
        #region Fields
        private KuffEntities _context;
        #endregion

        #region Constructors 
        public CategoryRepository()
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

        #region IRepository<DataTypeViewModel> methods
        public void Insert(CategoryDto item, bool save = true)
        {
            item.Id = Guid.NewGuid();
            Context.Categories.Add(MapDtoToModel(item));
            if (save)
            {
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// returns a query of Data Model objects from the database but since this query will be deferred till the execution it can be filtered several times by different predicates outside the repository.
        /// </summary>
        /// <returns>IQueryable</returns>
        public IQueryable<CategoryDto> Get()
        {
            try
            {
                // Unlike the insert method we couldn't use AutoMapper here cause putting Mappers method in the Select method of the context causes the Expression tree to use the method for providing
                // the query and since Expression tree is not aware of .Net methods it tries to find the method in the database and hence doesn't find it and throws an error. 
                return Context.Categories.Select( c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ProductTypes = c.ProductTypes.Select(p => new ProductTypeDto
                    {
                        Id = p.Id,
                        CategoryId = p.CategoryId,
                        Title = p.Title,                        
                    })
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates a Category entity
        /// </summary>
        /// <param name="item">Category View Model</param>
        /// <param name="save">Save changes if equals true</param>
        public virtual void Update(CategoryDto item, bool save = true)
        {
            try
            {
                // Get existing Category object from database
                Category oldCategory = GetFromModel().FirstOrDefault(x => x.Id.Equals(item.Id));

                // Set the new values for the fetched Category object
                if (oldCategory != null)
                {
                    oldCategory.Name = item.Name;
                    oldCategory.Description = item.Description;

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

        public virtual void Delete(CategoryDto item, bool save = true)
        {
            try
            {
                // Get existing model object from database
                Category oldItem = GetFromModel().FirstOrDefault(c => c.Id.Equals(item.Id));

                if (oldItem != null)
                {
                    Context.Categories.Remove(oldItem);
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
        /// <summary>
        /// Returns a query of Category data model objects
        /// </summary>
        /// <returns></returns>
        private IQueryable<Category> GetFromModel()
        {
            var query = Context.Categories;
            return query;
        }

        /// <summary>
        /// Save changes to database
        /// </summary>
        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Mappers
        public Category MapDtoToModel(CategoryDto viewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDto, Category>());
            var mapper = config.CreateMapper();
            return mapper.Map<Category>(viewModel);
        }

        public CategoryDto MapModelToDto(Category model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDto>());
            var mapper = config.CreateMapper();
            return mapper.Map<CategoryDto>(model);
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
