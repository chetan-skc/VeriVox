import React, { useEffect, useState } from 'react';
import { v4 as uuidv4 } from 'uuid';

const RadioButtons = (props) => {
  const [radioButtonOptions, setRadioButtonOptions] = useState([
    {OptionId: uuidv4(), OptionOrder: 1, OptionText: 'Option 1', OptionValue: 'Value 1' },
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
      setRadioButtonOptions(newOption);
    }
  },[])

  const addRadioButton = () => {
    if (radioButtonOptions.length >= 5) {
      alert('Maximum 5 options are allowed.');
      return;
    }

    const newOptionOrder = radioButtonOptions.length + 1;

    setRadioButtonOptions((prevRadioButtons) => [
      ...prevRadioButtons,
      {OptionId: uuidv4(), OptionOrder: newOptionOrder, OptionText: `Option ${newOptionOrder}`, OptionValue: `Value ${newOptionOrder}` },
    ]);
  };

  const deleteRadioButton = (OptionIdToDelete) => {
    const updatedRadioButtons = radioButtonOptions
      .filter((radioButton) => radioButton.OptionId !== OptionIdToDelete)
      .map((radioButton, index) => ({
        ...radioButton,
        OptionOrder: index + 1,
      }));
  
    if (updatedRadioButtons.length === 0) {
      alert('There should be at least 1 Radio button.');
      return;
    }
  
    setRadioButtonOptions(updatedRadioButtons);
  };

  const handleOptionChange = (optionId, attribute, newValue) => {
    setRadioButtonOptions((prevRadioButtons) =>
      prevRadioButtons.map((radioButton) =>
        radioButton.OptionId === optionId ? { ...radioButton, [attribute]: newValue } : radioButton
      )
    );
  };



  useEffect(() => {
    const updatedOptions = radioButtonOptions.map((radioButton) => ({
      optionOrder: radioButton.OptionOrder,
      optionText: radioButton.OptionText,
      optionValue: radioButton.OptionValue,
    }));

    if (!arraysEqual(updatedOptions, props.option)) {
      props.setOption(updatedOptions);
    }
  }, [radioButtonOptions]);


  function arraysEqual(arr1, arr2) {
    return JSON.stringify(arr1) === JSON.stringify(arr2);
  }
  

  return (
    <div>
      <div className="radio-container">
        {radioButtonOptions && radioButtonOptions.map((radioButton) => (
          <div key={radioButton.OptionId} className="radio-option">
            <input
              type="radio"
              className="form-select-lg mb-3"
              name="group"
              value={radioButton.OptionValue}
              id={`radio-option${radioButton.OptionId}`}
            />
            <label htmlFor={`radio-option${radioButton.OptionId}`} />
            <input
              type="text"
              className="form-select-lg mb-3 mx-2"
              value={radioButton.OptionText}
              id={`text-option${radioButton.OptionId}`}
              onChange={(e) => handleOptionChange(radioButton.OptionId, 'OptionText', e.target.value)}
            />
            <button
              onClick={() => deleteRadioButton(radioButton.OptionId)}
              className="btn btn-primary mx-2"
            >
              Delete Radio Button
            </button>
          </div>
        ))}
      </div>
      <button onClick={addRadioButton} className="btn btn-primary">
        Add Radio Button
      </button>
    </div>
  );
};

export default RadioButtons;
