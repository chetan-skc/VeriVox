import React, { useEffect, useState } from "react";
import { Modal, Button, Dropdown } from "react-bootstrap";
import AddLinkPage from "./AddLinkPage";
import companyimg from "./assets/final1.svg";
import productimg from "./assets/icondesktopcomputer.svg";
import { Icon } from "react-icons-kit";
import { ic_clear_twotone } from 'react-icons-kit/md/ic_clear_twotone';
import { ic_fiber_manual_record } from "react-icons-kit/md/ic_fiber_manual_record";
import { ic_keyboard_arrow_down } from "react-icons-kit/md/ic_keyboard_arrow_down";
import copyImage from "./assets/copy.svg";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit, faTrash } from "@fortawesome/free-solid-svg-icons";
import { useSelector } from "react-redux";

const LinkListPopup = ({ formId, productId, onClose }) => {
  const [linkData, setLinkData] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchQuery, setSearchQuery] = useState("");
  const [formName, setFormName] = useState("");
  const [companyName, setCompanyName] = useState("");
  const [companyShort, setcompanyShort] = useState("");
  const [productName, setProductName] = useState("");
  const [productShort, setproductShort] = useState("");
  const [showAddLinkPage, setShowAddLinkPage] = useState(false);
  const [selectedLink, setSelectedLink] = useState(null);
  const [statusCard, setstatusCard] = useState(false);
  const [copySuccessMessage, setCopySuccessMessage] = useState(null);
  const [deleteSuccessMessage, setDeleteSuccessMessage] = useState(null);
  const [editDescription, setEditDescription] = useState("");

  const showCopySuccessMessage = () => {
    setCopySuccessMessage("Link Copied Successfully");
    setTimeout(() => {
      setCopySuccessMessage(null);
    }, 1000);
  };

  const showDeleteSuccessMessage = () => {
    setDeleteSuccessMessage("Link Deleted Successfully");
    setTimeout(() => {
      setDeleteSuccessMessage(null);
    }, 1000);
  };

  const fetchLinkData = async () => {
    try {
      const response = await fetch(`https://localhost:7199/api/Link/${formId}?id2=${productId}`);
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }
      const data = await response.json();
      setFormName(data[0].form);
      setCompanyName(data[0].company);
      setcompanyShort(data[0].companyshort);
      setProductName(data[0].product);
      setproductShort(data[0].productshort)
      return data;
    } catch (error) {
      console.error("Error fetching data:", error);
      return [];
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`https://localhost:7199/api/Form/${formId}`);
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        const formData = await response.json();
        setFormName(formData.name);

        const linkData = await fetchLinkData();
        setLinkData(linkData);
        setIsLoading(false);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, [formId, productId]);

  const pageURL = "http://localhost:3000";

  const handleDeleteLink = async (linkId) => {
    try {
      if (!linkId) {
        console.error(`Link with ID ${linkId} not found.`);
        return;
      }

      const response = await fetch(`https://localhost:7199/api/Link/${linkId}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }

      const updatedLinkData = await fetchLinkData();
      setLinkData(updatedLinkData);

      showDeleteSuccessMessage();
    } catch (error) {
      console.error("Error deleting link:", error);
    }
  };

  const handleAddLink = () => {
    setShowAddLinkPage(true);
  };

  const closeModals = () => {
    setShowAddLinkPage(false);
    onClose();
  };

  const handleStatus = (link) => {
    setSelectedLink(link);
    setstatusCard(true);
  };

  const handleCancelStatus = () => {
    setstatusCard(false);
    setSelectedLink(null);
  };

  const handleConfirmStatus = () => {
    const newStatus = !selectedLink.isActive;

    console.log(newStatus);

    const requestBody = {
      isActive: newStatus,
    };

    console.log(requestBody);

    fetch(`https://localhost:7199/api/Link/Linkstatechange/${selectedLink.id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(requestBody),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((data) => {
        selectedLink.isActive = data.isActive;

        const updatedLinkData = linkData.map((link) =>
          link.id === selectedLink.id ? { ...link, status: data.isActive } : link
        );

        setLinkData(updatedLinkData);
      })
      .then(() => {
        setstatusCard(false);
        setSelectedLink(null);
      })
      .catch((error) => {
        console.error("Error changing status of link:", error);
        alert("An error occurred while changing the status of the link. Please try again later.");
      });
  };

  const handleEditLink = (link) => {
    setSelectedLink(link);
    setEditDescription(link.description);
  };

  const handleSaveEdit = () => {
    if (selectedLink) {
      console.log("Selected Link:", selectedLink);
      console.log("Edit Description:", editDescription);

      const requestBody = {
        description: editDescription,
      };

      console.log("description:", requestBody.description);
      console.log("Request Body:", requestBody);

      fetch(`https://localhost:7199/api/Link/Linkdescriptionchange/${selectedLink.id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(requestBody),
      })
        .then((response) => {
          if (!response.ok) {
            throw new Error("Network response was not ok");
          }
          return response.json();
        })
        .then((data) => {
          selectedLink.description = data.description;

          const updatedLinkData = linkData.map((link) =>
            link.id === selectedLink.id ? { ...link, description: editDescription } : link
          );

          setLinkData(updatedLinkData);
          setEditDescription("");
          setSelectedLink(null);
        })
        .catch((error) => {
          console.error("Error updating link description:", error);
          alert("An error occurred while updating the link description. Please try again later.");
        });
    }
  };
  const role = useSelector((state) => state.user.userRole);
  console.log(typeof (role));

  return (
    <Modal show={true} onHide={onClose} animation={false} size="lg">
      <Modal.Header closeButton>
        <Modal.Title>{`${formName} - Form Links`}</Modal.Title>
      </Modal.Header>
      <Modal.Body style={{ maxHeight: "600px", overflowY: "auto" }}>
        {statusCard && (
          <div className="card" style={{ height: "300px", width: "500px", position: "fixed", top: "50%", left: "50%", transform: "translate(-50%, -50%)", borderRadius: "5%" }}>
            <div className="card-body text-center">
              <Icon className="mt-2" icon={ic_clear_twotone} size={60} style={{ color: 'rgb(47, 52, 126)', backgroundColor: 'rgb(198, 201, 244)', borderRadius: '50%' }} />
              <p className="fs-3 fw-bold mt-2">Change status to {selectedLink.isActive ? "Inactive" : "Active"} </p>
              <p>You're about to change status of this link to {selectedLink.isActive ? "Inactive" : "Active"} </p>
              <button className="btn btn-secondary btn-lg mx-2 col-4" onClick={handleCancelStatus}>Cancel</button>
              <button className="btn btn-danger btn-lg mx-2 col-4" onClick={handleConfirmStatus}>Yes</button>
            </div>
          </div>
        )}
        {copySuccessMessage && (
          <div className="alert alert-success mt-2" role="alert" style={{ position: "fixed", width: "auto" }}>
            {copySuccessMessage}
          </div>
        )}
        {deleteSuccessMessage && (
          <div className="alert alert-success mt-2" role="alert" style={{ position: "fixed", backgroundColor: "red", color: "white" }}>
            {deleteSuccessMessage}
          </div>
        )}
        <div style={{ display: "flex", alignItems: "center" }}>
          <div style={{ display: "flex", alignItems: "center" }}>
            <img src={companyimg} alt="company" style={{ marginRight: "5px" }} />
            <p>{`${companyName}`}</p>
          </div>
          <span>&nbsp;&nbsp;&nbsp;&nbsp;</span>
          <div style={{ display: "flex", alignItems: "center" }}>
            <img src={productimg} alt="product" style={{ marginRight: "5px" }} />
            <p>{`${productName}`}</p>
          </div>
        </div>
        <div style={{ display: "flex", alignItems: "center" }}>
          <input type="text" style={{ width: "50%", borderRadius: "6px" }} placeholder="🔍 Search form link by description" value={searchQuery} onChange={(e) => setSearchQuery(e.target.value)} />
          {role == '1' || role == '3' || role == '5' ? (
            <Button
              variant="primary"
              style={{
                marginLeft: "auto",
                color: "white",
                backgroundColor: "#354E96",
                marginRight: "20px",
              }}
              onClick={handleAddLink}
            >
              + Link
            </Button>
          ) : (
            <div> </div>
          )}
        </div>
        <br />
        <table className="table">
          <thead>
            <tr>
              <th style={{ backgroundColor: "#F1F3FF" }}>Link</th>
              <th style={{ backgroundColor: "#F1F3FF" }}>Description</th>
              <th style={{ backgroundColor: "#F1F3FF" }}>Status</th>
              <th style={{ backgroundColor: "#F1F3FF" }}>Actions</th>
            </tr>
          </thead>
          <tbody>
            {isLoading ? (
              <tr>
                <td colSpan="4">Loading...</td>
              </tr>
            ) : linkData.length === 0 ? (
              <tr>
                <td colSpan="4">No results found.</td>
              </tr>
            ) : (
              linkData
                .filter((data) => {
                  const searchLowerCase = searchQuery.toLowerCase();
                  const dataNameLowerCase = data.description?.toLowerCase();
                  return (
                    searchLowerCase === "" ||
                    (dataNameLowerCase && dataNameLowerCase.includes(searchLowerCase))
                  );
                })
                .map((linkItem) => (
                  <tr key={linkItem.id}>
                    <td style={{ display: "flex" }}>
                      <input
                        type="text"
                        style={{
                          width: "250px",
                          height: "70px",
                          display: "flex",
                          overflow: "hidden",
                          textOverflow: "ellipsis",
                          border: "none",
                        }}
                        value={`${pageURL}/${linkItem.companyshort}/${linkItem.productshort}/${linkItem.form}.${linkItem.value}`}
                        readOnly
                        onMouseEnter={(e) => (e.target.style.overflow = "visible", e.target.style.textOverflow = "none")}
                        onMouseLeave={(e) => (e.target.style.overflow = "hidden", e.target.style.textOverflow = "ellipsis")}
                      />

                      <img
                        src={copyImage}
                        alt="Copy"
                        style={{ float: "right", cursor: "pointer", width: "20px" }}
                        onClick={() => {
                          navigator.clipboard.writeText(`${pageURL}/${linkItem.companyshort}/${linkItem.productshort}/${linkItem.form}/${linkItem.value}`);
                          showCopySuccessMessage();
                        }}
                      />
                    </td>
                    <td style={{ width: "50%", maxHeight: "70px", overflow: "hidden", textOverflow: "ellipsis" }}>
                      {selectedLink === linkItem ? (
                        <div>
                          <input
                            type="text"
                            value={editDescription}
                            onChange={(e) => setEditDescription(e.target.value)}
                          />
                          <button style={{ padding: "5px 15px", gap: "10px", borderRadius: "8px", border: "1.5px solid #354E96", backgroundColor: "#354E96", color: "white" }} onClick={handleSaveEdit}>Save</button>
                        </div>
                      ) : (
                        <input type="text" style={{ height: "70px", overflow: "hidden", textOverflow: "ellipsis", border: "none" }} value={`${linkItem.description}`} readOnly
                          onMouseEnter={(e) => (e.target.style.overflow = "visible")}
                          onMouseLeave={(e) => (e.target.style.overflow = "hidden")}
                        />
                      )}
                    </td>
                    <td style={{ width: "40%" }}>
                      {role == '1' || role == '3' || role == '5' ? (
                        <div onClick={() => handleStatus(linkItem)} style={{ cursor: "pointer", marginTop: "17px" }}>
                          {linkItem.isActive ? (
                            <button className="btn btn-outline-success btn-sm" style={{ color: "green", backgroundColor: "transparent" }}>
                              <Icon icon={ic_fiber_manual_record} size={15} style={{ color: "green" }} /> Active <Icon icon={ic_keyboard_arrow_down} size={25} style={{ color: "grey" }} />
                            </button>
                          ) : (
                            <button className="btn btn-outline-danger btn-sm" style={{ color: "red", backgroundColor: "transparent" }}>
                              <Icon icon={ic_fiber_manual_record} size={15} style={{ color: "red" }} /> Inactive <Icon icon={ic_keyboard_arrow_down} size={25} style={{ color: "grey" }} />
                            </button>
                          )}
                        </div>
                      ) : (
                        <div disabled style={{ marginTop: "17px" }}>
                          {linkItem.isActive ? (
                            <button className="btn btn-outline-success btn-sm" style={{ color: "green", backgroundColor: "transparent" }}>
                              <Icon icon={ic_fiber_manual_record} size={15} style={{ color: "green" }} /> Active <Icon icon={ic_keyboard_arrow_down} size={25} style={{ color: "grey" }} />
                            </button>
                          ) : (
                            <button className="btn btn-outline-danger btn-sm" style={{ color: "red", backgroundColor: "transparent" }}>
                              <Icon icon={ic_fiber_manual_record} size={15} style={{ color: "red" }} /> Inactive <Icon icon={ic_keyboard_arrow_down} size={25} style={{ color: "grey" }} />
                            </button>
                          )}
                        </div>
                      )}
                      {selectedLink === linkItem}
                    </td>
                    <td>
                      {role == '1' || role == '3' || role == '5' ? (
                        <div className='fw-bold' style={{ marginTop: "10px" }}>
                          <Dropdown>
                            <Dropdown.Toggle variant="transparent" id="dropdown-basic">
                              ...
                            </Dropdown.Toggle>
                            <Dropdown.Menu>
                              <Dropdown.Item onClick={() => handleEditLink(linkItem)}>
                                <FontAwesomeIcon icon={faEdit} className="mr-2" /> Edit
                              </Dropdown.Item>
                              <Dropdown.Item onClick={() => handleDeleteLink(linkItem.id)}>
                                <FontAwesomeIcon icon={faTrash} className="mr-2" /> Delete
                              </Dropdown.Item>
                            </Dropdown.Menu>
                          </Dropdown>
                        </div>
                      ) : (
                        <div className='fw-bold' style={{ marginTop: "10px" }}>
                          <Dropdown>
                            <Dropdown.Toggle variant="transparent" id="dropdown-basic">
                              ...
                            </Dropdown.Toggle>
                            <Dropdown.Menu>
                              <Dropdown.Item disabled onClick={() => handleEditLink(linkItem)}>
                                <FontAwesomeIcon icon={faEdit} className="mr-2" /> Edit
                              </Dropdown.Item>
                              <Dropdown.Item disabled onClick={() => handleDeleteLink(linkItem.id)}>
                                <FontAwesomeIcon icon={faTrash} className="mr-2" /> Delete
                              </Dropdown.Item>
                            </Dropdown.Menu>
                          </Dropdown>
                        </div>
                      )}
                    </td>
                  </tr>
                ))
            )}
          </tbody>
        </table>
        {showAddLinkPage && (
          <Modal show={showAddLinkPage} onHide={closeModals} size="lg">
            <Modal.Header closeButton>
              <Modal.Title>
                <h3>New Link Creation</h3>
              </Modal.Title>
            </Modal.Header>
            <Modal.Body style={{ maxHeight: "600px", overflowY: "auto" }}>
              <AddLinkPage
                onClose={closeModals}
                productId={productId}
                formId={formId}
                companyName={companyName}
                companyshort={companyShort}
                productName={productName}
                productshort={productShort}
                formName={formName}
              />
            </Modal.Body>
          </Modal>
        )}
      </Modal.Body>
    </Modal>
  );
};

export default LinkListPopup;
