import { useState } from "react";
import Input from "../common/Input";
import Button from "../common/Button";

const TYPES = ["Leave", "Equipment", "Reimbursement", "Other"].map((t) => ({ value: t, label: t }));

export default function RequestForm({ onSubmit }) {
  const [values, setValues] = useState({ title: "", type: TYPES[0].value, description: "" });
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState("");

  const handleChange = (e) => setValues({ ...values, [e.target.name]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();
    setBusy(true);
    setError("");
    try {
      await onSubmit(values);
    } catch (err) {
      setError(err.message);
    } finally {
      setBusy(false);
    }
  };

  return (
    <form
      className="mt-3 rounded-xl bg-white p-6 shadow-[0_1px_4px_rgba(0,0,0,0.06)]"
      onSubmit={handleSubmit}
    >
      <Input label="Title" name="title" value={values.title} onChange={handleChange} required />
      <Input label="Type" name="type" type="select" options={TYPES} value={values.type} onChange={handleChange} />
      <Input
        label="Description"
        name="description"
        type="textarea"
        value={values.description}
        onChange={handleChange}
        required
      />
      {error && <p className="mb-3 text-sm text-red-600">{error}</p>}
      <Button type="submit" disabled={busy}>
        {busy ? "Submitting..." : "Submit request"}
      </Button>
    </form>
  );
}
