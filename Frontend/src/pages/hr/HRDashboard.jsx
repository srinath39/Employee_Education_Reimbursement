import { Link } from "react-router-dom";
import StatsGrid from "../../components/dashboard/StatsGrid";
import Loader from "../../components/common/Loader";
import useRequests from "../../hooks/useRequests";
import { PATHS, STATUS } from "../../utils/constants";

export default function HRDashboard() {
  const { requests, loading } = useRequests("mine"); // HR gets all requests
  const count = (s) => requests.filter((r) => r.status === s).length;

  return (
    <>
      <div className="page-head">
        <h2>HR Dashboard</h2>
        <Link className="btn btn-primary" to={PATHS.HR_PENDING}>
          Pending Requests ({count(STATUS.PENDING)})
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
    </>
  );
}
