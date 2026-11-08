import { NavLink, useNavigate } from "react-router-dom";
import useAuth from "../../hooks/useAuth";
import { PATHS } from "../../utils/constants";

export default function Navbar({ menu }) {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate(PATHS.LOGIN);
  };

  return (
    <header className="flex flex-wrap items-center gap-4 border-b border-gray-200 bg-white px-6 py-3 sm:gap-6">
      <span className="text-xl font-bold">Request Portal</span>
      <nav className="flex flex-1 flex-wrap gap-4">
        {menu.map((item) => (
          <NavLink
            key={item.to}
            to={item.to}
            className={({ isActive }) =>
              isActive ? "font-bold text-blue-700" : "text-blue-600"
            }
          >
            {item.label}
          </NavLink>
        ))}
      </nav>
      <div className="flex items-center gap-3">
        <span className="text-sm text-gray-700">
          {user && user.name + user.role.toUpperCase()}
        </span>
        <button
          className="rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 font-medium text-gray-700 transition-colors hover:bg-gray-50"
          onClick={handleLogout}
        >
          Logout
        </button>
      </div>
    </header>
  );
}
