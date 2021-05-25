import { Component, Fragment, Container } from "react";
import { Router, Route, Switch } from "react-router-dom";
import { Catalog } from "./components/auctions/Catalog";
import { AppNavigation } from "./components/nav/AppNavigation";
import { ProvideAuth } from "./utils/authLib";

function App() {
  return (
    <Fragment>
      {/* <ProvideAuth> */}
      <AppNavigation />
      <Switch>
        <Route exact path={["/", "/auctions"]} component={Catalog} />
      </Switch>
      {/* </ProvideAuth> */}
    </Fragment>
  );
}

export default App;