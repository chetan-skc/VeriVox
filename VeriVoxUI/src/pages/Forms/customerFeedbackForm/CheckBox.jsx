import React, { useState, useEffect } from 'react';
import './CustomerCheckBoxStyle.css'

const CheckBox = (props) => {
    const [checkboxes, setCheckboxes] = useState([
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
          setCheckboxes(newOptions);
        }
        
      }, [props.option]);

    


  return (
    <div>
        <div className="checkbox-container">
        {checkboxes.map((checkbox) => (
          <div key={checkbox.OptionOrder} className="checkbox-option">
            <input
              type="checkbox"
              className="custom-checkbox "
              name="group"
              value={checkbox.OptionValue}
              id={`checkbox-option${checkbox.OptionOrder}`}
              style={{transform: 'scale(2)', border:'2px solid grey'}}
            />
            {/* <label htmlFor={`checkbox-option${checkbox.OptionOrder}`} /> */}
            <label className="form-select-lg  mx-2 fs-4 " htmlFor={`checkbox-option${checkbox.OptionOrder}`}> 
             {checkbox.OptionText}
            </label>
          </div>
        ))}
      </div>
    </div>
  )
}

export default CheckBox;
