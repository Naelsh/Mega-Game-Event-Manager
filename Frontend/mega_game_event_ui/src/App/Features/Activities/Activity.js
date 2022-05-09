import React from 'react';
import { Link } from 'react-router-dom';
import './Activity.css';

function Activity (props) {
  const editLink = "/activitydetails/" + props.activity.id;
  return (
    <li className='activity'>
      <h3>{props.activity.name}</h3>
      <p><strong>Starts:</strong> {props.activity.startDate}</p>
      <p><strong>Ends:</strong> {props.activity.endDate}</p>
      <p><strong>Location:</strong> {props.activity.location}</p>
      <p>{props.activity.description}</p>
      <Link to={editLink}>Edit</Link>
    </li>
  );
}

export default Activity;