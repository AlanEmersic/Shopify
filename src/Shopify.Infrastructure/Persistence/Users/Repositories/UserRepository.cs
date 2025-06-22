using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Users;
using Shopify.Domain.Users.Repositories;
using Shopify.Infrastructure.Persistence.Database;

namespace Shopify.Infrastructure.Persistence.Users.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ShopifyDbContext dbContext;

    public UserRepository(ShopifyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(User user)
    {
        dbContext.Add(user);

        await Task.CompletedTask;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await dbContext.Users.AsNoTracking().AnyAsync(x => x.Email == email);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
    }
}