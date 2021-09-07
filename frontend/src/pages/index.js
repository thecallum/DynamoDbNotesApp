import * as React from "react";
import { Link } from "gatsby";

import Layout from "../components/layout";
import HomepageModal from "../components/homepageModal";

// markup
const IndexPage = () => {
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

export default IndexPage;
