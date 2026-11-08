export const ROLES = { EMPLOYEE: "employee", HR: "hr" };

export const STATUS = {
  PENDING: "pending",
  APPROVED: "approved",
  REJECTED: "rejected",
};

export const PATHS = {
  REGISTER: "/register",
  LOGIN: "/login",

  EMP_DASHBOARD: "/employee/dashboard",
  EMP_NEW: "/emp/req/new",
  EMP_ALL: "/emp/reqs",
  EMP_DETAIL: (id = ":id") => `/emp/req/${id}`,

  HR_DASHBOARD: "/HR/dashboard",
  HR_PENDING: "/HR/pending",
  HR_DETAIL: (id = ":id") => `/HR/pending/${id}`,
};

export const homePathForRole = (role) =>
  role === ROLES.HR ? PATHS.HR_DASHBOARD : PATHS.EMP_DASHBOARD;
