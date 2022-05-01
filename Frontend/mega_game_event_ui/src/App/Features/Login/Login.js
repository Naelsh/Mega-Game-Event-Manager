import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

export function Login () {
  const[username, setUserName] = useState("");
  const[password, setPassword] = useState("");
  const navigate = useNavigate();

  let handleSubmit = async (e) => {
    e.preventDefault();
    try {
      let result = await fetch("https://localhost:7160/user/authenticate", {
        headers: {'Content-Type': 'application/json'},
        method: 'POST',
        mode: 'cors',
        body: JSON.stringify({
          username: username,
          password: password
        })
      });

      if (result.status === 200) {
        let token = await result.json().then(x => x.token);
        localStorage.token = token;
        navigate('/home', {replace: true});
      }
    } catch (error) {
      console.log(error);
    }
  }

  return (
    <div className="login-box">
      <h5>Login</h5>
      <form onSubmit={handleSubmit}>
        <input
          id="userName"
          type="email"
          value={username}
          placeholder="user@domain.com"
          onChange={(e) => setUserName(e.target.value)}
          required
        />
        <input
          id="password"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
        <button type="submit">Login</button>
      </form>
    </div>
  )
}