import React, {useState} from 'react'

 const BigText = () => {

const maxLength = 500;
const [textValue, setTextValue] = useState('');

const handleChange = (e) => {
    const inputValue = e.target.value;
    if (inputValue.length <= maxLength) {
    setTextValue(inputValue);
    }
};
  return (
    <div>
      <textarea
        placeholder="Enter your answer"
        maxLength={maxLength}
        className="form-control form-control-lg mt-1 mw"
        value={textValue}
        onChange={handleChange}
      />
      <div>Word count: {maxLength - textValue.length}</div>
    </div>
  )
}

export default BigText;
