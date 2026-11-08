// Handles text/email/textarea/select via the `type` prop.
export default function Input({ label, name, type = "text", options = [], ...props }) {
  let field;
  if (type === "select") {
    field = (
      <select id={name} name={name} {...props}>
        {options.map((o) => (
          <option key={o.value} value={o.value}>
            {o.label}
          </option>
        ))}
      </select>
    );
  } else if (type === "textarea") {
    field = <textarea id={name} name={name} rows={5} {...props} />;
  } else {
    field = <input id={name} name={name} type={type} {...props} />;
  }

  return (
    <div className="field">
      <label htmlFor={name}>{label}</label>
      {field}
    </div>
  );
}
