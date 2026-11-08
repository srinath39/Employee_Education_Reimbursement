import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import RequestDetailView from "../../components/requests/RequestDetailView";
import Loader from "../../components/common/Loader";
import { getRequestById } from "../../services/requestService";
import { PATHS } from "../../utils/constants";

export default function RequestDetails() {
  const { id } = useParams();
  const [request, setRequest] = useState(null);
  const [error, setError] = useState("");

  useEffect(() => {
    getRequestById(id).then(setRequest).catch((e) => setError(e.message));
  }, [id]);

  if (error) return <p className="error">{error}</p>;
  if (!request) return <Loader />;

  return (
    <>
      <Link to={PATHS.EMP_ALL}>← Back</Link>
      <RequestDetailView request={request} />
    </>
  );
}
