using Kuff.Common.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Common.Interfaces.UnitOfWork;
using Kuff.Dal.Repositories.ProductRelated;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Kuff.Common.Concretes.UnitOfWork;
using Kuff.Service.Interfaces.ProductRelated;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel.Unity;

namespace Kuff.Service.Services.ProductRelated
{
    public class CategoryService : ICategoryService
    {
        //private IRepository cat;

        #region Constructors

        //public CategoryService()
        //{
        //    cat = EnterpriseLibraryContainer.Current.GetInstance<CategoryRepository>();
        //    //UnityServiceLocator srv = new UnityServiceLocator(Kuff.Service.IocContainer.IocConfigurator.ConfigureServiceLayerContainer(new UnityContainer()));
        //    //var item = srv.GetInstance<IRepository<CategoryViewModel>>();
        //}
        public CategoryService(IRepository<CategoryDto> repository)
        {
            //cat = EnterpriseLibraryContainer.Current.GetInstance<CategoryRepository>();
            Repository = repository as CategoryRepository;
            //new CategoryRepository();
        }
        #endregion

        #region Fields
        private IUnitOfWork _unit;
        #endregion

        #region Properties
        protected CategoryRepository Repository { get; set; }
        IRepository IService.Repository
        {
            get
            {
                return Repository;
            }

            set
            {
                Repository = value as CategoryRepository;
            }
        }

        public IUnitOfWork Unit
        {
            get
            {
                return _unit;
            }

            set
            {
                _unit = value;
                //SetUnitOfWork(_unit);
            }
        }
        #endregion

        #region ICategoryService Methods


        /// <summary>
        /// Insert a new Category entity
        /// </summary>
        /// <param name="item">Category entity</param>
        public void Insert(CategoryDto item)
        {
            try
            {
                // if Unit is not null means that our service is called after another service.            
                if (Unit != null)
                {
                    this.Repository.Insert(item, false);  // call data access insert
                }
                // if Unit is null means that our service is the first service that is calling repository.
                else
                {
                    this.Repository.Insert(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the list of the categories specified by different filter parameters
        /// </summary>
        /// <param name="filter">Since we want to use the filter parameter inside "Where" we should pass the filter in the form of Expression and the delegate inside the expression to be used to construct the tree expression.example of this could be c => c.Name.Contains("cat")</param>
        /// <param name="orderBy">Example : c => c.OrderBy(model => model.Name);  this delegate gets an IQueryable as input and in the body of the method return an IOrderedQueryable by calling OrderBy from the Context</param>
        /// <param name="count">Since count is nullable we should get the value</param>
        /// <returns></returns>
        public List<CategoryDto> Get(Expression<Func<CategoryDto, bool>> filter = null, Func<IQueryable<CategoryDto>, IOrderedQueryable<CategoryDto>> orderBy = null, int? count = null)
        {
            var query = this.Repository.Get();

            // since we want to use filter inside "Where" we should pass the filter in the Expression<Func<CategoryDto, bool>>
            // we filter data here based on filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Sort data
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // Gets the specified number of objects
            if (count != null)
            {
                // count.Value gets the value of nullable object
                query = query.Take(count.Value);
            }

            return query.ToList();
        }

        public virtual void Update(CategoryDto item)
        {
            try
            {
                //if Unit is not null means that the method is called after another method in the service and so it should not save the changes and commit the transaction.
                if (Unit != null)
                {
                    Repository.Update(item, false);
                }
                //if Unit is null means that the method is the first and only method in the transaction and it can save the changes and commit the transaction.
                else
                {
                    Repository.Update(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes a Category entity
        /// </summary>
        /// <param name="item"></param>
        public void Delete(CategoryDto item)
        {
            if (Unit != null)
            {
                Repository.Delete(item, false);
            }
            else
            {
                Repository.Delete(item);
            }
        }

        public virtual void Delete(Expression<Func<CategoryDto, bool>> filter)
        {
            try
            {
                var queries = Repository.Get().Where(filter);

                //if Unit is not null means that the method is called after another method in the service and so it should not save the changes and commit the transaction.
                if (Unit != null)
                {
                    foreach (var categoryViewModel in queries)
                    {
                        Repository.Delete(categoryViewModel, false);
                    }
                }
                //if Unit is null means that the method is the first and only method in the transaction and it can save the changes and commit the transaction.
                else
                {
                    foreach (var categoryViewModel in queries)
                    {
                        // we could not pass true for save parameter because we get the error ""New transaction is not allowed because there are other threads running in the session".
                        Repository.Delete(categoryViewModel, false);
                    }

                    // if we try save change every item in the foreach loop inside the repository then the error : "New transaction is not allowed because there are other threads running in the session"
                    // will be thrown so we should generate the delete query for each entity and after that call save changes once.
                    Repository.Save();
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
        /// Sets the DbContext of data access for this service
        /// </summary>
        /// <param name="unit">Unit of work</param>
        protected void SetUnitOfWork(IUnitOfWork unit)
        {
            if (unit != null)
            {
                unit.AddService(this);
            }
        }

        /// <summary>
        /// Get a service and set unit of work for new service
        /// </summary>
        /// <typeparam name="T">Generic Type of service</typeparam>
        /// <returns>Service</returns>
        protected T GetService<T>() where T : IService
        {
            T service = EnterpriseLibraryContainer.Current.GetInstance<T>();
            if (this.Unit != null) this.Unit.AddService(service);
            return service;
        }

        #endregion

        public void CombineService()
        {
            Unit = new UnitOfWork(this);
            Insert(new CategoryDto {Name = "newCat", Description = "this is done by Combine method"});
            IDataTypeService service = GetService<IDataTypeService>();
            service.Insert(new DataTypeDto {Name = "newDataType", ControlToRender = "new Controller"});
            Unit.Save();

        }
    }
}
