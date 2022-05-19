import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import Faction from "../Factions/Faction";
import '../Styling/Form.css';
import User from "../Users/User";
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
  const [users, setUsers] = useState([]);
  const [userName, setUserName] = useState("");
  const [factionName, setFactionName] = useState("");
  const [factionDescription, setFactionDescription] = useState("");
  const [addFactionMessage, setAddFactionMessage] = useState("");
  
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
            setUsers(json.users)
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
        <Faction faction={faction} activityId={id}/>
      </ul>
    ));
    return items;
  }

  let userList = () => {
    let items = users.map((user) => (
      <ul key={user.id}>
        <User user={user} />
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

      if (result.status === 200) {
        var joined = users.concat({firstName:"", lastName:"", username:userName});
        setUsers(joined);
        setUserName("");
        setMessage("User added successfully");
      } else {
        setMessage(result.message);
      }
    } catch (error) {
      console.log(error);
    }
  }

  let addFaction = async (e) => {
    e.preventDefault();
    try {
      let result = await fetch("https://localhost:7160/factions/", {
        headers:
        {
          'Content-Type': 'application/json',
          'Authorization': localStorage.token
        },
        method: 'POST',
        mode: 'cors',
        body: JSON.stringify({
          name: factionName,
          description: factionDescription,
          activityId: id
        })
      });
      
      if (result.status === 200) {
        var joined = factions.concat({ id:0, name:factionName, description:factionDescription, activityId:id, roles:[]});
        setFactions(joined);
        setFactionName("");
        setFactionDescription("");
        setAddFactionMessage("Faction added successfully");
      } else {
        setMessage(result.message);
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
        <h3>Participants</h3>
        {userList()}
        <div className="form-box">
          <h4>Add user to event</h4>
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
          <div className="errormessages">
            <span className="message">{message ? <p>{message}</p> : null}</span>
          </div>
        </div>
      </div>
      <div className="factions">
        <h3>Factions</h3>
        <form onSubmit={addFaction}>
          <input 
            id="factionName"
            type="text"
            value={factionName}
            placeholder="Faction name"
            onChange={(e) => setFactionName(e.target.value)}
            required
          />
          <input 
            id="factionDescription"
            type="text"
            value={factionDescription}
            placeholder="Description"
            onChange={(e) => setFactionDescription(e.target.value)}
            required
          />
          <button type="submit">Add faction</button>
        </form>
        <div className="errormessages">
            <span className="message">{addFactionMessage ? <p>{addFactionMessage}</p> : null}</span>
        </div>
        {factionList()}
      </div>
    </div>
  );
}