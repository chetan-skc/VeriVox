import React from "react";
import "./CustomerFeedback.css";
import Layout from "../../Layout/Layout";
import { Tab, Tabs } from "react-bootstrap";
import TableList from "../../../components/TableList/TableList";
import Response from "../../../components/Response/Response";
const CustomerFeedback = () => {
  const object = [
    {
      numberofcol: 4,
      numberofrow: 3,
      data: [
        {
          0: "Company Name",
          1: "Description",
          2: "Feedback Date",
          3: "Actions",
        },
        {
          0: "Space",
          1: "kjalsdh dlksjf alksd",
          2: "Feedback Date",
          3: "Actions",
        },
        {
          0: "Space",
          1: "fsdgjkl sdfklgjsd",
          2: "Feedback Date",
          3: "Actions",
        },
        {
          0: "Space",
          1: "fsdgj sdl klfdj ",
          2: "Feedback Date",
          3: "Actions",
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
            <h4>Customer Feedback</h4>
            <Tabs
              defaultActiveKey="company"
              className="mb-3"
              style={{ marginTop: "3%" }}
            >
              <Tab
                className="company-information-tab"
                eventKey="insights"
                title="Insights"
              >
                <Response />
              </Tab>
              <Tab
                className="company-information-tab"
                eventKey="responses"
                title="Responses"
              >
                <TableList object={object[0]} />
              </Tab>
            </Tabs>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CustomerFeedback;
