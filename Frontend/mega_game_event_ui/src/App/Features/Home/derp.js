import React, { } from 'react';
import { NavLink } from 'react-router-dom';
import './Home.css';

export default function Home() {

  return (
    <div className='home'>
      <h1>Welcome home</h1>
      <ul>
        <li>
          <NavLink to='/login'>Login</NavLink>
        </li>
        <li>
          <NavLink to='/new-user'>Create new account</NavLink>
        </li>
      </ul>
    </div>
  )
}