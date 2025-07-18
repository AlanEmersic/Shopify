﻿using ErrorOr;
using MediatR;
using Shopify.Application.Authorization;
using Shopify.Domain.Users;

namespace Shopify.Application.Carts.Commands.AddToCart;

[Authorize(Roles = nameof(UserRoles.Customer))]
public sealed record AddToCartCommand(int ProductId, int Quantity, string Title, string Thumbnail, decimal Price) : IRequest<ErrorOr<Created>>;