using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.OrderRelated;
using Kuff.Common.Interfaces.Service;

namespace Kuff.Service.Interfaces.OrderRelated
{
    public interface IShipmentMethodService : IService
    {
        void Insert(ShipmentMethodDto item);
        List<ShipmentMethodDto> Get(Expression<Func<ShipmentMethodDto, bool>> filter = null, Func<IQueryable<ShipmentMethodDto>, IOrderedQueryable<ShipmentMethodDto>> orderBy = null, int? count = null);
        void Update(ShipmentMethodDto item);
        void Delete(ShipmentMethodDto item);
        void Delete(Expression<Func<ShipmentMethodDto, bool>> filter);
    }
}
