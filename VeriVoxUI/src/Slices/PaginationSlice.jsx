import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  companydata: [],
  page: 1,
  pageSize: 7,
  totalRecords: 0,
  searchResult: "",
  companyListChange: false,
};

const paginationSlice = createSlice({
  name: "pagination",
  initialState,
  reducers: {
    setCompanyData: (state, action) => {
      state.companydata = action.payload;
    },
    setPage: (state, action) => {
      state.page = action.payload;
    },
    setPageSize: (state, action) => {
      state.pageSize = action.payload;
    },
    setTotalRecords: (state, action) => {
      state.totalRecords = action.payload;
    },
    setSearchResult: (state, action) => {
      state.searchResult = action.payload;
    },
    setCompanyListChange: (state, action) => {
      state.companyListChange = action.payload;
    },
    resetPaginationState: (state) => {
      return { ...initialState };
    },
  },
});

export const {
  setCompanyData,
  setPage,
  setPageSize,
  setTotalRecords,
  setSearchResult,
  setCompanyListChange,
  resetPaginationState,
} = paginationSlice.actions;

export default paginationSlice.reducer;
