import StatusBadge from "./StatusBadge";

// Shared by Employee "specific request" and HR "approve request".
// `children` renders below the details (e.g. <RequestActions /> for HR).
export default function RequestDetailView({ request, children }) {
  return (
    <div className="card">
      <div className="detail-head">
        <h2>{request.title}</h2>
        <StatusBadge status={request.status} />
      </div>
      <dl className="detail-grid">
        <dt>Raised by</dt>
        <dd>
          {request.employeeName} ({request.employeeEmail})
        </dd>
        <dt>Type</dt>
        <dd>{request.type}</dd>
        <dt>Date</dt>
        <dt>Description</dt>
        <dd>{request.description}</dd>
      </dl>
      {children}
    </div>
  );
}
