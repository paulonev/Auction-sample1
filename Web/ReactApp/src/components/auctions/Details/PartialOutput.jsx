/* eslint-disable react/prop-types */
import React from 'react';
import { Badge } from 'react-bootstrap';

// display only {count} from bottom of list as div
export const PartialOutput = ({ list, count, display }) => {
  const skip = list.length < count ? 0 : list.length - count;
  const targetElements = list.slice(skip);
  
  return (
    <div className="list">
      {targetElements.map((bid, idx) => {
        return (
          <div key={idx} className="auction-details__bid">
            <span className="trader-name">{bid.traderName} {' '}
            {idx % 2 === 0 ? (
                <Badge bg="secondary" className="message" pill>
                  have bidded {bid.amount}$
                </Badge>
              ) : (
                  <Badge bg="primary" className="message" pill>
                    have bidded {bid.amount}$
                  </Badge>
                )}
            </span>
          </div>
        );
      })}
    </div>
  );
}