import React, { useEffect, useState } from "react";
import Activity from "./Activity";
import './ActivityList.css';

export default function ActivityLister() {
  const [activities, setActivities] = useState([]);
  const [dataIsLoaded, setDataIsLoaded] = useState(false);
  const [message, setMessage] = useState("");

  useEffect(() => {
    loadActivities();
  })

  let loadActivities = async () => {
    try {
      let result = fetch("https://localhost:7160/activities", {
        headers: {'Authorization': localStorage.getItem("token")}
      });

      let status = (await result).status;

      if (status === 200) {
        (await result).json()
          .then((json) => {
            setActivities(json)
            setDataIsLoaded(true)
          })
      }
      else {
        // setDataIsLoaded(true);
        let newMessage = "";
        switch (status) {
          case 401:
            newMessage = "Unathorized";
            break;
        
          default:
            break;
        }
        setMessage(status + ' ' + newMessage);
      }
    } catch (error) {
      console.log(error);
    }
  }

  let activityItems = () => {
    let items = activities.map((activity) => (
      <ul key={activity.id}>
        <Activity activity={activity}></Activity>
      </ul>
    ));
    return items;
  }

  let loadingScreen = () => {
    return (
      <header className="App-header">
        <h1>Please wait activities are loading...</h1>
      </header>
    );
  }

  return (
    <div className="activityList">
      {dataIsLoaded ? activityItems() : loadingScreen()}
      <span className="message">{message ? <p>{message}</p> : null}</span>
    </div>
  )
}