/* eslint-disable react/prop-types */
import React, { useEffect, useState, useRef } from 'react';
import { Container, Form, Row } from 'react-bootstrap';
import { SlotCard } from './SlotCard';
import "./SlotPicker.css";

export const SlotPicker = ({ userId, setSlotsAdded, slotsLoading, setSlotsLoading }) => {
    const [error, setError] = useState(null);
    const [disabled, setDisabled] = useState(true);

    const uploadedSlotsRef = useRef([]);

    useEffect(() => {
        async () => {
            setSlotsLoading(true);
            try {
                setError(null);
                uploadedSlotsRef.current = await slotServices.getTraderSlots(userId);
                setSlotsLoading(false);
            } catch {
                setError("exception uploading slots");
                setSlotsLoading(false);
            }
        }
    }, [userId, setSlotsLoading]); //??

    return (
        <>
            {!error ? (
                <Form.Group controlId="slotpicker" className="form-input--wide">
                    {/* <div className="slotpicker__controls">
                        <div className="slotpicker__text"> */}
                    <Form.Label>Slots</Form.Label>
                    <Form.Text>Select `Add` to add or `Skip` to not to add slot to the auction</Form.Text>
                    {/* </div> */}
                    {/* <button className="button-group_button button-view-slots" onClick={() => {setDisabled((prev) => !prev)}}>View them</button> */}
                    {/* { </div> */}
                    {!slotsLoading ? (
                    <Row>
                        {uploadedSlotsRef.current && uploadedSlotsRef.current.map((slot, slotIdx) => {
                            return (
                                <div key={slotIdx} className="col-3">
                                    <SlotCard slotData={slot} idx={slotIdx} setSlots={setSlotsAdded} />
                                </div>
                            );
                        })}
                    </Row>
                    ) : ("...Loading")}
                </Form.Group>
            ) : ("")}
        </>
    );
}