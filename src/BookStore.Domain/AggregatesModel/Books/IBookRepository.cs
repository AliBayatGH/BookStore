namespace BookStore.Domain.AggregatesModel.Books;
public interface IBookRepository
{
    Task<Book?> GetBookAsync(int id);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    void Add(Book book);
    void Remove(Book book);
}
