using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.DTOs.ProductRelated
{
    public class ProductAvailabilityDto
    {
        #region Keys
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        #endregion

        #region Scalar properties
        public string Date { get; set; }
        public bool IsAvailable { get; set; }
        #endregion

        #region Product properties
        public string ProductDateOfAdding { get; set; }
        public string ProductSummary { get; set; }
        public string ProductComments { get; set; }
        #endregion
    }
}
