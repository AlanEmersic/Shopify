import { create } from "zustand";

import type { CartItem } from "features";

type CartState = {
  items: CartItem[];
  setCart: (items: CartItem[]) => void;
  addToCart: (item: CartItem) => void;
  removeFromCart: (productId: number) => void;
  clearCart: () => void;
};

const useCartStore = create<CartState>((set, get) => ({
  items: [],
  setCart: items => set({ items }),
  addToCart: item => {
    const existing = get().items.find(i => i.productId === item.productId);

    if (existing) {
      set({
        items: get().items.map(i => (i.productId === item.productId ? { ...i, quantity: i.quantity + item.quantity } : i)),
      });
    } else {
      set({ items: [...get().items, item] });
    }
  },
  removeFromCart: productId => set({ items: get().items.filter(i => i.productId !== productId) }),
  clearCart: () => set({ items: [] }),
}));

export { useCartStore };
