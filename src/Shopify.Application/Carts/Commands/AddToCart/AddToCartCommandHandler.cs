using ErrorOr;
using MediatR;
using Shopify.Application.Carts.Mappings;
using Shopify.Application.Common.Interfaces;
using Shopify.Application.Users.Services;
using Shopify.Domain.Carts;
using Shopify.Domain.Carts.Repositories;

namespace Shopify.Application.Carts.Commands.AddToCart;

internal sealed class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, ErrorOr<Created>>
{
    private readonly ICurrentUserProvider currentUserProvider;
    private readonly ICartRepository cartRepository;
    private readonly IUnitOfWork unitOfWork;

    public AddToCartCommandHandler(ICurrentUserProvider currentUserProvider, ICartRepository cartRepository, IUnitOfWork unitOfWork)
    {
        this.currentUserProvider = currentUserProvider;
        this.cartRepository = cartRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Created>> Handle(AddToCartCommand command, CancellationToken cancellationToken)
    {
        int userId = currentUserProvider.GetCurrentUser().Id;
        CartItem? existing = await cartRepository.GetByUserIdAndProductIdAsync(userId, command.ProductId);

        if (existing is not null)
        {
            existing.Quantity += command.Quantity;
            await cartRepository.UpdateAsync(existing);
        }
        else
        {
            CartItem cartItem = command.MapToDomain(userId);
            await cartRepository.AddAsync(cartItem);
        }

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return Result.Created;
    }
}