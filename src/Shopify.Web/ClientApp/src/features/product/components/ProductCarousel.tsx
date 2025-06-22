import { useState } from "react";

import { ArrowLeft, ArrowRight } from "assets/icons";
import type { Product } from "features";

type ProductCarousel = {
  product: Product;
};

function ProductCarousel({ product }: Readonly<ProductCarousel>) {
  const [currentImageIndex, setCurrentImageIndex] = useState(0);

  const handlePreviousImage = () => {
    if (product?.images.length) {
      setCurrentImageIndex(previousIndex => (previousIndex - 1 + product.images.length) % product.images.length);
    }
  };

  const handleNextImage = () => {
    if (product.images.length) {
      setCurrentImageIndex(previousIndex => (previousIndex + 1) % product.images.length);
    }
  };

  return (
    <div className="relative flex w-full items-center justify-center">
      {product.images.length > 1 && (
        <button onClick={handlePreviousImage}>
          <ArrowLeft className="h-10 w-10 rounded-3xl bg-white text-gray-500 hover:cursor-pointer hover:bg-slate-200" />
        </button>
      )}

      {/* Image slider */}
      <div className="relative w-[400px] overflow-hidden rounded-lg">
        <div
          className="flex transform transition duration-500 ease-out"
          style={{
            width: `${product.images.length * 100}%`,
            transform: `translateX(-${currentImageIndex * (100 / product.images.length)}%)`,
          }}
        >
          {product.images.map(image => (
            <img key={image} className="h-80 w-full object-fill" src={image} alt={product.title} />
          ))}
        </div>
      </div>

      {product.images.length > 1 && (
        <button onClick={handleNextImage}>
          <ArrowRight className="h-10 w-10 rounded-3xl bg-white text-gray-500 hover:cursor-pointer hover:bg-slate-200" />
        </button>
      )}

      <div className="absolute bottom-0 flex w-full justify-center gap-3">
        {product.images.map((image, index) => (
          <div key={image} className={`h-3 w-3 rounded-full ${index === currentImageIndex ? "bg-blue-300" : "bg-gray-300"}`} />
        ))}
      </div>
    </div>
  );
}

export { ProductCarousel };
