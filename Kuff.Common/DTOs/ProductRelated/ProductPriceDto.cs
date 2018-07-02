using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.DTOs.ProductRelated
{
    public class ProductPriceDto
    {
        #region Keys
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        #endregion

        #region Scalar properties
        public string Date { get; set; }
        public decimal PriceWithoutDiscount { get; set; }
        public decimal PriceWithDiscount { get; set; }
        #endregion

        #region Readonly properties

        public decimal Discount
        {
            get { return PriceWithoutDiscount - PriceWithDiscount; }
        }

        public decimal DiscountInPercent
        {
            get
            {
               return  (1 - PriceWithDiscount / PriceWithoutDiscount) * 100;
            }
        }

        #endregion

        #region Product properties
        public string ProductDateOfAdding { get; set; }
        [Display(Name = "خلاصه تیتر محصول")]
        public string ProductSummary { get; set; }
        [Display(Name = "توضیحات محصول")]
        public string ProductComments { get; set; }
        #endregion
    }
}
