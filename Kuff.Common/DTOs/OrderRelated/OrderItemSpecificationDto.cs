using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.DTOs.OrderRelated
{
    public class OrderItemSpecificationDto
    {
        #region Keys
        public Guid Id { get; set; }
        public Guid OrderItemId { get; set; }
        #endregion

        #region Scalar properties

        public string Title { get; set; }
        public string Value { get; set; }
        #endregion

        #region OrderItem properties
        public int OrderItemQuantity { get; set; }
        public decimal OrderItemPriceWithoutDiscount { get; set; }
        public decimal OrderItemPriceWithDiscount { get; set; }
        public string OrderItemOrderItemDescription { get; set; }
        public string OrderItemProductSummary { get; set; }
        #endregion
    }
}
