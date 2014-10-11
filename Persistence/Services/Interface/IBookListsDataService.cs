using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Persistence.Services.Interface
{
    public interface IBookListsDataService
    {
        IEnumerable<BookListEntity> GetAll();

        BookListEntity GetById(int id);

        IEnumerable<BookListEntity> GetSpecific(Expression<Func<BookListEntity, bool>> condition);

        void Save(BookListEntity bookList);

        void Add(BookListEntity bookList);

        BookEntity GetById(string isbn);

        void SaveBook(BookEntity book);

        void AddBook(BookEntity book);
    }
}
