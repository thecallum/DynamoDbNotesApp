import React, { useState } from 'react'
import * as Validator from 'validatorjs';
import { createNote } from '../gateway/notes'
import { navigate } from "@reach/router"


const CreateNote = () => {
    const [title, setTitle] = useState("")
    const [authorName, setAuthorName] = useState("")
    const [contents, setContents] = useState("")

    const [titleError, setTitleError] = useState(null)
    const [authorNameError, setAuthorNameError] = useState(null)
    const [contentsError, setContentsError] = useState(null)

    const handleSubmit = e => {
        e.preventDefault();

        const validator = createValidator();

        if (validator.fails()) {
            setTitleError(validator.errors.first("title"))
            setAuthorNameError(validator.errors.first("authorName"))
            setContentsError(validator.errors.first("contents"))

            return
        }

        setTitleError(null)
        setAuthorNameError(null)
        setContentsError(null)


        sendRequest()
    }

    const sendRequest = () => {
        const newNote = {
            title, authorName, contents
        }
        console.log("sending request")


        createNote(newNote)
            .then(res => {
                console.log({ res })

                if (res === null) {
                    alert("Error creating note")
                    return
                }

                navigate(`/app/notes/${res.id}`)

            })
    }

    const createValidator = () => {
        let data = {
            title,
            authorName,
            contents
        };

        let rules = {
            title: 'required|min:3|max:50',
            authorName: 'required|min:3|max:50',
            contents: 'required|max:1000'
        };

        return new Validator(data, rules);
    }

    return (
        <div>
            <h1>Create Note</h1>

            <form onSubmit={handleSubmit}>

                <div>
                    <label>
                        Title
                        <input type="text" value={title} onChange={e => setTitle(e.target.value)} />
                        {!!titleError && <p style={{ color: "red" }}>{titleError}</p>}
                    </label>
                </div>

                <div>
                    <label>
                        Author Name
                        <input type="text" value={authorName} onChange={e => setAuthorName(e.target.value)} />
                        {!!authorNameError && <p style={{ color: "red" }}>{authorNameError}</p>}
                    </label>
                </div>

                <div>
                    <label>
                        Contents
                        <textarea value={contents} onChange={e => setContents(e.target.value)} />
                        {!!contentsError && <p style={{ color: "red" }}>{contentsError}</p>}
                    </label>
                </div>

                <button type="submit">Submit</button>

            </form>
        </div>
    )
}


export default CreateNote