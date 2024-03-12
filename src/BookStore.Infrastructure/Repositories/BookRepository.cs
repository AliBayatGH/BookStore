using BookStore.Domain.AggregatesModel.Books;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories;
public class BookRepository(ApplicationDbContext context) : IBookRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Book?> GetBookAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public void Add(Book book)
    {
        _context.Books.Add(book);
    }

    public void Remove(Book book)
    {
        _context.Books.Remove(book);
    }
}
