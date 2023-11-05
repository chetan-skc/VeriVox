import React, { useState, useEffect } from "react";
import LoginImage from "../../assets/VeriVox_Login_Image.jpg";
import "bootstrap/dist/css/bootstrap.min.css";
import "./Styles.css";
import GoogleAuthentication from "./GoogleAuth";
import { Icon } from "react-icons-kit";
import { eye } from "react-icons-kit/fa/eye";
import { eyeSlash } from "react-icons-kit/fa/eyeSlash";
import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { resetUserState } from "../../Slices/UserDetailSlice";

const LoginPage = () => {
  const [passwordVisible, setPasswordVisible] = useState(false);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [IsInvalidCredentials, setIsInvalidCredentials] = useState(false);

  const navigate = useNavigate();
  const dispatch = useDispatch();
  const handleEmail = (event) => {
    setEmail(event.target.value);
  };

  const handlePassword = (event) => {
    setPassword(event.target.value);
  };

  const togglePasswordVisibility = (e) => {
    e.preventDefault();
    setPasswordVisible(!passwordVisible);
  };
  const LoginCredentials = {
    email: email,
    password: password,
  };
  const handleLogin = (e) => {
    e.preventDefault();
    fetch("https://localhost:7199/api/User/login", {
      method: "POST",
      headers: { "content-type": "application/json" },

      body: JSON.stringify(LoginCredentials),
    })
      .then((res) => {
        if (res.ok) {
          return res.text();
        }
      })
      .then((token) => {
        if (token !== "Invalid Credentials!") {
          sessionStorage.setItem("jwtToken", token);
          sessionStorage.setItem("isLoggedIn", true);

          navigate("dashboard");
        } else setIsInvalidCredentials(true);
      })
      .catch((error) => {
        // handle error
        console.log(error);
      });

    dispatch(resetUserState());
  };

  return (
    <div className="LoginPage">
      <div className="container-fluid h-100 loginpage">
        <div className="row h-100">
          <div className="col-md-8 p-0 d-flex flex-column justify-content-between login-image">
            <img
              src={LoginImage}
              alt="Login"
              className="img-fluid"
              style={{ height: "100%" }}
            />
          </div>

          <div className="col-md-4 d-flex align-items-top justify-content-end border">
            <div className="login-container">
              <h1 className="wider-verivox">VeriVox</h1>
              <p className="motto">Your Voice, Our Insights</p>
              <div className="GetStarted">
                <h4>Get Started Now!</h4>
                <p style={{ color: "grey" }}>
                  Enter the credentials to access your account
                </p>
                <form action="" onSubmit={handleLogin}>
                  <label>
                    Email Address <span style={{ color: "red" }}>*</span>
                  </label>
                  <input
                    className="InputFields"
                    type="email"
                    placeholder="Enter your Email"
                    onChange={handleEmail}
                    required
                  />
                  <br />
                  <label style={{ marginTop: "1rem" }}>
                    Password <span style={{ color: "red" }}>*</span>
                  </label>

                  <div className="password-input">
                    <input
                      className="PasswordInputField"
                      type={passwordVisible ? "text" : "password"}
                      placeholder="Enter your Password"
                      onChange={handlePassword}
                      required
                    />

                    <button
                      className="input-group-text password-toggle"
                      style={{ color: "graytext" }}
                      onClick={togglePasswordVisibility}
                    >
                      <Icon icon={passwordVisible ? eye : eyeSlash} />
                    </button>
                  </div>
                  {IsInvalidCredentials && (
                    <p style={{ color: "red" }}>Invalid Credentials!</p>
                  )}
                  <button type="submit" className="loginbtn">
                    Login
                  </button>

                  <h5
                    style={{
                      marginLeft: "37%",
                      color: "#4e4848",
                      marginTop: "5%",
                    }}
                  >
                    Or
                  </h5>
                  <GoogleAuthentication />
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
export default LoginPage;
