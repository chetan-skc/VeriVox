import React, { useEffect } from "react";
import { Col, Row } from "react-bootstrap";
import ApiInterceptor from "../../pages/LoginPage/ApiInterceptor";
import ContactModal from "./ContactModal";
import { useState } from "react";
import { useLocation } from "react-router-dom";
import "../../pages/Companies/Companies.css";
import { Modal, Button, Form } from "react-bootstrap";

const ContactDetails = (props) => {
  const [showModal, setShowModal] = useState(false);
  const [Message, setMessage] = useState("");
  const [contactsData, setcontactsData] = useState([]);
  const [forbidMultipleRequest, setforbidMultipleRequest] = useState(true);
  const [NoContacts, setNoContacts] = useState(false);
  const [deleteContactMessage, setdeleteContactMessage] = useState("");
  const [editContactMessage, seteditContactMessage] = useState("");
  const [showPopup, setshowPopup] = useState(false);
  const [showEditPopup, setshowEditPopup] = useState(false);
  const [UserEmailTobeDeleted, setUserEmailTobeDeleted] = useState("");
  const [ContactAdded, setContactAdded] = useState(false);
  const [NoOfContacts, setNoOfContacts] = useState(0);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(3);
  const [ContactDeleted, setContactDeleted] = useState(false);
  const [ContactEdited, setContactEdited] = useState(false);
  const [EditContactData, setEditContactData] = useState({
    FirstName: "",
    LastName: "",
    Designation: "",
    Email: "",
    IsAdmin: false,
  });
  const location = useLocation();
  const { id } = location.state || {};

  const handleContact = () => {
    setShowModal(true);
  };

  const handleClose = () => {
    setShowModal(false);
  };

  const handleSave = async (formData) => {
    const apiUrl = `https://localhost:7199/api/User/CreateContact`;
    try {
      const addedContact = await ApiInterceptor(apiUrl, "POST", formData);
      setMessage(addedContact);
      setShowModal(false);
      if (addedContact === "Contact Added Successfully!") {
        setContactAdded(true);
      }
    } catch (error) {
      console.log("Error Saving details", error);
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
    setUserEmailTobeDeleted(contacts.emailId);
    setshowEditPopup(!showEditPopup);
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEditContactData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleCheckboxChange = (e) => {
    const { name, checked } = e.target;
    setEditContactData((prevData) => ({
      ...prevData,
      [name]: checked,
    }));
  };

  const handleDeleteContact = async (email) => {
    const apiUrl = `https://localhost:7199/api/User/deleteContact?email=${email}`;
    try {
      const deleteContact = await ApiInterceptor(apiUrl, "PUT");
      setdeleteContactMessage(deleteContact);
      setshowPopup(true);
      if (deleteContact === "Contact Deleted!") {
        setContactDeleted(true);
      }
    } catch (error) {
      console.log("Error deleting contact", error);
      throw error;
    }
  };

  const handleEditContact = async () => {
    const apiUrl = `https://localhost:7199/api/User/editContact?email=${UserEmailTobeDeleted}`;
    try {
      const editContact = await ApiInterceptor(apiUrl, "PUT", EditContactData);
      setdeleteContactMessage(editContact);
      setshowPopup(true);
      setshowEditPopup(false);
      if (editContact === "Contact Edited!") {
        setContactEdited(true);
      }
    } catch (error) {
      throw error;
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    handleEditContact();
  };

  useEffect(() => {
    const fetchContacts = async () => {
      const apiUrl = `https://localhost:7199/api/User?companyId=${id}&page=${page}&pageSize=${pageSize}`;

      try {
        const contacts = await ApiInterceptor(apiUrl, "GET");
        setcontactsData(contacts.data);
        setNoOfContacts(contacts.totalRecords);
        setPageSize(contacts.pageSize);
        setPage(contacts.page);
        if (contacts.data.length === 0) {
          setNoContacts(true);
        }
      } catch (error) {
        console.log("Error fetching contacts", error);
        throw error;
      }
    };

    fetchContacts();
  }, [page, pageSize]);

  useEffect(() => {
    let timer;
    if (showPopup) {
      timer = setTimeout(() => {
        setshowPopup(false);
      }, 2000);
    }

    return () => {
      clearTimeout(timer);
    };
  }, [showPopup]);

  return (
    <div>
      <Modal show={showEditPopup}>
        <Modal.Header>
          <Modal.Title style={{ paddingLeft: "30%" }}>Edit Contact</Modal.Title>
        </Modal.Header>
        <Modal.Body style={{ paddingTop: "0%", paddingLeft: "10%" }}>
          <Form onSubmit={handleSubmit}>
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
                    onChange={handleChange}
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
                    onChange={handleChange}
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
                    onChange={handleChange}
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
                    onChange={handleChange}
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
                onChange={handleCheckboxChange}
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
              >
                Save contact
              </button>
            </div>
          </Form>
        </Modal.Body>
      </Modal>

      <Row style={{ marginTop: "2%" }}>
        <Col>
          <h3 className="input-detail">Contact Details</h3>
          <p style={{ color: "GrayText" }}>
            Please provide the necessary information about your {props.display}{" "}
            here
          </p>
        </Col>

        <Col style={{ marginTop: "2%" }}>
          <button
            type="onClick"
            onClick={handleContact}
            style={{
              borderStyle: "solid",
              borderColor: "rgb(42,57,126)",
              outline: "none",
              color: "rgb(42,57,126)",
              borderRadius: "4px",
              backgroundColor: "white",
              marginLeft: "80%",
              marginTop: "0",
            }}
          >
            +Contact
          </button>
          <ContactModal
            show={showModal}
            handleClose={handleClose}
            handleSave={handleSave}
            Message={Message}
            ContactAdded={ContactAdded}
          />
          {/* For delete contact message */}
          {showPopup && ContactDeleted && (
            <div class="alert alert-danger">
              <strong>Deleted! </strong> {deleteContactMessage}
              <button
                type="button"
                className="close"
                onClick={() => setshowPopup(false)}
                style={{
                  marginLeft: "48%",
                  borderRadius: "5px",
                  color: "white",
                  backgroundColor: "red",
                }}
              >
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
          )}
          {/* For edit contact message */}
          {showPopup && ContactEdited && (
            <div class="alert alert-primary">
              <strong>Edited! </strong> {deleteContactMessage}
              <button
                type="button"
                className="close"
                onClick={() => setshowPopup(false)}
                style={{
                  marginLeft: "55%",
                  borderRadius: "5px",
                  color: "white",
                  backgroundColor: "blue",
                }}
              >
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
          )}
        </Col>
      </Row>

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
            <Col lg="3">Email Address</Col>
            <Col lg="3">Actions</Col>
          </Row>

          <hr
            style={{
              color: "rgb(42,57,126)",
              marginTop: "0px",
            }}
          />

          {contactsData.map((contacts) => (
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
              <Col lg="3">{contacts.emailId}</Col>
              <Col lg="3">
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
        <div>
          <h3>No Contacts Found!</h3>
        </div>
      )}
    </div>
  );
};

export default ContactDetails;
