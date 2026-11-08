import { Link } from "react-router-dom";
import StatusBadge from "./StatusBadge";

export default function RequestRow({ request, basePath, showEmployee }) {
  return (
    <tr>
      {showEmployee && <td>{request.employeeName}</td>}
      <td>{request.title}</td>
      <td>{request.type}</td>
      <td>
        <StatusBadge status={request.status} />
      </td>
      <td>
        <Link to={`${basePath}/${request.id}`}>View</Link>
      </td>
    </tr>
  );
}
