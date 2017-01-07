using Library.Models;
using Library.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;

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

        private string GetEmailFromRequestHeader ( )
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var Name = ClaimsPrincipal.Current.Identity.Name;

            return Name;
        }

        [HttpGet]
        public IHttpActionResult GetAuthors()
        {
            var authors = _context.Authors.ToList();

            if (authors == null)
                return NotFound();

            var authorDtos = ConversionUtility.AuthorsToAuthorDtos(authors);

            return Ok(authorDtos);
        }
        
        [HttpGet]
        public IHttpActionResult GetAuthor(int id)
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == id);

            if (author == null)
                return NotFound();

            AuthorDto authorDto = ConversionUtility.AuthorToAuthorDto(author);

            return Ok(authorDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IHttpActionResult AddAuthor(AuthorDto authorDto)
        {
            var Name = GetEmailFromRequestHeader();

            if ( Name == "example-admin@test.com" )
                return BadRequest("This user cannot make changes to the database!");

            if (!ModelState.IsValid)
                return BadRequest();

            if (authorDto == null)
                return BadRequest();

            Author author = ConversionUtility.AuthorDtoToAuthor(authorDto);

            var authorInDb = _context.Authors.Add(author);

            _context.SaveChanges();

            var authorToReturn = ConversionUtility.AuthorToAuthorDto(authorInDb);

            return Ok(authorToReturn);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public IHttpActionResult UpdateAuthor(int id, AuthorDto authorDto)
        {
            var Name = GetEmailFromRequestHeader();

            if ( Name == "example-admin@test.com" )
                return BadRequest( "This user cannot make changes to the database!" );

            if (!ModelState.IsValid)
                return BadRequest();

            if (id != authorDto.Id)
                return BadRequest();

            Author author = ConversionUtility.AuthorDtoToAuthor(authorDto);

            _context.Entry(author).State = EntityState.Modified;

            _context.SaveChanges();

            var authorToReturn = ConversionUtility.AuthorToAuthorDto(author);

            return Ok(authorToReturn);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IHttpActionResult DeleteAuthor(int id)
        {
            var Name = GetEmailFromRequestHeader();

            if ( Name == "example-admin@test.com" )
                return BadRequest( "This user cannot make changes to the database!" );

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
