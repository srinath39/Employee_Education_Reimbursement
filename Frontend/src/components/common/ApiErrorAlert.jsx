import { getApiErrorInfo } from "../../utils/apiError";

export default function ApiErrorAlert({ error, onRetry }) {
  if (!error) return null;

  const info = getApiErrorInfo(error);

  return (
    <section
      className={`api-error api-error--${info.category}`}
      role="alert"
      aria-live="assertive"
    >
      <div className="api-error-heading">
        <span className="api-error-category">{info.categoryLabel}</span>
        {info.status && (
          <span className="api-error-status">
            {info.status} · {info.statusLabel}
          </span>
        )}
      </div>
      <p className="api-error-message">{info.message}</p>
      {info.validationErrors.length > 0 && (
        <ul className="api-error-list">
          {info.validationErrors.map((message, index) => (
            <li key={`${message}-${index}`}>{message}</li>
          ))}
        </ul>
      )}
      {info.guidance && <p className="api-error-guidance">{info.guidance}</p>}
      {onRetry && (
        <button className="api-error-retry" onClick={onRetry} type="button">
          Try again
        </button>
      )}
    </section>
  );
}
