import { useCallback, useEffect, useState } from "react";
import useAuth from "./useAuth";
import { getAllPendingRequests, getEmployeesAllRequests } from "../services/requestService";
import {REQUESTs} from "../utils/constants";

// scope: "mine" (employee's own / all for HR) | "pending" (HR queue)
export default function useRequests(scope = REQUESTs.ALL_EMP_REQUESTS) {
  const { user } = useAuth();
  const [requests, setRequests] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const load = useCallback(async () => {
    setLoading(true);
    setError("");
    try {
      setRequests(scope === REQUESTs.ALL_HR_PENDING_REQUESTS ? await getAllPendingRequests() : await getEmployeesAllRequests());
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
