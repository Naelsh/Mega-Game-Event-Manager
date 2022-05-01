import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import ActivityList from './App/Features/Activities/ActivityList';
import { ActivityForm } from './App/Features/Activities/ActivityForm';
import { Login } from './App/Features/Login/Login';
import Home from './App/Features/Home/Home';
import CreateAccountForm from './App/Features/Login/CreateAccount';


const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <BrowserRouter>
    <Routes>
      <Route path="/" element={<App />} />
      <Route path='/login' element={<Login />} />
      <Route path='/new-user' element={<CreateAccountForm />} />
      {/* Change the next to private route when it is implemented */}
      <Route path='/home' element={<Home />} />
      <Route path='activities' element={<ActivityList />} /> 
      <Route path='new-activity' element={<ActivityForm />} />
    </Routes>
  </BrowserRouter>
);
