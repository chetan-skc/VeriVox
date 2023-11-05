import React, { useEffect } from "react";
import "./Companies.css";
import { Col, Form, InputGroup, Row } from "react-bootstrap";
import Layout from "../Layout/Layout";
import { Link } from "react-router-dom";
import ApiInterceptor from "../LoginPage/ApiInterceptor";
import { useNavigate } from "react-router-dom";
import { ic_fiber_manual_record } from "react-icons-kit/md/ic_fiber_manual_record";
import { Icon } from "react-icons-kit";
import { useDispatch } from "react-redux";

import {
  setCompanyData,
  setPage,
  setTotalRecords,
  setSearchResult,
} from "../../Slices/PaginationSlice";
import { useSelector } from "react-redux";

const Companies = () => {
  const dispatch = useDispatch();
  const companydata = useSelector((state) => state.pagination.companydata);
  const page = useSelector((state) => state.pagination.page);
  const pageSize = useSelector((state) => state.pagination.pageSize);
  const searchResult = useSelector((state) => state.pagination.searchResult);
  const totalRecords = useSelector((state) => state.pagination.totalRecords);
  const newCompanyRole = useSelector((state) => state.user.newCompanyRole);
  const role = useSelector((state) => state.user.userRole);

  useEffect(() => {
    function fetchData() {
      const token = sessionStorage.getItem("jwtToken");

      fetch(
        `https://localhost:7199/api/Company?page=${page}&pageSize=${pageSize}`,
        {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-type": "application/json; charset=UTF-8",
          },
        }
      )
        .then((response) => response.json())
        .then((response) => {
          const companies = response.data;
          dispatch(setCompanyData(companies));
          dispatch(setTotalRecords(response.totalRecords));
          if (role !== "1" && role !== "2") {
            navigate("/companies/companydetail", {
              state: { id: companies[0].id },
            });
          }
        })
        .catch((err) => console.error(err));
    }

    fetchData();
  }, [page, pageSize]);

  const navigateToCompanyInformation = (detailofcomponent) => {
    navigate("/companies/companydetail", {
      state: { id: detailofcomponent.props.id },
    });
  };

  const toPascalCase = (str) =>
    (str.match(/[a-zA-Z0-9]+/g) || [])
      .map((w) => `${w.charAt(0).toUpperCase()}${w.slice(1)}`)
      .join(" ");

  const navigate = useNavigate();

  const DetailTab = (props) => {
    const date = new Date(props.createdDate);
    const day = date.getDate();
    const month = new Intl.DateTimeFormat("en-US", { month: "short" }).format(
      date
    );
    const year = date.getFullYear();

    const formattedDate = `${day} ${month} ${year}`;

    let productsNumber = props.products.length;

    return (
      <div
        style={{ textDecoration: "none", cursor: "pointer" }}
        to={`companydetail`}
        onClick={() => {
          navigateToCompanyInformation({ props });
        }}
      >
        <Row className="DetailTab">
          <Col lg="2">{toPascalCase(props.name)}</Col>
          <Col lg="2">{props.industry}</Col>
          <Col lg="2">{toPascalCase(props.createdByName)}</Col>
          <Col lg="2">
            <div class="dropdown">
              {props.products[0]}
              {productsNumber > 1 ? (
                <span style={{ color: "blue" }}>+{productsNumber - 1}</span>
              ) : null}
              <div class="dropdown-content">
                {props.products.map((data) => (
                  <div>{data}</div>
                ))}
              </div>
            </div>
            <div class="hoverable-element"></div>
          </Col>
          <Col lg="2">{formattedDate}</Col>
          <Col lg="2">
            {props.isActive ? (
              <div className="company-active">
                {" "}
                <Icon
                  icon={ic_fiber_manual_record}
                  size={15}
                  style={{ color: "green" }}
                />
                Active
              </div>
            ) : (
              <div className="company-inactive">
                {" "}
                <Icon
                  icon={ic_fiber_manual_record}
                  size={15}
                  style={{ color: "red" }}
                />
                Inactive
              </div>
            )}
          </Col>
        </Row>
      </div>
    );
  };

  return (
    <div>
      <Layout />
      <div className="RoutePage">
        <div className="Company-Background">
          <div className="Page">
            <h2 className="Title">Companies</h2>
            <Link to="/newcompany">
              {newCompanyRole && (
                <button
                  class="button-7"
                  style={{
                    backgroundColor: "rgb(47, 52, 126)",
                  }}
                >
                  + Company
                </button>
              )}
            </Link>

            <hr />

            <Form>
              <InputGroup>
                <Form.Control
                  className="search"
                  placeholder="Search Company here"
                  onChange={(e) => {
                    dispatch(setSearchResult(e.target.value));
                  }}
                />
              </InputGroup>
            </Form>

            <Row className="CompanyTable" style={{ textAlign: "center" }}>
              <Col lg="2">Company Name</Col>
              <Col>Industry</Col>
              <Col>Admins</Col>
              <Col>Products</Col>
              <Col>Created Date</Col>
              <Col>Status</Col>
            </Row>
            <div>
              {companydata
                .filter((data) => {
                  const searchLowerCase = searchResult.toLowerCase();
                  const dataNameLowerCase = data.name.toLowerCase();
                  return (
                    searchLowerCase === "" ||
                    dataNameLowerCase.includes(searchLowerCase)
                  );
                })
                .map((data) => (
                  <DetailTab
                    api="cfa"
                    id={data.id}
                    key={data.id}
                    name={data.name}
                    industry={data.industry}
                    deleted={data.deleted}
                    createdBy={data.createdBy}
                    createdDate={data.createdDate}
                    isActive={data.isActive}
                    createdByName={data.createdByName}
                    products={data.products}
                  ></DetailTab>
                ))}
            </div>
            <div>
              <span>
                Showing{" "}
                <span style={{ fontWeight: "600" }}>
                  {(page - 1) * pageSize + 1 > totalRecords
                    ? totalRecords
                    : (page - 1) * pageSize + 1}{" "}
                  -{" "}
                  {page * pageSize > totalRecords
                    ? totalRecords
                    : page * pageSize}{" "}
                </span>
                of <span style={{ fontWeight: "600" }}>{totalRecords}</span>{" "}
                items
              </span>
              <button
                className="pagination-button"
                onClick={() => dispatch(setPage(page + 1))}
                disabled={page * pageSize >= totalRecords}
              >
                {`>`}
              </button>
              <span
                style={{ backgroundColor: "", float: "right", padding: "10px" }}
              >
                {page}
              </span>
              <button
                className="pagination-button"
                onClick={() => dispatch(setPage(page - 1))}
                disabled={page === 1}
              >
                {`<`}
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Companies;
