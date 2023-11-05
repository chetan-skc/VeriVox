import React, {useState} from 'react'

 const BigText = () => {

const maxLength = 4000;
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
        placeholder={`Paragraph (max. ${maxLength - textValue.length} characters only)`}
        maxLength={maxLength}
        className="form-control form-control-lg mt-1 mw"
        value={textValue}
        onChange={handleChange}
        style={{ width: '100%', height: '150px' , resize: 'none' }}
      />
      {/* <div>Word count: {maxLength - textValue.length}</div> */}
    </div>
  )
}

export default BigText;
