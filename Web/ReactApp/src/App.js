import React, { useState } from "react";
import { Fragment } from "react";
import { Route, Switch } from "react-router-dom";
import { Catalog } from "./components/auctions/Catalog/Catalog";
import { AppNavigation } from "./components/navigation/AppNavigation";
import { AuctionDetails } from "./components/auctions/Details/AuctionDetails"
import { Login } from "./components/account/Login";
import { Signup } from "./components/account/Signup";

function App() {

  return (
    <Fragment>
      <AppNavigation />
      <Switch>
        <Route exact path={["/", "/home"]} component={Catalog} />
        <Route exact path={"/auction/:categoryId?"} component={Catalog} />
        <Route path={"/auction/details/:auctionId"} component={AuctionDetails} />
        <Route path={"/login"}>
          <Login />
        </Route>
        <Route path={"/register"}>
          <Signup />
        </Route>
      </Switch>
    </Fragment>
  );
}

export default App;