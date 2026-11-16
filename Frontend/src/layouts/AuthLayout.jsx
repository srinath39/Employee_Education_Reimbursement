import { Navigate, Outlet } from "react-router-dom";
import useAuth from "../hooks/useAuth";
import { homePathForRole } from "../utils/constants";

// Shared by /register and /login
export default function AuthLayout() {
  const { user } = useAuth();
  if (user) return <Navigate to={homePathForRole(user.role)} replace />;

  return (
    <div className="auth-shell">
      <div className="auth-card">
        <h1 className="auth-brand">Education Reimbursement</h1>
        <Outlet />
      </div>
    </div>
  );
}
