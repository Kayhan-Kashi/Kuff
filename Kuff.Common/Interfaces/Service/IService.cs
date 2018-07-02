using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Common.Interfaces.UnitOfWork;

namespace Kuff.Common.Interfaces.Service
{
    public interface IService
    {
        IRepository Repository { get; set; }
        IUnitOfWork Unit { get; set; }
    }
}
