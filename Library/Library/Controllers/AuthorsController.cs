using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Authorization;

namespace Library.Controllers
{
    [Produces("application/json")]
    [Route("api/Authors")]
    public class AuthorsController : Controller
    {
        ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAuthors()
        {
            var authors = _context.Authors.ToList();

            if (authors == null)
                return NotFound();

            return Ok(authors);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetAuthor(int id)
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == id);

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid || author == null)
                return BadRequest();

            var authorInDb = _context.Authors.Add(author).Entity;
            _context.SaveChanges();

            return Ok(authorInDb);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] Author author)
        {
            if (!ModelState.IsValid || author == null || author.Id != id)
                return BadRequest();

            var updatedAuthor = _context.Authors.Update(author).Entity;
            _context.SaveChanges();

            return Ok(updatedAuthor);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == id);

            if (author == null)
                return NotFound();

            _context.Remove(author);
            _context.SaveChanges();

            return Ok();
        }
    }
}