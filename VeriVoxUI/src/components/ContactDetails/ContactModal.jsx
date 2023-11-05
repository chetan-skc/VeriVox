import React, { useState } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Modal, Button, Form, Col, Row } from "react-bootstrap";
import { useLocation } from "react-router-dom";
import { useEffect } from "react";

const ContactModal = ({
  show,
  handleClose,
  handleSave,
  Message,
  ContactAdded,
}) => {
  const [messageModalVisible, setMessageModalVisible] = useState(false);

  const location = useLocation();
  const { id } = location.state || {};

  const [formData, setFormData] = useState({
    FirstName: "",
    LastName: "",
    EmailId: "",
    Designation: "",
    IsAdmin: false,
    CompanyId: id,
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleCheckboxChange = (e) => {
    const { name, checked } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: checked,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    handleSave(formData);
    setMessageModalVisible(true);
  };
  const handlePopup = (e) => {
    setMessageModalVisible(false);
  };
  useEffect(() => {
    let timer;
    if (messageModalVisible) {
      timer = setTimeout(() => {
        setMessageModalVisible(false);
      }, 2000);
    }

    return () => {
      clearTimeout(timer);
    };
  }, [messageModalVisible]);

  return (
    <>
      <Modal show={show} onHide={handleClose}>
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
                    value={formData.FirstName}
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
                    value={formData.LastName}
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
                    value={formData.Designation}
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
                    value={formData.EmailId}
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
                checked={formData.IsAdmin}
                onChange={handleCheckboxChange}
              />
            </Form.Group>
            <div style={{ marginLeft: "57%" }}>
              <button
                onClick={handleClose}
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
      {messageModalVisible && ContactAdded && (
        <div class="alert alert-success">
          <strong>Success! </strong> {Message}
          <button
            type="button"
            className="close"
            onClick={handlePopup}
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
      {messageModalVisible && !ContactAdded && (
        <div class="alert alert-info">
          <strong>Info! </strong> {Message}
          <button
            type="button"
            className="close"
            onClick={handlePopup}
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
    </>
  );
};

export default ContactModal;
