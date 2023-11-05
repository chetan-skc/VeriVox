import React from "react";
import "./InformationTab.css";
import { Col, Row } from "react-bootstrap";

const InformationTab = (props) => {
  return (
    <div>
      <Row>
        <Col>
          <p className="company-detail-title">Industry Name</p>
          <p>{props.industry}</p>
        </Col>
        <Col>
          <p className="company-detail-title">Name on FORM's URL</p>
          <p>{props.shortName}</p>
        </Col>
      </Row>
      <Row>
        <Col style={{ marginTop: "2%" }}>
          <p className="company-detail-title">Description</p>
          <p>{props.description}</p>
        </Col>
      </Row>
    </div>
  );
};

export default InformationTab;
