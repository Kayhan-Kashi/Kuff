using System.Web.Mvc;

namespace Kuff.WebUI.Areas.Account
{
    public class AccountAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Account";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Account_default1",
                "Accounts/{action}/{email}/{token}",
                new { controller = "Accounts", action = "Index", id = UrlParameter.Optional, area = "Account" }
            );

            context.MapRoute(
                "Account_default",
                "Accounts/{action}/{id}",
                new { controller = "Accounts", action = "Index", id = UrlParameter.Optional, area = "Account" }
            );

        }
    }
}