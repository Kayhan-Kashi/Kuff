using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kuff.Common.DTOs.OrderRelated;

namespace Kuff.WebUI.Areas.Store.Models
{
    public class CartViewModel
    {
        public CartViewModel()
        {

        }

        public CartViewModel(ICollection<OrderItemDto> orderItems)
        {
            OrderItems = orderItems;
            decimal totalPriceWithEachOrderItemDiscount = 0;
            foreach (var ordItem in orderItems)
            {
                totalPriceWithEachOrderItemDiscount += ordItem.QuantityWholePriceWithDiscount;
            }
            TotalPriceWithEachOrderItemDiscount = totalPriceWithEachOrderItemDiscount;
            FinalPriceWithoutShipmentCost = TotalPriceWithEachOrderItemDiscount - OrderDiscount;
        }

        public IEnumerable<OrderItemDto> OrderItems { get; set; }
        public decimal TotalPriceWithEachOrderItemDiscount { get; set; }
        public decimal FinalPriceWithoutShipmentCost { get; set; }
        public decimal OrderDiscount { get; set; }
    }
}