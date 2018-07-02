using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Dal.DataModel.OrderRelated
{
    public class Payment
    {

        #region Keys
        public Guid Id { get; set; }
        public Guid PaymentMethodId { get; set; }
        public Guid OrderId { get; set; }
        #endregion

        #region Scalar properties
        public bool IsPayingDone { get; set; }
        public string DateOfPayment { get; set; }
        public decimal PayingAmount { get; set; }
        #endregion

        #region Navigational properties
        public virtual Order Order { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        #endregion
    }
}
