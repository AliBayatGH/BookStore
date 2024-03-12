using BookStore.Domain.AggregatesModel.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.EntityTypeConfigurations;
public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Book");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b=>b.Author)
            .HasMaxLength (100)
            .IsRequired();

        builder.Property(b => b.Price)
            .IsRequired();
    }
}
