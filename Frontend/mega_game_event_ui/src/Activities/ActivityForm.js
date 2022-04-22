import React, { useState } from "react";
import { Form, Input } from 'semantic-ui-react';
import Calendar from 'react-calendar'
import 'react-calendar/dist/Calendar.css';

const ActivityFormController = () => {
  const [value, onChange] = useState(new Date());

  return (
    <Form>
      <Form.Field
        id="name"
        control={Input}
        label="Activity Name"
        placeholder="Watch the skies"
      />
      <Form.Field
        id="description"
        control={Input}
        label="Description"
        placeholder="Once upon a time..."
      />
      <Calendar onChange={onChange} value={value}/>
    </Form>
  );
}

export default ActivityFormController