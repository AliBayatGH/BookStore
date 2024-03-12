using BookStore.API;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;

namespace BookStore.IntegrationTests;

public class BooksControllerIntegrationTests(WebApplicationFactory<IAssemblyMarker> factory) : IClassFixture<WebApplicationFactory<IAssemblyMarker>>
{
    private readonly WebApplicationFactory<IAssemblyMarker> _factory = factory;

    [Fact]
    public async Task GetBooks_ReturnsSuccessStatusCode()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/books");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetBookById_ExistingId_ReturnsSuccessStatusCode()
    {
        // Arrange
        var client = _factory.CreateClient();
        var existingId = 1;

        // Act
        var response = await client.GetAsync($"/api/books/{existingId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task AddBook_ValidBook_ReturnsCreatedStatusCode()
    {
        // Arrange
        var client = _factory.CreateClient();
        var bookDto = new
        {
            Title = "Test Book",
            Author = "Test Author",
            Price = 10.99m
        };
        var content = new StringContent(JsonSerializer.Serialize(bookDto), System.Text.Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/api/books", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}