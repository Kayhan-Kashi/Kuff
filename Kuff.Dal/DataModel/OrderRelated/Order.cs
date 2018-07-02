using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.AccountRelated;
using Kuff.Dal.DataModel.ProductRelated;

namespace Kuff.Dal.DataModel.OrderRelated
{
    public class Order
    {
        #region Keys
        public Guid Id { get; set; }
        public Guid PaymentId { get; set; }
        public Guid ShipmentCostId { get; set; }
        public string UserId { get; set; }
        #endregion

        #region Scalar properties
        public string Date { get; set; }
        public bool IsPaid { get; set; }
        public decimal Discount { get; set; }
        public string UserCommentsAboutOrder { get; set; }
        public decimal FinalPriceWithShipmentCost { get; set; }
        #endregion

        #region Navigational properties
        public virtual ApplicationUser User { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual ShipmentCost ShipmentCost { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
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

        #region Methods
        public void AddOrderItem(OrderItem orderItem)
        {
            if (OrderItems.Count == 0)
            {
                OrderItems.Add(orderItem);
            }
            else
            {
                OrderItem sameOrderItemFound = null;
                if (IsOrderItemExisted(orderItem, out sameOrderItemFound))
                {
                    sameOrderItemFound.Quantity += orderItem.Quantity;
                }
                else
                {
                    OrderItems.Add(orderItem);
                }
            }
        }

        public void RemoveOrderItem(OrderItem orderItem)
        {
            if (orderItem != null)
            {
                OrderItems.Remove(orderItem);
            }
        }
        public bool IsOrderItemExisted(OrderItem newOrderItem, out OrderItem sameOrderItemFound)
        {
            OrderItem orderItemSelected = null;
            int orderItemCounter = 0;
            int totalOrderItems = OrderItems.Count;

            foreach (OrderItem previousAddedOrderItem in OrderItems)
            {

                orderItemCounter++;
                if (newOrderItem.ProductPrice.Id == previousAddedOrderItem.ProductPrice.Id)
                {
                    int newSpecCounter = newOrderItem.OrderItemSpecifications.Count;
                    int counter = 0;

                    foreach (OrderItemSpecification ordSpecInNewOrderItem in newOrderItem.OrderItemSpecifications)
                    {
                        if (previousAddedOrderItem.OrderItemSpecifications.Where(o => o.Value == ordSpecInNewOrderItem.Value).FirstOrDefault() != null)
                        {
                            counter++;
                            if (counter == newSpecCounter)
                            {
                                orderItemSelected = previousAddedOrderItem;
                            }
                        }
                        else
                        {
                            sameOrderItemFound = null;
                            //return false;
                            break;
                        }
                    }
                }
                else if (orderItemCounter < totalOrderItems)
                {
                    sameOrderItemFound = null;
                    continue;
                }
                else
                {
                    sameOrderItemFound = null;
                    return false;
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////

                int c = 0;
                foreach (ProductPropertyValue newProdValue in previousAddedOrderItem.ProductPrice.Product.ProductPropertyValues)
                {
                    c++;
                    if (previousAddedOrderItem.ProductPrice.Product.ProductPropertyValues.Where(p => p.Value == newProdValue.Value).FirstOrDefault() != null)
                    {
                        if (c == previousAddedOrderItem.ProductPrice.Product.ProductPropertyValues.Count)
                        {
                            sameOrderItemFound = orderItemSelected;
                            return true;
                        }
                    }
                    else if (orderItemCounter < totalOrderItems)
                    {
                        sameOrderItemFound = null;
                        break;
                    }
                    else
                    {
                        sameOrderItemFound = null;
                        return false;
                    }
                }
            }

            sameOrderItemFound = orderItemSelected;
            return true;
        }
        #endregion
    }
}
