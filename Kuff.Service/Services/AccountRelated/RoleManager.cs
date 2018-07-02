using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Dal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Kuff.Service.Services.AccountRelated
{
    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager(RoleStore<IdentityRole> store) : base(store)
        {
            
        }

        public static RoleManager Create(IdentityFactoryOptions<RoleManager> options, IOwinContext context)
        {
            IdentityContext db = context.Get<IdentityContext>();
            RoleManager manager = new RoleManager(new RoleStore<IdentityRole>(db));  
            return manager;
        }
    }
}
