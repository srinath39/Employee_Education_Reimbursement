import RequestTable from "../../components/requests/RequestTable";
import ApiErrorAlert from "../../components/common/ApiErrorAlert";
import useRequests from "../../hooks/useRequests";
import {REQUESTs} from "../../utils/constants";

const BASE_PATH = "/emp/req";

export default function AllRequests() {
  const { requests, loading, error } = useRequests(REQUESTs.ALL_EMP_REQUESTS);

  return (
    <>
      <h2>All Requests</h2>
      <ApiErrorAlert error={error} />
      <RequestTable requests={requests} loading={loading} basePath={BASE_PATH} />
    </>
  );
}
