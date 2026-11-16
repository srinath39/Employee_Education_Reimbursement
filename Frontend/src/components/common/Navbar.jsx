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
    <header className="site-header">
      <div className="site-header-inner">
        <span className="brand">
          <span className="brand-mark" aria-hidden="true">ER</span>
          Education Reimbursement
        </span>
        <nav className="primary-nav" aria-label="Main navigation">
          {menu.map((item) => (
            <NavLink
              key={item.to}
              to={item.to}
              className={({ isActive }) =>
                isActive ? "nav-link nav-link-active" : "nav-link"
              }
            >
              {item.label}
            </NavLink>
          ))}
        </nav>
        <div className="account-actions">
          <div className="account-meta">
            <span className="account-name">{user?.name}</span>
            <span className="role-pill">{user?.role}</span>
          </div>
          <button
            className="logout-button rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 font-medium text-gray-700 transition-colors hover:bg-gray-50"
            onClick={handleLogout}
          >
            Logout
          </button>
        </div>
      </div>
    </header>
  );
}
