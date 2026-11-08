import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import RequestDetailView from "../../components/requests/RequestDetailView";
import RequestActions from "../../components/requests/RequestActions";
import Loader from "../../components/common/Loader";
import { getRequestById, updateRequestStatus } from "../../services/requestService";
import { PATHS, STATUS } from "../../utils/constants";

export default function ApproveRequest() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [request, setRequest] = useState(null);
  const [error, setError] = useState("");

  useEffect(() => {
    getRequestById(id).then(setRequest).catch((e) => setError(e.message));
  }, [id]);

  const handleDecision = async (status) => {
    await updateRequestStatus(id, status);
    navigate(PATHS.HR_PENDING);
  };

  if (error) return <p className="error">{error}</p>;
  if (!request) return <Loader />;

  return (
    <>
      <Link to={PATHS.HR_PENDING}>← Back</Link>
      <RequestDetailView request={request}>
        {request.status === STATUS.PENDING && <RequestActions onDecision={handleDecision} />}
      </RequestDetailView>
    </>
  );
}
