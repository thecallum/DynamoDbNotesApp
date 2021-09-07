import * as React from "react";
import { Helmet as ReactHelmet } from "react-helmet";

import Navbar from "./navbar";

const Layout = ({ children }) => {
  return (
    <div>
      <Navbar />
      <ReactHelmet>
        <meta charset="utf-8" />
        <meta
          name="viewport"
          content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"
        />
        <link
          rel="stylesheet"
          href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"
          integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh"
          crossorigin="anonymous"
        />
      </ReactHelmet>

      <main className="container">{children}</main>
    </div>
  );
};

export default Layout;
