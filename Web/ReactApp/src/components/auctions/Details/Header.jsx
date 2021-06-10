/* eslint-disable react/display-name */
/* eslint-disable react/prop-types */
import React, { useState, useEffect } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAngleLeft, faAngleRight } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";

export const Header = ({
  auction,
  setIsFinished
  // userInfo,
}) => {
  const { timeDiff, hasFinished, hasStarted } = useTime(auction.startedOn, auction.endedOn);

  const userInfo = { id: 1, name: "Demouser" };

  useEffect(() => {
    setIsFinished(hasFinished);
  }, [hasFinished, setIsFinished]);

  return (
    <>
      {auction.title !== undefined ? (
        <div>
          <h2>
            <FontAwesomeIcon icon={faAngleLeft} />{auction.title}<FontAwesomeIcon icon={faAngleRight} />
          </h2>
          <div style={{ marginTop: '-15px' }}>
            by <Link to={`/profiles/${userInfo.id}`}>{userInfo.name}</Link>
          </div>
          <div>
            {hasFinished ? <p>Finished</p> :
              (
                hasStarted ? <p>Finishes in {timeDiff}</p> : <p>Starts in {timeDiff}</p>
              )}
          </div>
        </div>
      ) : (
          ""
        )}
    </>
  );
}

function useTime(startedOn, endedOn) {
  const [timeDiff, setTimeDiff] = useState("");
  const [hasFinished, setHasFinished] = useState(false);
  const [hasStarted, setHasStarted] = useState(false);

  const humanFriendlyDate = (date) => {
    const MILLISECONDS_TO_SECONDS = 0.001;
    const SECONDS_IN_YEAR = 31557600;
    const SECONDS_IN_MONTH = 2629800;
    const SECONDS_IN_DAY = 86400;
    const SECONDS_IN_HOUR = 3600;
    const SECONDS_IN_MINUTE = 60;

    function millisecondsToSeconds(milliseconds) {
      return Math.floor(milliseconds * MILLISECONDS_TO_SECONDS);
    }

    /**
     * Break up a unix timestamp into its date & time components
     */
    function getDateTimeComponents(timestamp) {
      const components = {
        years: 0,
        months: 0,
        days: 0,
        hours: 0,
        minutes: 0,
        seconds: 0,
      };

      let remaining = timestamp;

      // years
      components.years = Math.floor(remaining / SECONDS_IN_YEAR);

      remaining -= components.years * SECONDS_IN_YEAR;

      // months
      components.months = Math.floor(remaining / SECONDS_IN_MONTH);

      remaining -= components.months * SECONDS_IN_MONTH;

      // days
      components.days = Math.floor(remaining / SECONDS_IN_DAY);

      remaining -= components.days * SECONDS_IN_DAY;

      // hours
      components.hours = Math.floor(remaining / SECONDS_IN_HOUR);

      remaining -= components.hours * SECONDS_IN_HOUR;

      // minutes
      components.minutes = Math.floor(remaining / SECONDS_IN_MINUTE);

      remaining -= components.minutes * SECONDS_IN_MINUTE;

      // seconds
      components.seconds = remaining;

      return components;
    }

    const unixTimeStamp = millisecondsToSeconds(date.valueOf());
    const now = millisecondsToSeconds(Date.now().valueOf());

    const { years, months, days, hours, minutes, seconds } =
      getDateTimeComponents(now - unixTimeStamp);

    let output = "";

    if (years > 0) {
      output += ` ${years} years`
    }
    if (months > 0) {
      output += ` ${months} months`
    }
    if (days > 0) {
      output += ` ${days} days`
    }
    if (hours > 0) {
      output += ` ${hours} hours`
    }
    if (minutes > 0) {
      output += ` ${minutes} minutes`
    }
    if (seconds > 0) {
      output += ` ${seconds} seconds`
    }

    return output;
  }

  useEffect(() => {
    const startDate = new Date(startedOn);
    const endDate = new Date(endedOn);
    if (startDate - Date.now() > 0) {
      // way before auction start
      setTimeDiff(humanFriendlyDate(startDate));
    }
    else if (endDate - Date.now() < 0) {
      // way after auction finish
      setHasFinished(true);
      setTimeDiff(humanFriendlyDate(endDate));
    }
    else {
      // just in auction time
      setTimeDiff(humanFriendlyDate(endDate));
      setHasStarted(true);
    }
  }, [startedOn, endedOn]);

  return {
    timeDiff,
    hasFinished,
    hasStarted
  }
}