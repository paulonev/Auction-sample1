import { React } from "react";
import { Card, Badge, Button } from "react-bootstrap";

function AuctionItem({item}) {
  return (
    <div className="card">
      <Card.Header className="card__header">Auction #{item.id}
        <h4><Badge variant="info" style={{color: "orange"}}>New</Badge></h4>
      </Card.Header>
      <Card.Body className="card__item">
        <Card.Title>{item.title}</Card.Title>
        <Card.Text>{item.description}</Card.Text>
      </Card.Body>
      <div className="card__item card__footer" style={{display: 'flex', flexDirection: 'row'}}>
        <p>StartPrice: <strong className="card__start-price">{item.startPrice}</strong></p>
        <Button className="card__btn" variant="primary">View</Button>
      </div>
    </div>
  );
}

export default AuctionItem;