import { QueryClientProvider } from "@tanstack/react-query";
import { Toaster } from "react-hot-toast";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import { queryClient } from "config";
import { Cart, Login, Navigation, ProductDetails, ProductList, Register, ROUTES, UserProfile } from "features";
import { ProtectedRoute } from "routes";

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Navigation />
        <Toaster position="top-center" />
        <Routes>
          <Route path={ROUTES.HOME} element={<ProductList />} />
          <Route path={ROUTES.PRODUCT_DETAILS} element={<ProductDetails />} />
          <Route path={ROUTES.LOG_IN} element={<Login />} />
          <Route path={ROUTES.REGISTER} element={<Register />} />

          <Route element={<ProtectedRoute />}>
            <Route path={ROUTES.PROFILE} element={<UserProfile />} />
            <Route path={ROUTES.CART} element={<Cart />} />
          </Route>

          <Route path={"*"} element={<ProductList />} />
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;
