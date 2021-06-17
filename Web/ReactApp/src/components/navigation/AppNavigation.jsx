/* eslint-disable react/prop-types */
// import _ from "./AppNavigation"; add css somehow
import React, { useState } from "react";
import { Container, Button, Nav, Navbar, NavDropdown, NavItem } from "react-bootstrap";
import { useAuth0 } from "../../react-hooks/useAuth0";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link } from "react-router-dom";
import "./AppNavigation.css";
import { AccountControls } from "./AccountControls";

export const AppNavigation = ({ accountControlDisabled }) => {
  const { isAuthenticated, logout } = useAuth0();

  // https://bootstrap-4.ru/articles/cheatsheet/#nav__justify-content
  return (
    <Navbar expand="md" className="header">
      <Navbar.Brand href="/" className="header__logo">
        <span id="logo_prefix">AU</span>Soft
      </Navbar.Brand>
      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav" style={{justifyContent: "space-between"}}> 
          <div className="nav__links">
            <Link to="/auction" className="header__link">Auctions</Link>
            <Link to="/idea" className="header__link">About</Link>
            <Link to="/support" className="header__link">Support</Link>
          </div>
          <div className="nav__controls">
            <AccountControls isAuthenticated={isAuthenticated} logout={logout}/>
          </div>
      </Navbar.Collapse>
    </Navbar>
  );
}