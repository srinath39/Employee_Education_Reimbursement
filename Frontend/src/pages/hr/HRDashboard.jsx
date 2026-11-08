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
      <div className="mb-2 flex items-center justify-between gap-4">
        <h2>HR Dashboard</h2>
        <Link
          className="inline-flex items-center justify-center rounded-lg bg-blue-600 px-4 py-2.5 font-medium text-white transition-colors hover:bg-blue-700"
          to={PATHS.HR_PENDING}
        >
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
