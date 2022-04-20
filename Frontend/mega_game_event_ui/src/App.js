import './App.css';
import React from 'react';
import Activity from './Activities/Activity';

class App extends React.Component {
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
        <div>
          <h1>Please wait activities are loading...</h1>
        </div>
      );
    }


    return (
      <div className="App">
        <h1> Fetch data from an api in react </h1>
        {
          activities.map((activity) => (
            <ul key = {activity.id}>
              <Activity activity = {activity}></Activity>
            </ul>
          ))
        }
      </div>
    );
  }
}

export default App;
