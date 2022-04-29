import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import ActivityList from './App/Features/Activities/ActivityList';
import { ActivityForm } from './App/Features/Activities/ActivityForm';


const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <BrowserRouter>
    <Routes>
      <Route path="/" element={<App />} />
      <Route path='activities' element={<ActivityList />} />
      <Route path='new-activity' element={<ActivityForm />} />
    </Routes>
  </BrowserRouter>
);
