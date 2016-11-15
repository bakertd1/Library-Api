using Library.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.Controllers
{
    [Authorize]
    public class AuthorsController : ApiController
    {
        private ApplicationDbContext _context;

        public AuthorsController()
        {
            _context = new ApplicationDbContext();
        }

        ~AuthorsController()
        {
            _context.Dispose();
        }
        
        [HttpGet]
        public IHttpActionResult GetAuthors()
        {
            var authors = _context.Authors.ToList();

            if (authors == null)
                return NotFound();

            return Ok(authors);
        }
        
        [HttpGet]
        public IHttpActionResult GetAuthor(int id)
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == id);

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IHttpActionResult AddAuthor(Author author)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (author == null)
                return BadRequest();

            var authorInDb = _context.Authors.Add(author);

            _context.SaveChanges();

            return Ok(authorInDb);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public IHttpActionResult UpdateAuthor(int id, Author author)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != author.Id)
                return BadRequest();

            _context.Entry(author).State = EntityState.Modified;

            _context.SaveChanges();

            return Ok(author);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IHttpActionResult DeleteAuthor(int id)
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == id);

            if (author == null)
                return NotFound();

            //get all the books associated with this author and delete them as well
            var books = _context.Books.Where(b => b.AuthorId == author.Id);
            _context.Books.RemoveRange(books);

            _context.Authors.Remove(author);

            _context.SaveChanges();

            return Ok();
        }
    }
}
