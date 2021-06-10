import React from 'react';
import api from "./apiLib";

export async function getAuthSettings() {
  try {
    return await api
      .get('/settings', { headers: { Accept: "application/json" }})
      .then((response) => response);
  } catch (error) {
    console.error("Auth settings api call error", error);
  }
}