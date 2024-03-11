using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoe.Application.Services;
public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task<BookDto> GetBookAsync(int id);
    Task<int> AddBookAsync(BookDto bookDto);
    Task UpdateBookAsync(int id, BookDto bookDto);
    Task DeleteBookAsync(int id);
}
public class BookService:IBookService
{
}
