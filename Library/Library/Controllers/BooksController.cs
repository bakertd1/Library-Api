using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _context.Books.Include(b => b.Author).ToList();

            if (books == null)
                return NotFound();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _context.Books.Include(b => b.Author).SingleOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

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

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid || book == null || book.Id != id)
                return BadRequest();

            var updatedBook = _context.Books.Update(book).Entity;

            var bookWithAuthor = _context.Books.Include(b => b.Author).SingleOrDefault(b => b.Id == updatedBook.Id);

            return Ok(bookWithAuthor);
        }

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