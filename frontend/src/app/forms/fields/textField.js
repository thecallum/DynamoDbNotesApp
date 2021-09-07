import React from "react";

const getValidationClassName = (formHasBeenSubmitted, errorMessage) => {
  if (!formHasBeenSubmitted) return "";

  if (!!errorMessage) {
    return "is-invalid";
  }

  return "is-valid";
};

const TextField = ({
  label,
  placeholder,
  errorMessage = null,
  value,
  onChange,
  formHasBeenSubmitted = false,
}) => {
  const id = `${label}__field`;

  return (
    <>
      <label for={id}>{label}</label>
      <input
        type="text"
        className={`form-control ${getValidationClassName(
          formHasBeenSubmitted,
          errorMessage
        )}`}
        id={id}
        placeholder={placeholder}
        value={value}
        onChange={(e) => onChange(e.target.value)}
      />
      {!!errorMessage && <div className="invalid-feedback">{errorMessage}</div>}
    </>
  );
};

export default TextField;
