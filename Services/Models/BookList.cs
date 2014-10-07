using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services.Models
{
    public class BookList
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<Book> Books { get; set; }
    }
}