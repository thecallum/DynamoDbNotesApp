import React, { useEffect, useState } from "react";
import Loading from "../components/loading";
import { getNoteById, updateNote } from "../gateway/notes";
import { navigate } from "@reach/router";
import { Link } from "gatsby";

import * as Validator from "validatorjs";

import TextField from "../forms/fields/textField";
import TextAreaField from "../forms/fields/textAreaField";

import LoadingSubmitButton from "../components/loadingSubmitButton";
import updateNoteValidator from "../forms/validation/updateNoteValidator";

const UpdateNote = ({ noteId }) => {
  const [note, setNote] = useState(null);
  const [loading, setLoading] = useState(true);
  const [updateNoteLoading, setUpdateNoteLoading] = useState(false);

  const [title, setTitle] = useState("");
  const [authorName, setAuthorName] = useState("");
  const [contents, setContents] = useState("");

  const [titleError, setTitleError] = useState(null);
  const [authorNameError, setAuthorNameError] = useState(null);
  const [contentsError, setContentsError] = useState(null);

  const [formHasBeenSubmitted, setFormHasBeenSubmitted] = useState(false);

  const handleSubmit = (e) => {
    e.preventDefault();

    if (updateNoteLoading) return;

    setFormHasBeenSubmitted(true);

    const validator = updateNoteValidator({ title, authorName, contents });

    if (validator.fails()) {
      setTitleError(validator.errors.first("title"));
      setAuthorNameError(validator.errors.first("authorName"));
      setContentsError(validator.errors.first("contents"));

      return;
    }

    setTitleError(null);
    setAuthorNameError(null);
    setContentsError(null);

    sendRequest();
  };

  const sendRequest = () => {
    setUpdateNoteLoading(true);

    const newNote = {
      title,
      authorName,
      contents,
    };
    console.log("sending request");

    updateNote(note.id, newNote)
      .then((res) => {
        console.log({ res });

        setUpdateNoteLoading(false);

        if (res === null) {
          alert("Error updating note");
          return;
        }

        navigate(`/app/notes/${note.id}`);
      })
      .catch(() => {
        setUpdateNoteLoading(false);
      });
  };

  useEffect(() => {
    getNoteById(noteId).then((note) => {
      setNote(note);

      setTitle(note.title);
      setAuthorName(note.authorName);
      setContents(note.contents);

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
      <h1>Update Note</h1>

      <form onSubmit={handleSubmit}>
        <div class="form-group">
          <TextField
            label="Note Title"
            placeholder="Name of the note"
            errorMessage={titleError}
            value={title}
            onChange={setTitle}
          />
        </div>

        <div class="form-group">
          <TextField
            label="Author"
            placeholder="Author of the note"
            errorMessage={authorNameError}
            value={authorName}
            onChange={setAuthorName}
          />
        </div>

        <div class="form-group">
          <TextAreaField
            label="Contents"
            errorMessage={contentsError}
            value={contents}
            onChange={setContents}
          />
        </div>

        <div class="form-group">
          <LoadingSubmitButton text="Update Note" loading={updateNoteLoading} />
        </div>
      </form>
    </div>
  );
};

export default UpdateNote;
