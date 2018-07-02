using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Service.Interfaces.ProductRelated
{
    public interface IProductTypeService : IService
    {
        void Insert(ProductTypeDto item);
        List<ProductTypeDto> Get(Expression<Func<ProductTypeDto, bool>> filter = null, Func<IQueryable<ProductTypeDto>, IOrderedQueryable<ProductTypeDto>> orderBy = null, int? count = null);
        void Update(ProductTypeDto item);
        void Delete(Expression<Func<ProductTypeDto, bool>> filter);
        void Delete(ProductTypeDto item);
    }

}
