import { useMutation, useQueryClient } from "@tanstack/react-query";

import { axiosConfig } from "config";

const removeCartItem = async (productId: number): Promise<void> => {
  await axiosConfig.delete(`/api/carts/${productId}`);
};

export function useRemoveCartItem() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: removeCartItem,
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ["cart"] }),
  });
}
