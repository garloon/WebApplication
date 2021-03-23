using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using WebApplication.Controllers;
using WebApplication.Models;
using WebApplication.Services;
using Xunit;

namespace WebApplication.Tests
{
    public class BooksControllerTests
    {
        private readonly Mock<IRepository<Book>> mockBookRepository;
        private readonly BooksController booksController;

        public BooksControllerTests()
        {
            mockBookRepository = new Mock<IRepository<Book>>();
            booksController = new BooksController(mockBookRepository.Object);
        }

        [Fact]
        public void GetAllBooks_ActionExecutes_ReturnsBooksList()
        {
            var result = booksController.GetAllBooks();
            Assert.IsType<ActionResult<List<Book>>>(result);
        }

        [Fact]
        public void GetAllBooks_ActionExecutes_ReturnsExactNumberOfBooks()
        {
            mockBookRepository.Setup(repo => repo.GetAll())
                .Returns(new List<Book>() { new Book(), new Book() });
            
            var result = booksController.GetAllBooks();
            var actionResult = Assert.IsType<ActionResult<List<Book>>>(result);
            var books = Assert.IsType<List<Book>>(actionResult.Value);
            
            Assert.Equal(2, books.Count);
        }

        [Fact]
        public void Create_InvalidModelState_ReturnsModel()
        {
            booksController.ModelState.AddModelError("Name", "Name is required");
            var book = new Book { Id = 1, Name = "Отчаяние", PublicationDate = DateTime.Now, AuthorId = 1, GenreId = 1 };
            var result = booksController.Post(book);
            var actionResult = Assert.IsType<ActionResult<Book>>(result);
            var testBook = Assert.IsType<Book>(actionResult.Value);
            Assert.Equal(book.Id, testBook.Id);
            Assert.Equal(book.Name, testBook.Name);
        }

        [Fact]
        public void Create_InvalidModelState_ReturnsError()
        {
            booksController.ModelState.AddModelError("Id", "Id is required");

            var book = new Book { Id = 1, PublicationDate = DateTime.Now, AuthorId = 1, GenreId = 1 };
            var result = booksController.Post(book);
            
            mockBookRepository.Verify(x => x.Create(It.IsAny<Book>()), Times.Once);
        }
    }
}
