import { Link, useNavigate } from "react-router-dom";
import AuthForm from "../../components/auth/AuthForm";
import useAuth from "../../hooks/useAuth";
import { PATHS, homePathForRole } from "../../utils/constants";

const FIELDS = [
  { name: "name", label: "Name" },
  { name: "email", label: "Email", type: "email" },
];

export default function Login() {
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (data) => {
    const user = await login(data);
    navigate(homePathForRole(user.role));
  };

  return (
    <AuthForm
      title="Login"
      fields={FIELDS}
      submitLabel="Login"
      onSubmit={handleSubmit}
      footer={
        <p className="muted">
          New here? <Link to={PATHS.REGISTER}>Register</Link>
        </p>
      }
    />
  );
}
