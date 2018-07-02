using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Service;
using System.Linq.Expressions;

namespace Kuff.Service.Interfaces.ProductRelated
{
    public interface IDataTypeService : IService
    {
        void Insert(DataTypeDto item);
        List<DataTypeDto> Get(Expression<Func<DataTypeDto, bool>> filter = null, Func<IQueryable<DataTypeDto>, IOrderedQueryable<DataTypeDto>> orderBy = null, int? count = null);
        void Update(DataTypeDto item);
        void Delete(Expression<Func<DataTypeDto, bool>> filter);
        void Delete(DataTypeDto item);
    }
}
