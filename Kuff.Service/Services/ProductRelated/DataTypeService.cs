using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Common.Interfaces.Service;
using Kuff.Common.Interfaces.UnitOfWork;
using Kuff.Dal;
using Kuff.Dal.Repositories.ProductRelated;
using Kuff.Service.Interfaces.ProductRelated;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Kuff.Service.Services.ProductRelated
{
    public class DataTypeService : IDataTypeService
    {
        #region Constructors
        public DataTypeService(IRepository<DataTypeDto> repository)
        {
            Repository = repository as DataTypeRepository;        
        }
        #endregion

        #region Fields
        private IUnitOfWork _unit;
        #endregion

        #region Properties
        public DataTypeRepository Repository { get; set; }

        public IUnitOfWork Unit
        {
            get
            {
                return _unit;
            }

            set { _unit = value; }
        }

        IRepository IService.Repository
        {
            get { return Repository; }

            set
            {
                Repository = value as DataTypeRepository;
            }
        }
        #endregion

        #region IDataTypeService Methods

        /// <summary>
        /// Insert a new DataType entity
        /// </summary>
        /// <param name="item">DataType entity</param>
        public void Insert(DataTypeDto item)
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
        /// Gets the list of the datatypes specified by different filter parameters
        /// </summary>
        /// <param name="filter">Since we want to use the filter parameter inside "Where" we should pass the filter in the form of Expression and the delegate inside the expression to be used to construct the tree expression.example of this could be d => c.Name.Contains("cat")</param>
        /// <param name="orderBy">Example : d => c.OrderBy(model => model.Name);  this delegate gets an IQueryable as input and in the body of the method return an IOrderedQueryable by calling OrderBy from the Context</param>
        /// <param name="count">Since count is nullable we should get the value</param>
        /// <returns></returns>
        public List<DataTypeDto> Get(Expression<Func<DataTypeDto, bool>> filter = null, Func<IQueryable<DataTypeDto>, IOrderedQueryable<DataTypeDto>> orderBy = null, int? count = default(int?))
        {
            var query = Repository.Get();

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
            if (count.HasValue)
            {
                // count.Value gets the value of nullable object
                query = query.Take(count.Value);
            }

            return query.ToList();
        }

        public void Update(DataTypeDto item)
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

        /// <summary>
        /// Deletes a DataType entity
        /// </summary>
        /// <param name="item"></param>
        public void Delete(DataTypeDto item)
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

        public void Delete(Expression<Func<DataTypeDto, bool>> filter)
        {
            var query = Repository.Get().Where(filter);

            //if Unit is not null means that the method is called after another method in the service and so it should not save the changes and commit the transaction.
            if (Unit != null)
            {
                foreach (DataTypeDto item in query)
                {
                    Repository.Delete(item, false);
                }
            }
            //if Unit is null means that the method is the first and only method in the transaction and it can save the changes and commit the transaction.
            else
            {
                foreach (DataTypeDto item in query)
                {
                    // we could not pass true for save parameter because we get the error ""New transaction is not allowed because there are other threads running in the session".
                    Repository.Delete(item, false);
                }

                // if we try save change every item in the foreach loop inside the repository then the error : "New transaction is not allowed because there are other threads running in the session"
                // will be thrown so we should generate the delete query for each entity and after that call save changes for once.
                Repository.Save();
            }

        }
        #endregion

        #region Methods
        protected void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
            {
                unitOfWork.AddService(this);
            }
        }

        protected T GetService<T>() where T : IService
        {
            T service = EnterpriseLibraryContainer.Current.GetInstance<T>();
            if (this.Unit != null) this.Unit.AddService(service);
            return service;
        }
        #endregion

    }
}
