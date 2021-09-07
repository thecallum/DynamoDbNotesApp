import React from "react";

const getValidationClassName = (formHasBeenSubmitted, errorMessage) => {
  if (!formHasBeenSubmitted) return "";

  if (!!errorMessage) {
    return "is-invalid";
  }

  return "is-valid";
};

const TextAreaField = ({
  label,
  errorMessage = null,
  value,
  onChange,
  formHasBeenSubmitted = false,
}) => {
  const id = `${label}__field`;

  return (
    <>
      <label for={id}>{label}</label>

      <textarea
        className={`form-control ${getValidationClassName(
          formHasBeenSubmitted,
          errorMessage
        )}`}
        id={id}
        rows="3"
        value={value}
        onChange={(e) => onChange(e.target.value)}
      ></textarea>

      {!!errorMessage && <div className="invalid-feedback">{errorMessage}</div>}
    </>
  );
};

export default TextAreaField;
