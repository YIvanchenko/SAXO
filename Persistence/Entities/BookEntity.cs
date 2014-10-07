namespace Persistence.Entities
{
    public class BookEntity
    {
        public string ISBN { get; set; }
                
        public virtual int BookListId { get; set; }
       
        public int SaxoId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string ImageURL { get; set; }
    }
}
