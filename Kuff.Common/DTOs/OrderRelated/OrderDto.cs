using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.AccountRelated;
using Kuff.Common.DTOs.ProductRelated;

namespace Kuff.Common.DTOs.OrderRelated
{
    public class OrderDto
    {
        #region Keys
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid PaymentId { get; set; }
        public Guid ShipmentCostId { get; set; }


        #endregion

        #region Scalar properties
        public string Date { get; set; }
        public bool IsPaid { get; set; }
        public decimal Discount { get; set; }
        public string UserCommentsAboutOrder { get; set; }
        public decimal FinalPriceWithShipmentCost { get; set; }
        #endregion

        #region Navigational properties
        public virtual IEnumerable<OrderItemDto> OrderItems { get; set; }
        #endregion

        #region Readonly properties

        public decimal TotalPriceWithoutEachOrderItemDiscount
        {
            get
            {
                return OrderItems.Sum(o => o.QuantityWholePriceWithoutDiscount);   
            }
        }

        public decimal TotalPriceWithEachOrderItemDiscount
        {
            get
            {
                return OrderItems.Sum(o => o.QuantityWholePriceWithDiscount);
            }
        }

        public decimal FinalPriceWithoutShipmentCost
        {
            get
            {
                return (TotalPriceWithEachOrderItemDiscount - Discount);
            }
        }
        #endregion

        //#region Methods
        //public void AddOrderItem(OrderItemDto orderItem)
        //{
        //    if (OrderItems.Count == 0)
        //    {
        //        OrderItems.Add(orderItem);
        //    }
        //    else
        //    {
        //        OrderItemDto sameOrderItemFound = null;
        //        if (IsOrderItemExisted(orderItem, out sameOrderItemFound))
        //        {
        //            sameOrderItemFound.Quantity += orderItem.Quantity;
        //        }
        //        else
        //        {
        //            OrderItems.Add(orderItem);
        //        }
        //    }
        //}

        //public void RemoveOrderItem(OrderItemDto orderItem)
        //{
        //    if (orderItem != null)
        //    {
        //        OrderItems.Remove(orderItem);
        //    }
        //}
        //public bool IsOrderItemExisted(OrderItemDto newOrderItem, out OrderItemDto sameOrderItemFound)
        //{
        //    OrderItemDto orderItemSelected = null;
        //    int orderItemCounter = 0;
        //    int totalOrderItems = OrderItems.Count;

        //    foreach (OrderItemDto previousAddedOrderItem in OrderItems)
        //    {

        //        orderItemCounter++;
        //        if (newOrderItem.ProductPriceId == previousAddedOrderItem.ProductPriceId)
        //        {
        //            int newSpecCounter = newOrderItem.OrderItemSpecifications.Count;
        //            int counter = 0;

        //            foreach (OrderItemSpecificationDto ordSpecInNewOrderItem in newOrderItem.OrderItemSpecifications)
        //            {
        //                if (previousAddedOrderItem.OrderItemSpecifications.Where(o => o.Value == ordSpecInNewOrderItem.Value).FirstOrDefault() != null)
        //                {
        //                    counter++;
        //                    if (counter == newSpecCounter)
        //                    {
        //                        orderItemSelected = previousAddedOrderItem;
        //                    }
        //                }
        //                else
        //                {
        //                    sameOrderItemFound = null;
        //                    //return false;
        //                    break;
        //                }
        //            }
        //        }
        //        else if (orderItemCounter < totalOrderItems)
        //        {
        //            sameOrderItemFound = null;
        //            continue;
        //        }
        //        else
        //        {
        //            sameOrderItemFound = null;
        //            return false;
        //        }

        //        ///////////////////////////////////////////////////////////////////////////////////////////////

        //        int c = 0;
        //        foreach (ProductPropertyValueDto newProdValue in previousAddedOrderItem.ProductPrice.Product.ProductPropertyValues)
        //        {
        //            c++;
        //            if (previousAddedOrderItem.ProductPrice.Product.ProductPropertyValues.Where(p => p.Value == newProdValue.Value).FirstOrDefault() != null)
        //            {
        //                if (c == previousAddedOrderItem.ProductPrice.Product.ProductPropertyValues.Count)
        //                {
        //                    sameOrderItemFound = orderItemSelected;
        //                    return true;
        //                }
        //            }
        //            else if (orderItemCounter < totalOrderItems)
        //            {
        //                sameOrderItemFound = null;
        //                break;
        //            }
        //            else
        //            {
        //                sameOrderItemFound = null;
        //                return false;
        //            }
        //        }
        //    }

        //    sameOrderItemFound = orderItemSelected;
        //    return true;
        //}
        //#endregion

        #region Payment properties
        public bool PaymentIsPayingDone { get; set; }
        public string PaymentDateOfPayment { get; set; }
        public decimal PaymentPayingAmount { get; set; }
        #endregion

        #region ShipmentCost properties
        public string ShipmentCostDepartureCity { get; set; }
        public string ShipmentCostDestinationCity { get; set; }
        public string ShipmentCostDateOfAddedShipmentCost { get; set; }
        public decimal ShipmentCostCost { get; set; }
        #endregion

        #region PaymentMethod properties
        public Guid PaymentMethodId { get; set; }    
        public string PaymentMethodDescription { get; set; }
        #endregion

        #region User 
        public string UserUserName { get; set; }
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserTelephoneNumber { get; set; }
        public string UserCityState { get; set; }
        public string UserCity { get; set; }
        public string UserAddress { get; set; }
        public string UserPostalCode { get; set; }
        public string UserCompany { get; set; }
        #endregion

        public ApplicationUser User { get; set; }
    }
}
