using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kuff.WebUI.Areas.Account.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا نام کاربری را وارد کنید")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا پسورد را وارد فرمائید")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}