using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Common.Interfaces.UnitOfWork;
using Kuff.Service.Interfaces.ProductRelated;
using Kuff.Common.Interfaces.Service;
using Kuff.Dal.Repositories.ProductRelated;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Kuff.Service.Services.ProductRelated
{
    public class ProductTypeService : IProductTypeService
    {

        #region Constructors
        public ProductTypeService(IRepository<ProductTypeDto> productTypeRepository)
        {
            Repository = productTypeRepository as ProductTypeRepository;
        }
        #endregion

        #region Properties
        public ProductTypeRepository Repository { get; set; }
        IRepository IService.Repository
        {
            get
            {
                return Repository;
            }

            set
            {
                Repository = value as ProductTypeRepository;
            }
        }
        public IUnitOfWork Unit { get; set; }
        #endregion

        #region IProductTypeService Methods

        public void Insert(ProductTypeDto item)
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

        public List<ProductTypeDto> Get(Expression<Func<ProductTypeDto, bool>> filter = null, Func<IQueryable<ProductTypeDto>, IOrderedQueryable<ProductTypeDto>> orderBy = null, int? count = default(int?))
        {
            var query = Repository.Get();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.ToList();
        }

        public void Update(ProductTypeDto item)
        {
            if (Unit != null)
            {
                Repository.Update(item, false);
            }
            else
            {
                Repository.Update(item);
            }
        }

        public void Delete(ProductTypeDto item)
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

        public void Delete(Expression<Func<ProductTypeDto, bool>> filter)
        {
            var query = Repository.Get().Where(filter);

            if (Unit != null)
            {
                foreach (ProductTypeDto item in query)
                {
                    Repository.Delete(item, false);
                }
            }
            else
            {
                foreach (ProductTypeDto item in query)
                {
                    Repository.Delete(item, false);
                }
            }
            Repository.Save();
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
