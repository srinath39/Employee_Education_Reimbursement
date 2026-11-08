import api from "./api";

export async function getEmployeeStatistics() {
  const response = await api.get("/eduReimburse/summary");
  return response.data;
}

export async function createRequest(user, { title, type, description }) {
  
}

export async function getEmployeesAllRequests(user) {
  
}

export async function getRequestById(id) {
  
}

export async function getHrStatistics(){

}

export async function getAllPendingRequests() {
  
}

export async function updateRequestWithDecision(id, status) {
  
}
