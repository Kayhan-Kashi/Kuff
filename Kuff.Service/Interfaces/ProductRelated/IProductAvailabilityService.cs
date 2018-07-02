using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Service;

namespace Kuff.Service.Interfaces.ProductRelated
{
    public interface IProductAvailabilityService : IService
    {
        void Insert(ProductAvailabilityDto item);
        List<ProductAvailabilityDto> Get(Expression<Func<ProductAvailabilityDto, bool>> filter = null, Func<IQueryable<ProductAvailabilityDto>, IOrderedQueryable<ProductAvailabilityDto>> orderBy = null, int? count = null);
        void Update(ProductAvailabilityDto item);
        void Delete(Expression<Func<ProductAvailabilityDto, bool>> filter);
        void Delete(ProductAvailabilityDto item);
    }
}
