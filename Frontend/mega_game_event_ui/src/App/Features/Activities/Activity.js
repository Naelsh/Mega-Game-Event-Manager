import React from 'react';
import './Activity.css';

function Activity (props) {
  return (
    <li className='activity'>
      <h3>{props.activity.name}</h3>
      <p><strong>Starts:</strong> {props.activity.startDate}</p>
      <p><strong>Ends:</strong> {props.activity.endDate}</p>
      <p><strong>Location:</strong> {props.activity.location}</p>
      <p>{props.activity.description}</p>
    </li>
  );
}

export default Activity;