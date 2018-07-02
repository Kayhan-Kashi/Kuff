using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.AccountRelated;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Kuff.Service.Services.AccountRelated
{
    public class SignInManager : SignInManager<ApplicationUser, string>
    {
        public SignInManager(UserManager userManager, IAuthenticationManager authManager) : base(userManager, authManager)
        {
            
        }

        public static SignInManager Create(IdentityFactoryOptions<SignInManager> options, IOwinContext context)
        {
            UserManager userMgr =  context.Get<UserManager>();
            IAuthenticationManager auth = context.Authentication;
            return new SignInManager(userMgr, auth);
        }
    }
}
