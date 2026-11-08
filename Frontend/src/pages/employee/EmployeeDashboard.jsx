import { Link } from "react-router-dom";
import StatsGrid from "../../components/dashboard/StatsGrid";
import Loader from "../../components/common/Loader";
import useDashBoard from "../../hooks/useDashboard";
import { PATHS, ROLES } from "../../utils/constants";

export default function EmployeeDashboard() {
  const { statistics, loading, error, reloadStatistics } = useDashBoard({role: ROLES.EMPLOYEE});

  return (
    <>
      <div className="mb-2 flex items-center justify-between gap-4">
        <h2>Dashboard</h2>
        <Link
          className="inline-flex items-center justify-center rounded-lg bg-blue-600 px-4 py-2.5 font-medium text-white transition-colors hover:bg-blue-700"
          to={PATHS.EMP_NEW}
        >
          + New Request
        </Link>
      </div>
      {loading ? (
        <Loader />
      ) : error ? (
        <>
          <StatsGrid
            stats={[
              { label: "Total requests", value: "-" },
              { label: "Pending", value: "-" },
              { label: "Approved", value: "-" },
              { label: "Rejected", value: "-" },
            ]}
          />
          <div className="flex items-center gap-3" role="alert">
            <p>Failed to load statistics. Please reload again.</p>
            <button
              className="rounded-lg bg-blue-600 px-4 py-2 font-medium text-white transition-colors hover:bg-blue-700"
              onClick={reloadStatistics}
              type="button"
            >
              Reload statistics
            </button>
          </div>
        </>
      ) : (
        <StatsGrid
          stats={[
            { label: "Total requests", value: statistics?.totalEmployeeRequests ?? "-" },
            { label: "Pending", value: statistics?.totalUnderReviewRequests ?? "-" },
            { label: "Approved", value: statistics?.totalApprovedRequests ?? "-" },
            { label: "Rejected", value: statistics?.totalRejectedRequests ?? "-" },
          ]}
        />
      )}
      {error ||  <Link to={PATHS.EMP_ALL}>View all requests →</Link>}
    </>
  );
}
