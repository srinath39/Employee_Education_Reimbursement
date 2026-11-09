export default function Input({ label, name, type = "text", options = [], className = "", ...props }) {
  const controlClass = `w-full rounded-lg border border-gray-300 p-2.5 font-sans text-gray-800 placeholder:text-gray-400 focus:border-blue-500 focus:outline-none focus:ring-2 focus:ring-blue-100 ${className}`;
  let field;
  if (type === "select") {
    field = (
      <select id={name} name={name} className={controlClass} {...props}>
        {options.map((o) => (
          <option key={o.value} value={o.value}>
            {o.label}
          </option>
        ))}
      </select>
    );
  } else if (type === "textarea") {
    field = <textarea id={name} name={name} rows={5} className={controlClass} {...props} />;
  } else {
    field = <input id={name} name={name} type={type} className={controlClass} {...props} />;
  }

  return (
    <div className="mb-3.5 flex flex-col gap-1.5">
      <label className="text-sm font-medium text-gray-700" htmlFor={name}>
        {label}
      </label>
      {field}
    </div>
  );
}
