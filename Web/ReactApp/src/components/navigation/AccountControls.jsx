/* eslint-disable react/prop-types */
import React, { useEffect } from 'react';
import { Button, NavItem } from 'react-bootstrap';
import { Link, useHistory } from 'react-router-dom';

export const AccountControls = ({
  disabled = false, //remove later
  isAuthenticated,
  logout
}) => {

  const history = useHistory();
  
  return (
    <>
      {!disabled ? (
        <>
          {isAuthenticated && (
            <NavItem onClick={() => { logout(); history.push("/") }}>
              <Button variant="primary">Log Out</Button>
            </NavItem>
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