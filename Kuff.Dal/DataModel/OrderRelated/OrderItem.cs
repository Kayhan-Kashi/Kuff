using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Dal.DataModel.ProductRelated;

namespace Kuff.Dal.DataModel.OrderRelated
{
    public class OrderItem
    {
        #region Keys
        public Guid Id { get; set; }
        public Guid ProductPriceId { get; set; }
        public Guid OrderId { get; set; }
        public Guid OrderItemPictureId { get; set; }
        #endregion

        #region Scalar properties
        public int Quantity { get; set; }
        public decimal PriceWithoutDiscount { get; set; }
        public decimal PriceWithDiscount { get; set; }
        //public string Description { get; set; }
        public string ProductSummary { get; set; }
        #endregion

        #region Readonly properties
        public decimal DiscountInPercent
        {
            get
            {
                try
                {
                    return (1 - ((decimal)PriceWithDiscount / PriceWithoutDiscount)) * 100;
                }
                catch
                {

                }
                return 0;
            }
        }
        public decimal Discount
        {
            get
            {
                return PriceWithoutDiscount - PriceWithDiscount;
            }
        }
        public decimal QuantityWholePriceWithDiscount
        {
            get
            {
                return PriceWithDiscount * Quantity;
            }
        }
        public decimal QuantityWholePriceWithoutDiscount
        {
            get
            {
                return PriceWithoutDiscount * Quantity;
            }
        }
        #endregion

        #region Navigational properties
        public virtual Order Order { get; set; }
        public virtual ProductPrice ProductPrice { get; set; }
        public virtual ICollection<OrderItemSpecification> OrderItemSpecifications { get; set; }
        #endregion
    }
}
