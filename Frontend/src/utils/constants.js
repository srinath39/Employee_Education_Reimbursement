export const ROLES = { EMPLOYEE: "employee", HR: "hr" };

export const STATUS = {
  PENDING: "under_review",
  APPROVED: "approved",
  REJECTED: "rejected",
};

export const STATUS_PATH={
  under_review:"underReview",
  approved:"approve",
  rejected:"reject",
};

export const REQUESTs = {
  ALL_EMP_REQUESTS: "allEmployeeRequests",
  ALL_HR_PENDING_REQUESTS: "allPendingRequests",
};

export const PATHS = {
  REGISTER: "/register",
  LOGIN: "/login",

  EMP_DASHBOARD: "/employee/dashboard",
  EMP_NEW: "/emp/req/new",
  EMP_ALL: "/emp/req/all",
  EMP_DETAIL: (id = ":id") => `/emp/req/${id}`,

  HR_DASHBOARD: "/HR/dashboard",
  HR_PENDING: "/HR/pending",
  HR_DETAIL: (id = ":id") => `/HR/pending/${id}`,
};

export const homePathForRole = (role) =>
  role === ROLES.HR ? PATHS.HR_DASHBOARD : PATHS.EMP_DASHBOARD;
