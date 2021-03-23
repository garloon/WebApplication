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
    public class GenresController : ControllerBase
    {
        private readonly IRepository<Genre> repository;

        public GenresController(IRepository<Genre> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<List<Genre>> GetAllAuthors()
        {
            return repository.GetAll();
        }

        [HttpPost]
        public ActionResult<Genre> Post(Genre genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }

            repository.Create(genre);
            return Ok(genre);
        }

        [HttpPut]
        public ActionResult<Genre> Put(Genre genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }

            repository.Update(genre);

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public ActionResult<Genre> Delete(int id)
        {
            Genre genre = repository.GetSelected(id);
            if (genre == null)
            {
                return NotFound();
            }
            repository.Delete(id);
            return Ok(genre);
        }
    }
}
