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
using Kuff.Dal.Repositories.ProductRelated;
using Kuff.Service.Interfaces.ProductRelated;

namespace Kuff.Service.Services.ProductRelated
{
    public class ProductAvailabilityService : IProductAvailabilityService
    {

        public ProductAvailabilityService(IRepository<ProductAvailabilityDto> repository)
        {
            Repository = repository as ProductAvailabilityRepository;
        }


        public ProductAvailabilityRepository Repository { get; set; }
   

        IRepository IService.Repository
        {
            get
            {
                return Repository;
            }

            set
            {
               Repository = value as ProductAvailabilityRepository;
            }
        }

        public IUnitOfWork Unit { get; set; }

        public void Delete(ProductAvailabilityDto item)
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

        public void Delete(Expression<Func<ProductAvailabilityDto, bool>> filter)
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

        public List<ProductAvailabilityDto> Get(Expression<Func<ProductAvailabilityDto, bool>> filter = null, Func<IQueryable<ProductAvailabilityDto>, IOrderedQueryable<ProductAvailabilityDto>> orderBy = null, int? count = default(int?))
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

        public void Insert(ProductAvailabilityDto item)
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

        public void Update(ProductAvailabilityDto item)
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
    }
}
