namespace BookStore.Domain.AggregatesModel.Books;
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public decimal Price { get; set; }
}
