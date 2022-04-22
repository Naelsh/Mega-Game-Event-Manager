import React, { useState } from "react";
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
import '../Styles/Calendar.css';

function ActivityFormController () {
  const [inputs, setInputs] = useState({});

  const handleChange = (event) => {
    const name = event.target.name;
    const value = event.target.value;
    setInputs(values => ({...values, [name]: value}));
  }

  const handleStartDateChange = (value) => {
    setInputs(values => ({...values, ["startDate"]: value}));
  }

  const handleEndDateChange = (value) => {
    setInputs(values => ({...values, ["endDate"]: value}));
  }

  const handleSubmit = (event) => {
    fetch("https://localhost:7160/activity",
    {
      method: 'POST',
      mode: 'cors',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(inputs)
    });

    event.preventDefault();
    // alert(JSON.stringify(inputs))
  }
  
  return (
    <form onSubmit={handleSubmit}>
      <label>Activity Name
      <input
        type="text"
        name="name"
        value={inputs.name || ""}
        onChange={handleChange}
      />
      </label>
      <label>Description
        <input
          type="text"
          name="description"
          value={inputs.description || ""}
          onChange={handleChange}
        />
      </label>
      <p>Start Date</p>
      <Calendar value={inputs.startDate} onChange={handleStartDateChange}/>
      <p>End Date</p>
      <Calendar value={inputs.endDate} onChange={handleEndDateChange}/>
      <button type="submit">Submit</button>
    </form>
  );
}

export default ActivityFormController