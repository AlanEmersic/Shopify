import { useQuery } from "@tanstack/react-query";

import { axiosConfig } from "config";
import type { ProductPaged, ProductsQuery } from "features";

const getProducts = async (query: ProductsQuery): Promise<ProductPaged> => {
  const params = new URLSearchParams();

  if (query.search) {
    params.append("search", query.search);
  }
  if (query.skip) {
    params.append("skip", query.skip.toString());
  }
  if (query.limit) {
    params.append("limit", query.limit.toString());
  }
  if (query.category) {
    params.append("category", query.category);
  }
  if (query.sortBy) {
    params.append("sortBy", query.sortBy);
  }
  if (query.order) {
    params.append("order", query.order);
  }

  const { data } = await axiosConfig.get(`/api/products?${params.toString()}`);

  return data;
};

export function useProducts(query: ProductsQuery) {
  return useQuery<ProductPaged>({
    queryKey: ["products", query],
    queryFn: () => getProducts(query),
    refetchOnWindowFocus: false,
  });
}
