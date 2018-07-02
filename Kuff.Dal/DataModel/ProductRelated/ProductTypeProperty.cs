using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Dal.DataModel.ProductRelated
{
    public class ProductTypeProperty
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

        #region Navigational properties
        public virtual ProductType ProductType { get; set; }
        public virtual DataType DataType { get; set; }
        #endregion

    }
}
