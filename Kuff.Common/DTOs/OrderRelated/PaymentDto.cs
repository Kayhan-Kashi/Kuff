using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.DTOs.OrderRelated
{
    public class PaymentDto
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

        #region PaymentMethod properties
        public string  PaymentMethodDescription { get; set; }
        #endregion
    }
}
