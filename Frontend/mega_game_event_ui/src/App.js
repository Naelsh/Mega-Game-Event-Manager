import './App.css';
import React from 'react';
import { BrowserRouter as Link } from 'react-router-dom';

export default function App () {
  return (
    <div>
      <h1>Bookkeeper!</h1>
      <nav style={{borderBottom: "solid 1px", paddingBottom: "1rem"}}>
        <Link to="/activities">Activities</Link> |{" "}
        <Link to="/new-activity">New Activity</Link>
      </nav>
    </div>
  );
}