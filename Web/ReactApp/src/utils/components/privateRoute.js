/* eslint-disable react/prop-types */
import React, {Component} from 'react';
import { Route, useHistory, useLocation } from 'react-router-dom';
import { useAuth0 } from '../../react-hooks/useAuth0';

export const PrivateRoute = ({
    component: Component,
    authTrusted=false,
    ...rest
}) => {
    const { user } = useAuth0();
    const history = useHistory();
    
    return (
        <Route
          {...rest}
          render={(props) => {
              if(user) {
                return <Component {...props} />
              } else {
                  return (
                      <>
                        {history.push("/login")}
                      </>
                  );
              }
          }}
        />
    );
};