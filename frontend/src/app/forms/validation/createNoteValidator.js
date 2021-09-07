import * as Validator from "validatorjs";

const CreateNoteValidator = ({ title, authorName, contents }) => {
  const data = {
    title,
    authorName,
    contents,
  };

  const rules = {
    title: "required|min:3|max:50",
    authorName: "required|min:3|max:50",
    contents: "required|max:1000",
  };

  return new Validator(data, rules);
};

export default CreateNoteValidator;
