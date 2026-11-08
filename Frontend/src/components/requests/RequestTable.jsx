import RequestRow from "./RequestRow";
import Loader from "../common/Loader";

// Shared by Employee "All Requests" and HR "Pending Requests".
// basePath: "/emp/req" or "/HR/pending"
// showEmployee: HR sees who raised the request
export default function RequestTable({ requests, loading, basePath, showEmployee = false, emptyText }) {
  if (loading) return <Loader />;
  if (!requests.length) return <p className="muted">{emptyText || "No requests found."}</p>;

  return (
    <table className="table">
      <thead>
        <tr>
          {showEmployee && <th>Employee</th>}
          <th>Title</th>
          <th>Type</th>
          <th>Date</th>
          <th>Status</th>
          <th />
        </tr>
      </thead>
      <tbody>
        {requests.map((r) => (
          <RequestRow key={r.id} request={r} basePath={basePath} showEmployee={showEmployee} />
        ))}
      </tbody>
    </table>
  );
}
