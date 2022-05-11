import React from 'react';

export default function User (props) {

  return (
    <li className='user'>
      <p>{props.user.firstName} {props.user.lastName}</p>
    </li>
  );
}