using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepository<Book> repository;

        public BooksController(IRepository<Book> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks()
        {
            return repository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            Book book = repository.GetSelected(id);
            if (book == null)
                return NotFound();
            return new ObjectResult(book);
        }

        [HttpPost]
        public ActionResult<Book> Post(Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            repository.Create(book);
            return book;
        }

        [HttpPut]
        public ActionResult<Book> Put(Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            repository.Update(book);
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public ActionResult<Book> Delete(int id)
        {
            Book book = repository.GetSelected(id);
            if (book == null)
            {
                return NotFound();
            }
            repository.Delete(id);
            return Ok(book);
        }
    }
}
