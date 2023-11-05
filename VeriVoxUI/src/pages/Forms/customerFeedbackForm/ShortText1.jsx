import React,{useState} from 'react'

const ShortText1 = () => {
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
        placeholder={`ShortText (max. ${maxLength - textValue.length} characters only)`}
        value={textValue}
        onChange={handleChange}
        maxLength={maxLength}
      />
    </div>
  )
}

export default ShortText1;
