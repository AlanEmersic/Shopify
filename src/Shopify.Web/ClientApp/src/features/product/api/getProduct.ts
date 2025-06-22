import { useQuery } from "@tanstack/react-query";

import { axiosConfig } from "config";
import type { Product } from "features";

const getProduct = async (id: number): Promise<Product> => {
  const { data } = await axiosConfig.get(`/api/products/${id}`);

  return data;
};

export function useProduct(id: number) {
  return useQuery<Product>({
    queryKey: ["product", id],
    queryFn: () => getProduct(id),
    refetchOnWindowFocus: false,
  });
}
