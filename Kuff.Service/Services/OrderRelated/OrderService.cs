using Kuff.Service.Interfaces.OrderRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.AccountRelated;
using Kuff.Common.DTOs.OrderRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Common.Interfaces.Service;
using Kuff.Common.Interfaces.UnitOfWork;
using Kuff.Dal.DataModel.OrderRelated;
using Kuff.Dal.Repositories.OrderRelated;
using Kuff.Service.Interfaces.AccountRelated;
using Microsoft.AspNet.Identity;

namespace Kuff.Service.Services.OrderRelated
{
    public class OrderService : IOrderService
    {
        private IRepository<OrderDto> _orderRepository;
        //private IUserManager _userManager;

        public OrderService(IRepository<OrderDto> orderRepository)
        {
            this._orderRepository = orderRepository;
            //this._userManager = userManager;
        }

        #region Fields
        private IUnitOfWork _unit;
        #endregion

        #region Properties
        public OrderRepository Repository { get; set; }

        public IUnitOfWork Unit
        {
            get
            {
                return _unit;
            }

            set { _unit = value; }
        }

        IRepository IService.Repository
        {
            get { return Repository; }

            set
            {
                Repository = value as OrderRepository;
            }
        }
        #endregion

        public void Insert(OrderDto item)
        {
            //item.Id = Guid.NewGuid();
            //var userId = _userManager.Manager.FindByEmail(item.UserEmail).Id;
            
            foreach (OrderItemDto ordItem in item.OrderItems)
            {
                ordItem.OrderId = item.Id;
                foreach (OrderItemSpecificationDto ordSpec in ordItem.OrderItemSpecifications)
                {
                    ordSpec.OrderItemId = ordItem.Id;
                }
            }
            _orderRepository.Insert(item);
        }
    }
}
