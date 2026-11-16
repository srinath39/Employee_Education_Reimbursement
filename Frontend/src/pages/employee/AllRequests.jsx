import RequestTable from "../../components/requests/RequestTable";
import useRequests from "../../hooks/useRequests";
import {REQUESTs} from "../../utils/constants";

const BASE_PATH = "/emp/req";

export default function AllRequests() {
  const { requests, loading, error } = useRequests(REQUESTs.ALL_EMP_REQUESTS);

  return (
    <>
      <h2>All Requests</h2>
      {error && <p className="inline-error mb-3 text-sm" role="alert">{error}</p>}
      <RequestTable requests={requests} loading={loading} basePath={BASE_PATH} />
    </>
  );
}
