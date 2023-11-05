import React, { useState, useEffect } from "react";
import "./NewCompanies.css";
import Layout from "../../Layout/Layout";
import { Col, Row } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import ApiInterceptor from "../../LoginPage/ApiInterceptor";

const NewCompanies = () => {
  const [isDragActive, setIsDragActive] = useState(false);
  const [companyName, setCompanyName] = useState("");
  const [industry, setIndustry] = useState("");
  const [description, setDescription] = useState("");
  const [logo, setLogo] = useState("");
  const [shortName, setShortName] = useState("");
  const [companyindustrydata, setcompanyindustry] = useState([]);
  const [isOverlayVisible, setIsOverlayVisible] = useState(false);
  const [isOverlayBoxVisible, setIsOverlayBoxVisible] = useState(false);
  const [IsInvalidCredentials, setIsInvalidCredentials] = useState(false);

  const handleCompanyNameChange = (e) => {
    setCompanyName(e.target.value);
  };

  const handleIndustryChange = (e) => {
    setIndustry(e.target.value);
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

  async function postcompany(data) {
    const apiUrl = `https://localhost:7199/api/Company`;
    try {
      const postCompany = await ApiInterceptor(apiUrl, "POST", data);
      console.log("Company registered!", postCompany.data);
    } catch (error) {
      console.error("Error posting company:", error);
      throw error;
    }
  }

  function addContact() {
    toggleOverlay();
    toggleOverlayBox();
  }
  const navigate = useNavigate();
  const handleSubmit = () => {
    if (!companyName || !industry || !description || !shortName || !logo) {
      setIsInvalidCredentials(true);
    } else {
      let data = {
        name: companyName,
        description: description,
        logoImage: logo,
        shortName: shortName,
        industryId: industry,
      };
      postcompany(data);
      navigate("/companies");
    }
  };

  useEffect(() => {
    fetch("https://localhost:7199/api/Company/companyindustry", {
      method: "GET",
      headers: {
        "content-type": "application/json",
      },
    })
      .then((res) => {
        if (res.ok) {
          return res.json();
        }
      })
      .then((tasks) => {
        setcompanyindustry(tasks);
      })
      .catch((error) => {});

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
            <h2 className="Title">New Company</h2>
            <p>provide the necessary information about your company here</p>
            <hr />
            <Row>
              <Col>
                Name<span class="required">*</span>
              </Col>
              <Col>
                Industry<span class="required">*</span>
              </Col>
            </Row>
            <Row>
              <Col>
                <input
                  className="input-area"
                  type="text"
                  name="text"
                  placeholder="Enter the full legal name of company"
                  value={companyName}
                  onChange={handleCompanyNameChange}
                />

                {IsInvalidCredentials && !companyName && (
                  <p style={{ color: "red" }}> Fill company name</p>
                )}
              </Col>
              <Col className="input-box-container">
                <select
                  className="industry-dropdown input-area"
                  value={companyindustrydata.id}
                  onChange={handleIndustryChange}
                >
                  <option className="industry-dropdown-option">
                    Choose Industry
                  </option>
                  {companyindustrydata.map((data) => (
                    <option value={data.id}>{data.name}</option>
                  ))}
                </select>
                {IsInvalidCredentials && !industry && (
                  <p style={{ color: "red" }}> Select industry</p>
                )}
              </Col>
            </Row>
            <Row className="input-detail">
              <Col>Description</Col>
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
                  <p style={{ color: "red" }}> Fill description</p>
                )}
              </Col>
            </Row>

            <Row className="input-detail">
              <Col lg="6">
                Name on Form's URL<span class="required">*</span>
              </Col>
              <Col>
                Company Logo<span class="required">*</span>
              </Col>
            </Row>
            <Row>
              <Col lg="6">
                <input
                  type="text"
                  name="text"
                  placeholder="CompanyName"
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
                  <p style={{ color: "red" }}> Select a logo</p>
                )}
              </Col>
            </Row>

            <Row>
              <Col>
                <h3 className="input-detail">Contact Details</h3>
                <p>
                  Please provide the necessary information about your company
                  here
                </p>
                <button
                  onClick={addContact}
                  className="button-contact"
                  style={{ backgroundColor: "rgb(47, 52, 126)" }}
                >
                  + Contact
                </button>
              </Col>
            </Row>
            <Row>
              <Col>
                <div className="no-contact input-detail">
                  <h4>No Contacts here!</h4>
                  <p>Contact you have added will appear here</p>
                </div>
              </Col>
            </Row>
          </div>
          <div className="create-company-box">
            <button className="create-company-button" onClick={handleSubmit}>
              Create Company
            </button>

            <Link to={"/companies"}>
              <button className="create-company-button">Cancel</button>
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default NewCompanies;
