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
      message: detail || responseMessage || validationErrors[0] || data?.title || "The request could not be completed.",
      validationErrors,
    };
  }

  if (status >= 500 && status < 600) {
    return {
      category: "server",
      message: detail || responseMessage || "The server could not complete your request.",
      validationErrors: [],
    };
  }

  if (error?.request && !response) {
    return {
      category: "network",
      message: "We couldn't reach the service.",
      validationErrors: [],
    };
  }

  return {
    category: "unknown",
    message: typeof error?.message === "string" ? error.message : "Something went wrong.",
    validationErrors: [],
  };
}
