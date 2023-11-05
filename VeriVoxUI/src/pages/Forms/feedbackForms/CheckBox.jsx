import React, { useState, useEffect } from 'react';
import { v4 as uuidv4 } from 'uuid';

export default function CheckBox(props) {
  const maxOptions = 5;
  const [checkboxes, setCheckboxes] = useState([
    {OptionId:uuidv4(), OptionOrder: 1, OptionText: 'Option 1', OptionValue: 'Value 1' },
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
      setCheckboxes(newOption);
    }
  },[])

  const addCheckbox = () => {
    if (checkboxes.length >= maxOptions) {
      alert(`Maximum ${maxOptions} options are allowed.`);
      return;
    }

    const optionNumber = checkboxes.length + 1;

    setCheckboxes([
      ...checkboxes,
      {OptionId :uuidv4(), OptionOrder: optionNumber, OptionText: `Option ${optionNumber}`, OptionValue: `Value ${optionNumber}` },
    ]);
  };

  const deleteCheckbox = (OptionIdToDelete) => {
    const updatedCheckBoxes = checkboxes
    .filter((checkBox)=> checkBox.OptionId !== OptionIdToDelete)
    .map((checkBox, index)=>({
      ...checkBox,
       OptionOrder:index+1
    }));

    if (checkboxes.length === 0) {
      alert('There should be at least 1 Checkbox.');
      return;
    }

    setCheckboxes(updatedCheckBoxes);
  };

  const handleOptionChange = (optionId, attribute, newValue) => {
    setCheckboxes((prevCheckboxes) =>
      prevCheckboxes.map((checkbox) =>
        checkbox.OptionId === optionId ? { ...checkbox, [attribute]: newValue } : checkbox
      )
    );
  };

  useEffect(()=>{
    const updatedOptions = checkboxes.map((checkBox)=>({
      optionOrder: checkBox.OptionOrder,
      optionText : checkBox.OptionText,
      optionValue: checkBox.OptionValue 
    }));

    if (!arraysEqual(updatedOptions, props.option)) {
      props.setOption(updatedOptions);
    }
  },[checkboxes])

  function arraysEqual(arr1, arr2) {
    return JSON.stringify(arr1) === JSON.stringify(arr2);
  }

  return (
    <div>
      <div className="checkbox-container">
        {checkboxes && checkboxes.map((checkbox) => (
          <div key={checkbox.OptionId} className="checkbox-option">
            <input
              type="checkbox"
              className="custom-checkbox custom-checkbox-lg"
              name="group"
              value={checkbox.OptionValue}
              id={`checkbox-option${checkbox.OptionId}`}
            />
            <label htmlFor={`checkbox-option${checkbox.OptionId}`} />
            <input
              type="text"
              className="form-select-lg mb-3 mx-2"
              value={checkbox.OptionText}
              id={`text-option${checkbox.OptionId}`}
              onChange={(e) => handleOptionChange(checkbox.OptionId, 'OptionText', e.target.value)}
            />
            <button onClick={()=>deleteCheckbox(checkbox.OptionId)} className="btn btn-primary mx-2">
              Delete Checkbox
            </button>
          </div>
        ))}
      </div>
      <button onClick={addCheckbox} className="btn btn-primary">
        Add Checkbox
      </button>
      
    </div>
  );
}
