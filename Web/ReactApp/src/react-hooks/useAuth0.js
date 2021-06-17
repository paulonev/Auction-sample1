/* eslint-disable react/prop-types */

import React, { createContext, useEffect, useState, useContext } from "react";
import { history } from "..";
import api, { setInterceptor } from "../utils/apiLib";
import { addUserToLocalStorage, removeUserFromLocalStorage, getUserFromLocalStorage } from "../utils/localStorage";

const Auth0Context = createContext();

export const useAuth0 = () => useContext(Auth0Context);

const DEFAULT_REDIRECT_CALLBACK = () => {
  window.history.replaceState({}, document.title, window.location.pathname);
}

export const AuthProvider = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState();

  useEffect(() => {
    setInterceptor(setError);
  }, []);

  useEffect(() => {
    if (user === null) {
      setUser(getUserFromLocalStorage());
    }
  }, [user, isAuthenticated, error]);

  // creds: (email, pass)
  const loginWithRedirect = (creds) => {
    return api
      .post("/auth/login", creds)
      .then((response) => {
        if (response && response.status === 200) {
          const userData = addUserToLocalStorage(response.data.token);
          setUser(userData);
          setIsAuthenticated(true);
        }
        return response.data;
      })
      .finally(() => setLoading(false));
  }

  const logout = () => {
    api.post("/auth/logout", {}).then(() => {
      removeUserFromLocalStorage();
      setUser(null);
      setIsAuthenticated(false);
    })
  }

  const signup = (creds) => {
    return api
      .post("/auth/register", creds)
      .then((response) => response);
  }

  return (
    <Auth0Context.Provider value={{
      isAuthenticated,
      user,
      loading,
      error,
      loginWithRedirect,
      logout,
      signup
    }}>
      {children}
    </Auth0Context.Provider>
  )
}