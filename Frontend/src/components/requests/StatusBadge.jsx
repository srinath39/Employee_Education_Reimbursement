const STATUS_CLASSES = {
  pending: "bg-amber-100 text-amber-800",
  approved: "bg-emerald-100 text-emerald-800",
  rejected: "bg-red-100 text-red-800",
};

export default function StatusBadge({ status }) {
  const statusClass = STATUS_CLASSES[String(status).toLowerCase()] || "bg-gray-100 text-gray-700";

  return (
    <span className={`rounded-full px-2.5 py-0.5 text-xs capitalize ${statusClass}`}>
      {status}
    </span>
  );
}
