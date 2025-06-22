using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Products;

namespace Shopify.Infrastructure.Persistence.Database.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ExternalId).IsRequired();
        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.Category).IsRequired();
        builder.Property(p => p.Brand).IsRequired();
        builder.Property(p => p.Sku).IsRequired();
        builder.Property(p => p.Price).HasPrecision(18, 4).IsRequired();
        builder.Property(p => p.DiscountPercentage).HasPrecision(18, 4).IsRequired();
        builder.Property(p => p.Rating).IsRequired();
        builder.Property(p => p.Stock).IsRequired();
        builder.Property(p => p.MinimumOrderQuantity).IsRequired();
        builder.Property(p => p.Weight).IsRequired();
        builder.Property(p => p.Thumbnail).IsRequired();

        builder.Property(p => p.CreatedAt).HasDefaultValueSql("GetUtcDate()").ValueGeneratedOnAdd();
        builder.Property(p => p.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        builder.Property(p => p.RowVersion).IsRowVersion();

        builder.OwnsOne(p => p.Dimensions, dim =>
        {
            dim.Property(d => d.Width).IsRequired();
            dim.Property(d => d.Height).IsRequired();
            dim.Property(d => d.Depth).IsRequired();
        });

        builder.OwnsOne(p => p.Meta, meta =>
        {
            meta.Property(m => m.CreatedAt).IsRequired();
            meta.Property(m => m.UpdatedAt).IsRequired();
            meta.Property(m => m.Barcode).IsRequired();
            meta.Property(m => m.QrCodeUrl).IsRequired();
        });

        ValueComparer<IReadOnlyCollection<string>> stringListComparer = new(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList());

        builder.Property(p => p.Images)
            .HasConversion(
                v => string.Join(';', v),
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries))
            .HasColumnType("text")
            .Metadata.SetValueComparer(stringListComparer);

        builder.Property(p => p.Tags)
            .HasConversion(
                v => string.Join(';', v),
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries))
            .HasColumnType("text")
            .Metadata.SetValueComparer(stringListComparer);

        builder.HasMany(p => p.Reviews)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}