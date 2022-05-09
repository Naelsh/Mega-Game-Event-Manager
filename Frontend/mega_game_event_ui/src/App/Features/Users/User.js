import React from 'react';

export default function User (props) {

  return (
    <li className='faction'>
      <p>{props.user.firstname} {props.user.lastname}</p>
    </li>
  );
}