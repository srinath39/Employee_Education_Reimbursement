export default function StatCard({ label, value }) {
  return (
    <div className="stat-card flex flex-col rounded-xl bg-white p-5">
      <span className="stat-value font-bold">{value}</span>
      <span className="stat-label">{label}</span>
    </div>
  );
}
