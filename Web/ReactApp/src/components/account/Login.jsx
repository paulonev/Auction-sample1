/* eslint-disable react/prop-types */
import React, { useEffect, useRef, useState } from 'react';
import { Alert, Button, Form, FormGroup, Card, Container } from 'react-bootstrap';
import { Link, useHistory, useLocation } from 'react-router-dom';
import { useAuth0 } from '../../react-hooks/useAuth0';
import { useForm } from 'react-hook-form';

// import { yupResolver } from '@hookform/resolvers/yup';
// import * as yup from "yup";
// import { history } from '../../index';

import { ErrorMessage } from '@hookform/error-message';
import "./Login.css";

export const Login = () => {
    const { loginWithRedirect, error } = useAuth0();

    const { register, handleSubmit, watch, formState: { errors, touchedFields } } = useForm({
        defaultValues: { email: "", password: "" },
        criteriaMode: 'all',
    });

    const [hasValidationErrors, setHasValidationErrors] = useState(false);

    const location = useLocation();
    const history = useHistory();
    const { from } = location.state || { from: { pathname: "/" } };

    async function onSubmit(data, e) {
        e.preventDefault();
        loginWithRedirect(data).then(() => {
            history.replace(from);
        }).catch((error) => {
            console.log("Login", error);
        });

        // .then(() => {
        //redirect user to where it came from
        // if (error) {
        //     return;
        // }
        // history.go(-1);
        // console.log(history.location.state);
        // history.location.state
        //     ? history.replace(history.location.state)
        //     : history.push("/");
        // } catch (error) {
        //     setError(error);
        // }
    }

    useEffect(() => {
        if (errors && errors.length != 0) {
            setHasValidationErrors(true);
        }
    }, [errors])

    return (
        <>
            <Container className="d-flex align-items-center justify-content-center">
                <div className="w-100" style={{ maxWidth: "400px" }}>
                    <Card>
                        <Card.Body>
                            <h2 className="text-center mb-4">Log In</h2>
                            {hasValidationErrors && (
                                <>
                                    <ErrorMessage errors={errors} name="email" render={({ message }) => <Alert variant="danger">{message}</Alert>} />
                                    <ErrorMessage errors={errors} name="password" render={({ message }) => <Alert variant="danger">{message}</Alert>} />
                                </>
                            )}
                            {
                                error && <Alert variant="danger">{error}</Alert>
                            }
                            <form onSubmit={handleSubmit(onSubmit)}>
                                <input
                                    className="w-100 mb-3 login-form__item"
                                    type="email"
                                    {...register("email", {
                                        required: "Email required",
                                        pattern: {
                                            value: /^\S+@\S+$/,
                                            message: "Please enter valid email address",
                                        }
                                    })}
                                    placeholder="Email"
                                />
                                <input
                                    className="w-100 mb-3 login-form__item"
                                    type="password"
                                    {...register("password", {
                                        required: "Password required",
                                        minLength: {
                                            value: 6,
                                            message: "Invalid password. Few characters."
                                        },
                                        maxLength: {
                                            value: 50,
                                            message: "Invalid password. Too much characters."
                                        }
                                    })}
                                    placeholder="Password"
                                />
                                <input
                                    className="w-100 login-form__item login-form__button"
                                    type="submit"
                                    value="Log In"
                                />
                            </form>
                        </Card.Body>
                        <div className="w-100 text-center mb-3">
                            Don&apos;t have an account?{" "}<Link to={"/register"}>Register</Link>
                        </div>
                    </Card>
                </div>
            </Container>
        </>

    )
}

{/* <Form onSubmit={handleSubmit(onSubmit)}>
                                <FormGroup controlId="formEmailInput">
                                    <Form.Label>Email</Form.Label>
                                    <Form.Control
                                        {...register("email", {
                                            required: "Email required",
                                            pattern: {
                                                value: /^\S+@\S+$/,
                                                message: "Please enter valid email address",
                                            }
                                        })}
                                        type="email"
                                    />
                                    <ErrorMessage errors={errors} name="email" as="p" />
                                </FormGroup>
                                <FormGroup controlId="formPasswordInput">
                                    <Form.Label>Password</Form.Label>
                                    <Form.Control
                                        {...register("password", {
                                            required: "Password required",
                                            minLength: {
                                                value: 6,
                                                message: "Invalid password. Few characters."
                                            },
                                            maxLength: {
                                                value: 50,
                                                message: "Invalid password. Too much characters."
                                            }
                                        })}
                                        type="password"
                                    />
                                    <ErrorMessage errors={errors} name="password" as="p" />

                                </FormGroup>
                                <Button className="w-100" variant="primary">Log In</Button>
                            </Form> */}