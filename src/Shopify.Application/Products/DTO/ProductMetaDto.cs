namespace Shopify.Application.Products.DTO;

public sealed record ProductMetaDto(DateTime CreatedAt, DateTime UpdatedAt, string Barcode, string QrCodeUrl);