using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new List<Book>
                {
                new Book { Id = 1, Name = "Отчаяние", PublicationDate = DateTime.Now, AuthorId = 1, GenreId = 1},
                new Book { Id = 2, Name = "Маг на побегушках", PublicationDate = DateTime.Now, AuthorId = 2, GenreId = 2},
                new Book { Id = 3, Name = "Пора туманов", PublicationDate = DateTime.Now, AuthorId = 3, GenreId = 3},
                new Book { Id = 4, Name = "Кукольный домик", PublicationDate = DateTime.Now, AuthorId = 3, GenreId = 3},
                new Book { Id = 5, Name = "Предложение", PublicationDate = DateTime.Now, AuthorId = 4, GenreId = 4},
                new Book { Id = 6, Name = "Иванов", PublicationDate = DateTime.Now, AuthorId = 4, GenreId = 4}
                });

            modelBuilder.Entity<Author>().HasData(
                new List<Author>
                {
                    new Author{ AuthorId = 1, Name = "Владимир" , Surname = "Набоков"},
                    new Author{ AuthorId = 2, Name = "Александр" , Surname = "Рудазов"},
                    new Author{ AuthorId = 3, Name = "Нил" , Surname = "Гейман"},
                    new Author{ AuthorId = 4, Name = "Антон" , Surname = "Чехов"}
                });

            modelBuilder.Entity<Genre>().HasData(
                new List<Genre>
                {
                    new Genre { GenreId = 1, Name = "Роман"},
                    new Genre { GenreId = 2, Name = "Фантастика"},
                    new Genre { GenreId = 3, Name = "Ужас"},
                    new Genre { GenreId = 4, Name = "Драмма"}
                });
        }
    }
}
