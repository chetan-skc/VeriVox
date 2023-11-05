import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  userRole: 0,
  deleteCompanyRole: false,
  deleteProductRole: false,
  editCompanyRole: false,
  editProductRole: false,
  newCompanyRole: false,
  newProductRole: false,
  newContactAdd: false,
  ContactEdit: false,
  ContactDelete: false,
};

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    setRole: (state, action) => {
      state.userRole = action.payload;
    },
    setDeleteCompanyRole: (state, action) => {
      state.deleteCompanyRole = action.payload;
    },
    setDeleteProductRole: (state, action) => {
      state.deleteProductRole = action.payload;
    },
    setEditCompanyRole: (state, action) => {
      state.editCompanyRole = action.payload;
    },
    setEditProductRole: (state, action) => {
      state.editProductRole = action.payload;
    },
    setNewCompanyRole: (state, action) => {
      state.newCompanyRole = action.payload;
    },
    setNewProductRole: (state, action) => {
      state.newProductRole = action.payload;
    },
    setNewContactAdd: (state, action) => {
      state.newContactAdd = action.payload;
    },
    setContactEdit: (state, action) => {
      state.ContactEdit = action.payload;
    },
    setContactDelete: (state, action) => {
      state.ContactDelete = action.payload;
    },
    resetUserState: (state) => {
      return { ...initialState };
    },
  },
});

export const {
  setRole,
  setDeleteCompanyRole,
  setDeleteProductRole,
  setEditCompanyRole,
  setEditProductRole,
  setNewCompanyRole,
  setNewProductRole,
  setNewContactAdd,
  setContactEdit,
  setContactDelete,
  resetUserState,
} = userSlice.actions;

export default userSlice.reducer;
