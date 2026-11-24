import { useState } from "react";
import Input from "../common/Input";
import Button from "../common/Button";
import ApiErrorAlert from "../common/ApiErrorAlert";

// Shared by Register and Login.
// fields: [{ name, label, type?, options? }]
export default function AuthForm({ title, fields, submitLabel, onSubmit, footer }) {
  const [values, setValues] = useState(() =>
    Object.fromEntries(fields.map((f) => [f.name, f.options?.[0]?.value ?? ""]))
  );
  const [error, setError] = useState("");
  const [busy, setBusy] = useState(false);

  const handleChange = (e) => setValues({ ...values, [e.target.name]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    setBusy(true);
    try {
      await onSubmit(values);
    } catch (err) {
      setError(err);
    } finally {
      setBusy(false);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>{title}</h2>
      {fields.map((f) => (
        <Input key={f.name} {...f} value={values[f.name]} onChange={handleChange} required />
      ))}
      <ApiErrorAlert error={error} />
      <Button type="submit" disabled={busy}>
        {busy ? "Please wait..." : submitLabel}
      </Button>
      {footer}
    </form>
  );
}
