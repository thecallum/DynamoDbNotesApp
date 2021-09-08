import React from "react";

const Loading = () => {
  return (
    <div
      style={{
        width: "100%",
        height: "calc(100vh - 56px)",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <div
        class="spinner-border"
        role="status"
        style={{
          width: "6rem",
          height: "6rem",
        }}
      >
        <span class="sr-only">Loading...</span>
      </div>
    </div>
  );
};

export default Loading;
