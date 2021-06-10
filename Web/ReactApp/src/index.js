import "./utils/wdyr/wdyr";

import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter as Router } from "react-router-dom";
import { createBrowserHistory } from "history";
import "bootstrap/dist/css/bootstrap.min.css";
import App from "./App";
import "./index.css";
import { AuthProvider } from "./react-hooks/useAuth0";

// import * as auth0SettingsApi from "./utils/auth0SettingsApi";
// import { Auth0Provider } from "@auth0/auth0-react";

// export const history = createBrowserHistory();

ReactDOM.render(
  <AuthProvider>
    <Router>
      <App />
    </Router>
  </AuthProvider>,
  document.getElementById("content")
);

// const onRedirectCallback = (appState) => {
//   history.push(
//     appState && appState.targetUrl
//       ? appState.targetUrl
//       : window.location.pathname
//   );
// };
//
// auth0SettingsApi
//   .getAuthSettings()
//   .then((response) => {
//     ReactDOM.render(
//       <Auth0Provider
//         // onRedirectCallback={onRedirectCallback}
//         domain={response.data.domain}
//         clientId={response.data.clientId}
//         // audience={response.data.audience}
//         redirectUri={window.location.origin}
//       >
//         <Router history={history}>
//           <App />
//         </Router>
//       </Auth0Provider>,
//       document.getElementById("content")
//     );
//   })
//   .catch((err) => {
//     console.error("App setup error", err);
//   });