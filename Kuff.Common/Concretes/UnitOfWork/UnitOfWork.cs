using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.Interfaces.Service;
using Kuff.Common.Interfaces.UnitOfWork;

namespace Kuff.Common.Concretes.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private List<IService> _services;
        public UnitOfWork(IService service)
        {
            _services = new List<IService>();
            if (service.Repository.Context != null)
            {
                Context = service.Repository.Context;
            }
            AddService(service);
        }
        public void AddService(IService service)
        {
            _services.Add(service);
            if (Context != null)
            {
                service.Repository.Context = Context;
            }
            service.Unit = this;

        }

        public DbContext Context { get; set; }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
