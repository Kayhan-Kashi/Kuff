using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Kuff.Common.DTOs.ProductRelated;

namespace Kuff.WebUI.Areas.Store.Models
{
    public class ProductDetailsViewModel
    {
        public ProductDto Product { get; set; }

       
        public ICollection<PropertyName_PossibleValues> HasToChooseProperties { get; set; }

        public ProductDetailsViewModel(ProductDto prod)
        {
            Product = prod;
            foreach (ProductPropertyValueDto pValue in prod.ProductPropertyValues)
            {
                HasToChooseProperties = GetHasToChoosePropertiesFromProduct(Product.ProductPropertyValues).ToArray();
            }
        }

        public IEnumerable<PropertyName_PossibleValues> GetHasToChoosePropertiesFromProduct(IEnumerable<ProductPropertyValueDto> prodPropValues)
        {
            foreach (ProductPropertyValueDto pValue in prodPropValues)
            {
                if (pValue.ProductTypePropertyIsUserDecision)
                {
                    yield return new PropertyName_PossibleValues { PropertyName = pValue.ProductTypePropertyTitle, propValues = pValue.Value.Split(';') };
                }
            }
        }
    }

    public class PropertyName_PossibleValues
    {
        public string PropertyName { get; set; }
        public string[] propValues { get; set; }
    }
}
