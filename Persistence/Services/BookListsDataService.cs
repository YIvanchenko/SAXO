using Persistence.Services.Interface;
using Persistence.Context;
using Persistence.Entities;
using System.Collections.Generic;
using System.Linq;
using Persistence.Exceptions;
using System.Data.Entity;

namespace Persistence.Services
{
    public class BookListsDataService : IBookListsDataService
    {
        public IEnumerable<BookListEntity> GetAll()
        {
            using (var context = new SAXODbContext())
            {
                return (from list in context.BookListEntities
                        select list).ToList();
            }
        }

        public BookListEntity GetById(int id)
        {
            using (var context = new SAXODbContext())
            {
                return (from list in context.BookListEntities
                        where list.Id == id
                        select list).Single();
            }
        }

        public void Save(BookListEntity bookList)
        {
            using (var context = new SAXODbContext())
            {
                context.BookListEntities.Attach(bookList);
                context.Entry(bookList).State = EntityState.Modified;
                if (!(context.SaveChanges() > 0))
                {
                    throw new BookListException();
                }
            }
        }

        public BookListEntity Create()
        {
            using (var context = new SAXODbContext())
            {
                return context.BookListEntities.Create();
            }
        }
    }
}
