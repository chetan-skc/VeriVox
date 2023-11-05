import React from "react";
import { Col, Row } from "react-bootstrap";
import { Pie } from "react-chartjs-2";
import "./Response.css";

const Response = () => {
  const labels = ["Yes", "No"];

  const data = {
    labels: labels,
    datasets: [
      {
        label: "",
        fill: true,
        backgroundColor: ["rgb(255, 99, 132)", "rgb(89, 117, 217)"],
        borderColor: ["rgb(255, 99, 132)", "rgb(89, 117, 217)"],
        data: [70, 30],
      },
    ],
  };
  return (
    <div className="response-box">
      <Row>
        <Col>Will you purchase our product again</Col>
      </Row>
      <Row style={{ width: "350px", marginLeft: "30%" }}>
        <Pie data={data} className="pie-chart" />
      </Row>
    </div>
  );
};

export default Response;
