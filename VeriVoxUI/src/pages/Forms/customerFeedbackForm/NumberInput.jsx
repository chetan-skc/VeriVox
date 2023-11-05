import React, { useState} from 'react'

const NumberInput = () => {

  const [numberValue, setNumberValue] = useState('');

  function validateNumberRange(e) {
    const number = parseFloat(e.target.value);
    const min = parseFloat(e.target.min);
    const max = parseFloat(e.target.max);

     if (number < min || number > max) {
      alert(`Please enter a number between ${min} and ${max}.`);
      setNumberValue('');
    } else {
      setNumberValue(number);
    }
  }
  return (
    <div>
      <input
        type="number"
        className="form-control-sm"
        min={0}
        max={10}
        value={numberValue}
        onChange={validateNumberRange}
      />
      <div>Enter a number</div>
      
    </div>
  )
}

export default NumberInput;
