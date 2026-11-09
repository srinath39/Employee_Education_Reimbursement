import api from "./api";

export async function getEmployeeStatistics() {
  const response = await api.get("/eduReimburse/summary");
  return response.data;
}

export async function createRequest({ courseName, claimAmount }) {
  const response = await api.post("/eduReimburse", {
    courseName,
    claimAmount: Number(claimAmount),
  });
  return response.data;
}

export async function getEmployeesAllRequests() {
  const response = await api.get("/eduReimburse/mine");
  return response.data;
}

export async function getRequestById(id) {
  const response = await api.get(`/eduReimburse/${id}`);
  return response.data;
}

export async function getHrStatistics() {
  const response = await api.get("/hr/summary");
  return response.data;
}

export async function getAllPendingRequests() {
  const response = await api.get("/hr/pendingRequests");
  return response.data;
}

export async function updateRequestWithDecision(id, status) {

}
