using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Dal.DataModel.OrderRelated
{
    public class ShipmentMethod
    {
        #region Keys
        public Guid Id { get; set; }
        #endregion

        #region Scalar properties
        public string Description { get; set; }
        #endregion

        #region Navigational propeties
        public virtual ICollection<ShipmentCost> ShipmentCosts { get; set; }
        #endregion
    }
}
