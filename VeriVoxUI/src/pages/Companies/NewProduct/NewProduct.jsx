import React, { useState, useEffect } from "react";
import "./NewProduct.css";
import Layout from "../../Layout/Layout";
import { Col, Row } from "react-bootstrap";
import ContactDetails from "../../../components/ContactDetails/ContactDetails";
import { useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";

const NewProduct = () => {
  const [isDragActive, setIsDragActive] = useState(false);
  const [productName, setProductName] = useState("");
  const [typeData, setType] = useState("");
  const [description, setDescription] = useState("");
  const [logo, setLogo] = useState("");
  const [shortName, setShortName] = useState("");
  const [isOverlayVisible, setIsOverlayVisible] = useState(false);
  const [isOverlayBoxVisible, setIsOverlayBoxVisible] = useState(false);
  const [IsInvalidCredentials, setIsInvalidCredentials] = useState(false);

  const location = useLocation();
  const { id } = location.state || {};

  const handleProductNameChange = (e) => {
    setProductName(e.target.value);
  };

  const handleIndustryChange = (e) => {
    setType(e.target.value);
  };

  const handleDescriptionChange = (e) => {
    setDescription(e.target.value);
  };

  const handleLogoChange = (e) => {
    setLogo(e.target.value);
    console.log(logo);
  };

  const handleShortNameChange = (e) => {
    setShortName(e.target.value);
  };
  const toggleOverlay = () => {
    setIsOverlayVisible(!isOverlayVisible);
  };

  const toggleOverlayBox = () => {
    setIsOverlayBoxVisible(!isOverlayBoxVisible);
  };

  function postproduct(data) {
    const token = sessionStorage.getItem("jwtToken");
    fetch("https://localhost:7199/api/Product", {
      method: "POST",
      body: JSON.stringify(data),
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-type": "application/json; charset=UTF-8",
      },
    }).catch((err) => {
      console.log(err);
    });
  }

  function addContact() {
    toggleOverlay();
    toggleOverlayBox();
  }

  const handleSubmit = () => {
    if (!productName || !typeData || !description || !shortName || !logo)
      setIsInvalidCredentials(true);
    else {
      let data = {
        name: productName,
        description: description,
        logoImage: logo,
        shortName: shortName,
        type: typeData,
        companyId: id,
      };
      postproduct(data);
      navigateToCompanyInformation();
    }
  };
  const navigate = useNavigate();
  const navigateToCompanyInformation = () => {
    navigate("/companies/companydetail", {
      state: { id: id },
    });
  };

  useEffect(() => {
    const dropArea = document.querySelector(".drag-area");
    const dragText = document.querySelector(".header");

    let button = dropArea.querySelector(".button");
    let input = dropArea.querySelector("input");

    let file;

    button.onclick = () => {
      input.click();
    };

    input.addEventListener("change", function () {
      file = this.files[0];
      dropArea.classList.add("active");
      displayFile();
    });

    dropArea.addEventListener("dragover", (event) => {
      event.preventDefault();
      dropArea.classList.add("active");
      dragText.textContent = "Release to Upload";
    });

    dropArea.addEventListener("dragleave", () => {
      dropArea.classList.remove("active");

      dragText.textContent = "Drag & Drop";
    });

    dropArea.addEventListener("drop", (event) => {
      event.preventDefault();

      file = event.dataTransfer.files[0];
      displayFile();
    });

    function displayFile() {
      let fileType = file.type;

      let validExtensions = ["image/jpeg", "image/jpg", "image/png"];

      if (validExtensions.includes(fileType)) {
        let fileReader = new FileReader();

        fileReader.onload = () => {
          let fileURL = fileReader.result;
          let imgTag = `<img src="${fileURL}" alt="">`;
          dropArea.innerHTML = imgTag;
          setLogo(fileURL);
        };

        fileReader.readAsDataURL(file);
      } else {
        alert("This is not an Image File");
        dropArea.classList.remove("active");
      }
    }
  }, []);

  return (
    <div>
      <Layout />
      <div
        className={`overlay ${isOverlayVisible ? "" : "hidden"}`}
        onClick={addContact}
      ></div>
      <div className="RoutePage">
        <div className={`overlay-box ${isOverlayBoxVisible ? "" : "hidden"}`}>
          <div>
            <p>Enter First Name:</p>
            <input
              className="overlay-input"
              type="text"
              placeholder="Enter First Name"
            />
          </div>
          <div>
            <p>Enter Second Name:</p>
            <input
              className="overlay-input"
              type="text"
              placeholder="Enter Second Name"
            />
          </div>

          <div>
            <p>Designation:</p>
            <input
              className="overlay-input"
              type="text"
              placeholder="Enter Designation"
            />
          </div>

          <div>
            <p>Email:</p>
            <input
              className="overlay-input"
              type="text"
              placeholder="Enter Email"
            />
          </div>

          <div className="hello">
            <input type="checkbox" />
            <p>Assign as Admin</p>
          </div>
          <div>
            <button className="create-contact-cancel" onClick={addContact}>
              Cancel
            </button>
            <button className="create-contact-button">Save</button>
          </div>
        </div>
        <div className="Company-Background">
          <div className="NewPage">
            <h2 className="Title">New Product</h2>
            <p>
              Please provide the necessary information about your product here
            </p>
            <hr />
            <Row>
              <Col>
                Name<span class="required">*</span>
              </Col>
              <Col>
                Type<span class="required">*</span>
              </Col>
            </Row>
            <Row>
              <Col>
                <input
                  className="input-area"
                  type="text"
                  name="text"
                  placeholder="Enter the full legal name of company"
                  value={productName}
                  onChange={handleProductNameChange}
                />
                {IsInvalidCredentials && !productName && (
                  <p style={{ color: "red" }}> Fill product name</p>
                )}
              </Col>
              <Col className="input-box-container">
                <input
                  className="input-area"
                  type="text"
                  name="text"
                  placeholder="Product Type"
                  value={typeData}
                  onChange={handleIndustryChange}
                />
                {IsInvalidCredentials && !typeData && (
                  <p style={{ color: "red" }}> Fill type</p>
                )}
              </Col>
            </Row>
            <Row className="input-detail">
              <Col>
                Description <span class="required">*</span>
              </Col>
            </Row>
            <Row>
              <Col>
                <input
                  cols="30"
                  className="description-area"
                  type="text"
                  name="text"
                  placeholder="Description"
                  value={description}
                  onChange={handleDescriptionChange}
                />
                {IsInvalidCredentials && !description && (
                  <p style={{ color: "red" }}> Fill description name</p>
                )}
              </Col>
            </Row>

            <Row className="input-detail">
              <Col lg="6">
                Name on Form's URL<span class="required">*</span>
              </Col>
              <Col>
                Product Logo<span class="required">*</span>
              </Col>
            </Row>
            <Row>
              <Col lg="6">
                <input
                  type="text"
                  name="text"
                  placeholder="ProductName"
                  className="input-area"
                  onChange={handleShortNameChange}
                />
                {IsInvalidCredentials && !shortName && (
                  <p style={{ color: "red" }}> Fill short name</p>
                )}
              </Col>
              <Col lg="5">
                <div className="container">
                  <div
                    className={`drag-area ${isDragActive ? "active" : ""}`}
                    onDragOver={(e) => {
                      e.preventDefault();
                      setIsDragActive(true);
                    }}
                    onDragLeave={() => setIsDragActive(false)}
                    onDrop={(e) => {
                      e.preventDefault();
                      console.log(e.dataTransfer.files[0]);
                      setIsDragActive(false);
                    }}
                    onChange={handleLogoChange}
                  >
                    <div className="icon">
                      <i className="fas fa-images"></i>
                    </div>
                    <span className="header">
                      {isDragActive ? "Release to Upload" : "Drag & Drop"}
                    </span>
                    <span className="header">
                      or <input type="file" style={{ display: "none" }} />
                      <span className="button" style={{ cursor: "pointer" }}>
                        Browse
                      </span>
                    </span>
                    <input type="file" hidden />

                    <span className="support">Supports: JPEG, JPG, PNG</span>
                  </div>
                </div>

                {IsInvalidCredentials && !logo && (
                  <p style={{ color: "red" }}> Fill Logo</p>
                )}
              </Col>
            </Row>

            <ContactDetails />
          </div>
          <div className="create-company-box">
            <button className="create-company-button" onClick={handleSubmit}>
              Create Product
            </button>

            <button
              className="create-company-button"
              onClick={() => {
                navigateToCompanyInformation();
              }}
            >
              Cancel
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default NewProduct;
