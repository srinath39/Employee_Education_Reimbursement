import { Outlet } from "react-router-dom";
import Navbar from "../components/common/Navbar";
import useAuth from "../hooks/useAuth";
import { PATHS, ROLES } from "../utils/constants";

const MENU = {
  [ROLES.EMPLOYEE]: [
    { label: "Dashboard", to: PATHS.EMP_DASHBOARD },
    { label: "New Request", to: PATHS.EMP_NEW },
    { label: "All Requests", to: PATHS.EMP_ALL },
  ],
  [ROLES.HR]: [
    { label: "Dashboard", to: PATHS.HR_DASHBOARD },
    { label: "Pending Requests", to: PATHS.HR_PENDING },
  ],
};

// Shared by employee + HR. Only the menu differs.
export default function DashboardLayout() {
  const { user } = useAuth();

  return (
    <div className="dash">
      <Navbar menu={MENU[user.role]} />
      <main className="dash-content">
        <Outlet />
      </main>
    </div>
  );
}
