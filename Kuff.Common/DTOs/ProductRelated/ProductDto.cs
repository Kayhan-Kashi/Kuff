using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.DTOs.ProductRelated
{
    public class ProductDto
    {
        #region Keys
        public Guid Id { get; set; }
        public Guid ProductTypeId { get; set; }
        #endregion

        #region Scalar Properties
        public string DateOfAdding { get; set; }
        [Display(Name = "خلاصه تیتر")]
        public string Summary { get; set; }
        [Display(Name = "توضیحات")]
        public string Comments { get; set; }
        #endregion

        #region ProductType properties
        public string ProductTypeCode { get; set; } 
        public string ProductTypeDateOfCreation { get; set; }
        public string ProductTypeComments { get; set; }
        public string ProductTypeTitle { get; set; }
        #endregion

        #region Navigational properties
        public virtual IEnumerable<ProductPropertyValueDto> ProductPropertyValues { get; set; }
        public virtual IEnumerable<ProductPictureDto> ProductPictures { get; set; }
        public virtual IEnumerable<ProductAvailabilityDto> ProductAvailabilities { get; set; }
        public virtual IEnumerable<ProductPriceDto> ProductPrices { get; set; }
        #endregion

        #region Readonly properties
        public ProductPriceDto LastProductPrice
        {
            get
            {
                try
                {
                    return ProductPrices.OrderByDescending(p => p.Date).FirstOrDefault();
                }
                catch
                {

                }
                return null;
            }
        }
        public Guid ProductPriceId
        {
            get
            {
                try
                {
                    if (LastProductPrice != null)
                    {
                        return LastProductPrice.Id;
                    }
                }
                catch
                {

                }
                return Guid.Empty;
            }
        }
        public decimal LastPriceWithDiscount
        {
            get
            {
                try
                {
                    if (LastProductPrice != null)
                    {
                        return LastProductPrice.PriceWithDiscount;
                    }
                }
                catch
                {

                }
                return 0;
            }
        }
        public decimal LastPriceWithoutDiscount
        {
            get
            {
                try
                {
                    if (LastProductPrice != null)
                    {
                        return LastProductPrice.PriceWithoutDiscount;
                    }
                }
                catch
                {

                }
                return 0;
            }
        }
        public bool IsAvailable
        {
            get
            {
                try
                {
                    var productAvailabilityDto = ProductAvailabilities.OrderByDescending(p => p.Date).FirstOrDefault();
                    return productAvailabilityDto != null && productAvailabilityDto.IsAvailable;
                }
                catch
                {

                }
                return false;
            }
        }
        public bool HasDiscount
        {
            get
            {
                return LastPriceWithoutDiscount > LastPriceWithDiscount;
            }
        }
        public decimal Discount
        {
            get
            {
                try
                {
                    if (ProductPrices != null)
                    {
                        return Math.Round(ProductPrices.OrderByDescending(p => p.Date).FirstOrDefault().Discount);
                    }
                }
                catch
                {

                }

                return 0;

            }
        }
        public decimal DiscountInPercent
        {
            get
            {
                try
                {
                    if (ProductPrices != null)
                    {
                        return Math.Round(ProductPrices.OrderByDescending(p => p.Date).FirstOrDefault().DiscountInPercent);
                    }
                }
                catch
                {

                }

                return 0;
            }
        }
        #endregion

        #region Category properties
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        #endregion
    }
}
