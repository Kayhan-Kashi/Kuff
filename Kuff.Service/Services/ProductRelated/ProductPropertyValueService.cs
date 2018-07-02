using Kuff.Common.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Common.Interfaces.UnitOfWork;
using Kuff.Dal.Repositories.ProductRelated;
using Kuff.Service.Interfaces.ProductRelated;

namespace Kuff.Service.Services.ProductRelated
{
    public class ProductPropertyValueService : IProductPropertyValueService
    {
        public ProductPropertyValueService()
        {
            Repository = new ProductPropertyValueRepository();
        }

        public ProductPropertyValueRepository Repository { get; set; }
        IRepository IService.Repository
        {
            get
            {
                return Repository;
            }

            set
            {
                Repository = value as ProductPropertyValueRepository;
            }
        }

        public IUnitOfWork Unit { get; set; }

        public List<ProductPropertyValueDto> Get(Expression<Func<ProductPropertyValueDto, bool>> filter)
        {
            var query = Repository.Get();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }
    }
}
