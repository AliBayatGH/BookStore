namespace BookStore.API.Models;

public class BookDto
{
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public decimal Price { get; set; }
}
