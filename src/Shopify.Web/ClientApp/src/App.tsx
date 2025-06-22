import { QueryClientProvider } from "@tanstack/react-query";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import { queryClient } from "config";
import { Login, Navigation, ProductDetails, ProductList, Register, ROUTES } from "features";

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Navigation />
        <Routes>
          <Route path={ROUTES.HOME} element={<ProductList />} />
          <Route path={ROUTES.PRODUCT_DETAILS} element={<ProductDetails />} />
          <Route path={ROUTES.LOG_IN} element={<Login />} />
          <Route path={ROUTES.REGISTER} element={<Register />} />

          <Route path={"*"} element={<ProductList />} />
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;
