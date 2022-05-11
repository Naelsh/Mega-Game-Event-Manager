import React, { useState } from 'react';
import Role from '../Roles/Role';
import './Factions.css';

export default function Faction(props) {

  const [roleName, setRoleName] = useState("");
  const [roleDescription, setRoleDescription] = useState("");
  const [message, setMessage] = useState("");

  let addRole = async (e) => {
    e.preventDefault();
    try {
      let result = await fetch("https://localhost:7160/roles/", {
        headers:
        {
          'Content-Type': 'application/json',
          'Authorization': localStorage.token
        },
        method: 'POST',
        mode: 'cors',
        body: JSON.stringify({
          name: roleName,
          description: roleDescription,
          factionId: props.faction.id
        })
      });

      if (result.status === 200) {
        setRoleName("");
        setRoleDescription("");
        setMessage("Role added successfully");
      } else {
        setMessage(result.message);
      }
    } catch (error) {
      console.log(error);
    }
  }

  let roles = () => {
    let items = props.faction.roles.map((role) => (
      <ul key={role.id}>
        <Role role={role} activityId={props.activityId} />
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
        <form onSubmit={addRole}>
          <input
            id="roleName"
            type="text"
            value={roleName}
            placeholder="Role name"
            onChange={(e) => setRoleName(e.target.value)}
            required
          />
          <input
            id="factionDescription"
            type="text"
            value={roleDescription}
            placeholder="Description"
            onChange={(e) => setRoleDescription(e.target.value)}
            required
          />
          <button type="submit">Add role</button>
        </form>
        <div className="errormessages">
            <span className="message">{message ? <p>{message}</p> : null}</span>
        </div>
        {roles()}
      </div>
    </li>
  );
}