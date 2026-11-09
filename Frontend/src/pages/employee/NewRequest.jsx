import { useNavigate } from "react-router-dom";
import RequestForm from "../../components/requests/RequestForm";
import { createRequest } from "../../services/requestService";
import { PATHS } from "../../utils/constants";

export default function NewRequest() {
  const navigate = useNavigate();

  const handleSubmit = async (data) => {
    await createRequest(data);
    navigate(PATHS.EMP_ALL);
  };

  return (
    <>
      <h2>New Request</h2>
      <RequestForm onSubmit={handleSubmit} />
    </>
  );
}
