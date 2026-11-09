import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import RequestDetailView from "../../components/requests/RequestDetailView";
import RequestActions from "../../components/requests/RequestActions";
import Loader from "../../components/common/Loader";
import { getRequestById, updateRequestWithDecision } from "../../services/requestService";
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
    await updateRequestWithDecision(id, status);
    navigate(PATHS.HR_PENDING);
  };

  if (error) return <p className="text-sm text-red-600">{error}</p>;
  if (!request) return <Loader />;

  return (
    <>
      <Link to={PATHS.HR_PENDING}>← Back</Link>
      <RequestDetailView request={request}>
        {String(request.requestStatus).toLowerCase() === STATUS.PENDING && <RequestActions onDecision={handleDecision} />}
      </RequestDetailView>
    </>
  );
}
