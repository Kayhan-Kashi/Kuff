using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.Concretes.Products;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Common.Interfaces.Service;
using Kuff.Common.Interfaces.UnitOfWork;
using Kuff.Dal.Repositories.ProductRelated;
using Kuff.Service.Interfaces.ProductRelated;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Kuff.Service.Services.ProductRelated
{
    public class ProductPriceService : IProductPriceService
    {
        #region Constructors
        public ProductPriceService(IRepository<ProductPriceDto> repository)
        {
            Repository = repository as ProductPriceRepository;
        }
        #endregion

        #region Fields
        private IUnitOfWork _unit;
        #endregion

        #region Properties
        protected ProductPriceRepository Repository { get; set; }
        IRepository IService.Repository
        {
            get
            {
                return Repository;
            }

            set
            {
                Repository = value as ProductPriceRepository;
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

        #region IProductPriceService methods
        public void Insert(ProductPriceDto item)
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

        public List<ProductPriceDto> Get(Expression<Func<ProductPriceDto, bool>> filter = null, Func<IQueryable<ProductPriceDto>, IOrderedQueryable<ProductPriceDto>> orderBy = null, int? count = default(int?))
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

        public void Update(ProductPriceDto item)
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

        public void Delete(ProductPriceDto item)
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

        public void Delete(Expression<Func<ProductPriceDto, bool>> filter)
        {
            try
            {
                var queries = Repository.Get().Where(filter);

                //if Unit is not null means that the method is called after another method in the service and so it should not save the changes and commit the transaction.
                if (Unit != null)
                {
                    foreach (var item in queries)
                    {
                        Repository.Delete(item, false);
                    }
                }
                //if Unit is null means that the method is the first and only method in the transaction and it can save the changes and commit the transaction.
                else
                {
                    foreach (var item in queries)
                    {
                        // we could not pass true for save parameter because we get the error ""New transaction is not allowed because there are other threads running in the session".
                        Repository.Delete(item, false);
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
        protected T GetService<T>() where T : IService
        {
            T service = EnterpriseLibraryContainer.Current.GetInstance<T>();
            if (this.Unit != null) this.Unit.AddService(service);
            return service;
        }
        #endregion
    }
}
