using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Dal.DataModel.ProductRelated
{
    public class ProductAvailability
    {
        #region Keys
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        #endregion

        #region Scalar properties
        public string Date { get; set; }
        public bool IsAvailable { get; set; }
        #endregion

        #region Navigation properties
        public Product Product { get; set; }
        #endregion
    }
}
