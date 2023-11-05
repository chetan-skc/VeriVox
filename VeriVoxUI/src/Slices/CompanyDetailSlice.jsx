import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  companydata: [],
  searchResult: "",
  products: [],
  product: [],
  isOverlayVisible: false,
  isOverlayBoxVisible: false,
  isProductOverlayVisible: false,
  isProductOverlayBoxVisible: false,
  industry: "",
  isActive: "",
  industryId: "",
  companyindustrydata: [],
  companyName: "",
  description: "",
  logo: "",
  shortName: "",
  productName: "",
  productDescription: "",
  productLogo: "",
  productShortName: "",
  productType: "",
  activeProduct: null,
  defaultProduct: true,
  limitRequest: true,
  flag: false,
};

const companyDetailsSlice = createSlice({
  name: "companyDetails",
  initialState,
  reducers: {
    setCompany: (state, action) => {
      state.companydata = action.payload;
    },
    setSearchResult: (state, action) => {
      state.searchResult = action.payload;
    },
    setProducts: (state, action) => {
      state.products = action.payload;
    },
    setProduct: (state, action) => {
      state.product = action.payload;
    },
    setIsOverlayVisible: (state, action) => {
      state.isOverlayVisible = action.payload;
    },
    setIsOverlayBoxVisible: (state, action) => {
      state.isOverlayBoxVisible = action.payload;
    },
    setIsProductOverlayVisible: (state, action) => {
      state.isProductOverlayVisible = action.payload;
    },
    setIsProductOverlayBoxVisible: (state, action) => {
      state.isProductOverlayBoxVisible = action.payload;
    },
    setIndustry: (state, action) => {
      state.industry = action.payload;
    },
    setIsActive: (state, action) => {
      state.isActive = action.payload;
    },
    setIndustryId: (state, action) => {
      state.industryId = action.payload;
    },
    setCompanyIndustryData: (state, action) => {
      state.companyindustrydata = action.payload;
    },
    setCompanyName: (state, action) => {
      state.companyName = action.payload;
    },
    setDescription: (state, action) => {
      state.description = action.payload;
    },
    setLogo: (state, action) => {
      state.logo = action.payload;
    },
    setShortName: (state, action) => {
      state.shortName = action.payload;
    },
    setProductName: (state, action) => {
      state.productName = action.payload;
    },
    setProductDescription: (state, action) => {
      state.productDescription = action.payload;
    },
    setProductLogo: (state, action) => {
      state.productLogo = action.payload;
    },
    setProductShortName: (state, action) => {
      state.productShortName = action.payload;
    },
    setProductType: (state, action) => {
      state.productType = action.payload;
    },

    setActiveProduct: (state, action) => {
      state.activeProduct = action.payload;
    },
    setDefaultProduct: (state, action) => {
      state.defaultProduct = action.payload;
    },
    setLimitRequest: (state, action) => {
      state.limitRequest = action.payload;
    },
    setFlag: (state, action) => {
      state.flag = action.payload;
    },
    resetCompanyState: (state) => {
      return { ...initialState };
    },
  },
});

export const {
  setCompany,
  setSearchResult,
  setProducts,
  setProduct,
  setIsOverlayVisible,
  setIsOverlayBoxVisible,
  setIsProductOverlayVisible,
  setIsProductOverlayBoxVisible,
  setIndustry,
  setIsActive,
  setIndustryId,
  setCompanyIndustryData,
  setCompanyName,
  setDescription,
  setLogo,
  setShortName,
  setProductName,
  setProductDescription,
  setProductLogo,
  setProductShortName,
  setProductType,
  setActiveProduct,
  setDefaultProduct,
  setLimitRequest,
  setFlag,
  resetCompanyState,
} = companyDetailsSlice.actions;

export default companyDetailsSlice.reducer;
