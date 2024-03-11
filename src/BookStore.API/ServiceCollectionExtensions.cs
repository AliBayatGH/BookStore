using BookStore.API.Application.Services;
using BookStore.Domain.AggregatesModel.Books;
using BookStore.Domain.SeedWork;
using BookStore.Infrastructure;
using BookStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
