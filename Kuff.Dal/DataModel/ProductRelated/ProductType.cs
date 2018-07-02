using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Dal.DataModel.ProductRelated
{
    public class ProductType
    {

        #region Keys
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        #endregion

        #region Scalar properties
        public string Code { get; set; }

        [Required]
        public string Title { get; set; }
        public string DateOfCreation { get; set; }
        public string Comments { get; set; }
        #endregion

        #region Navigational properties
        public virtual ICollection<ProductTypeProperty> ProductTypeProperties { get; set; }
        public virtual Category Category { get; set; }
        #endregion
    }
}
