using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Kuff.Common.DTOs.AccountRelated
{
    public class ApplicationUser : IdentityUser
    {
        //public string Id { get; set; }

        [Required(ErrorMessage = "لطفا نام را وارد فرمائید")]
        //[Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد فرمائید")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "لطفا ایمیل را وارد فرمائید")]
        //[DataType(DataType.EmailAddress)]
        //[StringLength(450)]
        ////StringLength is used for making the using of IsUnique possible because without it string will be mapped to nvarchar(max)
        //// which couldn't be used as index
        //public string Email { get; set; }

        //[Required(ErrorMessage = "لطفا شماره موبایل را وارد فرمائید")]
        //public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "لطفا شماره تلفن را وارد فرمائید")]
        public string TelephoneNumber { get; set; }

        [Required(ErrorMessage = "لطفا کد شهر را وارد فرمائید")]
        public string CityState { get; set; }

        [Required(ErrorMessage = "لطفا نام شهر وارد فرمائید")]
        public string City { get; set; }

        [Required(ErrorMessage = "لطفا آدرس را وارد فرمائید")]
        public string Address { get; set; }

        [Required(ErrorMessage = "لطفا کد پستی را وارد فرمائید")]
        public string PostalCode { get; set; }

        public string Company { get; set; }
    }
}
