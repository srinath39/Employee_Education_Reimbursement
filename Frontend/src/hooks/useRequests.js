import { useCallback, useEffect, useState } from "react";
import useAuth from "./useAuth";
import { getAllPendingRequests, getEmployeesAllRequests } from "../services/requestService";

// scope: "mine" (employee's own / all for HR) | "pending" (HR queue)
export default function useRequests(scope = "mine") {
  const { user } = useAuth();
  const [requests, setRequests] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const load = useCallback(async () => {
    setLoading(true);
    setError("");
    try {
      setRequests(scope === "pending" ? await getAllPendingRequests() : await getEmployeesAllRequests(user));
    } catch (e) {
      setError(e.message);
    } finally {
      setLoading(false);
    }
  }, [scope, user]);

  useEffect(() => {
    load();
  }, [load]);

  return { requests, loading, error, reload: load };
}
