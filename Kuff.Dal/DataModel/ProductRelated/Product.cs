using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Dal.DataModel.ProductRelated
{
    public class Product
    {
        #region Keys
        public Guid Id { get; set; }
        public Guid ProductTypeId { get; set; }
        #endregion

        #region Scalar Properties
        public string DateOfAdding { get; set; }

        public string Summary { get; set; }

        public string Comments { get; set; }
        #endregion

        #region Navigational Properties
        public virtual ProductType ProductType { get; set; }
        public virtual ICollection<ProductPropertyValue> ProductPropertyValues { get; set; }
        public virtual ICollection<ProductPicture> ProductPictures { get; set; }
        public virtual ICollection<ProductAvailability> ProductAvailabilities { get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
        #endregion

        //#region Readonly properties

        //public ProductPrice LastProductPrice
        //{
        //    get
        //    {
        //        try
        //        {
        //            return ProductPrices.OrderByDescending(p => p.Date).FirstOrDefault();
        //        }
        //        catch
        //        {

        //        }
        //        return null;
        //    }
        //}

        //public Guid ProductPriceId
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (LastProductPrice != null)
        //            {
        //                return LastProductPrice.Id;
        //            }
        //        }
        //        catch
        //        {

        //        }
        //        return Guid.Empty;
        //    }
        //}

        //public decimal LastPriceWithDiscount
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (LastProductPrice != null)
        //            {
        //                return LastProductPrice.PriceWithDiscount;
        //            }
        //        }
        //        catch
        //        {

        //        }
        //        return 0;
        //    }
        //}

        //public decimal LastPriceWithoutDiscount
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (LastProductPrice != null)
        //            {
        //                return LastProductPrice.PriceWithoutDiscount;
        //            }
        //        }
        //        catch
        //        {

        //        }
        //        return 0;
        //    }
        //}

        //public bool IsAvailable
        //{
        //    get
        //    {
        //        try
        //        {
        //            return ProductAvailabilities.OrderByDescending(p => p.Date).FirstOrDefault().IsAvailable;
        //        }
        //        catch
        //        {

        //        }
        //        return false;
        //    }
        //}

        //public bool HasDiscount
        //{
        //    get
        //    {
        //        return LastPriceWithoutDiscount > LastPriceWithDiscount;
        //    }
        //}

        //public decimal Discount
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (ProductPrices != null)
        //            {
        //                return Math.Round(ProductPrices.OrderByDescending(p => p.Date).FirstOrDefault().Discount);
        //            }
        //        }
        //        catch
        //        {

        //        }

        //        return 0;

        //    }
        //}

        //public decimal DiscountInPercent
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (ProductPrices != null)
        //            {
        //                return Math.Round(ProductPrices.OrderByDescending(p => p.Date).FirstOrDefault().DiscountInPercent);
        //            }
        //        }
        //        catch
        //        {

        //        }

        //        return 0;
        //    }
        //}
        //#endregion

        #region Methods

        public void SetProductSummary()
        {
            if (ProductType != null && ProductPropertyValues != null)
            {
                // these are the property values that they are not included in the userDecision set and so we want to show them in product summary
                List<ProductPropertyValue> properties = ProductPropertyValues.Where(p => p.ProductTypeProperty.IsUserDecision == false).ToList();

                string pSummary;

                // This is an array that keep those not user desicioned properties from the end til the begining because of right to left problems
                string[] strArray = new string[properties.Count + 1];
                int counter = properties.Count;

                // at first we add the product type title of the product in the array for example : تی شرت and after that we add product property values
                strArray[counter] = ProductType.Title;

                foreach (ProductPropertyValue pValue in properties)
                {
                    strArray[--counter] = pValue.Value;
                }

                pSummary = "";

                for (int i = strArray.Length - 1; i >= 0; i--)
                {
                    pSummary += strArray[i];
                    pSummary += " ";
                }

                Summary = pSummary;
            }
        }

        #endregion
    }
}
