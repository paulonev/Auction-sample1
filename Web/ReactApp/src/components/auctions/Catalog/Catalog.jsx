import React, {useEffect, useState} from "react";
import { Container, Row, Col } from "react-bootstrap";
import { useParams } from "react-router-dom";
import useDebounce from "../../../react-hooks/useDebounce";
import useItemsSearch from "../../../react-hooks/useItemsSearch";

import { AuctionList } from "./AuctionList";
import { SearchFilter } from "./SearchFilter";
import { Header } from "./Header"

export const Catalog = () => {
  // state represents query
  let [query, setQuery] = useState({
    title: null,
    startTime: null,
    endTime: null,
    categoryId: null,
  });
  const [pageNumber, setPageNumber] = useState(1);
  // return first arg last modified value that hasn't been changed during the time, specified as a second arg
  query = useDebounce(query, 500);
  const {
    makeRequest,
    items,
    responseItemsCount,
    hasMore,
    loading,
    error,
  } = useItemsSearch(query, pageNumber, setPageNumber);
  const { categoryId } = useParams();

  useEffect(() => {
    if (query.categoryId === categoryId) {
      makeRequest();
    } else {
      setQuery((prev) => ({...prev, categoryId}));
    }
  }, [categoryId, makeRequest]);
  
  return (
    <>
      <Container>
        <Row>
          <Col lg={12}>
            <Header totalItemsCount={responseItemsCount} />
          </Col>
          <Col lg={12}>
            <Row>
              <SearchFilter query={query} setQuery={setQuery} loading={loading}/>
              <Col lg={8}>
                <AuctionList
                  items={items}
                  loading={loading}
                  error={error}
                  setPageNumber={setPageNumber}
                />
              </Col>
            </Row>
          </Col>
        </Row>
      </Container>
    </>
  );
}