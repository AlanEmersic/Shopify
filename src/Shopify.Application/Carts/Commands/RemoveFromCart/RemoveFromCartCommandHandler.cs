using ErrorOr;
using MediatR;
using Shopify.Application.Common.Interfaces;
using Shopify.Application.Users.Services;
using Shopify.Domain.Carts;
using Shopify.Domain.Carts.Repositories;

namespace Shopify.Application.Carts.Commands.RemoveFromCart;

internal sealed class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand, ErrorOr<Deleted>>
{
    private readonly ICurrentUserProvider currentUserProvider;
    private readonly ICartRepository cartRepository;
    private readonly IUnitOfWork unitOfWork;

    public RemoveFromCartCommandHandler(ICurrentUserProvider currentUserProvider, ICartRepository cartRepository, IUnitOfWork unitOfWork)
    {
        this.currentUserProvider = currentUserProvider;
        this.cartRepository = cartRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(RemoveFromCartCommand command, CancellationToken cancellationToken)
    {
        int userId = currentUserProvider.GetCurrentUser().Id;
        CartItem? cartItem = await cartRepository.GetByUserIdAndProductIdAsync(userId, command.ProductId);

        if (cartItem is null)
        {
            return Error.NotFound(description: "Product not in cart.");
        }

        await cartRepository.DeleteAsync(cartItem);
        await unitOfWork.CommitChangesAsync(cancellationToken);

        return Result.Deleted;
    }
}