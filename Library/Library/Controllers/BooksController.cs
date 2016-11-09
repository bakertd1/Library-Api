using Library.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.Controllers
{
    public class BooksController : ApiController
    {
        private ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        ~BooksController()
        {
            _context.Dispose();
        }

        [HttpGet]
        public IHttpActionResult GetBooks()
        {
            var books = _context.Books.Include(b => b.Author).ToList();

            if (books == null)
                return NotFound();

            return Ok(books);
        }

        [HttpGet]
        public IHttpActionResult GetBook(int id)
        {
            var book = _context.Books.Include(b => b.Author).SingleOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public IHttpActionResult AddBook(Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (book == null)
                return BadRequest();

            var bookInDb = _context.Books.Add(book);

            _context.SaveChanges();

            //needed to return author data alongside book data
            var addedBook = _context.Books.Include(b => b.Author).Single(b => b.Id == bookInDb.Id);

            return Ok(addedBook);
        }

        [HttpPut]
        public IHttpActionResult UpdateBook(int id, Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != book.Id)
                return BadRequest();

            _context.Entry(book).State = EntityState.Modified;

            _context.SaveChanges();

            return Ok(book);
        }

        [HttpDelete]
        public IHttpActionResult DeleteBook(int id)
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
