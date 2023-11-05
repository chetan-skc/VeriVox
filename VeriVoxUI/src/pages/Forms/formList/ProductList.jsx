import React, { useState, useEffect } from 'react';

const ProductList = (props) => {
  const [options, setOptions] = useState([]);
  const [selectedOption, setSelectedOption] = useState('');
  const token = sessionStorage.getItem("jwtToken");

  // useEffect(() => {
  //   // console.log("Id: ", props.companyId);
  //   props.setProductId(null);
  // }, [props.companyId]);

  useEffect(() => {
    if (props.companyId !== undefined) {
      fetch(`https://localhost:7199/api/Product/productbycompanyid/${props.companyId}`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`,
        },
      })
        .then((response) => response.json())
        .then((data) => {
          setOptions(data);
        })
        .catch((error) => {
          console.error('Error fetching options from the API:', error);
        });
    }
  }, [props.companyId]);

  const handleProductChange = (e) => {
    setSelectedOption(e.target.value);

    props.setUrlText(null);

    // Set props.setProductId with the selected option's ID
    props.setProductId(e.target.value);
  };

  useEffect(()=>{
    if(props.productId===null)
    {
      setSelectedOption("");
      props.setUrlText(null);
    }
  },[props.productId])

  return (
    <div className="col-12">
      <label className="form-label">
        Type<span style={{ color: 'red' }}>*</span>
      </label>
      <select
        className="form-control"
        value={selectedOption}
        onChange={handleProductChange}
        disabled={!props.companyId}
      >
        <option value="" disabled>{selectedOption ? 'Select' : 'Select'}</option>
        {Array.isArray(options) && options.map((option) => (
          <option key={option.id} value={option.id}>
            {option.name}
          </option>
        ))}
      </select>
    </div>
  );
};

export default ProductList;
