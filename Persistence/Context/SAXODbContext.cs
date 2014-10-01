using Persistence.Entities;
using Persistence.Initializer;
using System.Data.Entity;

namespace Persistence.Context
{
    public class SAXODbContext : DbContext
    {
        public SAXODbContext()
            : base("SAXO.DbConnection")
        {
            Database.SetInitializer<SAXODbContext>(new SAXODbInitializer());
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntity>()
                        .HasKey(book => book.ISBN);

            modelBuilder.Entity<BookListEntity>()
                        .HasMany<BookEntity>(booklist => booklist.Books)
                        .WithRequired(book => book.BookList)
                        .HasForeignKey(book => book.BookListId);
        }

        public DbSet<BookListEntity> BookListEntities { get; set; }
    }
}
