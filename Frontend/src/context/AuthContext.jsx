import { createContext, useCallback, useMemo, useState } from "react";
import * as authService from "../services/authService";

export const AuthContext = createContext(null);

const getStoredUser = () => {
  try {
    return JSON.parse(localStorage.getItem("user"));
  } catch {
    return null;
  }
};

export function AuthProvider({ children }) {
  const [user, setUser] = useState(getStoredUser);

  const persist = (u) => {
    localStorage.setItem("user", JSON.stringify(u));
    setUser(u);
    return u;
  };

  const login = useCallback(async (data) => persist(await authService.login(data)), []);

  const register = useCallback(async (data) => {
    const created = await authService.register(data);
    return persist(created);
  }, []);

  const logout = useCallback(() => {
    localStorage.removeItem("user");
    setUser(null);
  }, []);

  const value = useMemo(
    () => ({ user, role: user?.role, login, register, logout }),
    [user, login, register, logout]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
