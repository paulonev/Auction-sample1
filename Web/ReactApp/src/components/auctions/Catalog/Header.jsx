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
      {/* <div className="generic-header-toolbar" role="toolbar">
        <Dropdown>
          <Dropdown.Toggle
            className="sort-btn"
            variant="light"
            id="dropdown-basic"
          >
            Sort by
          </Dropdown.Toggle>

          <Dropdown.Menu>
            <Dropdown.Item href="#/action-1">TODO</Dropdown.Item>
          </Dropdown.Menu>
        </Dropdown>
      </div> */}
    </div>
  );
};