using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IRepository<Author> repository;

        public AuthorsController(IRepository<Author> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<List<Author>> GetAllAuthors()
        {
            return repository.GetAll();
        }

        [HttpPost]
        public ActionResult<Author> AddAuthor(Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }

            repository.Create(author);
            return Ok(author);
        }

        [HttpPut]
        public ActionResult<Author> Put(Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }

            repository.Update(author);
            return Ok(author);
        }

        [HttpDelete("{id}")]
        public ActionResult<Author> Delete(int id)
        {
            Author author = repository.GetSelected(id);
            if (author == null)
            {
                return NotFound();
            }
            repository.Delete(id);
            return Ok(author);
        }
    }
}
