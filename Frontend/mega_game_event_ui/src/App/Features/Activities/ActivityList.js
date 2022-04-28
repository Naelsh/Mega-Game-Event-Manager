import React from "react";
import Activity from "./Activity";
import './ActivityList.css';

export default function ActivityList(props) {
  const activities = props.activities;
  const activityItems = activities.map((activity) => (
    <ul key = {activity.id}>
      <Activity activity = {activity}></Activity>
    </ul>
  ));

  return(
    activityItems
  );
}