import React, { useState } from "react";
import { Modal, Button, Form, Col, Row } from "react-bootstrap";
import ApiInterceptor from "../../pages/LoginPage/ApiInterceptor";
import { useLocation } from "react-router-dom";
import { useEffect } from "react";
import { setProduct } from "../../Slices/CompanyDetailSlice";

const ProductContactDetails = (props) => {
  const [productId, setproductId] = useState(props.id);
  useEffect(() => {
    setproductId(props.id);
  });

  const [ShowAddContactPopup, setShowAddContactPopup] = useState(false);
  const [showSuccessPopup, setshowSuccessPopup] = useState(false);
  const [NoContacts, setNoContacts] = useState(false);
  const [SuccessMessage, setSuccessMessage] = useState("");
  const [productContacts, setproductContacts] = useState([]);
  const [EmailofContactToBeEdited, setEmailofContactToBeEdited] = useState("");
  const [showEditPopup, setshowEditPopup] = useState(false);
  const [ContactAdded, setContactAdded] = useState(false);
  const [ContactDeleted, setContactDeleted] = useState(false);
  const [ContactEdited, setContactEdited] = useState(false);
  const [flag, setflag] = useState(false);
  const [NoOfContacts, setNoOfContacts] = useState(0);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(3);
  const location = useLocation();
  const { id } = location.state || {};
  const [AddContactData, setAddContactData] = useState({
    FirstName: "",
    LastName: "",
    Designation: "",
    EmailId: "",
    IsAdmin: false,
    CompanyId: id,
    ProductId: productId,
  });
  const [EditContactData, setEditContactData] = useState({
    FirstName: "",
    LastName: "",
    Designation: "",
    Email: "",
    IsAdmin: false,
  });

  const AddProductContact = async () => {
    AddContactData.ProductId = productId;
    const apiUrl = `https://localhost:7199/api/User/createProductContact`;
    try {
      const addContact = await ApiInterceptor(apiUrl, "POST", AddContactData);
      setshowSuccessPopup(true);
      setShowAddContactPopup(false);
      setSuccessMessage(addContact);
      if (addContact === "Contact Added Successfully!") {
        setContactAdded(true);
        setflag(true);
      } else if (addContact === "This Contact Already Exists!") {
        setflag(true);
      }
    } catch (error) {
      throw error;
    }
  };

  useEffect(() => {
    if (productId) {
      const GetProductsContact = async () => {
        const apiUrl = `https://localhost:7199/api/User/GetProductContact?productId=${productId}&page=${page}&pageSize=${pageSize}`;
        try {
          const contacts = await ApiInterceptor(apiUrl, "GET");

          setproductContacts(contacts.data);
          setNoOfContacts(contacts.totalRecords);
          setPageSize(contacts.pageSize);
          setPage(contacts.page);
          if (contacts.data.length === 0) {
            setNoContacts(true);
          } else {
            setNoContacts(false);
          }
        } catch (error) {
          console.log("Error fetching contacts", error);
          throw error;
        }
      };
      GetProductsContact();
    }
  }, [productId, page, pageSize]);

  const handleDeleteContact = async (email) => {
    const apiUrl = `https://localhost:7199/api/User/deleteContact?email=${email}`;
    try {
      const deleteContact = await ApiInterceptor(apiUrl, "PUT");
      setSuccessMessage(deleteContact);
      setshowSuccessPopup(true);
      if (deleteContact === "Contact Deleted!") {
        setContactDeleted(true);
      }
    } catch (error) {
      console.log("Error deleting contact", error);
      throw error;
    }
  };

  const handleEditPopup = (contacts) => {
    setEditContactData({
      FirstName: contacts.firstName,
      LastName: contacts.lastName,
      Designation: contacts.designation,
      EmailId: contacts.emailId,
    });
    setEmailofContactToBeEdited(contacts.emailId);
    setshowEditPopup(!showEditPopup);
  };

  const handleEditContact = async () => {
    const apiUrl = `https://localhost:7199/api/User/editContact?email=${EmailofContactToBeEdited}`;
    try {
      const editContact = await ApiInterceptor(apiUrl, "PUT", EditContactData);
      setSuccessMessage(editContact);
      setshowSuccessPopup(true);
      setshowEditPopup(false);
      if (editContact === "Contact Edited!") {
        setContactEdited(true);
      }
    } catch (error) {
      throw error;
    }
  };
  const handleEditChange = (e) => {
    const { name, value } = e.target;
    setEditContactData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleEditCheckboxChange = (e) => {
    const { name, checked } = e.target;
    setEditContactData((prevData) => ({
      ...prevData,
      [name]: checked,
    }));
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setAddContactData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleCheckboxChange = (e) => {
    const { name, checked } = e.target;
    setAddContactData((prevData) => ({
      ...prevData,
      [name]: checked,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    AddProductContact();
  };
  const handleEditSubmit = (e) => {
    e.preventDefault();
    handleEditContact();
  };

  const handleAddContactPopup = (e) => {
    e.preventDefault();
    setShowAddContactPopup(!ShowAddContactPopup);
  };

  const handleSuccessPopup = () => {
    setshowSuccessPopup(!showSuccessPopup);
  };

  useEffect(() => {
    let timer;
    if (showSuccessPopup) {
      timer = setTimeout(() => {
        setshowSuccessPopup(false);
      }, 2000);
    }

    return () => {
      clearTimeout(timer);
    };
  }, [showSuccessPopup]);

  return (
    <div style={{ marginTop: "10%" }}>
      <Row>
        <Col lg="6">
          <label style={{ fontWeight: "700" }}>
            <h5>Contact Details</h5>
          </label>

          <p style={{ color: "GrayText", marginTop: "0px" }}>
            Please provide the contact information here
          </p>
        </Col>
        <Col lg="5">
          <button
            type="onClick"
            style={{
              borderStyle: "solid",
              borderColor: "rgb(42,57,126)",
              outline: "none",
              color: "rgb(42,57,126)",
              borderRadius: "4px",
              backgroundColor: "white",
              marginLeft: "88%",
              marginTop: "0",
            }}
            onClick={handleAddContactPopup}
          >
            +Contact
          </button>
          {/* For Contact Addition */}
          {showSuccessPopup && ContactAdded && (
            <div class="alert alert-success">
              <strong>Success! </strong> {SuccessMessage}
              <button
                type="button"
                className="close"
                onClick={handleSuccessPopup}
                style={{
                  marginLeft: "33%",
                  borderRadius: "5px",
                  color: "white",
                  backgroundColor: "green",
                }}
              >
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
          )}
          {showSuccessPopup && !ContactAdded && flag && (
            <div class="alert alert-info">
              <strong>Info! </strong> {SuccessMessage}
              <button
                type="button"
                className="close"
                onClick={handleSuccessPopup}
                style={{
                  marginLeft: "33%",
                  borderRadius: "5px",
                  color: "white",
                  backgroundColor: "skyblue",
                }}
              >
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
          )}
          {/* For Contact Delete */}
          {showSuccessPopup && ContactDeleted && (
            <div class="alert alert-danger">
              <strong>Deleted! </strong> {SuccessMessage}
              <button
                type="button"
                className="close"
                onClick={handleSuccessPopup}
                style={{
                  marginLeft: "33%",
                  borderRadius: "5px",
                  color: "white",
                  backgroundColor: "red",
                }}
              >
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
          )}

          {/* For Contact edit */}
          {showSuccessPopup && ContactEdited && (
            <div class="alert alert-success">
              <strong>Success! </strong> {SuccessMessage}
              <button
                type="button"
                className="close"
                onClick={handleSuccessPopup}
                style={{
                  marginLeft: "33%",
                  borderRadius: "5px",
                  color: "white",
                  backgroundColor: "green",
                }}
              >
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
          )}
        </Col>
      </Row>
      <Modal show={showEditPopup}>
        <Modal.Header>
          <Modal.Title style={{ paddingLeft: "30%" }}>Edit Contact</Modal.Title>
        </Modal.Header>
        <Modal.Body style={{ paddingTop: "0%", paddingLeft: "10%" }}>
          <Form onSubmit={handleEditSubmit}>
            <Row>
              <Col>
                <Form.Group>
                  <Form.Label>
                    First Name<span style={{ color: "red" }}>*</span>
                  </Form.Label>
                  <Form.Control
                    type="text"
                    placeholder="enter first name"
                    name="FirstName"
                    value={EditContactData.FirstName}
                    onChange={handleEditChange}
                    required
                  />
                </Form.Group>
              </Col>
              <Col>
                <Form.Group>
                  <Form.Label>
                    Last Name<span style={{ color: "red" }}>*</span>
                  </Form.Label>
                  <Form.Control
                    type="text"
                    placeholder="enter last name"
                    name="LastName"
                    value={EditContactData.LastName}
                    onChange={handleEditChange}
                    required
                  />
                </Form.Group>
              </Col>
            </Row>
            <br />
            <Row>
              <Col>
                <Form.Group>
                  <Form.Label>
                    Designation<span style={{ color: "red" }}>*</span>
                  </Form.Label>
                  <Form.Control
                    type="text"
                    placeholder="enter designation"
                    name="Designation"
                    value={EditContactData.Designation}
                    onChange={handleEditChange}
                    required
                  />
                </Form.Group>
              </Col>
              <Col>
                <Form.Group>
                  <Form.Label>
                    Email<span style={{ color: "red" }}>*</span>
                  </Form.Label>
                  <Form.Control
                    type="email"
                    placeholder="enter email"
                    name="EmailId"
                    value={EditContactData.EmailId}
                    onChange={handleEditChange}
                    required
                  />
                </Form.Group>
              </Col>
            </Row>
            <Form.Group>
              <Form.Check
                type="checkbox"
                label="Assign as admin"
                name="IsAdmin"
                checked={EditContactData.IsAdmin}
                onChange={handleEditCheckboxChange}
              />
            </Form.Group>
            <div style={{ marginLeft: "57%" }}>
              <button
                type="button"
                onClick={handleEditPopup}
                style={{
                  border: "none",
                  color: "rgb(42,57,126)",
                  borderRadius: "4px",
                }}
                onMouseEnter={(e) => {
                  e.target.style.backgroundColor = "rgb(42,57,126)";
                  e.target.style.color = "white";
                }}
                onMouseLeave={(e) => {
                  e.target.style.backgroundColor = "white";
                  e.target.style.color = "rgb(42,57,126)";
                }}
              >
                Cancel
              </button>
              <button
                type="submit"
                style={{
                  marginLeft: "1%",
                  border: "none",
                  color: "rgb(42,57,126)",
                  borderRadius: "4px",
                }}
                onMouseEnter={(e) => (
                  (e.target.style.backgroundColor = "rgb(42,57,126)"),
                  (e.target.style.color = "white")
                )}
                onMouseLeave={(e) => (
                  (e.target.style.backgroundColor = "white"),
                  (e.target.style.color = "rgb(42,57,126)")
                )}
              >
                Save contact
              </button>
            </div>
          </Form>
        </Modal.Body>
      </Modal>
      <Modal show={ShowAddContactPopup}>
        <Modal.Header>
          <Modal.Title>Add New Contact</Modal.Title>
        </Modal.Header>
        <Modal.Body style={{ paddingTop: "0%" }}>
          <Form onSubmit={handleSubmit}>
            <Row>
              <Col>
                <Form.Group controlId="formFirstName">
                  <Form.Label>
                    First Name<span style={{ color: "red" }}>*</span>
                  </Form.Label>
                  <Form.Control
                    type="text"
                    placeholder="Enter First Name"
                    name="FirstName"
                    value={AddContactData.FirstName}
                    onChange={handleChange}
                    required
                  />
                </Form.Group>
              </Col>
              <Col>
                <Form.Group controlId="formLastName">
                  <Form.Label>
                    Last Name<span style={{ color: "red" }}>*</span>
                  </Form.Label>
                  <Form.Control
                    type="text"
                    placeholder="Enter Last Name"
                    name="LastName"
                    value={AddContactData.LastName}
                    onChange={handleChange}
                    required
                  />
                </Form.Group>
              </Col>
            </Row>
            <br />
            <Row>
              <Col>
                <Form.Group controlId="formDesignation">
                  <Form.Label>
                    Designation<span style={{ color: "red" }}>*</span>
                  </Form.Label>
                  <Form.Control
                    type="text"
                    placeholder="Enter Designation"
                    name="Designation"
                    value={AddContactData.Designation}
                    onChange={handleChange}
                    required
                  />
                </Form.Group>
              </Col>
              <Col>
                <Form.Group controlId="formEmail">
                  <Form.Label>
                    Email<span style={{ color: "red" }}>*</span>
                  </Form.Label>
                  <Form.Control
                    type="email"
                    placeholder="Enter Email"
                    name="EmailId"
                    value={AddContactData.EmailId}
                    onChange={handleChange}
                    required
                  />
                </Form.Group>
              </Col>
            </Row>
            <Form.Group controlId="formIsAdmin">
              <Form.Check
                type="checkbox"
                label="Assign as admin"
                name="IsAdmin"
                value={AddContactData.IsAdmin}
                onChange={handleCheckboxChange}
              />
            </Form.Group>
            <div style={{ marginLeft: "57%" }}>
              <button
                type="button"
                onClick={handleAddContactPopup}
                style={{
                  border: "none",
                  color: "rgb(42,57,126)",
                  borderRadius: "4px",
                }}
                onMouseEnter={(e) => (
                  (e.target.style.backgroundColor = "rgb(42,57,126)"),
                  (e.target.style.color = "white")
                )}
                onMouseLeave={(e) => (
                  (e.target.style.backgroundColor = "white"),
                  (e.target.style.color = "rgb(42,57,126)")
                )}
              >
                Cancel
              </button>
              <button
                type="submit"
                style={{
                  marginLeft: "1%",
                  border: "none",
                  color: "rgb(42,57,126)",
                  borderRadius: "4px",
                }}
                onMouseEnter={(e) => {
                  e.target.style.backgroundColor = "rgb(42,57,126)";
                  e.target.style.color = "white";
                }}
                onMouseLeave={(e) => {
                  e.target.style.backgroundColor = "white";
                  e.target.style.color = "rgb(42,57,126)";
                }}
              >
                Save contact
              </button>
            </div>
          </Form>
        </Modal.Body>
      </Modal>

      {!NoContacts ? (
        <div>
          <Row
            style={{
              backgroundColor: "rgb(236, 238, 243)",
              height: "40px",
              fontWeight: "600",
              color: "rgb(42,57,126)",
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              marginLeft: "0.05%",
              marginRight: "0.05%",
              textAlign: "center",
            }}
          >
            <Col lg="3">Contact Name</Col>
            <Col lg="3">Designation</Col>
            <Col lg="4">Email Address</Col>
            <Col lg="2">Actions</Col>
          </Row>

          <hr
            style={{
              color: "rgb(42,57,126)",
              marginTop: "0px",
            }}
          />
          {productContacts.map((contacts) => (
            <Row
              key={contacts.id}
              style={{
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                marginLeft: "0.05%",
                marginRight: "0.05%",
                marginBottom: "2%",
                textAlign: "center",
              }}
            >
              <Col lg="3">{contacts.firstName + " " + contacts.lastName}</Col>
              <Col lg="3">{contacts.designation}</Col>
              <Col lg="4">{contacts.emailId}</Col>
              <Col lg="2">
                <i
                  class="fa-solid fa-pencil"
                  onMouseEnter={(e) => {
                    e.target.style.cursor = "pointer";
                  }}
                  onClick={() => {
                    handleEditPopup(contacts);
                  }}
                ></i>
                <i
                  class="fa-solid fa-trash"
                  style={{ marginLeft: "10%" }}
                  onMouseEnter={(e) => {
                    e.target.style.cursor = "pointer";
                  }}
                  onClick={() => {
                    handleDeleteContact(contacts.emailId);
                  }}
                ></i>
              </Col>
            </Row>
          ))}
          <div style={{ display: "flex", justifyContent: "flex-end" }}>
            <button
              type="button"
              className="pagination-button"
              onClick={() => setPage(page - 1)}
              disabled={page === 1}
            >
              {"<"}
            </button>
            <span style={{ marginTop: ".8%" }}>{page}</span>
            <button
              type="button"
              className="pagination-button"
              onClick={() => setPage(page + 1)}
              disabled={page * pageSize >= NoOfContacts}
            >
              {">"}
            </button>
          </div>
        </div>
      ) : (
        <h5>No Contact Found!</h5>
      )}
    </div>
  );
};

export default ProductContactDetails;
