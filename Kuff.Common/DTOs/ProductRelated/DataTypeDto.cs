using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.DTOs.ProductRelated
{
    public class DataTypeDto
    {
        #region Keys
        public Guid Id { get; set; }
        #endregion

        #region Scalar properties
        public string Name { get; set; }
        public string ControlToRender { get; set; }
        #endregion
    }
}
