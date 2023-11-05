import { GoogleLoginButton } from "react-social-login-buttons";
import { LoginSocialGoogle } from "reactjs-social-login";
import "./Styles.css";
import { useNavigate } from "react-router-dom";
import { useState } from "react";

const GoogleAuthentication = () => {
  const navigate = useNavigate();
  const [InValidEmail, setInValidEmail] = useState(false);
  const handleLogin = async (email) => {
    var apiUrl = `https://localhost:7199/api/User/LoginByGoogle?email=${email}`;
    fetch(apiUrl, {
      method: "GET",
      headers: { "content-type": "application/json" },
    })
      .then((res) => {
        if (res.ok) {
          return res.text();
        }
        // handle error
      })
      .then((token) => {
        if (token !== "Invalid Credentials!") {
          sessionStorage.setItem("jwtToken", token);
          sessionStorage.setItem("isLoggedIn", true);
          navigate("dashboard");
        } else {
          setInValidEmail(true);
        }
      })
      .catch((error) => {
        // handle error
      });
  };

  return (
    <div className="googleauth">
      {InValidEmail && <p style={{ color: "red" }}>You are not registered!</p>}
      <LoginSocialGoogle
        client_id={
          "197993462439-eaukttrs98b8cjnioc5bqabcamcg5csi.apps.googleusercontent.com"
        }
        scope="profile email"
        //discoveryDocs="claims_supported"
        //access_type="offline"
        onResolve={({ provider, profile, data }) => {
          const userEmail = data.email;
          handleLogin(userEmail);
        }}
        onReject={(err) => {
          console.log(err);
        }}
      >
        <GoogleLoginButton />
      </LoginSocialGoogle>
    </div>
  );
};

export default GoogleAuthentication;
