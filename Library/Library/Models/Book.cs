using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicatonDate { get; set; }
        public int NumberOfPages { get; set; }
    }
}