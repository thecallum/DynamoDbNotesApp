import { Router as ReachRouter, Link } from "@reach/router";

import HomePage from "../pages/home";

import Dashboard from "../pages/app/dashboard";
import ViewNote from "../pages/app/viewNote";
import CreateNote from "../pages/app/createNote";
import UpdateNote from "../pages/app/updateNote";

import Layout from "@Components/layout";

const AppRoutes = ({ children }) => {
  return (
    <div>
      <Layout>{children}</Layout>
    </div>
  );
};

const Router = () => {
  return (
    <ReachRouter>
      <HomePage path="/" />

      <AppRoutes path="/app/">
        <Dashboard path="/" />
        <ViewNote path="/notes/:noteId/" />
        <CreateNote path="/notes/create/" />
        <UpdateNote path="/notes/:noteId/update/" />
      </AppRoutes>
    </ReachRouter>
  );
};

export default Router;
