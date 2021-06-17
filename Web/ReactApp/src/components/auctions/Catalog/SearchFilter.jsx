/* eslint-disable react/prop-types */
import React, {useEffect, useState} from 'react';
import { Col, Spinner } from "react-bootstrap";
import Form from "react-bootstrap/Form"
import "./SearchFilter.css";
import categoryService from "../../../services/categoryServices";
import { StartDateTimePicker, EndDateTimePicker } from "../../../utils/components/dateTimePicker";
import moment from "moment";
import { useHistory } from 'react-router-dom';

export const SearchFilter = ({ loading, query, setQuery}) => {
  const history = useHistory();

  const [startTime, setStartTime] = useState(
      moment().toDate()
  );
  const [endTime, setEndTime] = useState(
      moment().add(1, "months").add(20, "minutes").toDate()
  );
  const [categories, setCategories] = useState([]);
  const [dateDisabled, setDateDisabled] = useState(true);

  useEffect(() => {
    getCategories();
  }, []);

  useEffect(() => {
    if(!dateDisabled) {
      setQuery((prev) => ({
        ...prev,
        startTime: startTime.toISOString("dd/mm/yyyy HH:mm"),
        endTime: endTime.toISOString("dd/mm/yyyy HH:mm"),
      }));
    }
  }, [startTime, endTime, setQuery, dateDisabled]);
  
  const getCategories = () => {
    categoryService.getAll().then((response) => {
      setCategories(response.categories);
    })
  }
  
  return (
    <Col className="auction__filters" lg={4} sm={4}>
      <div>
        <div className="search-area default-item-search">
          <p>
            <i>Filters</i>
          </p>
          
          <Form>
            <Form.Group controlId="Title">
              <Form.Label>Title</Form.Label>
              <Form.Control
                type="input"
                onChange={(e) => {
                  const value = e.target.value;
                  setQuery((prev) => ({ ...prev, title: value })); //update title of state(query object)
                }}
                placeholder="Search by title"
                aria-label="Auction search input"
              />
            </Form.Group>
            <Form.Group controlId="Category">
              <Form.Label>Category</Form.Label>
              {!loading ? (
                  <Form.Control as="select" defaultValue={query.categoryId}>
                    <option onClick={() => history.replace("/auction")}>
                      All
                    </option>
                    {categories.map((category, index) => {
                      return (
                        <option
                          key={index}
                          onClick={(e) => {
                            history.replace(`/auction/${e.target.value}`);
                          }}
                          value={category.id}
                        >
                          {category.name}
                        </option>
                        );
                      })
                    }
                  </Form.Control>
              ) : (
                  <div>
                    <Spinner animation="border" />
                  </div>
              )}
            </Form.Group>
            <Form.Group className="mt-3" controlId="dateEnabled">
              <Form.Check
                  type="switch"
                  label="Use date range"
                  onChange={() => {
                    setDateDisabled(!dateDisabled);
                    if (!dateDisabled) {
                      console.log("Query before", query);
                      setQuery((prev) => ({
                        ...prev,
                        startTime: null,
                        endTime: null,
                      }));
                    }}
                  }
              />
            </Form.Group>
            <Form.Group controlId="StartingTime">
              <p>Starts on</p>
              <StartDateTimePicker
                  disabled={dateDisabled}
                  readOnly={true}
                  startTime={startTime}
                  setStartTime={setStartTime}
                  endTime={endTime}
              />
            </Form.Group>
            <Form.Group controlId="EndTime">
              <p>Ends on</p>
              <EndDateTimePicker
                  disabled={dateDisabled}
                  readOnly={true}
                  endTime={endTime}
                  setEndTime={setEndTime}
                  startTime={startTime}
              />
            </Form.Group>
          </Form>
        </div>
      </div>
    </Col>
  );
}