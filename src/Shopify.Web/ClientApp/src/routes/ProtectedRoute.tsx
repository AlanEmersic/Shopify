import { Navigate, Outlet } from "react-router-dom";
import { useShallow } from "zustand/shallow";

import { ROUTES } from "features";
import { useAuthStore } from "stores";

function ProtectedRoute() {
  const token = useAuthStore(useShallow(state => state.token));

  if (!token) {
    return <Navigate to={ROUTES.LOG_IN} replace />;
  }

  return <Outlet />;
}

export { ProtectedRoute };
