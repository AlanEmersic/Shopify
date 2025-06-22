using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Products;

namespace Shopify.Infrastructure.Persistence.Database.Configurations;

internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.ProductId).IsRequired();
        builder.Property(r => r.Rating).IsRequired();
        builder.Property(r => r.Comment).IsRequired();
        builder.Property(r => r.ReviewerName).IsRequired();
        builder.Property(r => r.ReviewerEmail).IsRequired();
        builder.Property(r => r.Date).IsRequired();

        builder.Property(r => r.CreatedAt).HasDefaultValueSql("GetUtcDate()").ValueGeneratedOnAdd();
        builder.Property(r => r.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        builder.Property(r => r.RowVersion).IsRowVersion();

        builder.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}