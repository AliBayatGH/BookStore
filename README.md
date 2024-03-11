BookStore/
│
├── src/
│   ├── BookStore.API/                   (Presentation Layer)
│   │   ├── Application/
│   │   │   └── Services/
│   │   │       └── BookService.cs
│   │   ├── Controllers/
│   │   │   └── BooksController.cs
│   │   ├── Models/
│   │   │   └── BookDto.cs
│   │   ├── Validators/
│   │   │   └── BookDTOValidator.cs
│   │   └── Program.cs                   (Entry point)
│   │
│   ├── BookStore.Domain/                (Domain Layer)
│   │   ├── AggregatesModel/
│   │   │   └── Books/
│   │   │       ├── Book.cs
│   │   │       ├── IBookRepository.cs
│   │   │       └── IBookService.cs
│   │   └── Extensions/                  (Extension methods)
│   │       └── ServiceCollectionExtensions.cs
│   │
│   ├── BookStore.Infrastructure/        (Infrastructure Layer)
│   │   ├── ApplicationDbContext.cs
│   │   ├── Repositories/
│   │   │   └── BookRepository.cs
│   │   └── Extensions/                  (Extension methods)
│   │       └── ServiceCollectionExtensions.cs
│   │
└── tests/
    └── BookStore.Tests/
        ├── Unit/
        │   ├── Domain/                  (Tests for Domain Layer)
        │   └── Infrastructure/         (Tests for Infrastructure Layer)
        │
        └── Integration/                (Integration Tests)


# Create the solution
dotnet new sln -n BookStore

# Create projects within the solution
dotnet new webapi -n BookStore.API -o src/BookStore.API
dotnet new classlib -n BookStore.Domain -o src/BookStore.Domain
dotnet new classlib -n BookStore.Infrastructure -o src/BookStore.Infrastructure
dotnet new xunit -n BookStore.Tests -o tests/BookStore.Tests

# Add projects to the solution 
# dotnet sln add (ls -r **/*.csproj) # in powershell
dotnet sln add src/BookStore.API/BookStore.API.csproj
dotnet sln add src/BookStore.Domain/BookStore.Domain.csproj
dotnet sln add src/BookStore.Infrastructure/BookStore.Infrastructure.csproj
dotnet sln add tests/BookStore.Tests/BookStore.Tests.csproj


# Steps:

1. Domain Layer (BookStore.Domain):
Define your domain entities and business logic. Start by implementing the Book class, representing the core entity in your application.
# Create Book.cs in the Domain project
touch src/BookStore.Domain/Book.cs


2. Infrastructure Layer (BookStore.Infrastructure):
Implement the data access layer and database-related functionalities.
Create the DbContext class, which represents your database context and defines the database schema.
Implement repositories to interact with the database. Start by defining the interfaces (IBookRepository) and their implementations (BookRepository).

# Add Entity Framework Core package to Infrastructure project
dotnet add src/BookStore.Infrastructure/BookStore.Infrastructure.csproj package Microsoft.EntityFrameworkCore.SqlServer
# Create ApplicationDbContext.cs in the Infrastructure project
touch src/BookStore.Infrastructure/ApplicationDbContext.cs
# Create IBookRepository.cs and BookRepository.cs in the Infrastructure project
mkdir src/BookStore.Infrastructure/Repositories
touch src/BookStore.Infrastructure/Repositories/IBookRepository.cs
touch src/BookStore.Infrastructure/Repositories/BookRepository.cs


3. Application Layer (BookStore.Application):
Implement the application services that orchestrate business logic and interact with the infrastructure layer.
Create the IBookService interface, which defines the contract for operations related to books.
Implement the BookService class, which provides the actual implementation of book-related operations using the repositories from the infrastructure layer.

# Create IBookService.cs and BookService.cs in the Application project
mkdir src/BookStore.Application/Books
touch src/BookStore.Application/Books/IBookService.cs
touch src/BookStore.Application/Books/BookService.cs

4. Presentation Layer (BookStore.API):
Implement the API controllers and models.
Create the BooksController class, which defines the API endpoints for managing books (e.g., CRUD operations).
Define the BookDTO class, which represents the data transfer object used for transferring book-related data between the client and server.

# Add FluentValidation package to API project
dotnet add src/BookStore.API/BookStore.API.csproj package FluentValidation.AspNetCore
# Create BookDTO.cs in the API project
touch src/BookStore.API/Models/BookDTO.cs
# Create BooksController.cs in the API project
mkdir src/BookStore.API/Controllers
touch src/BookStore.API/Controllers/BooksController.cs