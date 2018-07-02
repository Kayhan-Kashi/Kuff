using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.Interfaces.Service;
using Kuff.Common.DTOs.AccountRelated;
using Microsoft.AspNet.Identity;

namespace Kuff.Service.Interfaces.AccountRelated
{
    public interface IUserManager
    {
        UserManager<ApplicationUser> Manager { get; set; }
    }
}
