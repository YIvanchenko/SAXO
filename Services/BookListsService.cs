using AutoMapper;
using Persistence.Entities;
using Persistence.Services.Interface;
using Services.Interface;
using Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Services
{
    public class BookListsService : IBookListsService
    {
        private const string BOOK_DETAILS_API_URL_FORMAT = @"http://api.saxo.com/v1/Products/Products.json?key=563c52b3177047978c0bdfbfd776ebd3&ISBN={0}";
        private const string BOOK_PRICE_API_URL_FORMAT = @"http://api.saxo.com/v1/Prices/Prices.json?key=563c52b3177047978c0bdfbfd776ebd3&ProductID={0}";

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

        public IEnumerable<BookList> GetListBySpecification(string bookTile)
        {
            var bookListEntities = _bookListsDataService.GetSpecific(
                bookList => bookList.Books.Any(book => book.Title.ToLower().Contains(bookTile.ToLower())));
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

                    FillBookData(book);
                    _bookListsDataService.AddBook(book);
                }
                else
                {
                    FillBookData(book);
                    _bookListsDataService.SaveBook(book);
                }
            }
        }

        private void FillBookData(BookEntity book)
        {
            var bookJsonData = JsonRequest(string.Format(BOOK_DETAILS_API_URL_FORMAT, book.ISBN));
            if (bookJsonData.Any(p => p.Key == "id"))
            {
                book.SaxoId = Convert.ToInt32(bookJsonData["id"]);
            }

            if (bookJsonData.Any(p => p.Key == "title"))
            {
                book.Title = Convert.ToString(bookJsonData["title"]);
            }

            if (bookJsonData.Any(p => p.Key == "imageurl"))
            {
                book.ImageURL = Convert.ToString(bookJsonData["imageurl"]);
            }

            var bookpriceJsonData = JsonRequest(string.Format(BOOK_PRICE_API_URL_FORMAT, book.SaxoId));
            if (bookJsonData.Any(p => p.Key == "price"))
            {
                book.ImageURL = Convert.ToString(bookJsonData["price"]);
            }
        }

        private Dictionary<string, object> JsonRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    var js = new JavaScriptSerializer();
                    var jsonDictionary = js.DeserializeObject(reader.ReadToEnd()) as Dictionary<string, object>;
                    return (jsonDictionary.Values.First() as object[]).First() as Dictionary<string, object>;
                }
            }
            catch (WebException)
            {
                // Log error here in case you found that reasonable.
                return new Dictionary<string, object>();
            }
        }
    }
}
