import api from "../utils/apiLib";

const getAuctions = (query) => {
  return api
    .get("/auction", { params: query })
    .then((response) => {
      return {
        ...response,
        auctions: response.data.auctions,
        data: response.data
      };
    });
}

const getAuctionById = (id) => {
  console.log("Sending api request");
  return api
    .get(`/auction/${id}`)
    .then((response) => {
      return {
        ...response,
        data: {
          ...response.data,
        }
      };
    });
}

function createAuction(data) {
  //use api
}

function editAuction(data) {
  //use api
}

function deleteAuction(id) {
  //use api
}

function getLatestAuctions(days) {
  //use api
}

function getTopRankedAuctions() {
  //use api
}

export default {
  getAuctions,
  getAuctionById
};