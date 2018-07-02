using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs;
using System.Linq.Expressions;

namespace Kuff.Common.Interfaces.Repositories
{
    public interface IRepository<T> :IRepository
    {       
        void Insert(T item, bool save = true);
        System.Linq.IQueryable<T> Get();
        void Update(T item, bool save = true);
        void Delete(T item, bool save = true);
    }

    public interface IRepository : IDisposable
    {
        DbContext Context { get; set; }
    }

}
