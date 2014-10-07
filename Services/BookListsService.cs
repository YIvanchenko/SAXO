using AutoMapper;
using Persistence.Entities;
using Persistence.Services.Interface;
using Services.Interface;
using Services.Models;
using System;
using System.Collections.Generic;

namespace Services
{
    public class BookListsService : IBookListsService
    {
        private readonly IBookListsDataService _bookListsDataService;

        public BookListsService(IBookListsDataService bookListsDataService)
        {
            _bookListsDataService = bookListsDataService;
        }

        public IEnumerable<BookList> GetAllBookLists()
        {
            var bookListEntities = _bookListsDataService.GetAll();
            return Mapper.Map<IEnumerable<BookListEntity>, IEnumerable<BookList>>(bookListEntities);
        }

        public BookList GetBookListById(int id)
        {
            var bookListEntity = _bookListsDataService.GetById(id);
            return Mapper.Map<BookListEntity, BookList>(bookListEntity);
        }

        public Book GetBookById(string isbn)
        {
            var bookEntity = _bookListsDataService.GetById(isbn);
            return Mapper.Map<BookEntity, Book>(bookEntity);
        }

        public void ProcessBookLists(IEnumerable<Tuple<int, string>> bookListsLines)
        {
            foreach (var values in bookListsLines)
            {
                var bookList = _bookListsDataService.GetById(values.Item1);
                if (bookList == null)
                {
                    bookList = new BookListEntity()
                    {
                        Id = values.Item1,
                        Title = values.Item2
                    };

                    _bookListsDataService.Add(bookList);
                }
                else
                {
                    bookList.Title = values.Item2;
                    _bookListsDataService.Save(bookList);
                }
            }
        }

        public void ProcessBooks(IEnumerable<Tuple<string, int>> bookLines)
        {
            foreach (var values in bookLines)
            {
                var book = _bookListsDataService.GetById(values.Item1);
                if (book == null)
                {
                    book = new BookEntity()
                    {
                        ISBN = values.Item1,
                        BookListId = values.Item2
                    };

                    _bookListsDataService.AddBook(book);
                }
            }
        }

        //public void ProcessFileData(IEnumerable<string[]> lines)
        //{
        //    foreach (var values in lines)
        //    {
        //        var bookListId = Convert.ToInt32(values[1]);

        //        var bookList = _bookListsDataService.GetById(bookListId);
        //        if (bookList == null)
        //        {
        //            bookList = new BookListEntity()
        //            {
        //                Id = bookListId,
        //                Title = values[3]
        //            };

        //            _bookListsDataService.Add(bookList);
        //        }
        //        else
        //        {
        //            bookList.Title = values[3];
        //            _bookListsDataService.Save(bookList);
        //        }

        //        var isbn = values[0];
        //        var book = _bookListsDataService.GetById(isbn);
        //        if (book == null)
        //        {
        //            book = new BookEntity()
        //            {
        //                ISBN = isbn,
        //                BookListId = bookListId
        //            };

        //            _bookListsDataService.AddBook(book);
        //        }
        //    }
        //}
    }
}
