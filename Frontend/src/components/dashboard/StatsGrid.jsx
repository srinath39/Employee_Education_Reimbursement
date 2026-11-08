import StatCard from "./StatCard";

// stats: [{ label, value }]  -- shared by employee + HR dashboards
export default function StatsGrid({ stats }) {
  return (
    <div className="stats-grid">
      {stats.map((s) => (
        <StatCard key={s.label} {...s} />
      ))}
    </div>
  );
}
