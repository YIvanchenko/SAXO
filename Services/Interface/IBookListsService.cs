using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IBookListsService
    {
        IEnumerable<BookList> GetAllBookLists();

        BookList GetBookListById(int id);

        Book GetBookById(string isbn);

        void ProcessBookLists(IEnumerable<Tuple<int, string>> bookListsLines);

        void ProcessBooks(IEnumerable<Tuple<string, int>> bookLines);
    }
}
