import { getApiErrorInfo } from "../../utils/apiError";

export default function ApiErrorAlert({ error, onRetry }) {
  if (!error) return null;

  const info = getApiErrorInfo(error);

  return (
    <div
      className={`api-error api-error--${info.category}`}
      role="alert"
      aria-live="assertive"
    >
      {info.validationErrors.length > 0 && (
        <ul className="api-error-list">
          {info.validationErrors.map((message, index) => (
            <li key={`${message}-${index}`}>{message}</li>
          ))}
        </ul>
      )}
      {info.validationErrors.length === 0 && <p className="api-error-message">{info.message}</p>}
      {onRetry && (
        <button className="api-error-retry" onClick={onRetry} type="button">
          Try again
        </button>
      )}
    </div>
  );
}
