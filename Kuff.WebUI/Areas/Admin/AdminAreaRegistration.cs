﻿using System.Web.Mvc;

namespace Kuff.WebUI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "List", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Admin1_default",
                "Admin/{controller}/{action}/{id}/{propertyTitle}",
                new {action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}