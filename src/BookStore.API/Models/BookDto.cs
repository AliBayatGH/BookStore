namespace BookStore.API.Models;

public class BookDto
{
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public double Price { get; set; }
}
