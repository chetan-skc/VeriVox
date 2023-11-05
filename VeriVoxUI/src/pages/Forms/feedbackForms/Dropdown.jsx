import React, { useState, useEffect } from 'react';
import { v4 as uuidv4 } from 'uuid';

export default function Dropdown(props) {
  // const maxOptions = 20;
  const [dropdownOptions, setDropdownOptions] = useState([
    {OptionId: uuidv4(), OptionOrder: 1, OptionText: 'Option 1', OptionValue: 'Value 1' }
  ]);
  
  useEffect(()=>{
    // console.log("options: ",props.option);
    if(props.option.length>=1)
    {
      const newOption = props.option.map((option)=>{
        return{
          OptionId :uuidv4(),
          OptionOrder:option.optionOrder,
          OptionText: option.optionText,
          OptionValue: option.optionValue 
        }
      });
      setDropdownOptions(newOption);
    }
  },[])

  


  const deleteDropdownOption = (OptionIdToDelete) => {
    const updatedDropdownOptions = dropdownOptions.filter((dropdown)=> dropdown.OptionId !== OptionIdToDelete)
    .map((dropdown, index)=>({
      ...dropdown, 
      OptionOrder: index+1,
    }));

    if(updatedDropdownOptions.length === 0)
    {
      alert('There should be atleast 1 dropdown option.');
      return;
    }

    setDropdownOptions(updatedDropdownOptions);
  };

  const addDropdownOption = () => {
    if(dropdownOptions.length>=20)
    {
      alert('Maximum 20 options are allowed.')
      return;
    }

    const newOptionOrder =  dropdownOptions.length +1;

    setDropdownOptions((prevDropdown)=>[
      ...prevDropdown,
      {OptionId: uuidv4(), optionOrder: newOptionOrder, OptionText: `option ${newOptionOrder}`, OptionValue: `Value ${newOptionOrder}`},
    ]);
  };

  const handleOptionChange=(optionId, attribute, newValue)=>{
      setDropdownOptions((prevDropdown)=>
      prevDropdown.map((dropdownOp)=>
      dropdownOp.OptionId == optionId? {...dropdownOp,[attribute]:newValue}: dropdownOp
      ));
  };

  useEffect(()=>{
    const updatedOptions = dropdownOptions.map((dropdown)=>({
      optionOrder : dropdown.OptionOrder,
      optionText  : dropdown.OptionText,
      optionValue : dropdown.OptionValue
    }));

    if(!arraysEqual(updatedOptions, props.option)){
      props.setOption(updatedOptions);
    }
  },[dropdownOptions])

  function arraysEqual(arr1, arr2){
    return JSON.stringify(arr1) === JSON.stringify(arr2);
  }

  return (
    <div>
      <div className='dropdown-container'>
        {dropdownOptions && dropdownOptions.map((dropdown)=>(
          <div key={dropdown.OptionId} className='dropdown-option'>
            <label htmlFor={`dropdown-option${dropdown.OptionId}`}/>
            <input
              type='text'
              className='form-select-lg col-6 mb-3 mx-2'
              value={dropdown.OptionText}
              id={`text-option${dropdown.OptionId}`}
              onChange={(e)=> handleOptionChange(dropdown.OptionId, 'OptionText', e.target.value)} 
            />
            <button
            className='btn btn-primary '
            onClick={() => deleteDropdownOption(dropdown.OptionId)}
            >
              Delete Dropdown option
            </button>
          </div>
        ))}
      </div>
      <div>
      <button 
      className='btn btn-primary'
      onClick={addDropdownOption}
      >
        Add Dropdown Option
      </button>
      </div>
    </div>
  );
}
