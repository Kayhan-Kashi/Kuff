using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.AccountRelated;
using Kuff.Dal;
using Kuff.Service.Interfaces.AccountRelated;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Kuff.Service.Services.AccountRelated
{
    public class UserManager : UserManager<ApplicationUser>
    {

        public UserManager(IUserStore<ApplicationUser> store) : base(store)
        {

        }

        public static UserManager Create(IdentityFactoryOptions<UserManager> options, IOwinContext context)
        {
            IdentityContext db = context.Get<IdentityContext>();
            UserManager manager = new UserManager(new UserStore<ApplicationUser>(db));

            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = false
            };

            return manager;
        }






        //#region Fields
        //private KuffEntities db;
        //#endregion

        //#region Constructors
        //public UserManager() : base(new UserStore<ApplicationUser>(new KuffEntities()))
        //{
        //    //db = new KuffEntities();
        //    //Manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        //    UserValidator = new UserValidator<ApplicationUser>(this)
        //    {
        //        AllowOnlyAlphanumericUserNames = true,
        //        RequireUniqueEmail = true
        //    };

        //    PasswordValidator = new PasswordValidator
        //    {
        //        RequiredLength = 6,
        //        RequireNonLetterOrDigit = false,
        //        RequireDigit = false,
        //        RequireLowercase = true,
        //        RequireUppercase = false
        //    };
        //}
        //#endregion

        //#region Properties
        //public UserManager<ApplicationUser> Manager { get; set; }
        //#endregion

        //#region Static methods

        //public static UserManager Create()
        //{
        //    KuffEntities db = new KuffEntities();
        //    UserManager manager = new UserManager();

        //    manager.UserValidator = new UserValidator<ApplicationUser>(manager)
        //    {
        //        AllowOnlyAlphanumericUserNames = true,
        //        RequireUniqueEmail = true
        //    };

        //    manager.PasswordValidator = new PasswordValidator
        //    {
        //        RequiredLength = 8,
        //        RequireNonLetterOrDigit = false,
        //        RequireDigit = false,
        //        RequireLowercase = true,
        //        RequireUppercase = false
        //    };

        //    return manager;
        //}

        //#endregion
    }
}
