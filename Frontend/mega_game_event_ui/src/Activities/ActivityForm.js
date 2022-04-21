import React from "react";
import { Form, Input } from 'semantic-ui-react';

const ActivityFormController = () => {
  return (
    <Form>
      <Form.Group widths="equal">
        <Form.Field
          id="Name"
          control={Input}
          label="Activity Name"
          placeholder="Watch the skies"
        />
      </Form.Group>
    </Form>
  );
}

export default ActivityFormController