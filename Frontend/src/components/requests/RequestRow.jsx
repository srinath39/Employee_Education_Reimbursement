import { Link } from "react-router-dom";
import StatusBadge from "./StatusBadge";

export default function RequestRow({ request, basePath, showEmployee }) {
  const cellClass = "border-b border-gray-100 px-3 py-3 text-left";

  return (
    <tr>
      {showEmployee && <td className={cellClass}>{request.empId}</td>}
      <td className={cellClass}>{request.courseName}</td>
      <td className={cellClass}>{request.claimAmount}</td>
      <td className={cellClass}>
        <StatusBadge status={request.requestStatus} />
      </td>
      <td className={cellClass}>
        <Link to={`${basePath}/${request.requestId}`}>View</Link>
      </td>
    </tr>
  );
}
