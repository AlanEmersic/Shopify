using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Carts;

namespace Shopify.Infrastructure.Persistence.Database.Configurations;

internal sealed class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(ci => ci.Id);
        builder.Property(ci => ci.UserId).IsRequired();
        builder.Property(ci => ci.ProductId).IsRequired();
        builder.Property(ci => ci.Quantity).IsRequired();
        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.Thumbnail).IsRequired();
        builder.Property(p => p.Price).HasPrecision(18, 4).IsRequired();

        builder.Property(ci => ci.CreatedAt).HasDefaultValueSql("GetUtcDate()").ValueGeneratedOnAdd();
        builder.Property(ci => ci.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        builder.Property(ci => ci.RowVersion).IsRowVersion();

        builder.HasOne(ci => ci.User)
            .WithMany()
            .HasForeignKey(ci => ci.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}