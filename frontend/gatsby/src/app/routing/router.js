import * as React from "react"
import { Router as ReachRouter } from "@reach/router"
import { Link } from 'gatsby'

import Dashboard from '../pages/dashboard'
import ViewNote from '../pages/viewNote'
import CreateNote from '../pages/createNote'
import UpdateNote from '../pages/updateNote'

const Router = () => {
    return (

        /*
            - get all routes
            - get route by id
            - create route
            - udpate route
            - delete route

        */

        <ReachRouter basepath="/app/">
            <Dashboard path="/" />
            <ViewNote path="/notes/:noteId/" />
            <CreateNote path="/notes/create/" />
            <UpdateNote path="/notes/:noteId/update/" />
        </ReachRouter>

    )
}

export default Router;