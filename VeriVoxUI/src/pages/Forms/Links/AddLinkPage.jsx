import React, { useState, useEffect } from "react";
import deleteIcon from "./assets/page2.svg";
import copyImage from "./assets/copy.svg";
import ApiInterceptor from "../../LoginPage/ApiInterceptor";
import { useSelector } from "react-redux";

const AddLinkPage = ({ onClose, productId, formId, companyName, companyshort, productName, productshort, formName }) => {
    const [apiValue, setApiValue] = useState("");
    const [copySuccessMessage, setCopySuccessMessage] = useState(null);
    const [descriptionError, setDescriptionError] = useState("");
    const role = useSelector((state) => state.user.userRole);

    const showCopySuccessMessage = () => {
        setCopySuccessMessage("Link Copied Successfully");
        setTimeout(() => {
            setCopySuccessMessage(null);
        }, 1000);
    };

    const [linkData, setLinkData] = useState([
        {
            productId,
            formId,
            description: "",
            value: apiValue,
            responseLimit: 0
        },
    ]);

    console.log(apiValue);

    useEffect(() => {
        fetchApiString();
    }, []);

    const fetchApiString = async () => {
        try {
            const response = await fetch("https://localhost:7199/api/Link", {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                },
            });

            if (response.ok) {
                const data = await response.text();
                setApiValue(data);
            } else {
                throw new Error("Failed to fetch value from the API");
            }
        } catch (error) {
            console.error("Error fetching value from the API:", error);
        }
    };

    const addLinkPair = () => {
        const newapiValue = fetchApiString();

        const newLink = {
            productId,
            formId,
            description: "",
            value: newapiValue,
            responseLimit: 0
        };

        setLinkData((prevData) => [...prevData, newLink]);
    };

    const [showLinkDetails, setShowLinkDetails] = useState(false);

    const handleSubmit = async () => {
        if (linkData.some(link => !link.description)) {
            setDescriptionError("Please enter a description for your links.");
        } else {
            setDescriptionError("");
            try {
                setShowLinkDetails(true);
                const apiUrl = "https://localhost:7199/api/Link";
                const postLinks = await ApiInterceptor(apiUrl, "POST", linkData);
                console.log("Links registered!", linkData);
            } catch (error) {
                console.error("Error adding links:", error);
            }
        }
    };

    const handleDescriptionChange = (value, index) => {
        const updatedData = [...linkData];
        updatedData[index].description = value;
        updatedData[index].value = apiValue;
        setLinkData(updatedData);
        setDescriptionError("");
    };

    const handleDeletePair = (index) => {
        const updatedData = [...linkData];
        updatedData.splice(index, 1);
        setLinkData(updatedData);
    };

    const pageURL = "http://localhost:3000";

    return (
        <div>
            {showLinkDetails ? (
                <div style={{ height: "600px" }}>
                    <h2>Links Created</h2>
                    <br />
                    {copySuccessMessage && (
                        <div className="alert alert-success mt-2" role="alert">
                            {copySuccessMessage}
                        </div>
                    )}
                    <p style={{ marginBottom: "25px", opacity: "0.7" }}>{`You can copy the links of ‘${formName}’ form`}</p>
                    {linkData.map((link, index) => (
                        <div key={index} style={{ display: "flex" }}>
                            <input style={{ marginLeft: "10px", border: "none", width: "50%", overflow: "hidden", textOverflow: "ellipsis" }} value={`${pageURL}/${companyshort}/${productshort}/${formName}/${link.value}`} readOnly
                                onMouseEnter={(e) => (e.target.style.overflow = "visible", e.target.style.textOverflow = "none")}
                                onMouseLeave={(e) => (e.target.style.overflow = "hidden", e.target.style.textOverflow = "ellipsis")}
                            />
                            <input style={{ width: "40%", height: "50px", overflow: "hidden", border: "none", textOverflow: "ellipsis" }} value={link.description} readOnly
                                onMouseEnter={(e) => (e.target.style.overflow = "visible", e.target.style.textOverflow = "none")}
                                onMouseLeave={(e) => (e.target.style.overflow = "hidden", e.target.style.textOverflow = "ellipsis")}
                            />
                            <div onClick={() => {
                                navigator.clipboard.writeText(`${pageURL}/${companyshort}/${productshort}/${formName}/${link.value}`);
                                showCopySuccessMessage();
                            }} style={{ width: "20%", marginRight: "20px", marginTop: "10px", cursor: "pointer", display: "flex" }}>
                                <img style={{ width: "20%", height: "40%" }} src={copyImage} alt="Copy" />
                                <p style={{ marginLeft: "5px", color: "#354E96" }}>Copy link</p>
                            </div>
                            <br />
                        </div>
                    ))}
                    <button onClick={onClose} style={{ float: "right", marginRight: "20px", padding: "13px 20px", gap: "10px", borderRadius: "8px", border: "1.5px solid #354E96", backgroundColor: "#354E96", color: "white" }}>Done</button>
                </div>
            ) : (
                <div style={{ height: "600px" }}>
                    <div className="input-row" style={{ display: "flex", alignItems: "center" }}>
                        <div className="input-half" style={{ border: "none", width: "50%", opacity: "0.7" }}>
                            <label htmlFor="company">Company:</label>
                            <br />
                            <input type="text" id="company" style={{ opacity: "0.7" }} value={companyName} readOnly />
                        </div>
                        <div className="input-half" style={{ border: "none", width: "50%", opacity: "0.7" }}>
                            <label htmlFor="product">Product:</label>
                            <br />
                            <input type="text" id="product" style={{ opacity: "0.7" }} value={productName} readOnly />
                        </div>
                    </div>
                    <br />
                    <div className="input-row" style={{ opacity: "0.7" }}>
                        <label htmlFor="form">Form URL text</label>
                        <br />
                        <input type="text" style={{ border: "none", width: "100%", opacity: "0.7" }} id="form" value={`${companyshort}/${productshort}/${formName}`} readOnly />
                    </div>
                    <hr />
                    <h4 style={{ paddingLeft: "10px" }}>
                        Link Generated
                    </h4>
                    <br />

                    {descriptionError && (
                        <p style={{ color: "red", flexDirection: "verticle" }}>{descriptionError}</p>
                    )}

                    {linkData.map((link, index) => (
                        <div className="input-row" key={index} style={{ display: "flex", alignItems: "center" }}>

                            <div className="input-1" style={{ width: "1%" }}>
                                <input
                                    style={{ width: "50%", border: "none" }}
                                    type="text"
                                    id={`value_${index}`}
                                    value={linkData.value}
                                    readOnly
                                />
                            </div>
                            <div className="input-19" style={{ width: "40%", display: "flex" }}>
                                <input
                                    style={{ width: "100%", border: "none", textOverflow: "ellipsis" }}
                                    type="text"
                                    value={`${pageURL}/${companyshort}/${productshort}/${formName}/${apiValue}`}
                                    readOnly
                                    onMouseEnter={(e) => (e.target.style.overflow = "visible", e.target.style.textOverflow = "none")}
                                    onMouseLeave={(e) => (e.target.style.overflow = "hidden", e.target.style.textOverflow = "ellipsis")}
                                />
                            </div>

                            <div className="input-80" style={{ width: "100%", display: "flex" }}>
                                <input
                                    style={{ width: "100%", paddingBottom: "20px", borderRadius: "8px", border: "1px solid #C9C9D0" }}
                                    type="text"
                                    id={`description_${index}`}
                                    value={link.description}
                                    onChange={(e) => handleDescriptionChange(e.target.value, index)}
                                    placeholder="Add Link description"
                                />
                                <img
                                    src={deleteIcon}
                                    alt="Delete"
                                    style={{ cursor: "pointer" }}
                                    onClick={() => handleDeletePair(index)}
                                />

                            </div>
                        </div>
                    ))}
                    <button onClick={addLinkPair} style={{ backgroundColor: "white", border: "none", color: "#354E96" }}>+ Create Another Link</button>
                    <button onClick={handleSubmit} style={{ float: "right", marginTop: "40px", backgroundColor: "#354E96", borderRadius: "6px", border: "1.5px solid #354E96", color: "white", gap: "10px" }}>Save Link</button>
                </div>
            )}
        </div>
    );
};

export default AddLinkPage;