/* eslint-disable react/prop-types */
import React, { useState, useEffect } from "react";
import { Card, Form, Container, Row, Col } from "react-bootstrap";
import { Link, Redirect, useHistory } from "react-router-dom";
import { useAuth0 } from "../../../react-hooks/useAuth0";
import bidServices from "../../../services/bidServices";
import { BidBasket } from "./BidBasket";
import { BidInput } from "./BidInput";
import "./SlotList.css";
import { WinnerInfo } from "./WinnerInfo";

const SlotList = ({ auction, auctionId, auctionFinished }) => {
  const [items, setItems] = useState(auction.auctionSlotDtoItems);

  useEffect(() => {
    setItems(auction.auctionSlotDtoItems);
    return () => { }
  }, [auction]);

  return (
    <>
      {!items && items.length === 0 ? (<h2>Auction has no items</h2>) : (
        <>
          <Row>
            {items.map((item, idx) => {
              return (
                <>
                  <Col sm={4}>
                    <SlotCard key={idx} auctionId={auctionId} slot={item} auctionFinished={auctionFinished} />
                  </Col>
                </>
              )
            }
            )}
          </Row>
        </>
      )}
    </>
  )
}

const SlotCard = ({ auctionId, slot, auctionFinished }) => {
  const pictureDefaultURL = "https://thumbnail.xero.porn/thumbnail/noimage.png?123";

  const [bidBasketDisabled, setBidBasketDisabled] = useState(true);
  const [highestBidAmount, setHighestBidAmount] = useState(0);
  const [state, setState] = useState({ bids: [], loading: true });

  const { user } = useAuth0();
  const history = useHistory();

  // while slot.id isn't changed by any child component, this useEffect will be called once, after first render
  useEffect(() => {
    bidServices
      .getAllSlotBids(slot.id)
      .then((response) => {
        setState(({
          bids: response.data.bids,
          loading: false
        }));
      });
  }, [slot.id]);

  useEffect(() => {
    if (!state.loading) {
      setHighestBidAmount(state.bids[state.bids.length - 1].amount);
    }
  }, [state, slot.id]);

  const handleFormSubmit = (e) => {
    e.preventDefault();
    if (!user) {
      //redirect to login page
      history.push("/login", { from: `/auction/details/${auctionId}` });
      return;
      // return <Redirect to={{
      //   pathname: "/login",
      //   state: { from: `/auction/details/${auctionId}` }
      // }} />
    }
    const formData = new FormData(e.target);
    if (highestBidAmount < +formData.get('amount')) {
      formData.append('traderId', user.id);
      formData.append('slotId', slot.id);
      formData.append('date', new Date().toISOString("dd/mm/yyyy HH:mm"));

      bidServices
        .createBid(formData)
        .then((response) => {
          setState(({
            bids: response.data.bids,
            loading: false
          }));

          setBidBasketDisabled(false);
        });
    }
  }

  return (
    <div className="d-flex justify-content-center auction-details__slot">
      <Card className="w-100">
        <Card.Img variant="top" src={pictureDefaultURL} style={{ width: "150px" }} />
        <Card.Body>
          <Card.Title style={{ textAlign: 'center' }}>
            <Link to={`slot/${slot.id}`}>
              {slot.title}
            </Link>
          </Card.Title>
          {!auctionFinished ? (
            <>
              <Card.Text>
                Latest bid: {highestBidAmount}$
              </Card.Text>
              <BidInput
                latestBid={highestBidAmount}
                handleSubmit={handleFormSubmit} />
            </>
          ) : (
              <WinnerInfo query={slot.id} />
            )}
        </Card.Body>
        <div className="auction-details__bids">
          <BidBasket
            as="div"
            loading={state.loading}
            bids={state.bids}
            disabled={bidBasketDisabled}
          />
        </div>
      </Card>
    </div>
  );

}

// SlotCard.whyDidYouRender = true;

export default SlotList;