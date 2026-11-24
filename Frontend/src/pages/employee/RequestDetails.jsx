import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import RequestDetailView from "../../components/requests/RequestDetailView";
import Loader from "../../components/common/Loader";
import ApiErrorAlert from "../../components/common/ApiErrorAlert";
import { getRequestById } from "../../services/requestService";
import { PATHS } from "../../utils/constants";

export default function RequestDetails() {
  const { id } = useParams();
  const [request, setRequest] = useState(null);
  const [error, setError] = useState("");

  useEffect(() => {
    let active = true;
    getRequestById(id)
      .then((result) => {
        if (active) setRequest(result);
      })
      .catch((e) => {
        if (active) setError(e);
      });
    return () => {
      active = false;
    };
  }, [id]);

  if (error && !request) {
    return (
      <>
        <Link to={PATHS.EMP_ALL}>← Back to all requests</Link>
        <ApiErrorAlert error={error} />
      </>
    );
  }
  if (!request) return <Loader />;

  return (
    <>
      <Link to={PATHS.EMP_ALL}>← Back</Link>
      <ApiErrorAlert error={error} />
      <RequestDetailView request={request} />
    </>
  );
}
