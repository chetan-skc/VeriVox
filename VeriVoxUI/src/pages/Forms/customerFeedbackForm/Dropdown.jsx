import React, { useState, useEffect } from 'react';

const Dropdown = (props) => {
    const [dropdownOptions, setDropdownOptions] = useState([]);
    // const [optionText, setOptionText] = useState('');

    // useEffect(() => {
    //     const optionToUpdate = options.find((item) => item.Id === optionId);
    //     if (Array.isArray(optionToUpdate.values) && optionToUpdate.values.length > 0) {
    //       setDropdownOptions([optionToUpdate.values]);
    //     }
    //   }, []);

    useEffect(() => {
      // console.log("Radio button options:",props.option)
      if(props.option){
        const newOptions = props.option.map((option)=>{
          return{
              OptionOrder:option.optionOrder,
              OptionText:option.optionText,
              OptionValue:option.optionValue
          }
          
        });
        setDropdownOptions(newOptions);
      }
      
    }, [props.option]);

    const handleDropdownChange = (event) => {
        const selectedOption = event.target.value;
        console.log('Selected option:', selectedOption);
    };

  return (
    <div>
        <select className="form-select" onChange={handleDropdownChange}>
        <option disabled selected> Select an option</option>
        {dropdownOptions.map((option, index) => (
          <option key={option.OptionOrder} value={option.OptionValue}>
            {option.OptionText}
          </option>
        ))}
      </select>
    </div>
  )
}

export default Dropdown;
