import * as React from "react";
import Router from "../app/routing/router";
import { Link } from "gatsby";
import Helmet from "../app/helmet/helmet";

import Navbar from "../app/components/navbar";

const App = () => {
  return (
    <div>
      <Helmet />

      <Navbar />

      <main className="container">
        <Router />
      </main>
    </div>
  );
};

export default App;
