import React, { useState } from "react";
import './ActivityForm.css';

export function ActivityForm() {
  const[name, setName] = useState("");
  const[description, setDescription] = useState("");
  const[location, setLocation] = useState("");
  const[startDate, setStartDate] = useState("");
  const[endDate, setEndDate] = useState("");
  const[message, setMessage] = useState("");

  let handleSubmit = async (e) => {
    e.preventDefault();
    try {
      let result = await fetch("https://localhost:7160/activity", {
        headers: { 'Content-Type': 'application/json' },
        method: 'POST',
        mode: 'cors',
        body: JSON.stringify({
          name: name,
          description: description,
          location: location,
          startDate: startDate,
          endDate: endDate
        })
      });
      // let resultJson = await result.json();
      if (result.status === 200) {
        setName("");
        setDescription("");
        setLocation("");
        setStartDate("");
        setEndDate("");
        setMessage("Activity created successfully");
      } else {
        setMessage("Some error occured");
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div class="form-box">
      <h5>Create new activity</h5>
      <form onSubmit={handleSubmit}>
          <input 
            id="activityName"
            type="text"
            value={name}
            placeholder="Activity name"
            onChange={(e) => setName(e.target.value)}
            required
            minLength={4}
            maxLength={50}
          />
          <input 
            id="activityStartDate"
            type="datetime-local"
            value={startDate}
            onChange={(e) => setStartDate(e.target.value)}
            required
          />
          <input 
            id="activityEndDate"
            type="datetime-local"
            value={endDate}
            onChange={(e) => setEndDate(e.target.value)}
            required
          />
          <input 
            id="activityLocation"
            type="text"
            value={location}
            placeholder="Location"
            onChange={(e) => setLocation(e.target.value)}
            maxLength={100}
          />
          <textarea 
            id="activityDescription"
            type="text"
            value={description}
            placeholder="Description..."
            onChange={(e) => setDescription(e.target.value)}
            maxLength={10000}
          />
          <button type="submit">Create Activity</button>
      </form>
      <span className="message">{message ? <p>{message}</p> : null}</span>
    </div>
  );
}