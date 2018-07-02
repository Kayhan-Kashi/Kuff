using Kuff.Common.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        void AddService(IService service);
        void Save();
    }
}
