using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Kuff.Common.DTOs.AccountRelated;

namespace Kuff.WebUI.Areas.Account.Models
{
    public class RegisterViewModel
    {
        public Kuff.Common.DTOs.AccountRelated.ApplicationUser User { get; set; }


        [Required(ErrorMessage = "لطفا رمز عبور را وارد فرمائید")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "باید بیشتر از 3 حرف باشد", MinimumLength = 3)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "لطفا رمز عبور را صحیح تکرار کنید ")]
        public string ConfirmPass { get; set; }

        [Required]
        public bool Agreement { get; set; }
    }
}