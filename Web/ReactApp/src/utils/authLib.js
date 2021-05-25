// This auth lib is useful in the following scenarios
// 1. provide auth api such as sign-in, sign-up, changePassword
// 2. keep track of authorized users
// 3. 

import { createContext, useEffect, useState, useContext } from "react";

const authContext = createContext();

// Provider component that wraps your app and makes auth object ...
// ... available to any child component that calls useAuth().
export function ProvideAuth({ children }) {
  const auth = useProvideAuth();
  return (
    <authContext.Provider value={auth}>
      {children}
    </authContext.Provider>
  );
}

// Hook for child components to get the auth object 
// and re-render when it changes.
export const useAuth = () => {
  return useContext(authContext);
}

// Provider hook that creates auth object and handles state
function useProvideAuth() {
  const [user, setUser] = useState(null);
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  useEffect(() => {
    if (user === null) {
      setUser(/*a function that finds user*/);
      setIsLoggedIn(true);
    }
  }, [user, isLoggedIn]);

  const signIn = () => {
    //perform actions with localStorage api
  }

  const signOut = () => {

  }

  const signUp = () => {

  }

  return {
    user,
    isLoggedIn,
    signIn,
    signOut,
    signUp,
    // changePass
  }
}
