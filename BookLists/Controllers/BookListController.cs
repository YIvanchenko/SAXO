﻿using Services;
using Services.Interface;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookLists.Controllers
{
    public class BookListController : ApiController
    {
        private readonly IBookListsService _bookListsService;

        public BookListController(IBookListsService bookListsService)
        {
            _bookListsService = bookListsService;
        }

        public IEnumerable<BookList> Get()
        {
            return _bookListsService.GetAllBookLists();
        }

        [Route("api/booklist/GetListById/{id}")]
        [HttpGet]
        public BookList Get(int id)
        {
            return _bookListsService.GetBookListById(id);
        }

        [Route("api/booklist/GetListBySpecification/{bookTitle}")]
        [HttpGet]
        public IEnumerable<BookList> GetListBySpecification(string bookTitle)
        {            
            return _bookListsService.GetListBySpecification(bookTitle);
        }

        [Route("api/booklist/Create")]
        [HttpPost]
        public void Create(string isbn, int listId)
        {
            _bookListsService.ProcessBooks(new List<Tuple<string, int>>() 
            { 
                new Tuple<string, int>(isbn, listId) 
            });
        }

        public async Task<HttpResponseMessage> PostFormData()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var streamProvider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            foreach (var item in streamProvider.Contents)
            {
                await item.ReadAsStringAsync().ContinueWith(task =>
                {
                    var content = task.Result;
                    var lines = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    var separatedLines = from line in lines.Skip(1)
                                         where !string.IsNullOrEmpty(line)
                                         select line.Split(';');

                    return separatedLines;

                }).ContinueWith(task =>
                {
                    var separatedLines = task.Result;
                    var bookListsLines = from line in separatedLines
                                         group line by line[1] into g
                                         let bookListId = Convert.ToInt32(g.First()[1])
                                         let booklistTitle = g.First()[3]
                                         select new Tuple<int, string>(bookListId, booklistTitle);

                    _bookListsService.ProcessBookLists(bookListsLines);

                    return separatedLines;
                }).ContinueWith(task =>
                {
                    var separatedLines = task.Result;
                    var bookLines = from line in separatedLines
                                    let bookListId = Convert.ToInt32(line[1])
                                    let isbn = line[0]
                                    select new Tuple<string, int>(isbn, bookListId);

                    _bookListsService.ProcessBooks(bookLines);
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
