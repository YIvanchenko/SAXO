using Persistence.Context;
using Persistence.Entities;
using Persistence.Exceptions;
using Persistence.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Persistence.Services
{
    public class BookListsDataService : IBookListsDataService
    {
        public IEnumerable<BookListEntity> GetAll()
        {
            using (var context = new SAXODbContext())
            {
                return (from list in context.BookListEntities.Include(bookList => bookList.Books)
                        select list).ToList();
            }
        }

        public IEnumerable<BookListEntity> GetSpecific(Expression<Func<BookListEntity, bool>> condition)
        {
            using (var context = new SAXODbContext())
            {
                return context.BookListEntities.Include(bookList => bookList.Books).Where(condition).ToList();
            }
        }

        public BookListEntity GetById(int id)
        {
            using (var context = new SAXODbContext())
            {
                return (from list in context.BookListEntities.Include(bookList => bookList.Books)
                        where list.Id == id
                        select list).SingleOrDefault();
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

        public void Add(BookListEntity bookList)
        {
            using (var context = new SAXODbContext())
            {
                context.BookListEntities.Add(bookList);
                if (!(context.SaveChanges() > 0))
                {
                    throw new BookListException();
                }
            }
        }

        public BookEntity GetById(string isbn)
        {
            using (var context = new SAXODbContext())
            {
                return (from book in context.BookEntities
                        where book.ISBN == isbn
                        select book).SingleOrDefault();
            }
        }

        public void SaveBook(BookEntity book)
        {
            using (var context = new SAXODbContext())
            {
                context.BookEntities.Attach(book);
                context.Entry(book).State = EntityState.Modified;
                if (!(context.SaveChanges() > 0))
                {
                    throw new BookListException();
                }
            }
        }

        public void AddBook(BookEntity book)
        {
            using (var context = new SAXODbContext())
            {
                context.BookEntities.Add(book);
                if (!(context.SaveChanges() > 0))
                {
                    throw new BookListException();
                }
            }
        }
    }
}
