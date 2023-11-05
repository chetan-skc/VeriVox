import React from "react";
import Navbar from "../../components/NavBar/NavBar";
import SideBar from "../../components/SideBar/SideBar";
import Router from "../Routes/Router";

const Layout = () => {
  return (
    <div>
      <Navbar />
      <SideBar />
    </div>
  );
};

export default Layout;
