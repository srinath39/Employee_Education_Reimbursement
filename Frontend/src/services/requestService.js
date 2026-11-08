import { STATUS } from "../utils/constants";

// MOCK implementation backed by localStorage.
// To use a real API, replace bodies with calls to `api` from ./api.
// import api from "./api";

const KEY = "requests";
const read = () => JSON.parse(localStorage.getItem(KEY) || "[]");
const write = (list) => localStorage.setItem(KEY, JSON.stringify(list));

// Employee: only their own requests. HR: everyone's.
export async function getRequests(user) {
  const all = read();
  return user.role === "hr" ? all : all.filter((r) => r.userId === user.id);
}

export async function getPendingRequests() {
  return read().filter((r) => r.status === STATUS.PENDING);
}

export async function getRequestById(id) {
  const found = read().find((r) => r.id === id);
  if (!found) throw new Error("Request not found");
  return found;
}

export async function createRequest(user, { title, type, description }) {
  const request = {
    id: crypto.randomUUID(),
    userId: user.id,
    employeeName: user.name,
    employeeEmail: user.email,
    title,
    type,
    description,
    status: STATUS.PENDING,
    createdAt: new Date().toISOString(),
  };
  write([request, ...read()]);
  return request;
}

export async function updateRequestStatus(id, status) {
  const list = read().map((r) => (r.id === id ? { ...r, status } : r));
  write(list);
  return list.find((r) => r.id === id);
}
