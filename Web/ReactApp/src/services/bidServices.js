import api from "../utils/apiLib";

// request - bid data for bid creation
// response - created bid
const createBid = (query) => {
  console.log("Create bid query", query);
  return api
    .post("/bid", query)
    .then((response) => {
      return {
        ...response,
        data: {
          ...response.data
        }
      };
    });
}

// response - all bids of that slot
const getAllSlotBids = (query) => {
  console.log("Get slot bids query", query);
  return api
    .get("/bid", { params: {slotId: query} })
    .then((response) => {
      return {
        ...response,
        data: {
          ...response.data,
        }
      };
    });
}

const getHighestBid = (query) => {
  console.log("Get highest bid query", query);
  return api
    .get("/bid/latestBid", { params: {slotId: query} })
    .then((response) => {
      return {
        ...response,
        data: {
          ...response.data, //bid {id: aslkj3i4904, amount: 444, traderName: "test@test1.com"}
        }
      };
    });
}

export default {
  createBid,
  getAllSlotBids,
  getHighestBid
}