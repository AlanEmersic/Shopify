﻿using Shopify.Domain.Common.Models;
using Shopify.Domain.Users;

namespace Shopify.Domain.Carts;

public sealed class CartItem : VersionedEntity
{
    public required int ProductId { get; init; }
    public required int UserId { get; init; }
    public required string Title { get; init; }
    public required string Thumbnail { get; init; }
    public required decimal Price { get; init; }
    public required int Quantity { get; set; }

    public User? User { get; init; }
}