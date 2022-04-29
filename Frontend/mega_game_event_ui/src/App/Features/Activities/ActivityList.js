import React from "react";
import Activity from "./Activity";
import './ActivityList.css';

export default class ActivityList extends React.Component {

  constructor(props){
    super(props);
    this.state = {
      activities: [],
      DataIsLoaded: false
    };
  }

  componentDidMount() {
    fetch("https://localhost:7160/activity")
    .then((result) => result.json())
    .then((json) => {
      this.setState({
        activities: json,
        DataIsLoaded: true
      });
    })
  }

  
  render() {

    const {DataIsLoaded, activities} = this.state;
    if (!DataIsLoaded) {
      return (
        <div className="App">
          <header className="App-header">
            <h1>Please wait activities are loading...</h1>
          </header>
        </div>
      );
    }

    const activityItems = activities.map((activity) => (
      <ul key = {activity.id}>
        <Activity activity = {activity}></Activity>
      </ul>
    ));

    return(
      activityItems
    );
  }
}