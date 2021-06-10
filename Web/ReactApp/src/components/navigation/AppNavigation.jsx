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
      <Navbar.Collapse id="basic-navbar-nav"> 
        <Nav className="header__nav">
          <div className="nav__links">
            <Link to="/auction" className="header__link">Auctions</Link>
            <Link to="/idea" className="header__link">About</Link>
            <Link to="/support" className="header__link">Support</Link>
          </div>
          <div className="nav__controls">
            <AccountControls isAuthenticated={isAuthenticated} logout={logout}/>
          </div>
        </Nav>
      </Navbar.Collapse>
    </Navbar>
  );
}

    // <Navbar className="header__navigation" expand="sm">
    //   <Container>
    //     <Navbar.Brand href="/" className="header__logo">
    //       <span id="logo-prefix">AU</span>Soft
    //     </Navbar.Brand>
    //     <Navbar.Collapse>
    //       <Nav className="mr-auto header__links">
    //         <Link to="/auction" className="header__link">Auctions</Link>
    //         <Link to="/idea" className="header__link">About</Link>
    //         <Link to="/support" className="header__link">Support</Link>
    //       </Nav>
    //     </Navbar.Collapse>
    //     {appAuth.user ? (
    //       <NavDropdown
    //         title={<FontAwesomeIcon icon={faUser} />}
    //         style={{ textColor: "#fff", fontSize: "1.3rem" }}
    //         id="basic-nav-dropdown"
    //       >
    //         {appAuth.user.isAdmin ? (
    //           <NavDropdown.Item
    //             onClick={() => history.push("/admin")}
    //           >
    //             Admin
    //           </NavDropdown.Item>
    //         ) : (
    //             ""
    //           )}
    //         <NavDropdown.Item>Profile</NavDropdown.Item>
    //         <NavDropdown title="Create" id="basic-nav-dropdown">
    //           <NavDropdown.Item onClick={() => history.push("/auction-create")}><FontAwesomeIcon icon={faUser} /> Auction</NavDropdown.Item>
    //           <NavDropdown.Item onClick={() => history.push("/slot-create")}><FontAwesomeIcon icon={faUser} /> Slot</NavDropdown.Item>
    //         </NavDropdown>
    //         <NavDropdown.Divider />
    //         <NavDropdown.Item onClick={appAuth.signout()}>Log out</NavDropdown.Item>
    //       </NavDropdown>
    //     ) : (
    //         <Nav className="header__account">
    //           <NavItem className="account-link">
    //             <Nav.Link onClick={() => history.push("/sign-in")} className="account-link__login">Log in</Nav.Link>
    //           </NavItem>
    //           <NavItem className="account-link">
    //             <Nav.Link onClick={() => history.push("/sign-up")} className="account-link__register">Sign up</Nav.Link>
    //           </NavItem>
    //         </Nav>
    //       )
    //     }
    //   </Container>
    // </Navbar>