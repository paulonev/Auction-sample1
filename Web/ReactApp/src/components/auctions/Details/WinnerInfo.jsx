/* eslint-disable react/prop-types */
import React, { useEffect, useState } from 'react';
import bidServices from '../../../services/bidServices';

export const WinnerInfo = ({ query }) => {
  const [data, setData] = useState();

  useEffect(() => {
    bidServices.getHighestBid(query).then((response) => setData(response.data.bid));
  }, [query]);

  return (
    <div className="d-flex justify-content-center align-items-center" style={{flexDirection: "column"}}>
      <h3>Winner</h3>
      {data &&
        <>
          <p>{data.traderName}</p>
          <p>{data.amount}$</p>
        </>
      }
    </div>
  )
}