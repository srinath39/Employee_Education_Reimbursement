import { Link } from "react-router-dom";
import StatusBadge from "./StatusBadge";

export default function RequestRow({ request, basePath, showEmployee }) {
  const cellClass = "border-b border-gray-100 px-3 py-3 text-left";

  return (
    <tr>
      {showEmployee && <td className={cellClass}>{request.employeeName}</td>}
      <td className={cellClass}>{request.title}</td>
      <td className={cellClass}>{request.type}</td>
      <td className={cellClass}>
        <StatusBadge status={request.status} />
      </td>
      <td className={cellClass}>
        <Link to={`${basePath}/${request.id}`}>View</Link>
      </td>
    </tr>
  );
}
