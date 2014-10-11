using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services.Models
{
    public class Book
    {
        public string ISBN { get; set; }

        public int SaxoId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string ImageURL { get; set; }
    }
}