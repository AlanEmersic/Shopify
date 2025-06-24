import { useState } from "react";
import { toast } from "react-hot-toast";
import { useNavigate } from "react-router-dom";
import { useShallow } from "zustand/shallow";

import { ROUTES, useAddToCart, type Product } from "features";
import { useAuthStore, useCartStore } from "stores";

type ProductCardProps = {
  product: Product;
};

function ProductCard({ product }: Readonly<ProductCardProps>) {
  const navigate = useNavigate();
  const [quantity, setQuantity] = useState(1);

  const { token } = useAuthStore(useShallow(state => ({ token: state.token })));
  const addToCart = useCartStore(useShallow(state => state.addToCart));
  const { mutate: addCartItem } = useAddToCart();

  const handleOnProductClick = (id: number) => {
    navigate(`${ROUTES.PRODUCT_DETAILS.replace(":id", id.toString())}`);
  };

  const handleAddToCart = () => {
    const item = {
      productId: product.id,
      title: product.title,
      price: product.price,
      thumbnail: product.thumbnail,
      quantity: quantity,
    };

    addToCart(item);
    addCartItem(item);
    toast.success("Added to cart");
  };

  return (
    <div className="m-5 flex h-[400px] transform flex-col justify-between rounded-lg border border-gray-200 bg-white p-5 shadow-md transition duration-500 hover:scale-110">
      <div className="h-1/2 w-full overflow-hidden">
        <img className="h-full rounded-lg object-fill" src={product.thumbnail} alt={product.title} />
      </div>
      <div className="flex flex-1 flex-col justify-between pt-5">
        <h5 className="text-gray-90 mb-2 text-2xl font-bold tracking-tight">{product.title}</h5>
        <p className="mb-3 font-normal text-gray-700">{product.description}</p>
        <p className="mb-3 text-xl font-semibold text-gray-900">Price {product.price}€</p>
        <div className="mt-3 flex items-end justify-between">
          <div onClick={() => handleOnProductClick(product.id)}>
            <p className="flex w-32 cursor-pointer flex-row items-center rounded-lg bg-cyan-700 px-3 py-2 text-center text-sm font-medium text-white hover:bg-cyan-800 focus:ring-4 focus:ring-cyan-300 focus:outline-none">
              More details
              <svg className="ms-2 h-3.5 w-3.5 rtl:rotate-180" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 10">
                <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M1 5h12m0 0L9 1m4 4L9 9" />
              </svg>
            </p>
          </div>
          {token && (
            <>
              <div className="flex items-center gap-2 py-1">
                <label htmlFor="quantity" className="text-sm text-gray-600">
                  Qty:
                </label>
                <input
                  id="quantity"
                  type="number"
                  min={1}
                  value={quantity}
                  onChange={e => setQuantity(Number(e.target.value))}
                  className="w-16 rounded border border-gray-300 px-2 py-1 text-sm"
                />
              </div>
              <button
                className="cursor-pointer rounded-lg bg-cyan-500 px-3 py-2 text-sm font-medium text-white hover:bg-cyan-600"
                onClick={handleAddToCart}
              >
                Add to cart
              </button>
            </>
          )}
        </div>
      </div>
    </div>
  );
}

export { ProductCard };
