import { useQuery } from "@tanstack/react-query";

import { axiosConfig } from "config";
import type { Category } from "features";

const getCategories = async (): Promise<Category[]> => {
  const { data } = await axiosConfig.get(`/api/products/categories`);

  return data;
};

export function useCategories() {
  return useQuery<Category[]>({
    queryKey: ["categories"],
    queryFn: () => getCategories(),
    refetchOnWindowFocus: false,
  });
}
