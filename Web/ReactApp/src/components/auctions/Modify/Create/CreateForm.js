import React, { useEffect, useState } from 'react';
import { Col, Container, Form, Row } from 'react-bootstrap';
import './CreateForm.css';
import { useForm } from 'react-hook-form';
import { EndDateTimePicker, StartDateTimePicker } from '../../../../utils/components/dateTimePicker';
import moment from 'moment';
import { useTimer } from '../../../../react-hooks/useTimer';
import { useFormik } from 'formik';
import { useAuth0 } from '../../../../react-hooks/useAuth0';
import { SlotPicker } from './SlotPicker';
import auctionServices from '../../../../services/auctionServices';

export const CreateForm = () => {
    // const { register, handleSubmit, errors } = useForm();
    const [startDateTime, setStartDateTime] = useState();
    const [endDateTime, setEndDateTime] = useState();
    const { time } = useTimer();
    const { user } = useAuth0();
    // storage of slots indexes
    const [slots, setSlots] = useState({}); // {slotId: true/false}
    const [slotsLoading, setSlotsLoading] = useState(true);

    const onSubmit = async (values, e) => {
        // e.preventDefault();
        var formData = new FormData(e.target);
        formData.append("startTime", startDateTime.toISOString("dd/mm/yyyy HH:mm"));
        formData.append("endTime", endDateTime.toISOString("dd/mm/yyyy HH:mm"));
        formData.append("title", values.title);
        formData.append("description", values.desc);

        let slotsSelected = [];
        Object.getOwnPropertyNames(slots).forEach((value) => {
            if(slots[value]) {
                slotsSelected.push(value);
            }
        });
        formData.append("slots", slotsSelected);
        
        await auctionServices.createAuction(formData);
    };

    const formik = useFormik({
        initialValues: {
            title: "",
            desc: ""
        },
        onSubmit,
    });

    useEffect(() => {
        setStartDateTime(moment().add(3, "minutes").toDate());
        setEndDateTime(moment().add(1, "days").add(8, "hours").toDate());
    }, [time])

    return (
        <Container>
            <div className="mx-auto my-4 w-md-75 w-lg-50 create-auction-form-wrapper">
                <Form onSubmit={formik.handleSubmit} className="create-auction-form">
                    <h2>Create auction</h2>
                    <Form.Group controlId="title" className="form-input--wide">
                        <Form.Label>Title</Form.Label>
                        <Form.Control size={"lg"}
                            onChange={formik.handleChange}
                            type="text"
                            placeholder="Give us some title!"
                            value={formik.values.title}
                        />
                    </Form.Group>
                    <Form.Group controlId="desc" className="form-input--wide">
                        <Form.Label>Description</Form.Label>
                        <Form.Control size={"lg"}
                            as="textarea"
                            onChange={formik.handleChange}
                            type="text"
                            placeholder="Give us some description!"
                            value={formik.values.desc}
                        />
                    </Form.Group>
                    <Row className="form-input--wide">
                        {/* date input is inline, but shouldn't */}
                        <Form.Group className="col" controlId="startDateTime">
                            <Form.Label>Starts on</Form.Label>
                            <StartDateTimePicker
                                startTime={startDateTime}
                                setStartTime={setStartDateTime}
                                endTime={endDateTime}
                            />
                        </Form.Group>
                        <Form.Group className="col" controlId="endDateTime">
                            <Form.Label>Ends on</Form.Label>
                            <EndDateTimePicker
                                startTime={startDateTime}
                                setEndTime={setEndDateTime}
                                endTime={endDateTime}
                            />
                        </Form.Group>
                    </Row>

                    <SlotPicker userId={user.id} setSlotsAdded={setSlots} slotsLoading={slotsLoading} setSlotsLoading={setSlotsLoading}/>

                    <Form.Group controlId="formButtons">
                        <button disabled={slotsLoading} className="button-group_button button-submit" type="submit" name="submit">Submit</button>
                        <button className="button-group_button button-reset" type="reset" name="reset">Reset</button>
                        <button className="button-group_button button-cancel" name="submit" onClick={() => { }}>Cancel</button>
                    </Form.Group>
                </Form>
            </div>
        </Container>
    );
}