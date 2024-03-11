using BookStore.API.Models;
using BookStore.Domain.AggregatesModel.Books;
using BookStore.Domain.SeedWork;

namespace BookStore.API.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> AddBookAsync(BookDto bookDto, CancellationToken cancellationToken = default)
    {
        var book = new Book()
        {
            Ttitle = bookDto.Title,
            Author = bookDto.Author,
            Price = bookDto.Price,
        };

        _bookRepository.Add(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book.Id;
    }

    public async Task DeleteBookAsync(int id, CancellationToken cancellationToken = default)
    {
        var book = await _bookRepository.GetBookAsync(id);

        if (book == null)
            throw new Exception($"Book with id {id} not found!");

        _bookRepository.Remove(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync(CancellationToken cancellationToken = default)
    {
        var books = await _bookRepository.GetAllBooksAsync();
        var bookDtos = books.Select(b => new BookDto
        {
            Title = b.Ttitle,
            Author = b.Author,
            Price = b.Price,
        });

        return bookDtos;
    }

    public async Task<BookDto?> GetBookAsync(int id, CancellationToken cancellationToken = default)
    {
        var book = await _bookRepository.GetBookAsync(id);

        if (book == null)
        {
            return null;
        }

        return new BookDto
        {
            Title = book.Ttitle,
            Author = book.Author,
            Price = book.Price
        };
    }

    public async Task UpdateBookAsync(int id, BookDto bookDto, CancellationToken cancellationToken = default)
    {
        var book = await _bookRepository.GetBookAsync(id);

        if (book == null)
            throw new Exception($"Book with id {id} is not found!");

        book.Ttitle = bookDto.Title;
        book.Author = bookDto.Author;
        book.Price = bookDto.Price;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

