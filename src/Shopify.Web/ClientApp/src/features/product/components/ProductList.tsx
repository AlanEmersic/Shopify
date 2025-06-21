import { useProducts } from "../api/getProducts";
import { ProductCard } from "./ProductCard";

function ProductList() {
  const products = useProducts();

  return (
    <div className="m-auto grid w-[80%] grid-cols-3 content-center items-center justify-center gap-5 p-0">
      {products?.data?.products.map(product => <ProductCard key={product.id} product={product}></ProductCard>)}
    </div>
  );
}

export { ProductList };
