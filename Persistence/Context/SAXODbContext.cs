using Persistence.Entities;
using Persistence.Initializer;
using System.ComponentModel.DataAnnotations.Schema;
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
                        .HasKey(booklist => booklist.Id);

            modelBuilder.Entity<BookListEntity>()
                        .Property(booklist => booklist.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<BookListEntity>()
                        .HasMany<BookEntity>(booklist => booklist.Books)
                        .WithRequired()
                        .HasForeignKey(book => book.BookListId);
        }

        public DbSet<BookListEntity> BookListEntities { get; set; }

        public DbSet<BookEntity> BookEntities { get; set; }
    }
}
