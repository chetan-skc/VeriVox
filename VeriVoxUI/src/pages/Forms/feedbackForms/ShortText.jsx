import React, {useState} from 'react'

const ShortText = () => {
  const maxLength = 100;
  const [textValue, setTextValue] = useState('');

  const handleChange = (e) => {
    const inputValue = e.target.value;
    if (inputValue.length <= maxLength) {
      setTextValue(inputValue);
    }
  };

  return (
    <div>
      <input
        type="text"
        className="form-control form-control-lg mt-1 mw"
        placeholder="Enter your answer"
        value={textValue}
        onChange={handleChange}
        maxLength={maxLength}
      />
      <div >Word count: {maxLength - textValue.length}</div>
    </div>
  )
}

export default ShortText;
