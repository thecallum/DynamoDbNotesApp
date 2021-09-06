import React, { useEffect, useState } from "react"
import Loading from '../components/loading'
import { getNoteById, updateNote } from "../gateway/notes";
import { navigate } from "@reach/router"
import { Link } from 'gatsby'

import * as Validator from 'validatorjs';


const UpdateNote = ({ noteId }) => {
    const [note, setNote] = useState(null)
    const [loading, setLoading] = useState(true)

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


        updateNote(note.id, newNote)
            .then(res => {
                console.log({ res })

                if (res === null) {
                    alert("Error updating note")
                    return
                }

                navigate(`/app/notes/${note.id}`)

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

    useEffect(() => {
        getNoteById(noteId)
            .then(note => {
                setNote(note)

                setTitle(note.title)
                setAuthorName(note.authorName)
                setContents(note.contents)

                setLoading(false)
            })
    }, []);

    if (loading) return <Loading />

    if (note === null) {
        return (
            <div>
                <p>Note Couldnt be found!</p>
            </div>

        )
    }



    return (
        <div>
            <h1>Update Note</h1>

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

                <button type="submit">Update</button>

            </form>
        </div>
    )
}


export default UpdateNote