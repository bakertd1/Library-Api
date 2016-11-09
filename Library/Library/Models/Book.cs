using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [Required]
        public string Publisher { get; set; }

        public DateTime PublicationDate { get; set; }

        public int NumberOfPages { get; set; }
    }
}