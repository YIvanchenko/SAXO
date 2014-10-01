using Persistence.Entities;
using System.Collections.Generic;

namespace Persistence.Services.Interface
{
    public interface IBookListsDataService
    {
        IEnumerable<BookListEntity> GetAll();
    }
}
