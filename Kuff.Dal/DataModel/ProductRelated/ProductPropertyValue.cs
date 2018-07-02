
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Dal.DataModel.ProductRelated
{
    public class ProductPropertyValue
    {
        #region Keys

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductTypePropertyId { get; set; }
        #endregion

        #region Scalar Properties
        public string Value { get; set; }
        #endregion

        #region Navigational Properties
        public virtual Product Product { get; set; }
        public virtual ProductTypeProperty ProductTypeProperty { get; set; }
        #endregion
    }
}
