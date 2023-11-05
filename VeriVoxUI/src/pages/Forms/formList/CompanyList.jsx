import React, { useState, useEffect } from 'react';

const CompanyList = (props) => {
  const [options, setOptions] = useState([]);
  const [selectedOption, setSelectedOption] = useState(null);

  const token = sessionStorage.getItem("jwtToken");

  useEffect(() => {
    fetch('https://localhost:7199/api/Form/GetAllCompanies', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
    })
      .then((response) => response.json())
      .then((data) => {
        setOptions(data);
      })
      .catch((error) => {
        console.error('Error fetching options from the API:', error);
      });
  }, []);

  useEffect(() => {
    // Add this useEffect to reset the selectedOption when assignCard changes
    if (props.assignCard===false) {
      setSelectedOption("");
      props.setUrlText(null);
    }
  }, [props.assignCard]);

  
  const handleSelectChange = (e, optionId) => {
    setSelectedOption(e.target.value);
    
    props.setCompanyId(optionId);
    props.setProductId(null);
    props.setUrlText(null);
  };

  return (
    <div className="col-12">
      <label className="form-label">
        Company<span style={{ color: 'red' }}>*</span>
      </label>
      <select
        className="form-control"
        value={selectedOption}
        onChange={(e) => handleSelectChange(e, e.target.value)} 
        aria-label="Default select example"
        required
      >
        <option value="" disabled>{selectedOption ? 'Select' : 'Select'}</option>
        {options.map((option) => (
          <option key={option.id} value={option.id}>
            {option.name}
          </option>
        ))}
      </select>
    </div>
  );
};

export default CompanyList;
