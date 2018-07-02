using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.DTOs.OrderRelated
{
    public class ShipmentMethodDto
    {
        #region Keys
        public Guid Id { get; set; }
        #endregion

        #region Scalar properties
        public string Description { get; set; }
        #endregion

        #region Navigational properties
        public virtual IEnumerable<ShipmentCostDto> ShipmentCosts { get; set; }
        #endregion
    }
}
