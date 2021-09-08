import * as React from "react";

import Navbar from "./navbar";

const Layout = ({ children }) => {
  return (
    <div>
      <Navbar />

      <main className="container">{children}</main>
    </div>
  );
};

export default Layout;
