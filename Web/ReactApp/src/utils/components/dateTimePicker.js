/* eslint-disable react/prop-types */
import React, {useState, Fragment, useEffect} from "react";
import DatePicker from "react-datepicker";
import moment from "moment";
import "react-datepicker/dist/react-datepicker.css";
import { Form } from "react-bootstrap";

export const StartDateTimePicker = ({
    startTime,
    setStartTime,
    endTime,
    disabled,
    errorHideTimeOut=5000
}) => {
    const startTimeConf = createDatePickerConf(
        startTime,
        10,
        moment().endOf("day")
    );
    const [error, setError] = useState();

    useEffect(() => {
        const interval = setTimeout(() => setError(null), errorHideTimeOut);
        return () => {
            clearTimeout(interval);
        };
    }, [error, errorHideTimeOut]);

    const handleOnChange = (date) => {
        if (date >= endTime) {
            setError("Start time cannot be after end time.");
            return;
        }

        setError(null);
        setStartTime(date);
    };

    return (
        <Fragment>
            <DatePicker
                className="form-control custom-select"
                disabled={disabled}
                selected={startTime}
                onChange={(date) => handleOnChange(date)}
                filterDate={(date) => {
                    return date < endTime;
                }}
                maxDate={endTime}
                showTimeSelect
                timeFormat="HH:mm"
                timeCaption="Time"
                timeInvervals={10}
                dateFormat="MMMM d, yyyy h:mm aa"
            />
            {error && (
                <Form.Control.Feedback type="invalid">{error}</Form.Control.Feedback>
            )}
        </Fragment>
    );
}

export const EndDateTimePicker = ({
  endTime,
  setEndTime,
  startTime,
  disabled,
  errorHideTimeOut = 5000,
}) => {
    const [error, setError] = useState();
    const endTimeConf = createDatePickerConf(
        endTime, 
        30, 
        moment().endOf("day")
    );

    useEffect(() => {
        const interval = setTimeout(() => setError(null), errorHideTimeOut);
        return () => {
            clearTimeout(interval);
        };
    }, [error, errorHideTimeOut]);

    const handleOnChange = (date) => {
        if (date < moment()) {
            setError("End time cannot be before current time.");
            return;
        }
        if (date <= startTime) {
            setError("End time cannot be before start time.");
            return;
        }

        setEndTime(date);
    };

    return (
        <Fragment>
            <DatePicker
                className="form-control custom-select"
                disabled={disabled}
                selected={endTime}
                onChange={(date) => handleOnChange(date)}
                minDate={startTime}
                showTimeSelect
                timeFormat="HH:mm"
                timeCaption="Time"
                timeInvervals={10}
                dateFormat="MMMM d, yyyy h:mm aa"
                {...endTimeConf}
            />
            {error && (
                <Form.Control.Feedback type="invalid">{error}</Form.Control.Feedback>
            )}
        </Fragment>
    );
};

// play with config
const createDatePickerConf = (date, ceilNumber, endDay) => {
    const conf = {};
    if (moment(date).isSame(moment(), "day")) {
        conf.minTime = Math.ceil(
            moment().hours(moment().hour()).minutes(moment().minute()),
            ceilNumber
        );
        conf.maxTime = Math.ceil(
            moment().hours(endDay.hour()).minutes(endDay.minute()),
            ceilNumber
        );
    }

    return conf;
};