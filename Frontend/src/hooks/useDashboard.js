import { useCallback, useEffect, useState } from "react";
import { getHrStatistics, getEmployeeStatistics } from "../services/requestService";
import { ROLES } from "../utils/constants";

export default function useDashBoard({ role }) {
  const [statistics, setStatistics] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const loadStatistics = useCallback(async () => {
    setLoading(true);
    setError("");
    try {
      const result = role === ROLES.HR ? await getHrStatistics() : await getEmployeeStatistics();
      if (result == null) {
        throw new Error("Statistics response was empty");
      }
      setStatistics(result);
    } catch (e) {
      setError(e instanceof Error ? e.message : String(e));
    } finally {
      setLoading(false);
    }
  }, [role]);

  useEffect(() => {
    loadStatistics();
  }, [loadStatistics]);

  return { statistics, loading, error, reloadStatistics: loadStatistics };
}
