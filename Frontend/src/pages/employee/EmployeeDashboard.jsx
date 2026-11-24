import { Link } from "react-router-dom";
import StatsGrid from "../../components/dashboard/StatsGrid";
import Loader from "../../components/common/Loader";
import useDashBoard from "../../hooks/useDashboard";
import ApiErrorAlert from "../../components/common/ApiErrorAlert";
import { PATHS, ROLES } from "../../utils/constants";

export default function EmployeeDashboard() {
  const { statistics, loading, error, reloadStatistics } = useDashBoard({role: ROLES.EMPLOYEE});

  return (
    <>
      <div className="page-title-row">
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
          <ApiErrorAlert error={error} onRetry={reloadStatistics} />
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
      <Link to={PATHS.EMP_ALL}>View all requests →</Link>
    </>
  );
}
