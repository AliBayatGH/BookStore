using BookStore.API.Models;

namespace BookStore.API.Application.Services;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync(CancellationToken cancellationToken = default);
    Task<BookDto?> GetBookAsync(int id, CancellationToken cancellationToken = default);
    Task<int> AddBookAsync(BookDto bookDto, CancellationToken cancellationToken = default);
    Task UpdateBookAsync(int id, BookDto bookDto, CancellationToken cancellationToken = default);
    Task DeleteBookAsync(int id, CancellationToken cancellationToken = default);
}

