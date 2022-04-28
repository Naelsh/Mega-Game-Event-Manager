import './App.css';
import React from 'react';
import { Button } from 'semantic-ui-react';
import ActivityList from './App/Features/Activities/ActivityList';

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

  async addActivity() {
    const inputActivity = {
      "name": "new activity",
      "description": "with a new activity",
      "startDate": "2022-04-23T18:25:43.511Z",
      "endDate" : "2022-04-25T18:25:43.511Z",
      "location": "random place"
    };
    const response = await fetch("https://localhost:7160/activity",
      {
        headers: { 'Content-Type': 'application/json' },
        method: 'POST',
        mode: 'cors',
        body: JSON.stringify(inputActivity)
      }
    );
    return response.json();

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

    return (
      <div className="App">
        <header className="App-header">
          <h1>Mega game event manager</h1>
        </header>
        <section>
          <ActivityList activities={activities}/>
        </section>
        <section>
          <Button onClick={this.addActivity}>Create new activity</Button>
        </section>
      </div>
    );
  }
}

export default App;
