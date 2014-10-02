using Persistence.Entities;
using Persistence.Services.Interface;
using System.Collections.Generic;
using System.Web.Http;

namespace BookLists.Controllers
{
    public class BookListController : ApiController
    {
        private readonly IBookListsDataService _bookListsDataService;

        public BookListController(IBookListsDataService bookListsDataService)
        {
            _bookListsDataService = bookListsDataService;
        }

        // GET: api/BookList
        public IEnumerable<BookListEntity> Get()
        {
            return _bookListsDataService.GetAll();
        }

        // GET: api/BookList/5
        public BookListEntity Get(int id)
        {
            return _bookListsDataService.GetById(id);
        }

        // POST: api/BookList
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/BookList/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
