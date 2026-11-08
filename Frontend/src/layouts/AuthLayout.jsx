import { Navigate, Outlet } from "react-router-dom";
import useAuth from "../hooks/useAuth";
import { homePathForRole } from "../utils/constants";

// Shared by /register and /login
export default function AuthLayout() {
  const { user } = useAuth();
  if (user) return <Navigate to={homePathForRole(user.role)} replace />;

  return (
    <div className="auth-page">
      <div className="auth-card">
        <h1 className="brand">Request Portal</h1>
        <Outlet />
      </div>
    </div>
  );
}
