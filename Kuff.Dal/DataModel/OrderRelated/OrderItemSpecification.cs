using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Dal.DataModel.OrderRelated
{
    public class OrderItemSpecification
    {

        #region Keys
        public Guid Id { get; set; }
        public Guid OrderItemId { get; set; }
        #endregion

        #region Scalar properties
        public string Title { get; set; }
        public string Value { get; set; }
        #endregion

        #region Navigational properties
        public virtual OrderItem OrderItem { get; set; }
        #endregion
    }
}
