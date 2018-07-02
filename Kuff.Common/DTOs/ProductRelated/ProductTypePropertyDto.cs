using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.DTOs.ProductRelated
{
    public class ProductTypePropertyDto
    {
        #region Keys
        public Guid Id { get; set; }

        public Guid ProductTypeId { get; set; }
        public Guid DataTypeId { get; set; }

        #endregion

        #region Scalar properties
        [Required]
        public string Title { get; set; }

        public int OrderNumber { get; set; }

        [Required]
        public bool IsUserDecision { get; set; }
        #endregion

        #region ProductType properties
        public string ProductTypeCode { get; set; }

        public string ProductTypeTitle { get; set; }

        public string ProductTypeDateOfCreation { get; set; }

        public string ProductTypeComments { get; set; }
        #endregion

        #region DataType properties
        public string DataTypeName { get; set; }
        public string DataTypeControlToRender { get; set; }
        #endregion

    }
}
