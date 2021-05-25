function getAllAuctions() {
  //use api
  const params = {
    pageSize: 15, //change for constant
    urlEndpoint: "/auctions"
  }
  const defaultPath = "https://localhost:5001";
  const api = {axios: true, get: (path, body) => {}, post: (path, body) => {}};

  let prom = new Promise(api.get(defaultPath + params.urlEndpoint, {params}));
  return prom.then((response) => {
    return {
      response,
      data: []
    }
  });
}

function getAuctionById(id) {
  //use api
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

export {
  getAllAuctions
};