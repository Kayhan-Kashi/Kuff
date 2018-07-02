using Kuff.Common.DTOs.OrderRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Service;

namespace Kuff.Service.Interfaces.OrderRelated
{
    public interface IShipmentCostService : IService
    {
        void Insert(ShipmentCostDto item);
        List<ShipmentCostDto> Get(Expression<Func<ShipmentCostDto, bool>> filter = null, Func<IQueryable<ShipmentCostDto>, IOrderedQueryable<ShipmentCostDto>> orderBy = null, int? count = null);
        void Update(ShipmentCostDto item);
        void Delete(ShipmentCostDto item);
        void Delete(Expression<Func<ShipmentCostDto, bool>> filter);
    }
}
