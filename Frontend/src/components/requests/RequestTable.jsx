import RequestRow from "./RequestRow";
import Loader from "../common/Loader";

// Shared by Employee "All Requests" and HR "Pending Requests".
// basePath: "/emp/req" or "/HR/pending"
// showEmployee: HR sees who raised the request
export default function RequestTable({ requests, loading, basePath, showEmployee = false, emptyText }) {
  if (loading) return <Loader />;
  if (!requests.length) return <p className="text-gray-500">{emptyText || "No requests found."}</p>;

  return (
    <div className="table-shell">
      <table className="request-table">
        <thead>
          <tr>
            {showEmployee && <th>Employee</th>}
            <th>Course Name</th>
            <th>Claim Amount</th>
            <th>Status</th>
            <th><span className="sr-only">Actions</span></th>
          </tr>
        </thead>
        <tbody>
          {requests.map((r) => (
            <RequestRow key={r.requestId} request={r} basePath={basePath} showEmployee={showEmployee} />
          ))}
        </tbody>
      </table>
    </div>
  );
}
