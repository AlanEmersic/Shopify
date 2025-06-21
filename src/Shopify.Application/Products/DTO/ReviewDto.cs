namespace Shopify.Application.Products.DTO;

public sealed record ReviewDto(int Rating, string Comment, string ReviewerName, string ReviewerEmail, DateTime Date);