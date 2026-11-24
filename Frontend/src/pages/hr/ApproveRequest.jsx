import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import RequestDetailView from "../../components/requests/RequestDetailView";
import RequestActions from "../../components/requests/RequestActions";
import Loader from "../../components/common/Loader";
import ApiErrorAlert from "../../components/common/ApiErrorAlert";
import { getRequestById, updateRequestWithDecision } from "../../services/requestService";
import { PATHS, STATUS } from "../../utils/constants";

export default function ApproveRequest() {
  const { id } = useParams();
  const navigate = useNavigate();
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

  const handleDecision = async (status) => {
    setError("");
    try {
      await updateRequestWithDecision(id, status);
      navigate(PATHS.HR_PENDING);
    } catch (e) {
      setError(e);
    }
  };

  if (!request && error) {
    return (
      <>
        <Link to={PATHS.HR_PENDING}>← Back to pending requests</Link>
        <ApiErrorAlert error={error} />
      </>
    );
  }
  if (!request) return <Loader />;

  return (
    <>
      <Link to={PATHS.HR_PENDING}>← Back</Link>
      <ApiErrorAlert error={error} />
      <RequestDetailView request={request}>
        {String(request.requestStatus).toLowerCase() === STATUS.PENDING && <RequestActions onDecision={handleDecision} />}
      </RequestDetailView>
    </>
  );
}
