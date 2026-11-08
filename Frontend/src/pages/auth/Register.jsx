import { Link, useNavigate } from "react-router-dom";
import AuthForm from "../../components/auth/AuthForm";
import useAuth from "../../hooks/useAuth";
import { PATHS, ROLES, homePathForRole } from "../../utils/constants";

const FIELDS = [
  { name: "name", label: "Name" },
  { name: "email", label: "Email", type: "email" },
  {
    name: "role",
    label: "Role",
    type: "select",
    options: [
      { value: ROLES.EMPLOYEE, label: "Employee" },
      { value: ROLES.HR, label: "HR" },
    ],
  },
];

export default function Register() {
  const { register } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (data) => {
    const user = await register(data);
    navigate(homePathForRole(user.role));
  };

  return (
    <AuthForm
      title="Register"
      fields={FIELDS}
      submitLabel="Create account"
      onSubmit={handleSubmit}
      footer={
        <p className="mt-4 text-sm text-gray-500">
          Already registered? <Link to={PATHS.LOGIN}>Login</Link>
        </p>
      }
    />
  );
}
