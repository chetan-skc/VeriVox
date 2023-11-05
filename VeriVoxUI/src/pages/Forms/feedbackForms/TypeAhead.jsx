import React, { useState, useEffect } from 'react';
import { v4 as uuidv4 } from 'uuid';

export default function TypeAhead(props) {
  const maxOptions = 100;
  const [typeAheadOptions, setTypeAheadOptions] = useState([
    { OptionOrder: 1, OptionText: 'Option 1', OptionValue: 'Value 1' },
  ]);
  // const [optionText, setOptionText] = useState('');

  // useEffect(() => {
  //   const optionToUpdate = options.find((item) => item.Id === optionId);
  //   if (Array.isArray(optionToUpdate.values) && optionToUpdate.values.length > 0) {
  //     setTypeAheadOptions([...optionToUpdate.values]);
  //   }
  // }, []);

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
      setTypeAheadOptions(newOption);
    }
  },[])

  const deleteTypeAheadOption = (OptionIdToDelete) => {
    const updatedTypeAheadOption = typeAheadOptions.filter((typeAheadoption)=> typeAheadoption.OptionId !== OptionIdToDelete)
    .map((typeAheadoption, index)=>({
      ...typeAheadoption,
      OptionOrder: index+1,
    }))

    if (typeAheadOptions.length === 0) {
      alert('At least one option must remain');
      return;
    }

    
    setTypeAheadOptions(updatedTypeAheadOption);
  };

  const handleTypeAheadChange = (optionId,attribute,newValue, index) => {
    setTypeAheadOptions((prevTypeAheadOptions)=>
    prevTypeAheadOptions.map((prevTypeAhead)=>
    prevTypeAhead.OptionId === optionId ? {...prevTypeAhead, [attribute]: newValue} : prevTypeAhead
    ));
  };

  const addTypeAheadOption = () => {
    if(typeAheadOptions.length >= 100){
      alert('Maximum 100 TypeAhead options are allowed.');
      return;
    }

    const newOption = typeAheadOptions.length + 1;

    setTypeAheadOptions((prevTypeAheadOptions)=>[
      ...prevTypeAheadOptions,
      {OptionId: uuidv4(), OptionOrder: newOption, OptionText:`Option ${newOption}`, OptionValue: `Value ${newOption}`},
    ]);
  };

  useEffect(() => {
    const updatedOptions = typeAheadOptions.map((radioButton) => ({
      optionOrder: radioButton.OptionOrder,
      optionText: radioButton.OptionText,
      optionValue: radioButton.OptionValue,
    }));

    if (!arraysEqual(updatedOptions, props.option)) {
      props.setOption(updatedOptions);
    }
  }, [typeAheadOptions]);

  function arraysEqual(arr1, arr2) {
    return JSON.stringify(arr1) === JSON.stringify(arr2);
  }

  return (
    <div>
      <div className="typeahead-container">
        {typeAheadOptions && typeAheadOptions.map((option, index) => (
          <div key={option.OptionId} className='typeAhead-Option'>
            <label htmlFor={`option${option.OptionId}`}/>
            <input
            type='text'
            className='form-select-lg mb-3 mx-2'
            value={option.OptionText}
            id={`option${option.Optionid}`}
            onChange={(e) => handleTypeAheadChange(option.OptionId, 'OptionText', e.target.value, index)}
            />
            <button
            className='btn btn-primary mx-2'
            onClick={()=>deleteTypeAheadOption(option.OptionId)}
            >
              Delete TypeAhead Option
            </button>
          </div>
        ))}
      </div>
       <button 
       onClick={addTypeAheadOption} 
       className="btn btn-primary mt-3">
        Add Option
      </button>
      
    </div>
  );
}
