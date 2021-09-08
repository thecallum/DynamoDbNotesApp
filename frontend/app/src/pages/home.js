import * as React from "react";
import { Link } from "@reach/router";

import Layout from "../app/components/homepage/layout";
import HomepageModal from "../app/components/homepage/homepageModal";

// markup
const HomePage = () => {
  return (
    <Layout>
      <main className="container mt-3">
        <div class="jumbotron">
          <h1 class="display-4">Notes App</h1>
          <p class="lead">
            Gatsby powered frontend for ASP.NET Lambda function powered by
            DynamoDb.
          </p>
          <hr class="my-4" />
          <button
            type="button"
            class="btn btn-primary"
            data-toggle="modal"
            data-target="#myModal"
          >
            Open Application
          </button>
        </div>
      </main>

      <HomepageModal />
    </Layout>
  );
};

export default HomePage;
