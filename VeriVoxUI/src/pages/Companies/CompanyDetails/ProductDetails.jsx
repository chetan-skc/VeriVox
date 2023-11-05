import React from "react";
import DetailsHeader from "../../../components/DetailsHeader/DetailsHeader";
import ProductContactDetails from "../../../components/ContactDetails/ProductContactDetails";

const ProductDetails = (props) => {
  const object = props.object;

  if (Object.keys(object).length > 0) {
    return (
      <div>
        <DetailsHeader
          id={object.id}
          source="product"
          name={object.name}
          logoImage={object.logoImage}
          isActive={object.isActive}
        ></DetailsHeader>
        <div className="row" style={{ marginTop: "2%" }}>
          <div className="col-md-4">
            <label style={{ color: "grey" }}>Type</label>
            <p>{object.type}</p>
          </div>

          <div className="col-md-8">
            <label style={{ color: "grey" }}>Name on Form's Url</label>
            <p>{object.shortName}</p>
          </div>
        </div>
        <div>
          <label style={{ color: "grey" }}>Description</label>
          <p>{object.description}</p>
        </div>
        <ProductContactDetails id={props.id} />
      </div>
    );
  } else {
    return (
      <div>
        <h3>No Product found!</h3>
      </div>
    );
  }
};

export default ProductDetails;
