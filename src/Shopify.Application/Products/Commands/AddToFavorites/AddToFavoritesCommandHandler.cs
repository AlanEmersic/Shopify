using ErrorOr;
using MediatR;
using Shopify.Application.Common.Interfaces;
using Shopify.Application.Products.DTO;
using Shopify.Application.Products.Mappings;
using Shopify.Application.Products.Services;
using Shopify.Application.Users.Services;
using Shopify.Domain.Products;
using Shopify.Domain.Products.Repositories;

namespace Shopify.Application.Products.Commands.AddToFavorites;

internal sealed class AddToFavoritesCommandHandler : IRequestHandler<AddToFavoritesCommand, ErrorOr<Created>>
{
    private readonly ICurrentUserProvider currentUserProvider;
    private readonly IProductApiService productApiService;
    private readonly IProductRepository productRepository;
    private readonly IUnitOfWork unitOfWork;

    public AddToFavoritesCommandHandler(ICurrentUserProvider currentUserProvider, IProductApiService productApiService, IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        this.currentUserProvider = currentUserProvider;
        this.productApiService = productApiService;
        this.productRepository = productRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Created>> Handle(AddToFavoritesCommand command, CancellationToken cancellationToken)
    {
        int userId = currentUserProvider.GetCurrentUser().Id;
        ProductDto? product = await productApiService.GetProductAsync(command.ProductId, cancellationToken);

        if (product is null)
        {
            return Error.NotFound(description: "Product not found");
        }

        bool alreadyExists = await productRepository.ExistsAsync(userId, command.ProductId);

        if (alreadyExists)
        {
            return Error.Conflict(description: "Product already in favorites.");
        }

        FavoriteProduct favoriteProduct = command.MapToDomain(userId);

        await productRepository.AddAsync(favoriteProduct);
        await unitOfWork.CommitChangesAsync(cancellationToken);

        return Result.Created;
    }
}