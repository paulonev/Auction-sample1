import { useEffect, useState, useCallback } from "react";
import auctionServices from "../services/auctionServices";

const useItemsSearch = (query, pageNumber, setPageNumber) => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const [items, setItems] = useState([]);
  const [responseItemsCount, setResponseItemsCount] = useState(0);
  const [hasMore, setHasMore] = useState(false);

  useEffect(() => {
    setItems([]);
    setPageNumber(1);
  }, [query, setPageNumber]);

  //memoized callback
  const makeRequest = useCallback(() => {
    setLoading(true);
    setError(false);

    query.pageNumber = pageNumber;
    query.pageSize = 15;
    // response = {({})...response, auctions([]): response.data.auctions, data({}): response.data}
    auctionServices
      .getAuctions(query)
      .then((response) => {
        setItems((prevItems) => {
          console.log(response.auctions);
          return [...prevItems, ...response.auctions];
        });
        setResponseItemsCount(response.data.responseItemsCount);
        setHasMore(
          response.data.responseItemsCount > response.data.pageSize &&
            response.auctions.length > 0
        );
        setLoading(false);
      })
      .catch(() => setError(true));
  }, [query, pageNumber]);

  return {
    makeRequest,
    loading,
    error,
    items,
    responseItemsCount,
    hasMore,
  };
};

export default useItemsSearch;