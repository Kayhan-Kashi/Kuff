using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.DTOs.ProductRelated
{
    public class ProductTypeDto
    {

        #region Keys
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        #endregion

        #region Scalar properties
        public string Code { get; set; }

        [Required]
        [Display(Name = "عنوان نوع محصول")]
        public string Title { get; set; }

        public string DateOfCreation { get; set; }

        [Display(Name = "توضیحات")]
        public string Comments { get; set; }
        #endregion

        #region Navigational properties
        public virtual IEnumerable<ProductTypePropertyDto> ProductTypeProperties { get; set; }
        #endregion

        #region Category properties
        [Display(Name = "نام دسته بندی")]
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        #endregion
    }
}
