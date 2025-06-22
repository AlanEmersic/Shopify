using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Orders;

namespace Shopify.Infrastructure.Persistence.Database.Configurations;

internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);
        builder.Property(oi => oi.OrderId).IsRequired();
        builder.Property(oi => oi.ProductId).IsRequired();
        builder.Property(oi => oi.Quantity).IsRequired();
        builder.Property(oi => oi.UnitPrice).HasPrecision(18, 4).IsRequired();

        builder.Property(oi => oi.CreatedAt).HasDefaultValueSql("GetUtcDate()").ValueGeneratedOnAdd();
        builder.Property(oi => oi.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        builder.Property(oi => oi.RowVersion).IsRowVersion();

        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}