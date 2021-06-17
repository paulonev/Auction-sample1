import React, {useState, useEffect} from "react";
import auctionServices from "../services/auctionServices";

export const useAuctionSearch = (auctionId) => {
    const [state, setState] = useState({data: null, loading: true});
    const [error, setError] = useState(false);

    useEffect(() => {
        setState(state => ({data: state.data, loading: true}));
        auctionServices
            .getAuctionById(auctionId)
            .then((response) => {
                setState({data: response.data.auction, loading: false});
            })
            .catch(() => setError(true));
    }, [auctionId, setState]);
        
    return state;
}