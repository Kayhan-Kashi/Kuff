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
    public interface IProductPropertyValueService : IService
    {
        List<ProductPropertyValueDto> Get(Expression<Func<ProductPropertyValueDto, bool>> filter);
    }
}
