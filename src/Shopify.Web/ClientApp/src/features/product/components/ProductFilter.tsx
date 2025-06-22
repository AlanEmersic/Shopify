import { useCategories } from "features";

type ProductFilterProps = {
  search: string;
  onSearchChange: (value: string) => void;

  category?: string;
  onCategoryChange: (value?: string) => void;

  sortBy?: string;
  onSortByChange: (value: string) => void;

  order: "asc" | "desc";
  onOrderChange: (value: "asc" | "desc") => void;

  limit: number;
  onLimitChange: (value: number) => void;
};

function ProductFilter({
  search,
  onSearchChange,
  category,
  onCategoryChange,
  sortBy,
  onSortByChange,
  order,
  onOrderChange,
  limit,
  onLimitChange,
}: Readonly<ProductFilterProps>) {
  const categories = useCategories();

  if (categories.isLoading) {
    return <div className="m-auto text-center text-2xl">Loading categories...</div>;
  }
  if (!categories.data) {
    return <div className="m-auto text-center text-2xl">Categories not found.</div>;
  }

  return (
    <div className="flex flex-wrap items-center justify-center gap-4">
      <div className="flex items-center gap-2">
        <input
          type="text"
          placeholder="Search..."
          value={search}
          onChange={e => onSearchChange(e.target.value)}
          className="rounded border px-4 py-2"
        />
        {search && (
          <button type="button" onClick={() => onSearchChange("")} className="text-sm text-cyan-700 hover:cursor-pointer">
            Clear
          </button>
        )}
      </div>

      <select value={category ?? ""} onChange={e => onCategoryChange(e.target.value || undefined)} className="rounded border px-4 py-2">
        <option value="">All categories</option>
        {categories.data.map(category => (
          <option key={category.slug} value={category.name}>
            {category.name}
          </option>
        ))}
      </select>

      <select value={sortBy} onChange={e => onSortByChange(e.target.value)} className="rounded border px-4 py-2">
        <option value="title">Title</option>
        <option value="brand">Brand</option>
        <option value="category">Category</option>
        <option value="price">Price</option>
        <option value="rating">Rating</option>
      </select>

      <select value={order} onChange={e => onOrderChange(e.target.value as "asc" | "desc")} className="rounded border px-4 py-2">
        <option value="asc">Ascending</option>
        <option value="desc">Descending</option>
      </select>

      <select value={limit} onChange={e => onLimitChange(Number(e.target.value))} className="rounded border px-4 py-2">
        {[6, 9, 12, 18, 24].map(num => (
          <option key={num} value={num}>
            {num} per page
          </option>
        ))}
      </select>
    </div>
  );
}

export { ProductFilter };
