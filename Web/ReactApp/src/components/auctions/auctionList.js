import { React } from "react";
import AuctionItem from "./auctionItem";
import { Card, CardDeck } from "react-bootstrap";

export const AuctionList = () => {
  const data = [
    { id: 1, title: "Mr Jack's bowl", description: "A ball and a rocket", startPrice: 100 },
    { id: 2, title: "Mr Bob's party suit", description: "A ball and a rocket", startPrice: 200 },
    { id: 3, title: "Mr Phil's arts", description: "A ball and a rocket", startPrice: 300 },
    { id: 4, title: "Mr Paul's PC", description: "A ball and a rocket", startPrice: 400 },
  ];

  return (
    <CardDeck className="cards-wrapper">
      {data.map((item, idx) =>
        <Card bg="light" key={idx + 1} className="card-wrapper">
          <AuctionItem item={item} />
        </Card>)}
    </CardDeck>
  );
}