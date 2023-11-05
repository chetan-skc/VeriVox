import React, { useEffect, useState } from "react";
import Tab from "react-bootstrap/Tab";
import Tabs from "react-bootstrap/Tabs";
import { Col, Form, InputGroup, Nav, Row } from "react-bootstrap";
import Layout from "../../Layout/Layout";
import { useLocation, useNavigate } from "react-router-dom";
import "./CompanyDetails.css";
import InformationTab from "../../../components/InformationTab/InformationTab";
import DetailsHeader from "../../../components/DetailsHeader/DetailsHeader";
import ContactDetails from "../../../components/ContactDetails/ContactDetails";
import ApiInterceptor from "../../LoginPage/ApiInterceptor";
import ProductDetails from "./ProductDetails";
import {
  setCompanyName,
  setDescription,
  setLogo,
  setShortName,
  setIndustry,
  setIsProductOverlayBoxVisible,
  setProductName,
  setProductDescription,
  setProductLogo,
  setProductShortName,
  setProductType,
} from "../../../Slices/CompanyDetailSlice";

import { useSelector, useDispatch } from "react-redux";
import ProductContactDetails from "../../../components/ContactDetails/ProductContactDetails";
const CompanyDetails = () => {
  const location = useLocation();
  const { id } = location.state || {};

  const [companydata, setcompany] = useState([]);
  const [searchResult, setSearchResult] = useState("");
  const [products, setProducts] = useState([]);
  const [product, setProduct] = useState([]);
  const [isOverlayVisible, setIsOverlayVisible] = useState(false);
  const [isOverlayBoxVisible, setIsOverlayBoxVisible] = useState(false);
  const [isActive, setIsActive] = useState("");
  const [industryId, setIndustryId] = useState("");
  const [companyindustrydata, setcompanyindustry] = useState([]);
  const [activeProduct, setActiveProduct] = useState(null);
  const [defaultProduct, setdefaultProduct] = useState(true);
  const [limitRequest, setlimitRequest] = useState(true);
  const [isEditing, setIsEditing] = useState(false);
  const [selectedFile, setSelectedFile] = useState("");
  const [flag, setflag] = useState(false);
  const companyName = useSelector((state) => state.companyDetails.companyName);
  const description = useSelector((state) => state.companyDetails.description);
  const logo = useSelector((state) => state.companyDetails.logo);
  const shortName = useSelector((state) => state.companyDetails.shortName);
  const industry = useSelector((state) => state.companyDetails.industry);
  const productName = useSelector((state) => state.companyDetails.productName);
  const productDescription = useSelector(
    (state) => state.companyDetails.productDescription
  );
  const productLogo = useSelector((state) => state.companyDetails.productLogo);
  const productShortName = useSelector(
    (state) => state.companyDetails.productShortName
  );
  const productType = useSelector((state) => state.companyDetails.productType);

  const deleteCompanyRole = useSelector(
    (state) => state.user.deleteCompanyRole
  );
  const deleteProductRole = useSelector(
    (state) => state.user.deleteProductRole
  );
  const editCompanyRole = useSelector((state) => state.user.editCompanyRole);
  const editProductRole = useSelector((state) => state.user.editProductRole);
  const newProductRole = useSelector((state) => state.user.newProductRole);

  const dispatch = useDispatch();
  const isProductOverlayBoxVisible = useSelector(
    (state) => state.companyDetails.isProductOverlayBoxVisible
  );

  const toggleOverlay = () => {
    setIsOverlayVisible(!isOverlayVisible);
  };

  const toggleOverlayBox = () => {
    setIsOverlayBoxVisible(!isOverlayBoxVisible);
  };

  const toggleProductOverlayBox = () => {
    dispatch(setIsProductOverlayBoxVisible(!isProductOverlayBoxVisible));
  };

  const handleCompanyNameChange = (e) => {
    dispatch(setCompanyName(e.target.value));
  };

  const handleIndustryChange = (e) => {
    setIndustryId(e.target.value);
  };

  const handleDescriptionChange = (e) => {
    dispatch(setDescription(e.target.value));
  };

  const handleShortNameChange = (e) => {
    dispatch(setShortName(e.target.value));
  };

  const handleProductNameChange = (e) => {
    dispatch(setProductName(e.target.value));
  };

  const handleProductTypeChange = (e) => {
    dispatch(setProductType(e.target.value));
  };

  const handleProductDescriptionChange = (e) => {
    dispatch(setProductDescription(e.target.value));
  };

  const handleProductShortNameChange = (e) => {
    dispatch(setProductShortName(e.target.value));
  };

  useEffect(() => {
    async function fetchData() {
      const token = sessionStorage.getItem("jwtToken");
      try {
        const response = await fetch(
          `https://localhost:7199/api/Company/${id}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
              "Content-type": "application/json; charset=UTF-8",
            },
          }
        );

        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const result = await response.json();
        setcompany(result);
        dispatch(setCompanyName(result.name));
        setIndustryId(result.industryId);
        dispatch(setDescription(result.description));
        dispatch(setLogo(result.logoImage));
        dispatch(setShortName(result.shortName));
        dispatch(setIndustry(result.industry));
        setIsActive(result.isActive);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    }

    fetchData();

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
  }, []);
  const navigate = useNavigate();

  function newProduct() {
    navigate("/companies/companydetail/newproduct", {
      state: { id: companydata.id },
    });
  }
  function onDelete() {
    const apiUrl = `https://localhost:7199/api/Company/${id}`;
    ApiInterceptor(apiUrl, "DELETE");

    navigate("/companies");
  }

  function onDeleteProduct() {
    const apiUrl = `https://localhost:7199/api/Product/${activeProduct}`;
    ApiInterceptor(apiUrl, "DELETE");
    navigate("/companies");
  }

  function onUpdateProduct() {
    toggleOverlay();
    toggleProductOverlayBox();
  }

  function updatecompany(data) {
    const apiUrl = `https://localhost:7199/api/Company/${id}`;
    ApiInterceptor(apiUrl, "PUT", data);
  }

  function updateproduct(data) {
    const apiUrl = `https://localhost:7199/api/Product/${activeProduct}`;
    ApiInterceptor(apiUrl, "PUT", data);
  }

  useEffect(() => {
    async function getProducts() {
      try {
        const apiUrl = `https://localhost:7199/api/Product/productbycompanyid/${id}`;
        const products = await ApiInterceptor(apiUrl, "GET");
        setProducts(products);
        if (defaultProduct && products.length > 0) {
          handleProduct(products[0].id);
        }

        if (products.length > 0 && activeProduct === null) {
          setActiveProduct(products[0].id);
        }
      } catch (error) {
        console.log("Error fetching products", error);
        throw error;
      }
    }
    if (limitRequest) {
      getProducts();
      setlimitRequest(false);
    }
  }, [products, activeProduct]);

  const handleProduct = async (ProductId) => {
    setActiveProduct(ProductId);
    try {
      const apiUrl = `https://localhost:7199/api/Product/${ProductId}`;
      const product = await ApiInterceptor(apiUrl, "GET");
      setflag(true);

      const result = product.product;
      setProduct(result);
      dispatch(setProductName(result.name));
      dispatch(setProductType(result.type));
      dispatch(setProductLogo(result.logoImage));
      dispatch(setProductShortName(result.shortName));
      dispatch(setProductDescription(result.description));

      setdefaultProduct(false);
    } catch (error) {
      console.log("Error fetching product", error);
      throw error;
    }
  };

  const handleEditClick = () => {
    setIsEditing(true);
    setSelectedFile("");
  };

  const handleFileChange = (event) => {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e) => {
        const base64String = e.target.result;
        dispatch(setLogo(base64String));
      };
      reader.readAsDataURL(file);
    }
    setSelectedFile(file);
    setIsEditing(false);
  };

  const handleProductFileChange = (event) => {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e) => {
        const base64String = e.target.result;
        dispatch(setProductLogo(base64String));
      };
      reader.readAsDataURL(file);
    }
    setSelectedFile(file);
    setIsEditing(false);
  };

  function updateInformation() {
    toggleOverlay();
    toggleOverlayBox();
  }

  const handleSubmit = () => {
    console.log(logo);
    let data = {
      name: companyName,
      description: description,
      logoImage: logo,
      shortName: shortName,
      industryId: industryId,
    };
    updatecompany(data);
    updateInformation();
    setcompany(data);
  };

  const handleProductSubmit = () => {
    let data = {
      name: productName,
      description: productDescription,
      logoImage: productLogo,
      shortName: productShortName,
      type: productType,
    };
    updateproduct(data);
    onUpdateProduct();
    setProduct(data);
  };

  return (
    <div>
      <div
        className={`overlay ${isOverlayVisible ? "" : "hidden"}`}
        onClick={updateInformation}
      ></div>
      <div
        className={`company-update-overlay-box ${
          isOverlayBoxVisible ? "" : "hidden"
        }`}
      >
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
            {" "}
            <input
              className="overlay-input"
              type="text"
              onChange={handleCompanyNameChange}
              value={companyName}
            />
          </Col>
          <Col>
            <select
              className="industry-dropdown input-area"
              onChange={handleIndustryChange}
            >
              <option value={industryId} className="industry-dropdown-option">
                {companydata.industry}
              </option>
              {companyindustrydata.map((data) => (
                <option value={data.id}>{data.name}</option>
              ))}
            </select>
          </Col>
        </Row>
        <Row>
          <Col>
            Descripton<span class="required">*</span>
          </Col>
        </Row>
        <Row>
          <Col>
            {" "}
            <input
              onChange={handleDescriptionChange}
              className="description-area"
              type="text"
              placeholder="Enter First Name"
              value={description}
            />
          </Col>
        </Row>
        <Row>
          <Col>
            Name on Form's URl<span class="required">*</span>
          </Col>
          <Col>
            Logo Image<span class="required">*</span>
          </Col>
        </Row>
        <Row>
          <Col>
            {" "}
            <input
              className="overlay-input"
              type="text"
              placeholder="Enter First Name"
              value={shortName}
              onChange={handleShortNameChange}
            />
          </Col>
          <Col>
            {isEditing ? (
              <input type="file" accept="image/*" onChange={handleFileChange} />
            ) : (
              <div>
                {selectedFile ? (
                  <img
                    src={URL.createObjectURL(selectedFile)}
                    alt=""
                    height="100px"
                    width="150px"
                  />
                ) : (
                  <img
                    src={logo}
                    alt=""
                    srcset=""
                    height="100px"
                    width="150px"
                  />
                )}
                <p style={{ color: "red" }} onClick={handleEditClick}>
                  Edit
                </p>
              </div>
            )}
          </Col>
        </Row>
        <div>
          <button className="create-contact-cancel" onClick={updateInformation}>
            Cancel
          </button>
          <button onClick={handleSubmit} className="create-contact-button">
            Save
          </button>
        </div>
      </div>
      <Layout />

      <div className="RoutePage">
        {/* this is product overlay */}
        <div
          className={`company-update-overlay-box ${
            isProductOverlayBoxVisible ? "" : "hidden"
          }`}
        >
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
              {" "}
              <input
                className="overlay-input"
                type="text"
                onChange={handleProductNameChange}
                value={productName}
              />
            </Col>
            <Col>
              <input
                className="overlay-input"
                type="text"
                onChange={handleProductTypeChange}
                value={productType}
              />
            </Col>
          </Row>
          <Row>
            <Col>
              Descripton<span class="required">*</span>
            </Col>
          </Row>
          <Row>
            <Col>
              {" "}
              <input
                onChange={handleProductDescriptionChange}
                className="description-area"
                type="text"
                placeholder="Enter First Name"
                value={productDescription}
              />
            </Col>
          </Row>
          <Row>
            <Col>
              Name on Form's URl<span class="required">*</span>
            </Col>
            <Col>
              Logo Image<span class="required">*</span>
            </Col>
          </Row>
          <Row>
            <Col>
              {" "}
              <input
                className="overlay-input"
                type="text"
                placeholder="Enter First Name"
                value={productShortName}
                onChange={handleProductShortNameChange}
              />
            </Col>
            <Col>
              {isEditing ? (
                <input
                  type="file"
                  accept="image/*"
                  onChange={handleProductFileChange}
                />
              ) : (
                <div>
                  {selectedFile ? (
                    <img
                      src={URL.createObjectURL(selectedFile)}
                      alt=""
                      height="100px"
                      width="150px"
                    />
                  ) : (
                    <img
                      src={productLogo}
                      alt=""
                      srcset=""
                      height="100px"
                      width="150px"
                    />
                  )}
                  <p style={{ color: "red" }} onClick={handleEditClick}>
                    Edit
                  </p>
                </div>
              )}
            </Col>
          </Row>
          <div>
            <button className="create-contact-cancel" onClick={onUpdateProduct}>
              Cancel
            </button>
            <button
              className="create-contact-button"
              onClick={handleProductSubmit}
            >
              Save
            </button>
          </div>
        </div>
        <div className="Company-Background">
          <div className="Page">
            <DetailsHeader
              source="company"
              key={companydata.id}
              id={companydata.id}
              name={companydata.name}
              logoImage={companydata.logoImage}
              isActive={isActive}
            />
            {editCompanyRole && (
              <button
                className="edit-button"
                onClick={() => {
                  updateInformation();
                }}
              >
                Edit
              </button>
            )}

            {deleteCompanyRole && (
              <button
                className="delete-button"
                onClick={() => {
                  onDelete();
                }}
              >
                Delete
              </button>
            )}

            <Tabs
              defaultActiveKey="company"
              className="mb-3"
              style={{ marginTop: "3%" }}
            >
              <Tab
                className="company-information-tab"
                eventKey="company"
                title="Company Information"
              >
                <InformationTab
                  industry={industry}
                  description={description}
                  shortName={shortName}
                />
                <ContactDetails display="company" />
              </Tab>
              <Tab
                className="product-information-tab"
                eventKey="product"
                title="Product Information"
              >
                <Row>
                  <Col lg="4">
                    <InputGroup>
                      <Form.Control
                        className="search"
                        placeholder="search product name"
                        onChange={(e) => {
                          setSearchResult(e.target.value);
                        }}
                      />
                    </InputGroup>
                    <div style={{ marginBottom: "5%" }}></div>
                    <Tab.Container
                      id="left-tabs-example"
                      defaultActiveKey="first"
                    >
                      <Row>
                        <Col>
                          <Nav variant="pills" className="flex-column">
                            <Nav.Item>All Products</Nav.Item>
                            <Nav.Item>
                              {products
                                .filter((data) => {
                                  const searchLowerCase =
                                    searchResult.toLowerCase();
                                  const dataNameLowerCase =
                                    data.name.toLowerCase();
                                  return (
                                    searchLowerCase === "" ||
                                    dataNameLowerCase.includes(searchLowerCase)
                                  );
                                })
                                .map((product) => (
                                  <Nav.Link
                                    key={product.id}
                                    eventKey={product.id}
                                    onClick={() => handleProduct(product.id)}
                                    active={activeProduct === product.id}
                                    style={{
                                      backgroundColor:
                                        activeProduct === product.id
                                          ? "rgb(42,57,126)"
                                          : "",
                                      color:
                                        activeProduct === product.id
                                          ? "white"
                                          : "rgb(42,57,126)",
                                    }}
                                  >
                                    {product.name}
                                  </Nav.Link>
                                ))}
                            </Nav.Item>
                          </Nav>
                        </Col>
                      </Row>
                    </Tab.Container>
                  </Col>

                  <Col>
                    {newProductRole && (
                      <button
                        className="new-product-button"
                        onClick={newProduct}
                        style={{
                          backgroundColor: "rgb(47, 52, 126)",
                        }}
                      >
                        + Product
                      </button>
                    )}
                    {/* {flag && editProductRole && (
                      <button
                        className="product-edit-button"
                        onClick={() => {
                          onUpdateProduct();
                        }}
                      >
                        Edit
                      </button>
                    )}

                    {flag && deleteProductRole && (
                      <button
                        className="product-delete-button"
                        onClick={() => {
                          onDeleteProduct();
                        }}
                      >
                        Delete
                      </button>
                    )} */}

                    <ProductDetails
                      object={product}
                      id={activeProduct}
                    ></ProductDetails>
                  </Col>
                </Row>
              </Tab>
            </Tabs>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CompanyDetails;
