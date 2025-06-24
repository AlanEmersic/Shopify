import { useMutation, useQueryClient } from "@tanstack/react-query";

import { axiosConfig } from "config";
import type { CartItem } from "features";

const addCartItem = async (item: CartItem): Promise<void> => {
  await axiosConfig.post("/api/carts", item);
};

export function useAddToCart() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: addCartItem,
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ["cart"] }),
  });
}
