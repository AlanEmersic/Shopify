import { ProductCard, ProductFilter, useProductList } from "features";

function ProductList() {
  const {
    search,
    setSearch,
    category,
    setCategory,
    sortBy,
    setSortBy,
    order,
    setOrder,
    limit,
    setLimit,
    skip,
    setSkip,
    products,
    total,
    handlePrevious,
    handleNext,
    currentPage,
    totalPages,
  } = useProductList();

  if (products.isLoading) {
    return <div className="m-auto text-center text-2xl">Loading...</div>;
  }
  if (!products.data) {
    return <div className="m-auto text-center text-2xl">Products not found.</div>;
  }

  return (
    <div className="flex flex-col items-center gap-5 p-6">
      {/* Filters */}
      <ProductFilter
        search={search}
        onSearchChange={value => {
          setSkip(0);
          setSearch(value);
        }}
        category={category}
        onCategoryChange={value => {
          setSkip(0);
          setCategory(value);
        }}
        sortBy={sortBy}
        onSortByChange={value => setSortBy(value)}
        order={order}
        onOrderChange={value => setOrder(value)}
        limit={limit}
        onLimitChange={value => {
          setSkip(0);
          setLimit(value);
        }}
      />

      {/* Products  */}
      <div className="grid w-[80%] grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3">
        {products.data?.products.map(product => <ProductCard key={product.id} product={product} />)}
      </div>

      {/* Pagination */}
      {products.data.products.length > 0 && (
        <div className="mt-4 flex items-center gap-4">
          <button disabled={skip === 0} onClick={handlePrevious} className="rounded bg-gray-200 px-4 py-2 disabled:opacity-50">
            Previous
          </button>

          <span>
            Page {currentPage} of {totalPages}
          </span>

          <button disabled={skip + limit >= total} onClick={handleNext} className="rounded bg-gray-200 px-4 py-2 disabled:opacity-50">
            Next
          </button>
        </div>
      )}
    </div>
  );
}

export { ProductList };
