import React from "react";
import { Col, Row } from "react-bootstrap";

const TableList = (props) => {
  const tableData = props.object.data;
  const numberofcol = props.object.numberofcol;
  const numberofrow = props.object.numberofrow;

  const sizeofcol = 12 / numberofcol;

  return (
    <div>
      <Row style={{ textAlign: "center" }}>
        {tableData.map((row, rowIndex) => (
          <Row className={rowIndex === 0 ? "CompanyTable" : ""} key={rowIndex}>
            {Object.values(row).map((cell, cellIndex) => (
              <Col key={cellIndex}>{cell}</Col>
            ))}
          </Row>
        ))}
      </Row>
    </div>
  );
};

export default TableList;
