import { ROUTES, type Product } from "features";
import { useNavigate } from "react-router-dom";

type ProductCardProps = {
  product: Product;
};

function ProductCard({ product }: Readonly<ProductCardProps>) {
  const navigate = useNavigate();

  const handleOnProductClick = (id: number) => {
    navigate(`${ROUTES.PRODUCT_DETAILS.replace(":id", id.toString())}`);
  };

  return (
    <div
      onClick={() => handleOnProductClick(product.id)}
      className="m-5 flex h-[400px] transform cursor-pointer flex-col justify-between rounded-lg border border-gray-200 bg-white p-5 shadow-md transition duration-500 hover:scale-110"
    >
      <div className="h-1/2 w-full overflow-hidden">
        <img className="h-full rounded-lg object-fill" src={product.images[0]} alt={product.title} />
      </div>
      <div className="flex flex-1 flex-col justify-between pt-5">
        <h5 className="text-gray-90 mb-2 text-2xl font-bold tracking-tight">{product.title}</h5>
        <p className="mb-3 font-normal text-gray-700">{product.description}</p>
        <div className="mt-3 flex items-end justify-between">
          <p className="flex w-32 cursor-pointer flex-row items-center rounded-lg bg-cyan-700 px-3 py-2 text-center text-sm font-medium text-white hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 focus:outline-none">
            More details
            <svg className="ms-2 h-3.5 w-3.5 rtl:rotate-180" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 10">
              <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M1 5h12m0 0L9 1m4 4L9 9" />
            </svg>
          </p>
        </div>
      </div>
    </div>
  );
}

export { ProductCard };
