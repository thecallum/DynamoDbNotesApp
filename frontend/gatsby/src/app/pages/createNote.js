import React, { useState } from "react";
import * as Validator from "validatorjs";
import { createNote } from "../gateway/notes";
import { navigate } from "@reach/router";

import LoadingSubmitButton from "../components/loadingSubmitButton";
import createNoteValidator from "../forms/validation/createNoteValidator";

import TextField from "../forms/fields/textField";
import TextAreaField from "../forms/fields/textAreaField";

const CreateNote = () => {
  const [title, setTitle] = useState("");
  const [authorName, setAuthorName] = useState("");
  const [contents, setContents] = useState("");

  const [titleError, setTitleError] = useState(null);
  const [authorNameError, setAuthorNameError] = useState(null);
  const [contentsError, setContentsError] = useState(null);

  const [createNoteLoading, setCreateNoteLoading] = useState(false);

  const [formHasBeenSubmitted, setFormHasBeenSubmitted] = useState(false);

  const handleSubmit = (e) => {
    e.preventDefault();

    if (createNoteLoading) return;

    setFormHasBeenSubmitted(true);

    const validator = createNoteValidator({ title, authorName, contents });

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
    setCreateNoteLoading(true);

    const newNote = {
      title,
      authorName,
      contents,
    };
    console.log("sending request");

    createNote(newNote)
      .then((res) => {
        console.log({ res });

        setCreateNoteLoading(false);

        if (res === null) {
          alert("Error creating note");
          return;
        }

        navigate(`/app/notes/${res.id}`);
      })
      .catch(() => {
        setCreateNoteLoading(false);
      });
  };

  return (
    <div>
      <h1>Create Note</h1>

      <form onSubmit={handleSubmit}>
        <div class="form-group">
          <TextField
            label="Note Title"
            placeholder="Name of the note"
            errorMessage={titleError}
            value={title}
            onChange={setTitle}
            formHasBeenSubmitted={formHasBeenSubmitted}
          />
        </div>

        <div class="form-group">
          <TextField
            label="Author"
            placeholder="Author of the note"
            errorMessage={authorNameError}
            value={authorName}
            onChange={setAuthorName}
            formHasBeenSubmitted={formHasBeenSubmitted}
          />
        </div>

        <div class="form-group">
          <TextAreaField
            label="Contents"
            errorMessage={contentsError}
            value={contents}
            onChange={setContents}
            formHasBeenSubmitted={formHasBeenSubmitted}
          />
        </div>

        <div class="form-group">
          <LoadingSubmitButton text="Create Note" loading={createNoteLoading} />
        </div>
      </form>
    </div>
  );
};

export default CreateNote;
