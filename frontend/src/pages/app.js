import * as React from "react"
import Router from '../app/routing/router'
import { Link } from 'gatsby'

const App = () => {
    return (
        <div>
            <header style={{ background: "hsl(0, 50%, 50%)" }}>
                <ul>
                    <li>
                        <Link to="/app/">Dashboard</Link>
                    </li>

                </ul>
            </header>



            <Router />

        </div>
    )
}

export default App;