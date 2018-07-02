using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.DTOs.OrderRelated
{
    public class OrderItemDto
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

        public string Description
        {
            get
            {
                string summary = ProductSummary;
                string str = "";
                if (OrderItemSpecifications != null)
                {
                    int counter = OrderItemSpecifications.Count();
                    //string[] strArray = new string[counter];
                    foreach (OrderItemSpecificationDto ordSpec in OrderItemSpecifications)
                    {
                        //strArray[--counter] = ordSpec.Value;
                        str += " " + ordSpec.Value + " ";
                    }
                }
                return summary + str;
            }
        }
        public string ProductSummary { get; set; }
        #endregion

        #region Readonly properties

        public bool HasDiscount
        {
            get { return Discount > 0; }
        }

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
        public virtual IEnumerable<OrderItemSpecificationDto> OrderItemSpecifications { get; set; }
        #endregion

        //#region Order properties
        //public string OrderDate { get; set; }
        //public bool OrderIsPaid { get; set; }
        //public decimal OrderDiscount { get; set; }
        //public string OrderUserCommentsAboutOrder { get; set; }
        //public decimal OrderFinalPrice { get; set; }
        //#endregion

        #region ProductPrice properties
        public string ProductPriceDateOfPrice { get; set; }
        public decimal ProductPricePriceWithoutDiscount { get; set; }
        public decimal ProductPricePriceWithDiscount { get; set; }
        #endregion
    }
}
