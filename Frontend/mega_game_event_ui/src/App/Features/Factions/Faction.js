import React from 'react';
import Role from '../Roles/Role';
import './Factions.css';

export default function Faction (props) {

  let roles = () => {
    let items = props.faction.roles.map((role) => (
      <ul key={role.id}>
        <Role role={role}/>
      </ul>
    ));
    return items;
  }
  return (
    <li className='faction'>
      <p><strong>{props.faction.name}</strong></p>
      <p>{props.faction.description}</p>
      <div className='roles'>
        <h4>Roles</h4>
        {roles()}
      </div>
    </li>
  );
}