/* eslint-disable react/prop-types */
import { React } from "react";
import {Card, CardDeck, Row, Spinner} from "react-bootstrap";
import {Col, Button} from "react-bootstrap";
import {Link, useHistory} from "react-router-dom";

export const AuctionList = ({
    loading,
    setPageNumber,
    items,
    error,
}) => {
    const dateParser = (date) => {
        let d = new Date(date);
        const minutes = d.getUTCMinutes() < 10 ? '0'+d.getUTCMinutes() : d.getUTCMinutes();
        return `${d.getUTCDate()}/${d.getUTCMonth()+1} at ${d.getUTCHours()}:${minutes}`;
    }
   
    const history = useHistory();
    
    return (
        <>
            <Row>
            {!loading && items.length === 0 ? 
            (" ") : (
                items.map((item, idx) => {
                    return (
                    <Col key={idx} sm={8}>
                        <Card>
                            <Card.Header>#{item.categoryNames}</Card.Header>
                            <Card.Body>
                                <Card.Title>
                                    <Link to={`auction/details/${item.id}`}>{item.title}</Link>
                                    <span style={{color: "red", fontStyle:"italic"}}>Ends: {dateParser(item.endedOn)}</span>
                                </Card.Title>
                                <div className="auction__slots">
                                    {item.auctionSlotDtoItems.map((i, idx) => {
                                        return (
                                           <div key={idx} className="auction__slot">
                                               <div className="slot__title-desc">
                                                   <p className="slot__title">{i.title}</p>
                                                   <p className="slot__desc">{i.description}</p> 
                                               </div>
                                               <div style={{fontStyle:"italic", fontWeight: "bold"}} className="slot__price">
                                                   {i.startPrice}$
                                               </div>
                                           </div>
                                        )
                                    })}
                                </div>
                                <div className="auction__price">
                                    <p style={{position:"relative", float:"right",fontStyle:"italic", fontWeight: "bold"}}>
                                        {item.auctionSlotDtoItems.reduce((acc, next) => acc + next.startPrice, 0)}
                                        $
                                    </p>
                                </div>
                                <Button onClick={() => history.push(`auction/details/${item.id}`)} variant="primary">View</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                    )
                })                
            )}
            </Row>
            {loading ? (
                <div className="ml-5 pt-3 pb-3">
                    Loading...
                    <Spinner animation="border" />
                </div>
            ) : (
                ""
            )}
            {error ?? "error happened"}
        </>
    )
}