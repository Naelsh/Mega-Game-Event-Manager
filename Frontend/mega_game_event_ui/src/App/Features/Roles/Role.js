import React, { useState } from 'react';
import User from '../Users/User';

export default function Role(props) {
  const [userName, setUserName] = useState("");
  const [message, setMessage] = useState("");

  let addPlayer = async (e) => {
    e.preventDefault();
    try {
      let result = await fetch("https://localhost:7160/roles/" + props.role.id + "/add-user", {
        headers:
        {
          'Content-Type': 'application/json',
          'Authorization': localStorage.token
        },
        method: 'POST',
        mode: 'cors',
        body: JSON.stringify({
          userName: userName
        })
      });
      // let resultJson = await result.json();
      if (result.status === 200) {
        setUserName("");
        setMessage("User added successfully");
      } else {
        setMessage("Some error occured");
      }
    } catch (error) {
      console.log(error);
    }
  }

  let users = () => {
    let users = props.role.users.map((user) => (
      <ul key={user.id}>
        <User user={user} />
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
        <div className='form-box'>
          <form onSubmit={addPlayer}>
            <input
              id='roleUserName'
              type="email"
              value={userName}
              placeholder="Username"
              onChange={(e) => setUserName(e.target.value)}
              required
            />
            <button type="submit">+</button>
          </form>
          <div className="errormessages">
            <span className="message">{message ? <p>{message}</p> : null}</span>
          </div>
        </div>
      </div>
    </li>
  );
}