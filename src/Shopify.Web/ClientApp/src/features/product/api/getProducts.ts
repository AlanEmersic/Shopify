import { useQuery } from "@tanstack/react-query";

import { axiosConfig } from "config";
import type { ProductPaged } from "features";

const getProducts = async (): Promise<ProductPaged> => {
  const skip = 0;
  const limit = 9;
  const { data } = await axiosConfig.get(`/api/products?skip=${skip}&limit=${limit}`);

  return data;
};

export function useProducts() {
  return useQuery<ProductPaged>({
    queryKey: ["products"],
    queryFn: () => getProducts(),
    refetchOnWindowFocus: false,
  });
}
