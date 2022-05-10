import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import Faction from "../Factions/Faction";
import '../Styling/Form.css';
import './ActivityDetails.css';

export default function ActivityDetails() {
  const {id} = useParams();
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [location, setLocation] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [message, setMessage] = useState("");
  const [factions, setFactions] = useState([]);
  const [userName, setUserName] = useState("");

  

  useEffect(() => {
    loadActivity();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])

  let errorMessage = (responseStatus) => {
    switch (responseStatus) {
      case 401:
        return "401 - Unathorized";
      default:
        return "An error occured";
    }
  }

  let loadActivity = async () => {
    try {
      let result = fetch("https://localhost:7160/activities/" + id + "/details", {
        headers: { 'Authorization': localStorage.getItem("token") }
      });

      let status = (await result).status;

      if (status === 200) {
        (await result).json()
          .then((json) => {
            setName(json.name)
            setDescription(json.description)
            setLocation(json.location)
            setStartDate(json.startDate)
            setEndDate(json.endDate)
            setFactions(json.factions)
          })
      }
      else {
        setMessage(errorMessage(status));
      }
    } catch (error) {
      console.log(error);
    }
  }

  let factionList = () => {
    let items = factions.map((faction) => (
      <ul key={faction.id}>
        <Faction faction={faction} />
      </ul>
    ));
    return items;
  }

  let addPlayer = async (e) => {
    e.preventDefault();
    try {
      let result = await fetch("https://localhost:7160/activities/" + id + "/add-user", {
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

  return (
    <div className="activityDetails">
      <div className="activity">
        <h2>{name}</h2>
        <p>{startDate}</p>
        <p>{endDate}</p>
        <p>{description}</p>
        <p>{location}</p>
        <div className="form-box">
          <h5>Add user to event</h5>
          <form onSubmit={addPlayer}>
            <input
              id="userName"
              type="email"
              value={userName}
              placeholder="Username"
              onChange={(e) => setUserName(e.target.value)}
              required
            />
            <button type="submit">Add user</button>
          </form>
        </div>
      </div>
      <div className="factions">
        <h3>Factions</h3>
        {factionList()}
      </div>
      <div className="errormessages">
        <span className="message">{message ? <p>{message}</p> : null}</span>
      </div>
    </div>
  );
}