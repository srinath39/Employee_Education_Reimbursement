import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || "http://localhost:5000/api",
});

export const setApiUserId = (userId) => {
  if (userId) {
    api.defaults.headers.common["UserId"] = userId;
  } else {
    delete api.defaults.headers.common["UserId"];
  }
};

export default api;