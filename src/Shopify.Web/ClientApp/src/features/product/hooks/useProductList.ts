import { useDebounce, useProducts, type ProductsQuery } from "features";
import { useState } from "react";

function useProductList() {
  const [search, setSearch] = useState("");
  const [category, setCategory] = useState<string | undefined>(undefined);
  const [categories, setCategories] = useState<string[]>([]);
  const [sortBy, setSortBy] = useState<string | undefined>(undefined);
  const [order, setOrder] = useState<"asc" | "desc">("asc");
  const [limit, setLimit] = useState(6);
  const [skip, setSkip] = useState(0);
  const debouncedSearch = useDebounce(search, 500);
  const query: ProductsQuery = { search: debouncedSearch, skip, limit, category, sortBy, order };
  const products = useProducts(query);

  const total = products.data?.total ?? 0;

  const handlePrevious = () => setSkip(previous => Math.max(0, previous - limit));
  const handleNext = () => setSkip(previous => (previous + limit < total ? previous + limit : previous));

  const currentPage = Math.floor(skip / limit) + 1;
  const totalPages = Math.ceil(total / limit);

  return {
    search,
    setSearch,
    category,
    setCategory,
    categories,
    setCategories,
    sortBy,
    setSortBy,
    order,
    setOrder,
    limit,
    setLimit,
    skip,
    setSkip,
    query,
    products,
    total,
    handlePrevious,
    handleNext,
    currentPage,
    totalPages,
  };
}

export { useProductList };
