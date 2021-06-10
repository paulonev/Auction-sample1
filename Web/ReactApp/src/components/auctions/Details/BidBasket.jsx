/* eslint-disable react/display-name */
/* eslint-disable react/prop-types */

import React, { useEffect } from "react";
import { Badge, Spinner } from "react-bootstrap";
import { PartialOutput } from "./PartialOutput";

// import "./BidBasket.css";

export const BidBasket = ({ bids, disabled, loading }) => {
  return (
    <>
      {disabled ? ("") : (
        !loading ? (
          <PartialOutput list={bids} count={5}/>
          ) : (
            <div className="ml-5 pt-3 pb-3">
              Loading...
              <Spinner animation="border" />
            </div>
          )
      )}
    </>
  );
}