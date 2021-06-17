/* eslint-disable react/prop-types */
import React, { useState, useEffect } from 'react';
import { Alert, Button, Form, FormGroup, Card, Container } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { useAuth0 } from '../../react-hooks/useAuth0';
import { useForm } from 'react-hook-form';
import { ErrorMessage } from '@hookform/error-message';
import "./Signup.css";

export const Signup = () => {
  const { signup, error } = useAuth0();

  const { register, handleSubmit, watch, formState: { errors, touchedFields } } = useForm({
    defaultValues: { email: "", password: "" },
    criteriaMode: 'all',
  });

  const [hasValidationErrors, setHasValidationErrors] = useState(false);
  const [validationError, setValidationError] = useState();
  const [loading, setLoading] = useState(false);

  async function onSubmit(data, e) {
    e.preventDefault();

    if (data.password !== data.confirmPassword) {
      return setValidationError("Passwords do not match");
    }

    try {
      setValidationError(null);
      setLoading(true);
      await signup(data);
      history.go(-1);
    } catch {
      console.log("Error with sign up");
    }

    setLoading(false);
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
              <h2 className="text-center mb-4">Sign Up</h2>
              {hasValidationErrors && (
                <>
                  <ErrorMessage errors={errors} name="email" render={({ message }) => <Alert variant="danger">{message}</Alert>} />
                  <ErrorMessage errors={errors} name="password" render={({ message }) => <Alert variant="danger">{message}</Alert>} />
                </>
              )}
              {
                validationError && <><Alert variant="danger">{validationError}</Alert></>
              }
              {
                error && <><Alert variant="danger">{error}</Alert></>
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
                  className="w-100 mb-3 login-form__item"
                  type="password"
                  {...register("confirmPassword", {
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
                  placeholder="Confirm password"
                />
                <input
                  className="w-100 login-form__item login-form__button"
                  type="submit"
                  value="Sign Up"
                  disabled={loading}
                />
              </form>
            </Card.Body>
            <div className="w-100 text-center mb-3">
              Have an account?{" "}<Link to={"/login"}>Login</Link>
            </div>
          </Card>
        </div>
      </Container>
    </>

  )
}