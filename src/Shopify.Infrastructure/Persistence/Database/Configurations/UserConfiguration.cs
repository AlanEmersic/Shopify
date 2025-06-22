using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Users;

namespace Shopify.Infrastructure.Persistence.Database.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Email).IsRequired();
        builder.Property(u => u.Password).IsRequired();
        builder.Property(u => u.Address).IsRequired();

        builder.Property(u => u.CreatedAt).HasDefaultValueSql("GetUtcDate()").ValueGeneratedOnAdd();
        builder.Property(u => u.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        builder.Property(u => u.RowVersion).IsRowVersion();

        ValueComparer<IReadOnlyCollection<UserRoles>> enumListComparer = new(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList());

        builder.Property(u => u.Roles)
            .HasConversion(
                roles => string.Join(',', roles),
                roles => roles.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(Enum.Parse<UserRoles>).ToList())
            .HasColumnType("text")
            .Metadata.SetValueComparer(enumListComparer);

        builder.HasMany(u => u.Orders)
            .WithOne()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}