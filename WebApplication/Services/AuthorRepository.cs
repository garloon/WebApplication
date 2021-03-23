using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class AuthorRepository : IRepository<Author>
    {
        private readonly ApplicationContext context;

        public AuthorRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Create(Author item)
        {
            context.Authors.Add(item);
            Save();
        }

        public void Delete(int id)
        {
            var item = context.Authors.Find(id);
            if (item != null)
                context.Authors.Remove(item);
            Save();
        }

        public List<Author> GetAll()
        {
            return context.Authors.ToList();
        }

        public Author GetSelected(int id)
        {
            return context.Authors.Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Author item)
        {
            context.Entry(item).State = EntityState.Modified;
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
