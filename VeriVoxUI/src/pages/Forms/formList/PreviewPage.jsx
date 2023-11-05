import React from "react";
import { Modal } from "react-bootstrap";
import star from "../Links/assets/Star.svg";

const FormPreviewPopup = ({ onClose, object }) => {

    return (
        <Modal show={true} onHide={onClose} animation={false} size="lg">
            <Modal.Header closeButton>
                <Modal.Title>{"Preview"}</Modal.Title>
            </Modal.Header>
            <Modal.Body style={{ maxHeight: "600px", overflowY: "auto" }}>
                <div>
                    {
                        <div key={object.id}>
                            <div style={{ display: "flex" }}>
                                {/* <img src={productlogo} style={{ height: "65px", width: "80px", border: "0.822px solid #C9C9D0", borderRadius: "6.573px" }} alt="Product Logo" /> */}
                                {/* <div style={{ display: "flex", flexDirection: "column", marginLeft: "20px" }}> */}
                                    <h2 style={{fontFamily: "Manrope", fontStyle: "normal"}}><strong>{object.name} </strong> </h2>
                                    <div style={{ display: "flex" }}>
                                        {/* <img src={companylogo} style={{ height: "30px", width: "40px" }} alt="Company Logo" /> */}
                                        {/* <p style={{ marginLeft: "7px", opacity: "0.7" }}>Created by <b>{company}</b></p> */}
                                    </div>
                                {/* </div> */}
                            </div>
                            <p style={{ opacity: "0.7" }}>
                                Please provide honest and detailed feedback to help us understand your experiences better. Fields marked with an asterisk (*) are mandatory, but we encourage you to complete the entire form.If you have multiple products to provide feedback on, please submit separate forms for each one.All information shared will be kept confidential and used solely for product improvement purposes.
                            </p>
                            <hr />
                            <br />
                            <ul style={{ listStyleType: "none" }}>
                                {object.formQuestion.map((question) => (
                                    <li key={question.questionNumber}>
                                        <p style={{ fontWeight: "630" }} > {question.questionText} </p>
                                        {(() => {
                                            switch (question.questionTypeId) {
                                                case 1:
                                                    return (
                                                        <input disabled type="text" placeholder={`Line (max. 100 chars only)`} style={{ width: "70%" }} />
                                                    );
                                                case 2:
                                                    return (
                                                        <input disabled type="number" />
                                                    );
                                                case 3:
                                                    return (
                                                        <textarea disabled rows="4" cols="50" placeholder={`Paragraph (max. 200 words only)`} style={{ width: "93%" }} />
                                                    );
                                                case 4:
                                                    return (
                                                        <div disabled class="rating">
                                                            <img src={star} alt="Star" />
                                                            <img src={star} alt="Star" />
                                                            <img src={star} alt="Star" />
                                                            <img src={star} alt="Star" />
                                                            <img src={star} alt="Star" />
                                                        </div>
                                                    );
                                                case 5:
                                                    return (
                                                        <select disabled>
                                                            {question.questionOption.map((option) => (
                                                                <option key={option.id} value={option.optionValue}>
                                                                    {option.optionText}
                                                                </option>
                                                            ))}
                                                        </select>
                                                    );
                                                case 6:
                                                    return (
                                                        <div disabled>
                                                            {question.questionOption.map((option) => (
                                                                <div key={option.id}>
                                                                    <input
                                                                        type="radio"
                                                                        id={option.optionValue}
                                                                        name={question.id}
                                                                        value={option.optionValue}
                                                                        style={{ marginRight: "10px" }}
                                                                    />
                                                                    <label htmlFor={option.optionValue}>{option.optionText}</label>
                                                                    <br />
                                                                </div>
                                                            ))}
                                                        </div>
                                                    );
                                                case 7:
                                                    return (
                                                        <input disabled type="text" placeholder="Start typing..." />

                                                    );
                                                case 8:
                                                    return (
                                                        <div disabled>
                                                            {question.questionOption.map((option) => (
                                                                <div key={option.id}>
                                                                    <input
                                                                        type="checkbox"
                                                                        id={option.optionValue}
                                                                        name={question.id}
                                                                        value={option.optionValue}
                                                                    />
                                                                    <label htmlFor={option.optionValue}>{option.optionText}</label>
                                                                </div>
                                                            ))}
                                                        </div>
                                                    );
                                                default:
                                                    return (
                                                        <div>
                                                        </div>
                                                    );
                                            }
                                        })()}
                                        <br />
                                        <br />
                                        <br />
                                    </li>
                                ))}
                            </ul>
                        </div>

                    }
                </div>
            </Modal.Body>
            <Modal.Footer>
                <div>
                    <button disabled style={{ border: "none", marginRight: "10px", color: "#354E96" }}>
                        Clear form
                    </button>
                    <button disabled style={{ paddingLeft: "35px", paddingRight: "35px", paddingTop: "5px", paddingBottom: "5px", borderRadius: "6px", backgroundColor: "#354E96", color: "white" }}>
                        Submit
                    </button>
                </div>
            </Modal.Footer>
        </Modal>
    );
};

export default FormPreviewPopup;
