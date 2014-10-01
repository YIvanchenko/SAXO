using Persistence.Services.Interface;
using Persistence.Context;
using Persistence.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Services
{
    public class BookListsDataService : IBookListsDataService
    {
        public IEnumerable<BookListEntity> GetAll()
        {
            using (var context = new SAXODbContext())
            {
                return from account in context.BookListEntities select account;
            }
        }
    }
}
