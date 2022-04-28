import './App.css';
import React from 'react';
import ActivityList from './App/Features/Activities/ActivityList';
import { ActivityForm } from './App/Features/Activities/ActivityForm';

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
          <ActivityForm/>
        </section>
        <footer>
          <p>Designed by: <a href="https://github.com/Naelsh" target={'_blank'} rel="noreferrer">Niklas Lindblad</a></p>
        </footer>
      </div>
    );
  }
}

export default App;
