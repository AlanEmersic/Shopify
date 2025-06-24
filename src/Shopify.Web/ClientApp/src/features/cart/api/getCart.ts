import { useQuery } from "@tanstack/react-query";

import { axiosConfig } from "config";
import type { CartItem } from "features";

const getCart = async (): Promise<CartItem[]> => {
  const { data } = await axiosConfig.get("/api/carts");

  return data.cartItems;
};

export function useCart() {
  return useQuery<CartItem[]>({
    queryKey: ["cart"],
    queryFn: () => getCart(),
    refetchOnWindowFocus: false,
  });
}
