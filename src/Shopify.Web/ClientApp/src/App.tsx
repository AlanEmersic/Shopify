import { QueryClientProvider } from "@tanstack/react-query";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import { queryClient } from "config";
import { ProductList, ROUTES } from "features";
import { ProductDetails } from "features/product/components/ProductDetails";

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Routes>
          <Route path={ROUTES.HOME} element={<ProductList />} />
          <Route path={ROUTES.PRODUCT_DETAILS} element={<ProductDetails />} />

          <Route path={"*"} element={<ProductList />} />
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;
