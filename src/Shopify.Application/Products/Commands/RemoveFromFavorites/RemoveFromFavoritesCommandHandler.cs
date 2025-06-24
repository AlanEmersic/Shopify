using ErrorOr;
using MediatR;
using Shopify.Application.Common.Interfaces;
using Shopify.Application.Products.Mappings;
using Shopify.Application.Users.Services;
using Shopify.Domain.Products;
using Shopify.Domain.Products.Repositories;

namespace Shopify.Application.Products.Commands.RemoveFromFavorites;

internal sealed class RemoveFromFavoritesCommandHandler : IRequestHandler<RemoveFromFavoritesCommand, ErrorOr<Deleted>>
{
    private readonly ICurrentUserProvider currentUserProvider;
    private readonly IProductRepository productRepository;
    private readonly IUnitOfWork unitOfWork;

    public RemoveFromFavoritesCommandHandler(ICurrentUserProvider currentUserProvider, IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        this.currentUserProvider = currentUserProvider;
        this.productRepository = productRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(RemoveFromFavoritesCommand command, CancellationToken cancellationToken)
    {
        int userId = currentUserProvider.GetCurrentUser().Id;
        FavoriteProduct? favoriteProduct = await productRepository.GetByUserIdAndProductIdAsync(userId, command.ProductId);

        if (favoriteProduct is null)
        {
            return Error.NotFound(description: "Product not found");
        }

        await productRepository.DeleteAsync(favoriteProduct);
        await unitOfWork.CommitChangesAsync(cancellationToken);

        return Result.Deleted;
    }
}