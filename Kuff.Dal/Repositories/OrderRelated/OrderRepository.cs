using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kuff.Common.DTOs.AccountRelated;
using Kuff.Common.DTOs.OrderRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Dal.DataModel.OrderRelated;

namespace Kuff.Dal.Repositories.OrderRelated
{
    public class OrderRepository : IRepository<OrderDto>
    {
        #region Fields
        private KuffEntities _context;
        #endregion

        #region Constructors 
        public OrderRepository()
        {

        }
        #endregion 

        #region Properties
        public KuffEntities Context
        {
            get { return _context ?? (_context = new KuffEntities()); } // if context is null it will be initialized
            set
            {
                _context = value;
            }
        }
        DbContext IRepository.Context
        {
            get
            {
                return Context;
            }

            set
            {
                Context = value as KuffEntities;
            }
        }

        #endregion

        #region IRepository methods
        public void Insert(OrderDto item, bool save = true)
        {

            var order = MapDtoToModel(item);
            order.Payment = new Payment
            {
                Id = item.PaymentId,
                DateOfPayment = PersianDateTime.Now.ToString(),
                IsPayingDone = false,
                OrderId = item.Id,
                PayingAmount = item.PaymentPayingAmount,
                PaymentMethodId = item.PaymentMethodId
            };
        
            //item.UserId = Guid.Parse(item.User.Id);
            //m.User = null;
            Context.Orders.Add(order);

            if (save)
            {
                Context.SaveChanges();
            }
        }

        public IQueryable<OrderDto> Get()
        {
            return Context.Orders.Select(o => new OrderDto
            {
                Id = o.Id,
                Date = o.Date,
                FinalPriceWithShipmentCost = o.FinalPriceWithShipmentCost,
                IsPaid = o.IsPaid,
                Discount = o.Discount,
                OrderItems = o.OrderItems.Select(ordItem => new OrderItemDto
                {
                    PriceWithoutDiscount = ordItem.PriceWithoutDiscount,
                    Id = ordItem.Id,
                    PriceWithDiscount = ordItem.PriceWithDiscount,
                    ProductSummary = ordItem.ProductSummary,
                    ProductPriceId = ordItem.ProductPriceId,
                    OrderId = o.Id,
                    //Description = ordItem.OrderItemDescription,
                    OrderItemPictureId = ordItem.OrderItemPictureId,
                    OrderItemSpecifications = ordItem.OrderItemSpecifications.Select(os => new OrderItemSpecificationDto
                    {
                        Id = os.Id,
                        OrderItemId = os.OrderItemId,
                        Title = os.Title,
                        Value = os.Value
                    }),
                    ProductPriceDateOfPrice = ordItem.ProductPrice.Date,
                    ProductPricePriceWithDiscount = ordItem.ProductPrice.PriceWithDiscount,
                    ProductPricePriceWithoutDiscount = ordItem.ProductPrice.PriceWithoutDiscount
                }),
                //TotalPriceWithoutEachOrderItemDiscount = o.OrderItems.Sum(ordItem => ordItem.QuantityWholePriceWithoutDiscount),
                //TotalPriceWithEachOrderItemDiscount = o.OrderItems.Sum(ordItem => ordItem.QuantityWholePriceWithDiscount),
                //FinalPriceWithoutShipmentCost = o.TotalPriceWithEachOrderItemDiscount - o.Discount,
                PaymentId = o.PaymentId,
                PaymentMethodId = o.Payment.PaymentMethodId
            });
        }

        public void Update(OrderDto item, bool save = true)
        {
            try
            {
                // Get existing Category object from database
                Order olditem = GetFromModel().FirstOrDefault(x => x.Id.Equals(item.Id));

                // Set the new values for the fetched Category object
                if (olditem != null)
                {
                    olditem.Date = item.Date;
                    olditem.Discount = item.Discount;
                    olditem.FinalPriceWithShipmentCost = item.FinalPriceWithShipmentCost;

                    if (save)
                    {
                        this.Context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Delete(OrderDto item, bool save = true)
        {
            try
            {
                // Get existing model object from database
                Order oldItem = GetFromModel().FirstOrDefault(o => o.Id.Equals(item.Id));

                if (oldItem != null)
                {
                    Context.Orders.Remove(oldItem);
                }

                if (save)
                {
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IDisposable Methods
        public void Dispose()
        {
            Context.Dispose();
        }
        #endregion

        #region Methods 
        private IQueryable<Order> GetFromModel()
        {
            var query = Context.Orders;
            return query;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
        #endregion

        #region Mappers
        public Order MapDtoToModel(OrderDto viewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderDto, Order>();
                cfg.CreateMap<PaymentDto, Payment>();
                cfg.CreateMap<ShipmentCostDto, ShipmentCost>();
                cfg.CreateMap<OrderItemDto, OrderItem>();
                cfg.CreateMap<OrderItemSpecificationDto, OrderItemSpecification>();
                cfg.CreateMap<ApplicationUser, ApplicationUser>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<Order>(viewModel);
        }

        public OrderDto MapModelToDto(Order model)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<Payment, PaymentDto>();
                cfg.CreateMap<ShipmentCost, ShipmentCostDto>();
                cfg.CreateMap<OrderItem, OrderItemDto>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<OrderDto>(model);
        }
        #endregion
    }
}
