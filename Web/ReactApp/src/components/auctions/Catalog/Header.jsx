/* eslint-disable react/prop-types */
import React from "react";
import "./Header.css";
import { Dropdown } from "react-bootstrap";

export const Header = ({ totalItemsCount }) => {
  return (
    <div className="generic-header">
      <div className="auctions-header">
        <h4>All Auctions</h4>
        <p>Auctions count: {totalItemsCount}</p>
      </div>
    </div>
  );
};