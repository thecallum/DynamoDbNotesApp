import * as React from "react"
import { Link } from 'gatsby'


// markup
const IndexPage = () => {
  return (
    <div>
      <h1>Landing page</h1>

      <Link to="/app/">Link to Application</Link>
    </div>
  )
}

export default IndexPage
