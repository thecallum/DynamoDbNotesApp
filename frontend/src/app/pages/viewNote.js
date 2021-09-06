import React, { useEffect, useState } from "react"
import { navigate } from "@reach/router"
import { getNoteById, deleteNote } from "../gateway/notes";
import { Link } from 'gatsby'

import Loading from '../components/loading'

const deleteNoteHandler = noteId => {
    const decision = window.confirm("Are you sure you want to delete this note?");

    if (decision !== true) return;

    deleteNote(noteId).then(res => {
        if (res === false) {
            alert("Error deleting note")
            return
        }

        navigate("/app/")
    })
}

const ViewNote = ({ noteId }) => {
    const [note, setNote] = useState(null)
    const [loading, setLoading] = useState(true)

    useEffect(() => {
        getNoteById(noteId)
            .then(note => {
                setNote(note)
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
            <h1>{note.title}</h1>

            <dl>
                <dt>Author</dt>
                <dd>{note.authorName}</dd>

                <dt>Created</dt>
                <dd>{note.created}</dd>
                <dt>Last Modified</dt>
                <dd>{note.modified}</dd>
            </dl>

            <div style={{
                "padding": 30,
                "margin": "15px 0",
                "background": "#ddd"
            }}>
                {note.contents}
            </div>

            <button onClick={() => deleteNoteHandler(noteId)}>Delete Note</button>

            <Link to={`/app/notes/${noteId}/update/`}>Update Note</Link>
        </div>
    )
}

export default ViewNote;