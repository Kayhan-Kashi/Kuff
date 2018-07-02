using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Dal.DataModel.ProductRelated
{
    public class Category
    {
        #region Keys
        public Guid Id { get; set; }
        #endregion

        #region Scalar properties
        public string Name { get; set; }
        public string Description { get; set; }
        #endregion

        #region Navigational properties
        public virtual ICollection<ProductType> ProductTypes { get; set; }
        #endregion 
    }
}
