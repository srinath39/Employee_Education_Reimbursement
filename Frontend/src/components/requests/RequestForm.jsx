import { useState } from "react";
import Input from "../common/Input";
import Button from "../common/Button";

export default function RequestForm({ onSubmit }) {
  const [values, setValues] = useState({ courseName: "", claimAmount: ""});
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
      className="request-form mt-3"
      onSubmit={handleSubmit}
    >
      <Input label="Course name" name="courseName" value={values.courseName} onChange={handleChange} required />
      <Input
        label="Claim amount"
        name="claimAmount"
        type="number"
        step="any"
        value={values.claimAmount}
        onChange={handleChange}
        required
      />
      {error && <p className="inline-error mb-3 text-sm" role="alert">{error}</p>}
      <Button type="submit" disabled={busy}>
        {busy ? "Submitting..." : "Submit request"}
      </Button>
    </form>
  );
}
