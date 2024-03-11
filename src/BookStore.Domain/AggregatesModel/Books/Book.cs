namespace BookStore.Domain.AggregatesModel.Books;
public class Book
{
    public int Id { get; set; }
    public string Ttitle { get; set; } = default!;
    public string Author { get; set; } = default!;
    public double Price { get; set; }
}
