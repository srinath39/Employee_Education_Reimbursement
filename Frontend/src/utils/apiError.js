const STATUS_LABELS = {
  400: "Bad request",
  401: "Unauthorized",
  403: "Forbidden",
  404: "Not found",
  409: "Conflict",
  422: "Validation failed",
  429: "Too many requests",
  500: "Internal server error",
  501: "Not implemented",
  502: "Bad gateway",
  503: "Service unavailable",
  504: "Gateway timeout",
};

function getValidationErrors(errors) {
  if (!errors || typeof errors !== "object") return [];

  return Object.entries(errors).flatMap(([field, messages]) => {
    const values = Array.isArray(messages) ? messages : [messages];
    const label = field === "$" ? "Request" : field;
    return values
      .filter((message) => typeof message === "string" && message.trim())
      .map((message) => `${label}: ${message}`);
  });
}

function getGuidance(status) {
  switch (status) {
    case 400:
      return "Review the information and try again.";
    case 401:
      return "Sign in again, then retry your request.";
    case 403:
      return "Your account may not have permission to do this.";
    case 404:
      return "Check the link or return to the request list.";
    case 409:
      return "The request may have changed. Refresh the page and try again.";
    case 501:
      return "This feature is not available yet.";
    default:
      if (status >= 500) {
        return "Try again in a moment. If the problem continues, contact support.";
      }
      return status >= 400 ? "Review the request and try again." : "";
  }
}

export function getApiErrorInfo(error) {
  const response = error?.response;
  const status = Number(response?.status ?? error?.status) || null;
  const data = response?.data;
  const validationErrors = getValidationErrors(data?.errors);
  const detail = typeof data?.detail === "string" ? data.detail : "";
  const responseMessage = typeof data?.message === "string" ? data.message : "";

  if (status >= 400 && status < 500) {
    return {
      category: "client",
      categoryLabel: "Client error (4xx)",
      status,
      statusLabel: STATUS_LABELS[status] ?? "Request error",
      message: detail || responseMessage || (validationErrors.length ? "Check the information below." : data?.title) || "The request could not be completed.",
      validationErrors,
      guidance: getGuidance(status),
    };
  }

  if (status >= 500 && status < 600) {
    return {
      category: "server",
      categoryLabel: "Server error (5xx)",
      status,
      statusLabel: STATUS_LABELS[status] ?? "Server error",
      message: detail || responseMessage || "The server could not complete your request.",
      validationErrors: [],
      guidance: getGuidance(status),
    };
  }

  if (error?.request && !response) {
    return {
      category: "network",
      categoryLabel: "Connection problem",
      status: null,
      statusLabel: "",
      message: "We couldn't reach the service.",
      validationErrors: [],
      guidance: "Check your internet connection and try again.",
    };
  }

  return {
    category: "unknown",
    categoryLabel: "Unexpected problem",
    status: null,
    statusLabel: "",
    message: typeof error?.message === "string" ? error.message : "Something went wrong.",
    validationErrors: [],
    guidance: "Try again. If the problem continues, contact support.",
  };
}
