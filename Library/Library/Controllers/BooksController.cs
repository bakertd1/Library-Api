using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Library.Controllers
{
    [Produces("application/json")]
    [Route("api/Books")]
    public class BooksController : Controller
    {
        private ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _context.Books.Include(b => b.Author).ToList();

            if (books == null)
                return NotFound();

            return Ok(books);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _context.Books.Include(b => b.Author).SingleOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (!ModelState.IsValid || book == null)
                return BadRequest();

            var bookInDb = _context.Books.Add(book).Entity;
            _context.SaveChanges();

            var bookWithAuthor = _context.Books.Include(b => b.Author).Single(b => b.Id == bookInDb.Id);

            return Ok(bookWithAuthor);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid || book == null || book.Id != id)
                return BadRequest();

            var updatedBook = _context.Books.Update(book).Entity;

            var bookWithAuthor = _context.Books.Include(b => b.Author).SingleOrDefault(b => b.Id == updatedBook.Id);

            return Ok(bookWithAuthor);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();

            return Ok();
        }
    }
}