import React, { useState } from "react";
// import Calendar from 'react-calendar';
import { Button, Form, Segment } from 'semantic-ui-react'
import 'react-calendar/dist/Calendar.css';
import '../../Styles/Calendar.css';

function ActivityFormController (editId) {
  const [inputs, setInputs] = useState({
    id: editId || "",
    name:"",
    startDate:"",
    endDate:"",
    description:"",
    location:""
  });

  function handleInputChange(event){
    const {name, value} = event.target;
    setInputs({...inputs, [name]: value});
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
  }
  
  return (
    <Segment>
      <Form autocomplete="off">
        <Form.Input placeholder="Name" value={inputs.name} name="name" onChange={handleInputChange}/>
        <Form.TextArea placeholder='Description' value={inputs.description } name='description' onChange={handleInputChange}/>
        <Form.Input type='date' placeholder='Start date' value={inputs.date } name='startDate' onChange={handleInputChange}/>
        <Form.Input type='date' placeholder='End date' value={inputs.date } name='endDate' onChange={handleInputChange}/>
        <Form.Input placeholder='Location' value={inputs.location } name='location' onChange={handleInputChange}/>
        <Button onClick={handleSubmit} floated="right" positive type='button' content='Submit'/>
      </Form>
    </Segment>
  );
}

export default ActivityFormController