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
    public interface IPaymentMethodService : IService
    {
        void Insert(PaymentMethodDto item);
        List<PaymentMethodDto> Get(Expression<Func<PaymentMethodDto, bool>> filter = null, Func<IQueryable<PaymentMethodDto>, IOrderedQueryable<PaymentMethodDto>> orderBy = null, int? count = null);
        void Update(PaymentMethodDto item);
        void Delete(PaymentMethodDto item);
        void Delete(Expression<Func<PaymentMethodDto, bool>> filter);
    }
}
