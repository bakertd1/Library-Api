using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.DTOs
{
    public static class ConversionUtility
    {
        #region Author
        public static  IEnumerable<AuthorDto> AuthorsToAuthorDtos(IEnumerable<Author> authors)
        {
            if (authors == null)
                return null;

            List<AuthorDto> dtos = new List<AuthorDto>();

            foreach (Author author in authors)
            {
                dtos.Add(new AuthorDto
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Birthdate = author.Birthdate,
                    Deathdate = author.Deathdate
                });
            }

            return dtos;
        }

        public static AuthorDto AuthorToAuthorDto(Author author)
        {
            if (author == null)
                return null;

            AuthorDto authorDto = new AuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Birthdate = author.Birthdate,
                Deathdate = author.Deathdate
            };

            return authorDto;
        }

        public static Author AuthorDtoToAuthor(AuthorDto authorDto)
        {
            if (authorDto == null)
                return null;

            Author author = new Author
            {
                Id = authorDto.Id,
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                Birthdate = authorDto.Birthdate,
                Deathdate = authorDto.Deathdate
            };

            return author;
        }

        #endregion

        #region Book

        public static IEnumerable<BookDto> BooksToBookDtos(IEnumerable<Book> books)
        {
            if (books == null)
                return null;

            List<BookDto> bookDtos = new List<BookDto>();

            foreach(Book book in books)
            {
                bookDtos.Add(new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    AuthorId = book.AuthorId,
                    Author = AuthorToAuthorDto(book.Author),
                    Publisher = book.Publisher,
                    PublicationDate = book.PublicationDate,
                    NumberOfPages = book.NumberOfPages
                });
            }

            return bookDtos;
        }

        public static Book BookDtoToBook(BookDto bookDto)
        {
            if (bookDto == null)
                return null;

            Book book = new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                AuthorId = bookDto.AuthorId,
                Publisher = bookDto.Publisher,
                PublicationDate = bookDto.PublicationDate,
                NumberOfPages = bookDto.NumberOfPages
            };

            return book;
        }

        public static BookDto BookToBookDto(Book book)
        {
            if (book == null)
                return null;

            BookDto bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId,
                Author = AuthorToAuthorDto(book.Author),
                Publisher = book.Publisher,
                PublicationDate = book.PublicationDate,
                NumberOfPages = book.NumberOfPages
            };

            return bookDto;
        }

        #endregion
    }
}