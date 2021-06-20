/* eslint-disable react/display-name */
/* eslint-disable react/prop-types */
import React, { useEffect, forwardRef } from 'react';
import { Button, Dropdown, DropdownButton, NavItem } from 'react-bootstrap';
import { Link, useHistory } from 'react-router-dom';

const CustomToggle = forwardRef(({ children, onClick }, ref) => (
  <a
    href=""
    ref={ref}
    onClick={(e) => {
      e.preventDefault();
      onClick(e);
    }}
  >
    {children}
    &#x25bc;
  </a>
));

export const AccountControls = ({
  disabled = false, //to remove
  isAuthenticated,
  logout
}) => {

  const history = useHistory();
  // pass user id to make request for his name and display dropdown with userName being a header
  const userName = "Marcus";

  return (
    <>
      {!disabled ? (
        <>
          {isAuthenticated && (
            <Dropdown alignRight="true">
              <Dropdown.Toggle as={CustomToggle} id="dropdown-custom-components">
                {userName}
              </Dropdown.Toggle>
              <Dropdown.Menu>
                <Dropdown.Item eventKey="1" onClick={() => {history.push("auction/create")}}>Create new auction</Dropdown.Item>
                <Dropdown.Divider />
                <Dropdown.Item eventKey="2" as="button" onClick={() => { logout(); history.push("/") }}>Log out</Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
          )}
          {!isAuthenticated && (
            <>
              <NavItem>
                <Link to={"/login"}>
                  <Button variant="primary">Log In</Button>
                </Link>
              </NavItem>
              <NavItem>
                <Link to={"/register"}>
                  <Button variant="primary">Sign Up</Button>
                </Link>
              </NavItem>
            </>
          )}
        </>
      ) : ("")
      }
    </>
  )
}