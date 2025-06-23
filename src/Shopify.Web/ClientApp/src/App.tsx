import { QueryClientProvider } from "@tanstack/react-query";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import { queryClient } from "config";
import { Login, Navigation, ProductDetails, ProductList, Register, ROUTES, UserProfile } from "features";
import { ProtectedRoute } from "routes";

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

          <Route element={<ProtectedRoute />}>
            <Route path={ROUTES.PROFILE} element={<UserProfile />} />
          </Route>

          <Route path={"*"} element={<ProductList />} />
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;
