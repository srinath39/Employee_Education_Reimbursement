import { Navigate, Route, Routes } from "react-router-dom";
import AuthLayout from "../layouts/AuthLayout";
import DashboardLayout from "../layouts/DashboardLayout";
import Register from "../pages/auth/Register";
import Login from "../pages/auth/Login";
import EmployeeDashboard from "../pages/employee/EmployeeDashboard";
import NewRequest from "../pages/employee/NewRequest";
import AllRequests from "../pages/employee/AllRequests";
import RequestDetails from "../pages/employee/RequestDetails";
import HRDashboard from "../pages/hr/HRDashboard";
import PendingRequests from "../pages/hr/PendingRequests";
import ApproveRequest from "../pages/hr/ApproveRequest";
import useAuth from "../hooks/useAuth";
import {PATHS, homePathForRole } from "../utils/constants";

export default function AppRoutes() {
  const { user } = useAuth();

  return (
    <Routes>
      <Route
        path="/"
        element={<Navigate to={"/employee/dashboard"} replace />}
      />

      <Route element={<AuthLayout />}>
        <Route path={PATHS.REGISTER} element={<Register />} />
        <Route path={PATHS.LOGIN} element={<Login />} />
      </Route>

        <Route element={<DashboardLayout />}>
          <Route path={PATHS.EMP_DASHBOARD} element={<EmployeeDashboard />} />
          <Route path={PATHS.EMP_NEW} element={<NewRequest />} />
          <Route path={PATHS.EMP_ALL} element={<AllRequests />} />
          <Route path={PATHS.EMP_DETAIL()} element={<RequestDetails />} />
        </Route>

        <Route element={<DashboardLayout />}>
          <Route path={PATHS.HR_DASHBOARD} element={<HRDashboard />} />
          <Route path={PATHS.HR_PENDING} element={<PendingRequests />} />
          <Route path={PATHS.HR_DETAIL()} element={<ApproveRequest />} />
        </Route>

      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  );
}
