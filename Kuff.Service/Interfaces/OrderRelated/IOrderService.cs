using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.Interfaces.Service;
using Kuff.Common.DTOs.OrderRelated;

namespace Kuff.Service.Interfaces.OrderRelated
{
    public interface IOrderService : IService
    {
        void Insert(OrderDto viewModel);
    }
}
