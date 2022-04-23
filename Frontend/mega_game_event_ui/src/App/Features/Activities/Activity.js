import React from 'react';
import { Button } from 'semantic-ui-react';
import './Activity.css';

function Activity (props) {
  return (
    <li>
      <h3>{props.activity.name}</h3>
      <p><strong>Starts:</strong> {props.activity.startDate}</p>
      <p><strong>Ends:</strong> {props.activity.endDate}</p>
      <p><strong>Location:</strong> {props.activity.location}</p>
      <p>{props.activity.description}</p>
      <Button>Edit</Button>
    </li>
  );
}

export default Activity;