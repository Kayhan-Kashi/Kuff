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
    public interface ICategoryService : IService
    {
        void Insert(CategoryDto item);
        List<CategoryDto> Get(Expression<Func<CategoryDto, bool>> filter = null, Func<IQueryable<CategoryDto>, IOrderedQueryable<CategoryDto>> orderBy = null, int? count = null);
        void Update(CategoryDto item);
        void Delete(CategoryDto item);
        void Delete(Expression<Func<CategoryDto, bool>> filter);
        void CombineService();
    }
}
