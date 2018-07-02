using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.Interfaces.Service;
using Kuff.Common.DTOs.ProductRelated;

namespace Kuff.Service.Interfaces.ProductRelated
{
    public interface IProductPriceService : IService
    {
        void Insert(ProductPriceDto item);
        List<ProductPriceDto> Get(Expression<Func<ProductPriceDto, bool>> filter = null, Func<IQueryable<ProductPriceDto>, IOrderedQueryable<ProductPriceDto>> orderBy = null, int? count = null);
        void Update(ProductPriceDto item);
        void Delete(ProductPriceDto item);
        void Delete(Expression<Func<ProductPriceDto, bool>> filter);
    }
}
