using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.DTOs.OrderRelated
{
    public class ShipmentCostDto
    {
        #region Keys
        public Guid Id { get; set; }
        public Guid ShipmentMethodId { get; set; }
        #endregion

        #region Scalar propeties
        public string DepartureCity { get; set; }
        public string DestinationCity { get; set; }
        public string DateOfAddedShipmentCost { get; set; }
        public decimal Cost { get; set; }
        #endregion

        #region ShipmentMethod propeties
        public string ShipmentMethodDescription { get; set; }
        #endregion
    }
}
