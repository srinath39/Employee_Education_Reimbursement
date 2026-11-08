import { useState } from "react";
import Button from "../common/Button";
import { STATUS } from "../../utils/constants";

// HR only: Approve / Reject
export default function RequestActions({ onDecision }) {
  const [busy, setBusy] = useState(false);

  const decide = async (status) => {
    setBusy(true);
    try {
      await onDecision(status);
    } finally {
      setBusy(false);
    }
  };

  return (
    <div className="actions">
      <Button disabled={busy} onClick={() => decide(STATUS.APPROVED)}>
        Approve
      </Button>
      <Button variant="danger" disabled={busy} onClick={() => decide(STATUS.REJECTED)}>
        Reject
      </Button>
    </div>
  );
}
