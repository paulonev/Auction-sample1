/* eslint-disable react/prop-types */
import React, { useEffect, useState } from 'react';
import { Form, Row } from 'react-bootstrap';

export const SlotCard = ({ slotData, setSlots, idx }) => {
    useEffect(() => {
        setSlots((prev) => ({ ...prev, [slotData.id]: false }));
    }, []);

    return (
        <>
            <p className="slot-title">{slotData.title}</p>
            <h2 className="slot-price">{slotData.startPrice}</h2>
            <Form.Group controlId="slotRadios">
                <Form.Check type="radio" label="Add" name={`formCheck${idx}`} id={`formCheckAdd${idx}`} onChange={() => { setSlots((prev) => ({ ...prev, [slotData.id]: true })) }} />
                {'   '}
                <Form.Check type="radio" label="Skip" name={`formCheck${idx}`} id={`formCheckSkip${idx}`} onChange={() => { setSlots((prev) => ({ ...prev, [slotData.id]: false })) }} />
            </Form.Group>
        </>
    );
}