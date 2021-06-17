import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { Header } from "./Header";
import SlotList from "./SlotList";
import { useAuctionSearch } from "../../../react-hooks/useAuctionSearch";
import { Container, Row } from "react-bootstrap";

export const AuctionDetails = () => {
    // component state and some functions: use hooks
    const { auctionId } = useParams();
    const { data, loading } = useAuctionSearch(auctionId);
    const [isFinished, setIsFinished] = useState(false);

    return (
        <>
            {data && (
                <Container>
                    <Row>
                        <Header auction={data} setIsFinished={setIsFinished}/>
                    </Row>
                    <div className="auction-details__slots">
                        <SlotList auction={data} auctionId={auctionId} auctionFinished={isFinished}/>
                    </div>
                </Container>
            )}
        </>
    );
}