import './App.css';
import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Home from './App/Features/Home/Home';
import CreateAccountForm from './App/Features/Login/CreateAccount';
import Login from './App/Features/Login/Login';
import ActivityList from './App/Features/Activities/ActivityList';
import ActivityForm from './App/Features/Activities/ActivityForm';
import NavBar from './App/Features/Layout/NavBar';
import ActivityDetails from './App/Features/Activities/ActivityDetails';

export default function App() {
  return (

    <BrowserRouter>
      <NavBar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path='/login' element={<Login />} />
        <Route path='/new-user' element={<CreateAccountForm />} />
        {/* Change the next to private route when it is implemented */}
        <Route path='/activities' element={<ActivityList />} />
        <Route path='/new-activity' element={<ActivityForm />} />
        <Route path='/activitydetails/:id' element={<ActivityDetails />} />
      </Routes>
    </BrowserRouter>
  );
}