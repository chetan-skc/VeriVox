import { configureStore } from "@reduxjs/toolkit";
import paginationReducer from "./Slices/PaginationSlice";
import companyDetailsReducer from "./Slices/CompanyDetailSlice";
import userDetailReducer from "./Slices/UserDetailSlice";

const store = configureStore({
  reducer: {
    pagination: paginationReducer,
    companyDetails: companyDetailsReducer,
    user: userDetailReducer,
  },
});

export default store;
