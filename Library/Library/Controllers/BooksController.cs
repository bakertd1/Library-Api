﻿using Library.Models;
using Library.DTOs;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;

namespace Library.Controllers
{
    [Authorize]
    public class BooksController : ApiController
    {
        private ApplicationDbContext _context;
        AccountController ac;

        public BooksController()
        {
            _context = new ApplicationDbContext();
            ac = new AccountController();
        }

        ~BooksController()
        {
            _context.Dispose();
        }

        private string GetEmailFromRequestHeader ( )
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var Name = ClaimsPrincipal.Current.Identity.Name;

            return Name;
        }

        [HttpGet]
        public IHttpActionResult GetBooks()
        {
            var books = _context.Books.Include(b => b.Author).ToList();

            if (books == null)
                return NotFound();

            var bookDtos = ConversionUtility.BooksToBookDtos(books);

            return Ok(bookDtos);
        }

        [HttpGet]
        public IHttpActionResult GetBook(int id)
        {
            var book = _context.Books.Include(b => b.Author).SingleOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound();

            var bookDto = ConversionUtility.BookToBookDto(book);

            return Ok(bookDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IHttpActionResult AddBook(BookDto bookDto)
        {
            var Name = GetEmailFromRequestHeader();

            if ( Name == "example-admin@test.com" )
                return BadRequest( "This user cannot make changes to the database!" );

            if (!ModelState.IsValid)
                return BadRequest();

            if (bookDto == null)
                return BadRequest();

            var book = ConversionUtility.BookDtoToBook(bookDto);

            var bookInDb = _context.Books.Add(book);

            _context.SaveChanges();

            //needed to return author data alongside book data
            var addedBook = _context.Books.Include(b => b.Author).Single(b => b.Id == bookInDb.Id);

            var bookToReturn = ConversionUtility.BookToBookDto(addedBook);

            return Ok(bookToReturn);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public IHttpActionResult UpdateBook(int id, BookDto bookDto)
        {
            var Name = GetEmailFromRequestHeader();

            if ( Name == "example-admin@test.com" )
                return BadRequest( "This user cannot make changes to the database!" );

            if (!ModelState.IsValid)
                return BadRequest();

            if (id != bookDto.Id)
                return BadRequest();

            var book = ConversionUtility.BookDtoToBook(bookDto);

            _context.Entry(book).State = EntityState.Modified;

            _context.SaveChanges();

            //needed to return author data alongside book data
            book = _context.Books.Include(b => b.Author).Single(b => b.Id == book.Id);

            var bookToReturn = ConversionUtility.BookToBookDto(book);

            return Ok(bookToReturn);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IHttpActionResult DeleteBook(int id)
        {
            var Name = GetEmailFromRequestHeader();

            if ( Name == "example-admin@test.com" )
                return BadRequest( "This user cannot make changes to the database!" );

            var book = _context.Books.SingleOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound();

            _context.Books.Remove(book);

            _context.SaveChanges();

            return Ok();
        }
    }
}
