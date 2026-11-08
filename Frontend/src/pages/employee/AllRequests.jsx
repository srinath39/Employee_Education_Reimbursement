import RequestTable from "../../components/requests/RequestTable";
import useRequests from "../../hooks/useRequests";

const BASE_PATH = "/emp/req";

export default function AllRequests() {
  const { requests, loading, error } = useRequests("mine");

  return (
    <>
      <h2>All Requests</h2>
      {error && <p className="mb-3 text-sm text-red-600">{error}</p>}
      <RequestTable requests={requests} loading={loading} basePath={BASE_PATH} />
    </>
  );
}
