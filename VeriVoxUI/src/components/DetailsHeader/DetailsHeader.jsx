import React, { useState } from "react";
import ApiInterceptor from "../../pages/LoginPage/ApiInterceptor";
import "./DetailsHeader.css";
const DetailsHeader = (props) => {
  const [status, setStatus] = useState(props.isActive);
  const [productstatus, setProductStatus] = useState(props.isActive);

  const handleStatusChange = () => {
    if (props.source === "company") {
      let newStatus = !status;
      let data = {
        isActive: newStatus,
      };
      setStatus(newStatus);
      const apiUrl = `https://localhost:7199/api/Company/companystatechange/${props.id}`;
      ApiInterceptor(apiUrl, "PUT", data);
    } else if (props.source === "product") {
      let newStatus = !productstatus;
      let data = {
        isActive: newStatus,
      };
      setProductStatus(newStatus);
      const apiUrl = `https://localhost:7199/api/Product/productstatechange/${props.id}`;
      ApiInterceptor(apiUrl, "PUT", data);
    }
  };

  return (
    <div>
      <div className="Title-Holder">
        <div>
          <img
            src={props.logoImage}
            alt=""
            height="100px"
            width="150px"
            className="image-container"
          />
        </div>
        <div style={{ marginLeft: "2%" }}>
          <h2 className="Title">{props.name}</h2>
          <div style={{ display: "flex" }}>
            {" "}
            <p className="Status">Status</p>
            <select
              onChange={handleStatusChange}
              style={{
                color: status ? "#004225" : "#C51605",
                width: "80px",
                height: "80%",
                border: "none",
                marginLeft: "2%",
              }}
            >
              <option>
                {props.isActive ? <span>Active</span> : <span>InActive</span>}
              </option>
              <option>
                {props.isActive ? <span>InActive</span> : <span>Active</span>}
              </option>
            </select>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DetailsHeader;
