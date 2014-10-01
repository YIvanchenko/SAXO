using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLists.Models
{
    public class Book
    {
        public string ISBN { get; set; }

        public int SAXOId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string ImageURL { get; set; }
    }
}