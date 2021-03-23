using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class BookRepository : IRepository<Book>
    {
        private readonly ApplicationContext context;


        public BookRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Create(Book item)
        {
            context.Books.Add(item);
            Save();
        }

        public void Delete(int id)
        {
            var book = context.Books.Find(id);
            if (book != null)
                context.Books.Remove(book);
            Save();
        }

        public List<Book> GetAll()
        {
            List<Book> books = context.Books.ToList();

            foreach (var book in books)
            {
                var author = context.Authors.Find(book.AuthorId);
                var genre = context.Genres.Find(book.GenreId);
                book.Author = $"{author.Name} {author.Surname}";
                book.Genre = $"{genre.Name}";
            }

            return books;
        }

        public Book GetSelected(int id)
        {
            return context.Books.Find(id);
        }

        public void Update(Book item)
        {
            context.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
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
