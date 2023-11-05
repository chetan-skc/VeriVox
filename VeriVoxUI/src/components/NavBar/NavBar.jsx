import React, { useEffect, useState } from "react";
import "./Navbar.css"; // Import the CSS file
import jwtDecode from "jwt-decode";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import { useDispatch } from "react-redux";
import {
  setRole,
  setDeleteCompanyRole,
  setDeleteProductRole,
  setEditCompanyRole,
  setEditProductRole,
  setNewCompanyRole,
  setNewProductRole,
  resetUserState,
} from "../../Slices/UserDetailSlice";
import { resetCompanyState } from "../../Slices/CompanyDetailSlice";
import { resetPaginationState } from "../../Slices/PaginationSlice";
const Navbar = () => {
  const [UserFirstName, setUserFirstName] = useState();
  const [UserLastName, setUserLastName] = useState();
  const [RoleName, setRoleName] = useState();
  const [UserImage, setUserImage] = useState();

  const dispatch = useDispatch();
  const navigate = useNavigate();
  useEffect(() => {
    // Get the JWT token from localStorage
    const token = sessionStorage.getItem("jwtToken");

    if (token) {
      try {
        // Decode the JWT token
        const decodedToken = jwtDecode(token);

        setUserFirstName(decodedToken.FirstName);
        setUserLastName(decodedToken.LastName);
        setUserImage(decodedToken.FirstName.charAt(0));
        var role = decodedToken.Role;
        dispatch(setRole(role));

        switch (role) {
          case "1": {
            setRoleName("System Admin");
            dispatch(setDeleteCompanyRole(true));
            dispatch(setDeleteProductRole(true));
            dispatch(setEditCompanyRole(true));
            dispatch(setEditProductRole(true));
            dispatch(setNewCompanyRole(true));
            dispatch(setNewProductRole(true));
            break;
          }
          case "2": {
            setRoleName("System Viewer");
            break;
          }
          case "3": {
            setRoleName("Company Admin");
            dispatch(setDeleteProductRole(true));
            dispatch(setEditProductRole(true));
            dispatch(setEditCompanyRole(true));
            dispatch(setNewProductRole(true));
            break;
          }
          case "4": {
            setRoleName("Company Viewer");
            break;
          }
          case "5": {
            setRoleName("Product Admin");
            dispatch(setEditProductRole(true));
            break;
          }
          default: {
            setRoleName("Product Viewer");
            break;
          }
        }
        if (decodedToken.exp < Date.now() / 1000) {
          sessionStorage.setItem("isLoggedIn", false);
          alert("session expired! Login Again");
          navigate("/");
        }
      } catch (error) {
        console.error("Error decoding token:", error);
      }
    } else {
      // Token not found in sessionStorage
      alert("Login Please!");
      navigate("/");
    }
  }, []);
  const handleLogout = () => {
    sessionStorage.setItem("isLoggedIn", false);
    sessionStorage.removeItem("jwtToken");
    dispatch(resetUserState());
  };
  return (
    <nav>
      <div className="user-profile">
        <div className="profile-image">
          <span className="profile-text">{UserImage}</span>
        </div>
        <div className="user-details">
          <span className="upper-span">
            {UserFirstName + " " + UserLastName}
          </span>
          <div className="d-flex align-items-center">
            <div className="user-details ml-2">
              <span className="role">{RoleName}</span>
              <div className="dropdown-container">
                <button
                  className="dropdown-button"
                  style={{ backgroundColor: "white", border: "none" }}
                >
                  <span className="dropdown-arrow">&#9662;</span>
                </button>
                <Link to={"/"}>
                  <span onClick={handleLogout} className="logout-text">
                    Logout
                  </span>
                </Link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
