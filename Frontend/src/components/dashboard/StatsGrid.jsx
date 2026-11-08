import StatCard from "./StatCard";

// stats: [{ label, value }]  -- shared by employee + HR dashboards
export default function StatsGrid({ stats }) {
  return (
    <div className="my-4 mb-6 grid grid-cols-[repeat(auto-fit,minmax(160px,1fr))] gap-4">
      {stats.map((s) => (
        <StatCard key={s.label} {...s} />
      ))}
    </div>
  );
}
