using ErrorOr;
using MediatR;
using Shopify.Application.Common.Interfaces;
using Shopify.Application.Users.Services;
using Shopify.Domain.Carts.Repositories;

namespace Shopify.Application.Carts.Commands.ClearCart;

internal sealed class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, ErrorOr<Deleted>>
{
    private readonly ICurrentUserProvider currentUserProvider;
    private readonly ICartRepository cartRepository;
    private readonly IUnitOfWork unitOfWork;

    public ClearCartCommandHandler(ICurrentUserProvider currentUserProvider, ICartRepository cartRepository, IUnitOfWork unitOfWork)
    {
        this.currentUserProvider = currentUserProvider;
        this.cartRepository = cartRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(ClearCartCommand command, CancellationToken cancellationToken)
    {
        int userId = currentUserProvider.GetCurrentUser().Id;

        await cartRepository.DeleteAllByUserIdAsync(userId);
        await unitOfWork.CommitChangesAsync(cancellationToken);

        return Result.Deleted;
    }
}