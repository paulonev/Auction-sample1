/* eslint-disable react/prop-types */

// This auth lib is useful in the following scenarios
// 1. provide auth api such as sign-in, sign-up, changePassword
// 2. keep track of authorized users
// 3. 
import React, { createContext, useEffect, useState, useContext } from "react";
import { history } from "..";
// import createAuth0Client from "@auth0/auth0-spa-js";
import api, { setInterceptor } from "../utils/apiLib";
import { addUserToLocalStorage, removeUserFromLocalStorage, getUserFromLocalStorage } from "../utils/localStorage";

const Auth0Context = createContext();

// Hook for child components to get the auth object 
// and re-render when it changes.
export const useAuth0 = () => useContext(Auth0Context);

const DEFAULT_REDIRECT_CALLBACK = () => {
  window.history.replaceState({}, document.title, window.location.pathname);
}

// Provider component that wraps your app and makes auth object ...
// ... available to any child component that calls useAuth().

// Auth0Provider wrapper from Auth0 lib
// export const Auth0Provider = ({ 
//   children, 
//   onRedirectCallback = DEFAULT_REDIRECT_CALLBACK, 
//   ...initOptions 
// }) => {
//   const [isAuthenticated, setIsAuthenticated] = useState();
//   const [user, setUser] = useState();
//   const [auth0Client, setAuth0Client] = useState();
//   const [loading, setLoading] = useState(true);
//   // const [popupOpen, setPopupOpen] = useState(false);
//   const [error, setError] = useState();
//
//   useEffect(() => {
//     console.log(initOptions);
//     const initAuth0 = async () => {
//       const auth0HookClient = await createAuth0Client(initOptions);
//       setAuth0Client(auth0HookClient);
//
//       if (window.location.search.includes("code=")) {
//         const { appState } = await auth0HookClient.handleRedirectCallback();
//         onRedirectCallback(appState);
//       }
//
//       const isAuthenticated = await auth0HookClient.isAuthenticated();
//
//       setIsAuthenticated(isAuthenticated);
//
//       if (isAuthenticated) {
//         const user = await auth0HookClient.getUser();
//         setUser(user);
//       }
//
//       setLoading(false);
//     };
//
//     initAuth0();
//     // eslint-disable-next-line
//   }, []);
//
//   const loginWithRedirect = async (params = {}) => {
//     try {
//       await auth0Client.loginWithRedirect(params);
//     } catch (error) {
//       setError(error);
//     }
//
//     const user = await auth0Client.getUser(user);
//     setUser(user);
//     setIsAuthenticated(true);
//   }
//
//   const handleRedirectCallback = async () => {
//     setLoading(true);
//     try {
//       await auth0Client.handleRedirectCallback();
//     } catch (error) {
//       setError(error);
//     }
//     const user = await auth0Client.getUser();
//     setLoading(false);
//     setIsAuthenticated(true);
//     setUser(user);
//   };
//
//   return (
//     <Auth0Context.Provider
//       value={{
//         isAuthenticated,
//         user,
//         loading,
//         loginWithRedirect,
//         handleRedirectCallback,
//         getIdTokenClaims: (...p) => auth0Client.getIdTokenClaims(...p),
//         loginWithRedirect: (...p) => auth0Client.loginWithRedirect(...p),
//         getTokenSilently: (...p) => auth0Client.getTokenSilently(...p),
//         getTokenWithPopup: (...p) => auth0Client.getTokenWithPopup(...p),
//         logout: (...p) => auth0Client.logout(...p)
//       }}
//     >
//       {children}
//     </Auth0Context.Provider>
//   );
// }

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
          setUser(userData); //id, isAdmin
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