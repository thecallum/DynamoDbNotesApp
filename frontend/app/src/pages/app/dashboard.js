import React, { useEffect, useState } from "react";
import { getAllNotes } from "../../app/gateway/notes";
import { Link } from "@reach/router";
import Loading from "../../app/components/loading";

const Note = ({ note }) => {
  return (
    <tr>
      <td>
        <Link to={`/app/notes/${note.id}`}>{note.title}</Link>
      </td>
      <td>{note.authorName}</td>
    </tr>
  );
};

const Dashboard = () => {
  const [notes, setNotes] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getAllNotes().then((notes) => {
      console.log({ notes });
      setNotes(notes);
      setLoading(false);
    });
  }, []);

  if (loading) return <Loading />;

  if (notes === null || notes.length === 0) {
    return (
      <div>
        <p>Notes Couldnt be found!</p>

        <Link to="/app/notes/create/">Create Note</Link>
      </div>
    );
  }

  return (
    <div>
      <div className="jumbotron mt-4">
        <h1 class="display-4">Dashboard</h1>
        <p class="lead">
          This is a simple hero unit, a simple jumbotron-style component for
          calling extra attention to featured content or information.
        </p>
        <hr class="my-4" />
        <p class="lead">
          <Link
            class="btn btn-primary btn-lg"
            to="/app/notes/create/"
            role="btton"
          >
            Create Note
          </Link>
        </p>
      </div>

      <h2>Notes</h2>
      <table class="table">
        <thead>
          <tr>
            <th scope="col">Note Title</th>
            <th scope="col">Author</th>
          </tr>
        </thead>
        <tbody>
          {notes.map((note, index) => {
            return <Note note={note} key={index} />;
          })}
        </tbody>
      </table>
    </div>
  );
};

export default Dashboard;
