﻿using Kuff.Common.DTOs.OrderRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Common.Interfaces.UnitOfWork;
using Kuff.Dal.Repositories.OrderRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.Interfaces.Service;
using Kuff.Service.Interfaces.OrderRelated;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Kuff.Service.Services.OrderRelated
{
    public class PaymentMethodService : IPaymentMethodService
    {
        #region Constructors
        public PaymentMethodService(IRepository<PaymentMethodDto> repository)
        {
            Repository = repository as PaymentMethodRepository;
        }
        #endregion

        #region Fields
        private IUnitOfWork _unit;
        #endregion

        #region Properties
        public PaymentMethodRepository Repository { get; set; }

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
                Repository = value as PaymentMethodRepository;
            }
        }
        #endregion

        #region IPaymentMethodService Methods

        /// <summary>
        /// Insert a new PaymentMethodDto entity
        /// </summary>
        /// <param name="item">PaymentMethodDto entity</param>
        public void Insert(PaymentMethodDto item)
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
        /// Gets the list of the PaymentMethodDto specified by different filter parameters
        /// </summary>
        /// <param name="filter">Since we want to use the filter parameter inside "Where" we should pass the filter in the form of Expression and the delegate inside the expression to be used to construct the tree expression.example of this could be d => c.Name.Contains("cat")</param>
        /// <param name="orderBy">Example : d => c.OrderBy(model => model.Name);  this delegate gets an IQueryable as input and in the body of the method return an IOrderedQueryable by calling OrderBy from the Context</param>
        /// <param name="count">Since count is nullable we should get the value</param>
        /// <returns></returns>
        public List<PaymentMethodDto> Get(Expression<Func<PaymentMethodDto, bool>> filter = null, Func<IQueryable<PaymentMethodDto>, IOrderedQueryable<PaymentMethodDto>> orderBy = null, int? count = default(int?))
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

        public void Update(PaymentMethodDto item)
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
        public void Delete(PaymentMethodDto item)
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

        public void Delete(Expression<Func<PaymentMethodDto, bool>> filter)
        {
            var query = Repository.Get().Where(filter);

            //if Unit is not null means that the method is called after another method in the service and so it should not save the changes and commit the transaction.
            if (Unit != null)
            {
                foreach (PaymentMethodDto item in query)
                {
                    Repository.Delete(item, false);
                }
            }
            //if Unit is null means that the method is the first and only method in the transaction and it can save the changes and commit the transaction.
            else
            {
                foreach (PaymentMethodDto item in query)
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
