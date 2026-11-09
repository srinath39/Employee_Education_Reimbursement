import RequestTable from "../../components/requests/RequestTable";
import useRequests from "../../hooks/useRequests";
import { PATHS } from "../../utils/constants";
import { REQUESTs } from "../../utils/constants";

export default function PendingRequests() {
  const { requests, loading, error } = useRequests(REQUESTs.ALL_HR_PENDING_REQUESTS);

  return (
    <>
      <h2>Pending Requests</h2>
      {error && <p className="mb-3 text-sm text-red-600">{error}</p>}
      <RequestTable
        requests={requests}
        loading={loading}
        basePath={PATHS.HR_PENDING}
        showEmployee={true}
        emptyText="No pending requests 🎉"
      />
    </>
  );
}
