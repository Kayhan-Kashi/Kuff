using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.DTOs.ProductRelated
{
    public class ProductPropertyValueDto
    {
        #region Keys

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductTypePropertyId { get; set; }
        #endregion

        #region Scalar Properties
        public string Value { get; set; }
        #endregion

        #region Product Properties
        public string ProductDateOfAdding { get; set; }

        public string ProductSummary { get; set; }

        public string ProductComments { get; set; }
        #endregion

        #region ProductTypeProperty properties
        
        public string ProductTypePropertyTitle { get; set; }

        public int ProductTypePropertyOrderNumber { get; set; }

        public bool ProductTypePropertyIsUserDecision { get; set; }
        #endregion
    }
}
