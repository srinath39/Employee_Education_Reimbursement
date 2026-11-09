import RequestRow from "./RequestRow";
import Loader from "../common/Loader";

// Shared by Employee "All Requests" and HR "Pending Requests".
// basePath: "/emp/req" or "/HR/pending"
// showEmployee: HR sees who raised the request
export default function RequestTable({ requests, loading, basePath, showEmployee = false, emptyText }) {
  if (loading) return <Loader />;
  if (!requests.length) return <p className="text-gray-500">{emptyText || "No requests found."}</p>;

  const headerClass = "border-b border-gray-100 px-3 py-3 text-left font-semibold";

  return (
    <div className="overflow-x-auto rounded-xl">
      <table className="w-full border-collapse bg-white">
        <thead>
          <tr>
            {showEmployee && <th className={headerClass}>Employee</th>}
            <th className={headerClass}>Course Name</th>
            <th className={headerClass}>Claim Amount</th>
            <th className={headerClass}>Status</th>
            <th className={headerClass} />
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
