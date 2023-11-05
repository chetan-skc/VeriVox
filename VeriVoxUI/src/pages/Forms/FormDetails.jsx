import React, { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import { Col, Row, Button } from "react-bootstrap";
import { Icon } from "react-icons-kit";
import { eye } from "react-icons-kit/fa/eye";
import Layout from "../Layout/Layout";
import LinkListPopup from "./Links/LinkListPopup";
import FormPreviewPopup from "./FormPreviewPopup";
import { ic_fiber_manual_record } from "react-icons-kit/md/ic_fiber_manual_record";
import { ic_keyboard_arrow_down } from "react-icons-kit/md/ic_keyboard_arrow_down";

const FormDetails = () => {
  // Extracted formId from the URL
  const { formId } = useParams();

  const [isDescriptionVisible, setDescriptionVisible] = useState(true);
  const [page, setPage] = useState(1);
  const [pageSize] = useState(3);
  const [totalRecords, setTotalRecords] = useState(0);
  const [result, setFormData] = useState([]);
  const [pageTitle, setPageTitle] = useState("");
  const [pageDescription, setPageDescription] = useState("");
  const [dataLoaded, setDataLoaded] = useState(false);
  const [titleDescriptionLoaded, setTitleDescriptionLoaded] = useState(false);
  const [selectedRow, setSelectedRow] = useState(null);
  const [showLinkListPopup, setShowLinkListPopup] = useState(false);
  const [showPreviewPopup, setShowPreviewPopup] = useState(false);
  const [previewData, setPreviewData] = useState(null);





  // Function to toggle the description visibility
  const toggleDescription = () => {
    setDescriptionVisible(!isDescriptionVisible);
  };

  // Function to handle button click
  const handleButtonClick = () => {
    toggleDescription();
  };

  // Calculated the total number of pages
  const totalPages = Math.ceil(totalRecords / pageSize);

  // Function to handle the next page button click
  const handleNextPage = () => {
    if (page < totalPages) {
      setPage(page + 1);
    }
  };

  // Function to handle the previous page button click
  const handlePrevPage = () => {
    if (page > 1) {
      setPage(page - 1);
    }
  };

  // Function to handle row click
  const handleRowClick = (formId, productId) => {
    setSelectedRow({ formId, productId });
    setShowLinkListPopup(true);
  };

  // Function to fetch particular line data
  const fetchSpecificLineData = async (formId, productId) => {
    try {
      const token = sessionStorage.getItem("jwtToken");
      const response = await fetch(
        `https://localhost:7199/api/Link/form-detail/${formId}?page=${page}&pageSize=${pageSize}`, {
        method: "GET",
        headers: {
          "content-type": "application/json",
          "Authorization": `Bearer ${token}`
        }
      });
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }
      const data = await response.json();

      setFormData(data.data);
      setTotalRecords(data.totalRecords);
      setDataLoaded(true);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  // Function to close the link list popup
  const closeLinkListPopup = () => {
    setSelectedRow(null);
    setShowLinkListPopup(false);
    // Reload the particular line to refresh data 
    fetchSpecificLineData(selectedRow.formId, selectedRow.productId);
  };

  // Function to open the preview popup
  const openPreviewPopup = (formId, company, companylogo, productlogo) => {
    setSelectedRow({ formId, company, companylogo, productlogo });
    console.log(formId);

    const token = sessionStorage.getItem("jwtToken");
    fetch(`https://localhost:7199/api/Form/${formId}`, {
      method: "GET",
      headers: {
        "content-type": "application/json",
        "Authorization": `Bearer ${token}`
      },
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setPreviewData(data);
        console.log("preview", previewData);
      })
      .catch((error) => {
        console.error("Error fetching preview data:", error);
      });

    if (previewData != null) {
      setShowPreviewPopup(true);
    }

  };

  // Function to close the preview popup
  const closePreviewPopup = () => {
    setShowPreviewPopup(false);
    setSelectedRow(null);
  };

  // Function to fetch data from the API
  const fetchData = async () => {

    const token = sessionStorage.getItem("jwtToken");
    try {
      const response = await fetch(
        `https://localhost:7199/api/Link/form-detail/${formId}?page=${page}&pageSize=${pageSize}`, {
        method: "GET",
        headers: {
          "content-type": "application/json",
          "Authorization": `Bearer ${token}`
        },
      }
      );
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }
      const data = await response.json();
      console.log(data);
      setFormData(data.data);
      setTotalRecords(data.totalRecords);
      setDataLoaded(true);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  // Function to fetch page title and description
  const fetchPageTitleAndDescription = async () => {
    try {
      const token = sessionStorage.getItem("jwtToken");
      const response = await fetch(
        `https://localhost:7199/api/Link/form-detail/${formId}`, {
        method: "GET",
        headers: {
          "content-type": "application/json",
          "Authorization": `Bearer ${token}`
        },
      }
      );
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }
      const data = await response.json();

      setPageTitle(data.data[0].name);
      setPageDescription(data.data[0].description);

      setTitleDescriptionLoaded(true);
    } catch (error) {
      console.error("Error fetching page title and description:", error);
    }
  };

  // Fetch data and page title/description on component mount and when page and pageSize change
  useEffect(() => {
    fetchData();
    fetchPageTitleAndDescription();
  }, [page, pageSize]);





  const showPreviewBox = () => {

    return (
      <div className="preview-popup-container">
        <FormPreviewPopup
          formId={selectedRow.formId}
          company={selectedRow.company}
          companylogo={selectedRow.companylogo}
          productlogo={selectedRow.productlogo}
          onClose={closePreviewPopup}
          object={previewData}
        />
      </div>
    );

  };

  return (
    <div>
      <Layout />
      {/* Render the LinkListPopup when showLinkListPopup is true and for a selected row */}
      {showLinkListPopup && selectedRow && (
        <div className="link-list-popup-container">
          <LinkListPopup
            formId={selectedRow.formId}
            productId={selectedRow.productId}
            onClose={closeLinkListPopup}
          />
        </div>
      )}
      {/* Render the FormPreviewPopup when showPreviewPopup is true and for a selected row */}
      {showPreviewPopup && showPreviewBox()}
      <div
        style={{
          width: "85%",
          position: "absolute",
          marginLeft: "15%",
          marginTop: "60px",
        }}
      >
        <div
          style={{
            padding: "5%",
            paddingTop: "3%",
            backgroundColor: "#eceef3",
          }}
        >
          <p style={{ opacity: "0.7" }}>
            <Link
              style={{
                opacity: "1",
                textDecoration: "none",
                color: "black",
                fontWeight: "bold",
              }}
              to="/forms"
            >
              Forms
            </Link>
            {` > ${pageTitle}`}
          </p>
          <div
            style={{
              padding: "2%",
              borderRadius: "6px",
              backgroundColor: "#ffffff",
            }}
          >
            <div>
              <div
                style={{
                  fontFamily: "Arial, Helvetica, sans-serif",
                  fontWeight: 600,
                  display: "flex",
                  alignItems: "center",
                }}
              >
                <div style={{ flex: 1 }}>
                  {titleDescriptionLoaded ? (
                    <h2>{pageTitle}</h2>
                  ) : (
                    <h2>Loading...</h2>
                  )}
                </div>
                <div>
                  <Button
                    style={{
                      backgroundColor: "#F1F3FF",
                      border: "none",
                      borderRadius: "50%",
                      boxShadow: "rgba(255, 255, 255, 0.4) 0 1px 0 0 inset",
                      boxSizing: "border-box",
                      color: "#354E96",
                      cursor: "pointer",
                      fontSize: "15px",
                      fontWeight: 400,
                      outline: "none",
                      padding: "5px 0.8em",
                      position: "relative",
                      textDecoration: "none",
                      whiteSpace: "nowrap",
                    }}
                    role="button"
                    onClick={handleButtonClick}
                  >
                    {isDescriptionVisible ? "∧" : "∨"}
                  </Button>
                </div>
              </div>
              <hr />
              {isDescriptionVisible && (
                <div>
                  <h5 style={{opacity: "0.7"}}>Description</h5>
                  {titleDescriptionLoaded ? (
                    <p>{pageDescription}</p>
                  ) : (
                    <p>Loading...</p>
                  )}
                </div>
              )}
            </div>
            <div>
              <div
                style={{
                  display: "flex",
                  flexDirection: "verticle",
                  fontWeight: "bold",
                  padding: "8px",
                }}
              >
                <Row
                  style={{
                    textAlign: "center",
                    backgroundColor: "#F1F3FF",
                    paddingTop: "1%",
                    fontWeight: "600",
                    width: "100%",
                  }}
                >
                  <Col lg="3" style={{ textAlign: "left" }}>Company</Col>
                  <Col lg="1">Product</Col>
                  <Col lg="3">Name on Form's URL</Col>
                  <Col lg="2">No. of Links</Col>
                  <Col lg="2">Status</Col>
                  <Col lg="1">Preview</Col>
                </Row>
              </div>
              {Array.isArray(result) && result.length > 0 ? (
                result.map((dataItem) => (
                  <div
                    style={{ display: "flex", padding: "8px" }}
                    key={dataItem.id}
                  >
                    <Row
                      style={{
                        paddingTop: "1%",
                        fontWeight: "400",
                        width: "100%",
                      }}
                    >
                      <Col lg="3" style={{ textAlign: "left" }}>
                        <span
                          onClick={() =>
                            handleRowClick(formId, dataItem.productId)
                          }
                          onMouseEnter={(e) =>
                            (e.target.style.cursor = "pointer")
                          }
                          onMouseLeave={(e) => (e.target.style.cursor = "auto")}
                        >
                          {dataItem.company}
                        </span>
                      </Col>
                      <Col
                        lg="1"
                        style={{
                          paddingLeft: "2%",
                          paddingRight: "0%",
                        }}
                      >
                        <span
                          onClick={() =>
                            handleRowClick(formId, dataItem.productId)
                          }
                          onMouseEnter={(e) =>
                            (e.target.style.cursor = "pointer")
                          }
                          onMouseLeave={(e) => (e.target.style.cursor = "auto")}
                        >
                          {dataItem.product}
                        </span>
                      </Col>
                      <Col
                        lg="3"
                        style={{
                          textAlign: "center",
                          paddingRight: "1%",
                          height: "10%",
                        }}
                      >
                        <span
                          onClick={() =>
                            handleRowClick(formId, dataItem.productId)
                          }
                          onMouseEnter={(e) =>
                            (e.target.style.cursor = "pointer")
                          }
                          onMouseLeave={(e) => (e.target.style.cursor = "auto")}
                        >
                          {`${dataItem.companyShort}/${dataItem.productShort}/${dataItem.shortName}`}
                        </span>
                      </Col>
                      <Col
                        lg="2"
                        style={{ textAlign: "center", paddingLeft: "0%" }}
                      >
                        <span
                          onClick={() =>
                            handleRowClick(formId, dataItem.productId)
                          }
                          onMouseEnter={(e) =>
                            (e.target.style.cursor = "pointer")
                          }
                          onMouseLeave={(e) => (e.target.style.cursor = "auto")}
                        >
                          {dataItem.noOfLinks}
                        </span>
                      </Col>
                      <Col style={{ textAlign: "center" }}>
                        {dataItem.isActive ? (
                          <button
                            onClick={() =>
                              handleRowClick(formId, dataItem.productId)
                            }
                            className="btn btn-outline-success btn-sm"
                            style={{
                              color: "green",
                              backgroundColor: "transparent",
                            }}
                          >
                            <Icon
                              icon={ic_fiber_manual_record}
                              size={15}
                              style={{ color: "green" }}
                            />{" "}
                            Active{" "}
                            <Icon
                              icon={ic_keyboard_arrow_down}
                              size={25}
                              style={{ color: "grey" }}
                            />
                          </button>
                        ) : (
                          <button
                            onClick={() =>
                              handleRowClick(formId, dataItem.productId)
                            }
                            className="btn btn-outline-danger btn-sm"
                            style={{
                              color: "red",
                              backgroundColor: "transparent",
                            }}
                          >
                            <Icon
                              icon={ic_fiber_manual_record}
                              size={15}
                              style={{ color: "red" }}
                            />{" "}
                            Inactive{" "}
                            <Icon
                              icon={ic_keyboard_arrow_down}
                              size={25}
                              style={{ color: "grey" }}
                            />
                          </button>
                        )}
                      </Col>
                      <Col
                        lg="1"
                        style={{
                          textAlign: "center",
                          color: "#354E96",
                          cursor: "pointer",
                          display: "flex",
                          paddingRight: "0%",
                        }}
                      >
                        <span
                          onClick={() => {
                            openPreviewPopup(
                              formId,
                              dataItem.company,
                              dataItem.companylogo,
                              dataItem.productlogo
                            )

                          }

                          }
                          onMouseEnter={(e) =>
                            (e.target.style.cursor = "pointer")
                          }
                          onMouseLeave={(e) => (e.target.style.cursor = "auto")}
                        >
                          <div style={{ display: "flex" }}>
                            <Icon icon={eye} />{" "}
                            <span style={{ paddingLeft: "3%", padding: "3px 6px", gap: "6px", borderRadius: "3px", background: "#F1F3FF" }}> Preview</span>
                          </div>
                        </span>
                      </Col>
                    </Row>
                  </div>
                ))
              ) : (
                <p>No data available.</p>
              )}
            </div>
            <br />
            <div>
              <span>
                Showing{" "}
                {(page - 1) * pageSize + 1 > totalRecords
                  ? totalRecords
                  : (page - 1) * pageSize + 1}
                -
                {page * pageSize > totalRecords
                  ? totalRecords
                  : page * pageSize}{" "}
                of {totalRecords} items
              </span>
              <Button
                style={{
                  backgroundColor: "white",
                  color: "#DAD3D3",
                  border: "none",
                  marginLeft: "70%",
                }}
                onMouseEnter={(e) => (
                  (e.target.style.backgroundColor = "rgb(42, 57, 126)"),
                  (e.target.style.color = "white")
                )}
                onMouseLeave={(e) => (
                  (e.target.style.backgroundColor = "white"),
                  (e.target.style.color = "rgb(42, 57, 126)")
                )}
                onClick={handlePrevPage}
                disabled={page === 1}
              >
                {`<`}
              </Button>
              <Button
                style={{
                  color: "black",
                  backgroundColor: "#F1F3FF",
                  borderColor: "#F1F3FF",
                  marginLeft: ".5%",
                }}
              >
                {`${page}`}
              </Button>
              <Button
                style={{
                  backgroundColor: "white",
                  color: "#DAD3D3",
                  border: "none",
                  marginLeft: ".5%",
                }}
                onMouseEnter={(e) => (
                  (e.target.style.backgroundColor = "rgb(42, 57, 126)"),
                  (e.target.style.color = "white")
                )}
                onMouseLeave={(e) => (
                  (e.target.style.backgroundColor = "white"),
                  (e.target.style.color = "rgb(42, 57, 126)")
                )}
                onClick={handleNextPage}
                disabled={page * pageSize >= totalRecords}
              >
                {`>`}
              </Button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default FormDetails;
