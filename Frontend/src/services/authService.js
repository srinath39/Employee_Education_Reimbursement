// MOCK implementation backed by localStorage so the UI runs without a backend.
// To use a real API, replace bodies with: api.post("/auth/register", data) etc.
// import api from "./api";

const USERS_KEY = "users";
const read = () => JSON.parse(localStorage.getItem(USERS_KEY) || "[]");

export async function register({ name, email, role }) {
  const users = read();
  if (users.some((u) => u.email.toLowerCase() === email.toLowerCase())) {
    throw new Error("Email already registered");
  }
  const user = { id: crypto.randomUUID(), name, email, role };
  localStorage.setItem(USERS_KEY, JSON.stringify([...users, user]));
  return user;
}

export async function login({ name, email }) {
  const user = read().find(
    (u) =>
      u.email.toLowerCase() === email.toLowerCase() &&
      u.name.toLowerCase() === name.toLowerCase()
  );
  if (!user) throw new Error("Invalid name or email");
  return user;
}
