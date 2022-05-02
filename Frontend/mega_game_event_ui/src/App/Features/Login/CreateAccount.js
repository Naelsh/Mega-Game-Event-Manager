import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import '../Styling/Form.css';

export default function CreateAccountForm() {
  const [firstName, setFirstName] = useState("");
  const [lastName, setlastName] = useState("");
  const [password, setPassword] = useState("");
  const [username, setUserName] = useState("");
  const [message, setMessage] = useState("");
  const navigate = useNavigate();

  let handleSubmit = async (e) => {
    e.preventDefault();
    try {
      let result = await fetch("https://localhost:7160/user/register", {
        headers: { 'Content-Type': 'application/json' },
        method: 'POST',
        mode: 'cors',
        body: JSON.stringify({
          firstName: firstName,
          lastName: lastName,
          username: username,
          password: password
        })
      });

      if (result.status === 200) {
        setMessage("User " + username + " has been successfully created");
        setFirstName("");
        setlastName("");
        setUserName("");
        setPassword("");
      }
    } catch (error) {
      console.log(error);
    }
  }

  let goToLogin = () => {
    navigate('/login', {replace: true});
  }

  return (
    <div className="form-box">
      <h5>Register new account</h5>
      <form onSubmit={handleSubmit}>
        <input
          id="firstName"
          type="text"
          value={firstName}
          placeholder="First name"
          onChange={(e) => setFirstName(e.target.value)}
          required
          maxLength={255}
        />
        <input
          id="lastName"
          type="text"
          value={lastName}
          placeholder="Last name"
          onChange={(e) => setlastName(e.target.value)}
          required
          maxLength={255}
        />
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
          placeholder="password"
          onChange={(e) => setPassword(e.target.value)}
          required
          minLength={6}
          maxLength={255}
        />
        <button type="submit">Create Account</button>
        <button onClick={goToLogin}>Go to login</button>
      </form>
      <span className="message">{message ? <p>{message}</p> : null}</span>
    </div>
  )
}