using System;
using Library.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int AuthorId { get; set; }

        public AuthorDto Author { get; set; }

        [Required]
        public string Publisher { get; set; }

        public DateTime PublicationDate { get; set; }

        public int NumberOfPages { get; set; }
    }
}