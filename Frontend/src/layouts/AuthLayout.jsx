import { Navigate, Outlet } from "react-router-dom";
import useAuth from "../hooks/useAuth";
import { homePathForRole } from "../utils/constants";

// Shared by /register and /login
export default function AuthLayout() {
  const { user } = useAuth();
  if (user) return <Navigate to={homePathForRole(user.role)} replace />;

  return (
    <div className="grid min-h-screen place-items-center p-4">
      <div className="w-full max-w-sm rounded-xl bg-white p-8 shadow-[0_2px_12px_rgba(0,0,0,0.08)]">
        <h1 className="mb-4 text-xl font-bold">Request Portal</h1>
        <Outlet />
      </div>
    </div>
  );
}
