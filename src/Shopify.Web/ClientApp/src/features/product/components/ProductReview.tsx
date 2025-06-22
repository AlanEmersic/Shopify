import type { Review } from "features";

type ProductReviewProps = {
  review: Review;
};

function ProductReview({ review }: Readonly<ProductReviewProps>) {
  return (
    <div key={review.date + review.reviewerEmail} className="mb-4 border-b border-gray-200 p-4">
      <div className="mb-2 flex items-center justify-between">
        <span className="text-yellow-500">
          {"★".repeat(review.rating)} {"☆".repeat(5 - review.rating)}
        </span>
        <span className="text-ls text-gray-500">{new Date(review.date).toLocaleDateString()}</span>
      </div>
      <h3 className="text-lg font-semibold">{review.reviewerName}</h3>
      <p className="text-gray-700">{review.comment}</p>
    </div>
  );
}

export { ProductReview };
