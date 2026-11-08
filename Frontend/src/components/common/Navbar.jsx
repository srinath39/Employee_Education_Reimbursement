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
    <header className="navbar">
      <span className="brand">Request Portal</span>
      <nav>
        {menu.map((item) => (
          <NavLink key={item.to} to={item.to} className={({ isActive }) => (isActive ? "active" : "")}>
            {item.label}
          </NavLink>
        ))}
      </nav>
      <div className="navbar-user">
        <span>
          {user.name} ({user.role.toUpperCase()})
        </span>
        <button className="btn btn-outline" onClick={handleLogout}>
          Logout
        </button>
      </div>
    </header>
  );
}
