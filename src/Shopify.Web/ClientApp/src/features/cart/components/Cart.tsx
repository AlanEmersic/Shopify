import { toast } from "react-hot-toast";
import { useShallow } from "zustand/shallow";

import { useCart, useClearCart, useRemoveCartItem } from "features";
import { useMemo } from "react";
import { useCartStore } from "stores";

function Cart() {
  const { data } = useCart();
  const { items, setCart, removeFromCart, clearCart } = useCartStore(
    useShallow(state => ({
      items: state.items,
      setCart: state.setCart,
      removeFromCart: state.removeFromCart,
      clearCart: state.clearCart,
    })),
  );
  const { mutate: removeCartItem } = useRemoveCartItem();
  const { mutate: clearCartApi } = useClearCart();

  useMemo(() => {
    if (data) {
      setCart(data);
    }
  }, [data, setCart]);

  const total = items.reduce((sum, item) => sum + item.price * item.quantity, 0);

  const handleRemove = (productId: number) => {
    removeCartItem(productId);
    removeFromCart(productId);

    toast.success("Product removed from cart");
  };

  const handleClearCart = () => {
    clearCartApi();
    clearCart();

    toast.success("Cart cleared");
  };

  return (
    <div className="m-auto flex w-[50%] flex-col">
      <h2 className="text-2xl font-bold">Your Cart</h2>
      {items.length === 0 && <p>Your cart is empty.</p>}

      {items.length !== 0 && (
        <>
          <ul>
            {items?.map(item => (
              <li key={item.productId} className="flex items-center gap-10 py-2">
                <div>
                  <div className="h-56 w-full overflow-hidden">
                    <img className="h-full rounded-lg object-fill" src={item.thumbnail} alt={item.title} />
                  </div>
                  <p className="text-gray-90 mb-2 text-xl font-semibold tracking-tight">{item.title}</p>
                  <p className="font-semibold text-gray-700">
                    {item.quantity} × {item.price} €
                  </p>
                </div>
                <button onClick={() => handleRemove(item.productId)} className="cursor-pointer text-red-500">
                  Remove
                </button>
              </li>
            ))}
          </ul>

          <p className="mt-4 font-bold">Total: {total.toFixed(2)} €</p>
          <button onClick={handleClearCart} className="mt-4 w-28 cursor-pointer rounded-xl bg-gray-200 px-4 py-2 hover:bg-gray-300">
            Clear cart
          </button>
        </>
      )}
    </div>
  );
}

export { Cart };
