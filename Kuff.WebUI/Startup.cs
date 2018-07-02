using System;
using System.Threading.Tasks;
using System.Web;
using Kuff.Common.DTOs.AccountRelated;
using Microsoft.Owin;
using Owin;
using Kuff.Service.Services.AccountRelated;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(Kuff.WebUI.Startup))]

namespace Kuff.WebUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.CreatePerOwinContext<IdentityContext>(IdentityContext.Create);
            app.CreatePerOwinContext<UserManager>(UserManager.Create);
            app.CreatePerOwinContext<RoleManager>(RoleManager.Create);
            app.CreatePerOwinContext<SignInManager>(SignInManager.Create);
            //AddAdmin();
            //CreateRoles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Accounts/Login")
            });
        }

        //private SignInManager CreateSignInManagerCallback()
        //{
        //    IAuthenticationManager auth = HttpContext.Current.GetOwinContext().Authentication;
        //    var usermng = HttpContext.Current.GetOwinContext().GetUserManager<UserManager>();
        //    return SignInManager.Create(usermng, auth);
        //}

        private void CreateRoles()
        {
            var context = new IdentityContext();
            UserManager userManager = new UserManager(new UserStore<ApplicationUser>(context));
            RoleManager roleManager = new RoleManager(new RoleStore<IdentityRole>(context));
            

            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser
                {
                    FirstName = "کیهان",
                    LastName = "کاشی",
                    Email = "Kayhan_Kashi@yahoo.com",
                    UserName = "Kayhan_Kashi",
                    City = "کرج",
                    Company = "K",
                    Address = "Mehrvila",
                    PhoneNumber = "0935",
                    CityState = "البرز",
                    PostalCode = "026",
                    TelephoneNumber = "323"
                };

                var result = userManager.Create(user, "Roland123456");
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);

            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);

            }
        }

        private void AddAdmin()
        {
            var context = new IdentityContext();
            UserManager userManager = new UserManager(new UserStore<ApplicationUser>(context));
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "کیهان",
                LastName = "کاشی",
                Email = "Kayhan_Kashi@yahoo.com",
                UserName = "Kayhan_Kashi",
                City = "کرج",
                Company = "K",
                Address = "Mehrvila",
                PhoneNumber = "0935",
                CityState = "البرز",
                PostalCode = "026",
                TelephoneNumber = "323",                        
            };

            var result = userManager.Create(user, "Roland123456");
            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
