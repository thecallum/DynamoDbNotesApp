import React, { useEffect, useState } from "react";
import { navigate, Link } from "@reach/router";
import { getNoteById, deleteNote } from "@Gateway/notes";
import ReactTimeAgo from "react-time-ago";
import Loading from "@Components/loading";

import TimeAgo from "javascript-time-ago";

import en from "javascript-time-ago/locale/en";
// import ru from 'javascript-time-ago/locale/ru'

TimeAgo.addDefaultLocale(en);
// TimeAgo.addLocale(ru)

const deleteNoteHandler = (noteId) => {
  const decision = window.confirm("Are you sure you want to delete this note?");

  if (decision !== true) return;

  deleteNote(noteId).then((res) => {
    if (res === false) {
      alert("Error deleting note");
      return;
    }

    navigate("/app/");
  });
};

const ViewNote = ({ noteId }) => {
  const [note, setNote] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getNoteById(noteId).then((note) => {
      setNote(note);
      setLoading(false);
    });
  }, []);

  if (loading) return <Loading />;

  if (note === null) {
    return (
      <div>
        <p>Note Couldnt be found!</p>
      </div>
    );
  }

  return (
    <div>
      <h1>{note.title}</h1>

      <dl class="row">
        <dt class="col-sm-3">Author</dt>
        <dd class="col-sm-9">{note.authorName}</dd>

        <dt class="col-sm-3">Created</dt>
        <dd class="col-sm-9">
          <ReactTimeAgo date={note.created} locale="en-UK" />
        </dd>

        <dt class="col-sm-3">Last Modified</dt>
        <dd class="col-sm-9">
          <ReactTimeAgo date={note.modified} locale="en-UK" />
        </dd>
      </dl>

      <div class="jumbotron jumbotron-fluid">
        <div class="container">{note.contents} </div>
      </div>

      <button
        className="btn btn-danger mr-2"
        onClick={() => deleteNoteHandler(noteId)}
      >
        Delete Note
      </button>

      <Link className="btn btn-secondary" to={`/app/notes/${noteId}/update/`}>
        Update Note
      </Link>
    </div>
  );
};

export default ViewNote;
