﻿using System.Collections.Generic;

namespace Persistence.Entities
{
    public class BookListEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }
                
        public virtual ICollection<BookEntity> Books { get; set; }
    }
}
