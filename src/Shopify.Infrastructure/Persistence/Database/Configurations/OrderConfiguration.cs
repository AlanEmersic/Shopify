using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Orders;

namespace Shopify.Infrastructure.Persistence.Database.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.UserId).IsRequired();
        builder.Property(o => o.Status).IsRequired();

        builder.Property(o => o.CreatedAt).HasDefaultValueSql("GetUtcDate()").ValueGeneratedOnAdd();
        builder.Property(o => o.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        builder.Property(o => o.RowVersion).IsRowVersion();

        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}