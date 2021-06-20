/* eslint-disable react/prop-types */
import React, { useEffect, useState } from 'react';
import { Container, Form, Row } from 'react-bootstrap';
import { SlotCard } from './SlotCard';
import "./SlotPicker.css";

export const SlotPicker = ({ userId, setSlotsAdded }) => {
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
    const [disabled, setDisabled] = useState(true);

    let uploadedSlots = [
        { id: "1", title: "S1", startPrice: 200 },
        { id: "2", title: "S2", startPrice: 220 },
        { id: "3", title: "S3", startPrice: 240 },
        { id: "4", title: "S4", startPrice: 260 }
    ];

    useEffect(() => {
        try {
            setError(null);
            // uploadedSlots = await slotServices.getTraderSlots(userId); 
            setLoading(false);
        } catch {
            setError("exception uploading slots");
            setLoading(false);
        }
    }, [userId]);

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
                    {/* </div>
                    {!disabled ? ( */}
                        <Row>
                            {uploadedSlots && uploadedSlots.map((slot, slotIdx) => {
                                return (
                                    <div key={slotIdx} className="col-3">
                                        <SlotCard slotData={slot} idx={slotIdx} setSlots={setSlotsAdded} />
                                    </div>
                                );
                            })}
                        </Row>
                    {/* ) : ("")} */}
                </Form.Group>
            ) : ("")}
        </>
    );
}