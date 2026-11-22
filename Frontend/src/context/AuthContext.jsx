import { createContext, useCallback, useMemo, useState ,useEffect} from "react";
import * as authService from "../services/authService";
import {setApiUserId} from '../services/api';

export const AuthContext = createContext(null);


export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);

  const login = useCallback(async (data) => setUser(await authService.login(data)), []);

  const register = useCallback(async (data) => {
    const created = await authService.register(data);
    return setUser(created);
  }, []);

  const logout = useCallback(() => {
    setUser(null);
  }, []);

  const value = useMemo(
    () => ({ user, role: user?.role, login, register, logout }),
    [user, login, register, logout]
  );

  useEffect(() => {
    setApiUserId(user?.userId ?? null);
  }, [user?.userId]);

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
