import React from 'react';

export const addUserToLocalStorage = (token) => {
    //https://jwt.io/introduction
    const jwtPayload = JSON.parse(atob(token.split(".")[1]));
    const id = jwtPayload.id;
    const isAdmin = jwtPayload.role?.toLowerCase().includes("admin");
    
    const data = {
        id,
        isAdmin: isAdmin ?? false
    };
    
    localStorage.setItem("user", JSON.stringify(data));
    return data;
}

export const removeUserFromLocalStorage = () => {
    localStorage.removeItem("user");
}

export const getUserFromLocalStorage = () => {
    return JSON.parse(localStorage.getItem("user"));
}