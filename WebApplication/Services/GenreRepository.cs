using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class GenreRepository : IRepository<Genre>
    {
        private readonly ApplicationContext context;

        public GenreRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Create(Genre item)
        {
            context.Genres.Add(item);
            Save();
        }

        public void Delete(int id)
        {
            var item = context.Genres.Find(id);
            if (item != null)
                context.Genres.Remove(item);
            Save();
        }

        public List<Genre> GetAll()
        {
            return context.Genres.ToList();
        }

        public Genre GetSelected(int id)
        {
            return context.Genres.Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Genre item)
        {
            context.Entry(item).State = EntityState.Modified;
            Save();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
