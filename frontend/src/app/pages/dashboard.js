import React, { useEffect, useState } from "react"
import { getAllNotes } from "../gateway/notes";
import { Link } from 'gatsby'
import Loading from '../components/loading'


const Note = ({ note }) => {
    return (
        <li>
            <Link to={`/app/notes/${note.id}`}>{note.title}</Link>

        </li>
    )
}

const Dashboard = () => {
    const [notes, setNotes] = useState([])
    const [loading, setLoading] = useState(true)


    useEffect(() => {
        // Update the document title using the browser API
        // alert("useaffect")

        getAllNotes()
            .then(notes => {
                console.log({ notes })
                setNotes(notes)
                setLoading(false)

            })
    }, []);

    if (loading) return <Loading />


    if (notes === null || notes.length === 0) {
        return (
            <div>
                <p>Notes Couldnt be found!</p>


                <Link to="/app/notes/create/">Create Note</Link>
            </div>

        )
    }


    return (
        <div>
            <h1>Dashboard</h1>


            <h2>Notes</h2>
            <ul>
                {notes.map((note, index) => {
                    return (
                        <Note note={note} key={index} />
                    )
                })}
            </ul>

            <Link to="/app/notes/create/">Create Note</Link>

        </div>
    )
}

export default Dashboard;