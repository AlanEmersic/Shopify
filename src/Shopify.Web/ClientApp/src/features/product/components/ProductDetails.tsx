import { useParams } from "react-router-dom";

import { ProductCarousel, ProductDetailsInformation, ProductReview, useProduct } from "features";

function ProductDetails() {
  const { id } = useParams();
  const product = useProduct(parseInt(id!));

  if (product.isLoading) {
    return <div className="m-auto text-center text-2xl">Loading...</div>;
  }
  if (product.isError) {
    return <div className="m-auto text-center text-2xl">Error loading product details.</div>;
  }
  if (!product.data) {
    return <div className="m-auto text-center text-2xl">Product not found.</div>;
  }

  return (
    <div className="m-auto flex flex-col items-center justify-center align-middle">
      <h1 className="mb-4 text-center text-5xl leading-none font-extrabold tracking-tight text-gray-900">{product.data.title}</h1>

      <ProductCarousel product={product.data} />

      {/* Tags */}
      <div className="mt-4 flex flex-wrap items-center justify-center gap-2">
        {product.data?.tags.map(tag => (
          <span key={tag} className="rounded-full bg-cyan-100 px-3 py-1 text-sm font-medium text-cyan-800">
            {tag}
          </span>
        ))}
      </div>

      {/* Product Details */}
      <ProductDetailsInformation product={product.data} />

      {/* Reviews */}
      <div className="mt-8 w-full max-w-4xl rounded-lg bg-white px-4 py-6 shadow-md">
        <h2 className="mb-4 text-2xl font-semibold text-gray-900">Reviews</h2>
        {product.data.reviews?.length ? (
          product.data.reviews.map(review => <ProductReview key={review.date + review.reviewerEmail} review={review} />)
        ) : (
          <p className="text-gray-500">No reviews yet.</p>
        )}
      </div>
    </div>
  );
}

export { ProductDetails };
