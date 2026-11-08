import { Link } from "react-router-dom";
import StatsGrid from "../../components/dashboard/StatsGrid";
import Loader from "../../components/common/Loader";
import useRequests from "../../hooks/useRequests";
import { PATHS, STATUS } from "../../utils/constants";

export default function EmployeeDashboard() {
  const { requests, loading } = useRequests("mine");
  const count = (s) => requests.filter((r) => r.status === s).length;

  return (
    <>
      <div className="page-head">
        <h2>Dashboard</h2>
        <Link className="btn btn-primary" to={PATHS.EMP_NEW}>
          + New Request
        </Link>
      </div>
      {loading ? (
        <Loader />
      ) : (
        <StatsGrid
          stats={[
            { label: "Total requests", value: requests.length },
            { label: "Pending", value: count(STATUS.PENDING) },
            { label: "Approved", value: count(STATUS.APPROVED) },
            { label: "Rejected", value: count(STATUS.REJECTED) },
          ]}
        />
      )}
      <Link to={PATHS.EMP_ALL}>View all requests →</Link>
    </>
  );
}
