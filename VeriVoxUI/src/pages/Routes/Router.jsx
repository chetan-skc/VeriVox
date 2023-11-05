import React, { useState, useEffect } from "react";
import "./RouteStyle.css";
import { Route, Routes } from "react-router-dom";
import LoginPage from "../LoginPage/LoginPage";
import DashBoard from "../DashBoard/DashBoard";
import Layout from "../Layout/Layout";
import Companies from "../Companies/Companies";
import Forms from "../Forms/formList/FormList";
import NewCompanies from "../Companies/NewCompanies/NewCompanies";
import FormPage from "../Forms/feedbackForms/FormPage";
import FormDetails from "../Forms/FormDetails";
import CompanyDetails from "../Companies/CompanyDetails/CompanyDetails";
import NewProduct from "../Companies/NewProduct/NewProduct";
import LinkList from "../Forms/Links/LinkListPopup";
import FormPreviewPopup from "../Forms/FormPreviewPopup";
import CustomerFeedback from "../DashBoard/CustomerFeedback/CustomerFeedback";
import CustomerFeedbackForm from '../Forms/customerFeedbackForm/CustomerFeedbackForm';

const Router = () => {
  var isLoggedIn = useState(sessionStorage.getItem("isLoggedIn") === "true");

  useEffect(() => {
    sessionStorage.setItem("isLoggedIn", isLoggedIn);
  }, [isLoggedIn]);
  return (
    <div>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        {isLoggedIn && <Route path="home" element={<Layout />} />}
        {isLoggedIn && <Route path="companies" element={<Companies />} />}
        {isLoggedIn && (
          <Route path="companies/companydetail" element={<CompanyDetails />} />
        )}
        {isLoggedIn && <Route path="newcompany" element={<NewCompanies />} />}
        {isLoggedIn && (
          <Route
            path="companies/companydetail/newproduct"
            element={<NewProduct />}
          />
        )}
        {isLoggedIn && <Route path="dashboard" element={<DashBoard />} />}
        {isLoggedIn && (
          <Route
            path="dashboard/customerfeedback"
            element={<CustomerFeedback />}
          />
        )}
        {isLoggedIn && <Route path="forms" element={<Forms />} />}
        {isLoggedIn && <Route path="formpage" element={<FormPage />} />}
        {isLoggedIn && (
          <Route path="formdetails/:formId" element={<FormDetails />} />
        )}
        {isLoggedIn && (
          <Route path="formdetails/:formId" element={<FormDetails />} />
        )}
        {isLoggedIn && <Route path="/link-list" element={<LinkList />} />}
        {isLoggedIn && <Route path="formList" element={<Forms />} />}
        {isLoggedIn && (
          <Route path="formpage/:selectedFormId" element={<FormPage />} />
        )}
        {isLoggedIn && (
          <Route
            path="previewPage/:selectedFormId"
            element={<FormPreviewPopup />}
          />
        )}
        {/* <Route
          path="/customerForms"
          element={<CustomerFeedbackForm/>}
        /> */}
        <Route  path="/customerForms/:category/:product/:identifier" element={<CustomerFeedbackForm/>}  /> //Don't remove this 
      </Routes>
    </div>
  );
};

export default Router;
