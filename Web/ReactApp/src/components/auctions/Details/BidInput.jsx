/* eslint-disable react/prop-types */
import { React } from 'react';
import { Form } from 'react-bootstrap';

export const BidInput = ({ handleSubmit, latestBid}) => {
  return (
    <Form onSubmit={handleSubmit} style={{display: "flex", justifyContent: "space-between"}}>
      <Form.Control
        name="amount"
        type="number"
        placeholder={latestBid}
        style={{width: "120px"}}
      />
      <Form.Control
        variant="primary"
        type="submit"
        value="Place your bid"
        style={{ cursor: "pointer" }}
      />
    </Form>
  );
}