import React, { useState } from "react";
import { Col } from "react-bootstrap";
import "./SideBarStyle.css";
import { Link } from "react-router-dom";

const SideBar = () => {
  const [activeButtons, setActiveButtons] = useState({
    dashboard: false,
    companies: false,
    forms: false,
  });

  const handleToggle = (buttonName) => {
    setActiveButtons({
      ...activeButtons,
      [buttonName]: !activeButtons[buttonName],
    });
  };

  return (
    <div>
      <Col className="SideBar" lg="2">
        <h2 className="BrandName">VeriVox</h2>
        <p className="BrandSlogan">Your Voice,Our Insights</p>
        <hr />
        <div>
          <Link style={{ textDecoration: "none" }} to="/dashboard">
            <btn
              className={`toggle-button ${
                activeButtons.dashboard ? "active" : ""
              } pages`}
              onClick={() => handleToggle("dashboard")}
            >
              DashBoard
            </btn>
          </Link>

          <Link style={{ textDecoration: "none" }} to="/companies">
            <btn
              className={`toggle-button ${
                activeButtons.companies ? "active" : ""
              } pages`}
              onClick={() => handleToggle("companies")}
            >
              Companies
            </btn>
          </Link>

          <Link style={{ textDecoration: "none" }} to="/forms">
            <btn
              className={`toggle-button ${
                activeButtons.forms ? "active" : ""
              } pages`}
              onClick={() => handleToggle("forms")}
            >
              Forms
            </btn>
          </Link>
          {/* <Link style={{ textDecoration: "none" }} to="/customerForms">
            <btn
              className={`toggle-button ${
                activeButtons.forms ? "active" : ""
              } pages`}
              onClick={() => handleToggle("customerForms")}
            >
              Customer Form
            </btn>
          </Link> */}
        </div>
      </Col>
    </div>
  );
};

export default SideBar;
