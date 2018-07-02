using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Kuff.Service.Services.AccountRelated
{
    public class IdentityContext :IdentityDbContext
    {
        public IdentityContext() : base("kayhanka_KuffDbContext")
        {
            
        }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }

        public DbSet<Kuff.Common.DTOs.AccountRelated.ApplicationUser> AppUsers { get; set; }
    }
}
