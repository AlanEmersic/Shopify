import { QueryClientProvider } from "@tanstack/react-query";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import { queryClient } from "config";
import { ProductList, ROUTES } from "features";

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Routes>
          <Route path={ROUTES.HOME} element={<ProductList />} />

          <Route path={"*"} element={<ProductList />} />
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;
