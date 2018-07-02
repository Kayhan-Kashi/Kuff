using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Dal.DataModel.OrderRelated;

namespace Kuff.Dal.DataModel.ProductRelated
{
    public class ProductPrice
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

        //#region Readonly properties
        //public decimal Discount
        //{
        //    get
        //    {
        //        return PriceWithoutDiscount - PriceWithDiscount;
        //    }
        //}

        //public decimal DiscountInPercent
        //{
        //    get
        //    {
        //        return (1 - (PriceWithDiscount / PriceWithoutDiscount)) * 100;
        //    }
        //}
        //#endregion

        #region Navigational Properties
        public virtual Product Product { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        #endregion
    }
}
