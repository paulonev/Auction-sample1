import { useEffect, useState } from "react";
import { AuctionList } from "./auctionList";

export const Auctions = () => {
  const [auctions, setAuctions] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    
  });

  const getActiveAuctions = () => {
    // make a generic interface with crud operations over auctions, slots, blogs and so on
    auctionService.getActiveAuctions().then((response) => {
      setItems(response.data.data);
      setTotalItems(response.data.totalDataCount);
      setIsLoading(false);
    });
  }

  return (
    <div className="catalog">
      <AuctionList />
    </div>
  );
}