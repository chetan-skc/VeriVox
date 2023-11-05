import React from "react";
import Layout from "../Layout/Layout";
import "./Dashboard.css";
import { Button, Col, Form, InputGroup, Row } from "react-bootstrap";
import LineChart from "../../components/ChartGraph/ChartGraph";
import TableList from "../../components/TableList/TableList";

const DashBoard = () => {
  const object = [
    {
      numberofrow: 4,
      numberofcol: 3,
      data: [
        {
          0: "Product Name",
          1: "Feedback Form",
          2: "Feedback Volume",
        },
        {
          0: "Space",
          1: "User Feedback",
          2: "250",
        },
        {
          0: "Space",
          1: "User Feedback",
          2: "250",
        },
        {
          0: "Space",
          1: "User Feedback",
          2: "250",
        },
      ],
    },
  ];

  return (
    <div>
      <Layout />
      <div className="RoutePage">
        <div className="Company-Background">
          <div className="Page">
            <h3 className="dashboard-title">Dashboard</h3>
            <hr />
            <Row>
              <Col lg="3">
                <InputGroup className="dashboard-search">
                  <Form.Control placeholder="Search Product Name" />
                </InputGroup>
              </Col>
              <Col lg="3">
                <InputGroup className="dashboard-search">
                  <Form.Control placeholder="Search Form Name" />
                </InputGroup>
              </Col>
              <Col lg="3">
                <Form.Select aria-label="Default select example">
                  <option value="1">Last 7 days</option>
                  <option value="2">Last 14 days</option>
                  <option value="3">Last 30 days</option>
                  <option value="3">Last 90 days</option>
                  <option value="3">Custom Range</option>
                </Form.Select>
              </Col>
              <Col lg="3">
                <Button variant="light">Clear All</Button>{" "}
              </Col>
            </Row>
            <Row>
              <Col>
                <h3>Daily Trends</h3>
              </Col>
            </Row>
            <Row>
              <Col>Last Updated:19h ago</Col>
            </Row>
            <Row>
              <LineChart />
            </Row>
            <Row>
              <Col>
                <h4>Overall Summary</h4>
              </Col>
            </Row>
            <TableList object={object[0]} />
          </div>
        </div>
      </div>
    </div>
  );
};

export default DashBoard;
