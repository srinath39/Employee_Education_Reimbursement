export default function StatCard({ label, value }) {
  return (
    <div className="flex flex-col rounded-xl bg-white p-5 shadow-[0_1px_4px_rgba(0,0,0,0.06)]">
      <span className="text-[28px] font-bold">{value}</span>
      <span className="text-gray-500">{label}</span>
    </div>
  );
}
