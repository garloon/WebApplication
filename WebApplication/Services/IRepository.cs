using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        List<T> GetAll();
        T GetSelected(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();

    }
}
