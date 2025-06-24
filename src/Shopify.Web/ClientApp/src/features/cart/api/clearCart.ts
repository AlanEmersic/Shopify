import { useMutation, useQueryClient } from "@tanstack/react-query";

import { axiosConfig } from "config";

const clearCart = async (): Promise<void> => {
  await axiosConfig.delete(`/api/carts/clear`);
};

export function useClearCart() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: clearCart,
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ["cart"] }),
  });
}
