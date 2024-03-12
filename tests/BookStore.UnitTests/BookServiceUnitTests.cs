using BookStore.API.Application.Services;
using BookStore.API.Models;
using BookStore.Domain.AggregatesModel.Books;
using BookStore.Domain.SeedWork;
using Moq;

namespace BookStore.Tests;

public class BookServiceUnitTests
{
    [Fact]
    public async Task AddBookAsync_ValidBook_ReturnsBookId()
    {
        // Arrange
        var bookRepositoryMock = new Mock<IBookRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var service = new BookService(bookRepositoryMock.Object, unitOfWorkMock.Object);
        var bookDto = new BookDto { Title = "Test Book", Author = "Test Author", Price = 10.99m };

        var bookId = 1;

        bookRepositoryMock.Setup(repo => repo.Add(It.IsAny<Book>())).Callback<Book>(b =>
        {
            // Simulate adding the book to the database by assigning an ID
            b.Id = bookId;
        });

        unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await service.AddBookAsync(bookDto);

        // Assert
        Assert.Equal(bookId, result);
    }


    [Fact]
    public async Task DeleteBookAsync_ExistingBook_DeletesBook()
    {
        // Arrange
        var bookRepositoryMock = new Mock<IBookRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var service = new BookService(bookRepositoryMock.Object, unitOfWorkMock.Object);
        var bookId = 1;

        bookRepositoryMock.Setup(repo => repo.GetBookAsync(bookId)).ReturnsAsync(new Book());
        unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        await service.DeleteBookAsync(bookId);

        // Assert
        bookRepositoryMock.Verify(repo => repo.Remove(It.IsAny<Book>()), Times.Once);
        unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllBooksAsync_ReturnsAllBooks()
    {
        // Arrange
        var bookRepositoryMock = new Mock<IBookRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var service = new BookService(bookRepositoryMock.Object, unitOfWorkMock.Object);

        var books = new List<Book>
            {
                new Book { Title = "Book 1", Author = "Author 1", Price = 10.99m },
                new Book { Title = "Book 2", Author = "Author 2", Price = 12.99m }
            };
        var expectedBookDtos = books.Select(b => new BookDto { Title = b.Title, Author = b.Author, Price = b.Price });

        bookRepositoryMock.Setup(repo => repo.GetAllBooksAsync()).ReturnsAsync(books);

        // Act
        var result = await service.GetAllBooksAsync();

        // Assert
        Assert.NotNull(result);
        //Assert.Equal(expectedBookDtos, result);
    }

    [Fact]
    public async Task GetBookAsync_ExistingBook_ReturnsBookDto()
    {
        // Arrange
        var bookRepositoryMock = new Mock<IBookRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var service = new BookService(bookRepositoryMock.Object, unitOfWorkMock.Object);

        var bookId = 1;
        var book = new Book { Id = bookId, Title = "Test Book", Author = "Test Author", Price = 10.99m };

        bookRepositoryMock.Setup(repo => repo.GetBookAsync(bookId)).ReturnsAsync(book);

        // Act
        var result = await service.GetBookAsync(bookId);

        // Assert
        Assert.NotNull(result);
        //Assert.Equal(bookId, result.Id);
        Assert.Equal(book.Title, result.Title);
        Assert.Equal(book.Author, result.Author);
        Assert.Equal(book.Price, result.Price);
    }

    [Fact]
    public async Task UpdateBookAsync_ExistingBook_UpdatesBook()
    {
        // Arrange
        var bookRepositoryMock = new Mock<IBookRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var service = new BookService(bookRepositoryMock.Object, unitOfWorkMock.Object);

        var bookId = 1;
        var bookDto = new BookDto { Title = "Updated Title", Author = "Updated Author", Price = 15.99m };
        var book = new Book { Id = bookId, Title = "Original Title", Author = "Original Author", Price = 10.99m };

        bookRepositoryMock.Setup(repo => repo.GetBookAsync(bookId)).ReturnsAsync(book);
        unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        await service.UpdateBookAsync(bookId, bookDto);

        // Assert
        Assert.Equal(bookDto.Title, book.Title);
        Assert.Equal(bookDto.Author, book.Author);
        Assert.Equal(bookDto.Price, book.Price);
    }

}