const VARIANT_CLASSES = {
  primary: "border-transparent bg-blue-600 text-white hover:bg-blue-700",
  danger: "border-transparent bg-red-600 text-white hover:bg-red-700",
  outline: "border-gray-300 bg-transparent text-gray-700 hover:bg-gray-50",
};

export default function Button({ variant = "primary", className = "", children, ...props }) {
  return (
    <button
      className={`inline-flex cursor-pointer items-center justify-center rounded-lg border px-4 py-2.5 font-medium transition-colors disabled:cursor-not-allowed disabled:opacity-60 ${VARIANT_CLASSES[variant] || VARIANT_CLASSES.primary} ${className}`}
      {...props}
    >
      {children}
    </button>
  );
}
