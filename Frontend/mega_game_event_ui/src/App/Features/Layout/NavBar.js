import React from "react";
import { Link } from "react-router-dom";

export default function NavBar () {
  return (
    <div fixed='top' className="navbar">
      <ul>
        <li>
          <Link to="/activities">Activities</Link>
        </li>
        <li>
          <Link to="/new-activity">Create Activity</Link>
        </li>
      </ul>
    </div>
  )
}