import StatusBadge from "./StatusBadge";

// Shared by Employee "specific request" and HR "approve request".
// `children` renders below the details (e.g. <RequestActions /> for HR).
export default function RequestDetailView({ request, children }) {
  return (
    <div className="mt-3 rounded-xl bg-white p-6 shadow-[0_1px_4px_rgba(0,0,0,0.06)]">
      <div className="flex items-center justify-between gap-4">
        <h2>Request Details</h2>
        <StatusBadge status={request.requestStatus} />
      </div>
      <dl className="grid grid-cols-1 gap-x-4 gap-y-2.5 sm:grid-cols-[140px_1fr]">
        <dt className="text-gray-500">Raised by</dt>
        <dd>
           {request.employeeName} 
        </dd>
        <dt className="text-gray-500">Employee Id</dt>
        <dd>{request.empId}</dd>
        <dt className="text-gray-500">Course Name</dt>
        <dd>{request.courseName}</dd>
        <dt className="text-gray-500">Claim Amount</dt>
        <dd>{request.claimAmount}</dd>
      </dl>
      {children}
    </div>
  );
}
