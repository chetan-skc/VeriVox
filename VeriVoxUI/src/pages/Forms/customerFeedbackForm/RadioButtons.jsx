import React, { useEffect, useState } from 'react';

const RadioButtons = (props) => {

    const [radioButtonOptions, setRadioButtonOptions] = useState([
        { OptionOrder: 1, OptionText: 'Option 1', OptionValue: 'Value 1' },
      ]);

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
          setRadioButtonOptions(newOptions);
        }
        
      }, [props.option]);

    // const handleOptionChange = (optionOrder, attribute, newValue) => {
    //     setRadioButtonOptions((prevRadioButtons) =>
    //       prevRadioButtons.map((radioButton) =>
    //         radioButton.OptionOrder === optionOrder ? { ...radioButton, [attribute]: newValue } : radioButton
    //       )
    //     );
    //   };

  return (
    <div>
        <div className="radio-container">
        {radioButtonOptions && radioButtonOptions.map((radioButton) => (
          <div key={radioButton.OptionOrder} className="radio-option ">
            <input
              type="radio"
              className=" mb-3"
              name="group"
              value={radioButton.OptionValue}
              id={`radio-option${radioButton.OptionOrder}`}
              style={{ transform: 'scale(2)', border:'3px solid grey'}}
            />
            {/* <label htmlFor={`radio-option${radioButton.OptionOrder}`} /> */}
                <label className='mx-3 fs-4' htmlFor={`radio-option${radioButton.OptionOrder}`}>
                                {radioButton.OptionText}
                </label>
          </div>
        ))}
      </div>
    </div>
  )
}

export default RadioButtons;
