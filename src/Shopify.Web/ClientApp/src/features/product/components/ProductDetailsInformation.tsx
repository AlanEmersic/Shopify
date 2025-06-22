import type { Product } from "features";

type ProductDetailsInformationProps = {
  product: Product;
};

function ProductDetailsInformation({ product }: Readonly<ProductDetailsInformationProps>) {
  return (
    <div className="mt-8 w-full max-w-4xl rounded-lg bg-white px-4 py-6 shadow-md">
      <h2 className="mb-4 text-2xl font-semibold text-gray-900">Product Details</h2>
      <p className="mb-3 text-lg font-normal text-gray-500">{product.description}</p>
      <p className="mt-6 text-2xl font-semibold text-gray-900">Price: {product.price}€</p>
      <p className="text-gray-700">Category: {product.category}</p>
      <p className="text-gray-700">
        Rating: {product?.rating} ({product.reviews?.length} reviews)
      </p>
      <p className="text-gray-700">Stock: {product.stock}</p>
      <p className="text-gray-700">Brand: {product.brand}</p>
      <p className="text-gray-700">
        Dimensions: {product?.dimensions ? `${product.dimensions.width} x ${product.dimensions.height} x ${product.dimensions.depth}` : "N/A"}
      </p>
      <p className="text-gray-700">Weight: {product?.weight ? `${product.weight}` : "N/A"}</p>
      <p className="text-gray-700">Warranty information: {product.warrantyInformation}</p>
      <p className="text-gray-700">Return policy: {product.returnPolicy}</p>
      <p className="text-gray-700">Shipping information: {product.shippingInformation}</p>
      <p className="text-gray-700">Availability status: {product.availabilityStatus}</p>
    </div>
  );
}

export { ProductDetailsInformation };
