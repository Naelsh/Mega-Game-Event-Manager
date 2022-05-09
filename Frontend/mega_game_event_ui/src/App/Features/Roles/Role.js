import React from 'react';
import User from '../Users/User';

export default function Role (props) {

  let users = () => {
    let users = props.role.users.map((user) => (
      <ul key={user.id}>
        <User user={user}/>
      </ul>
    ));
    return users;
  }

  return (
    <li className='role'>
      <p><strong>{props.role.name}</strong></p>
      <p>{props.role.description}</p>
      <div className='users'>
        <h5>Players</h5>
        {users()}
      </div>
    </li>
  );
}