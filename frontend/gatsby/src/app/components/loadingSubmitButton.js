import React from "react";

const LoadingSubmitButton = ({ loading, text }) => {
  return (
    <>
      <button class="btn btn-primary" type="submit" disabled={loading}>
        {loading ? <>Loading...</> : <>{text}</>}
      </button>
    </>
  );
};

export default LoadingSubmitButton;
