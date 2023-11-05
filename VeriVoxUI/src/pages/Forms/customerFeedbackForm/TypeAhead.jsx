import React, { useState, useEffect } from 'react';
import Autosuggest from 'react-autosuggest';

const TypeAhead = (props) => {
  const [typeAheadOptions, setTypeAheadOptions] = useState([]);
  const [value, setValue] = useState('');
  const [suggestions, setSuggestions] = useState([]);

  useEffect(() => {
    if (props.option) {
      const newOptions = props.option.map((option) => ({
        OptionText: option.optionText,
        OptionValue: option.optionValue,
      }));
      setTypeAheadOptions(newOptions);
    }
  }, [props.option]);

  const getSuggestions = (inputValue) => {
    return typeAheadOptions.filter((option) =>
      option.OptionText.toLowerCase().includes(inputValue.trim().toLowerCase())
    );
  };

  const getSuggestionValue = (suggestion) => suggestion.OptionText;

  const renderSuggestion = (suggestion) => <div>{suggestion.OptionText}</div>;

  const onChange = (event, { newValue }) => {
    setValue(newValue);
  };

  const onSuggestionsFetchRequested = ({ value }) => {
    setSuggestions(getSuggestions(value));
  };

  const onSuggestionsClearRequested = () => {
    setSuggestions([]);
  };

  return (
    <div>
      <Autosuggest
        suggestions={suggestions}
        onSuggestionsFetchRequested={onSuggestionsFetchRequested}
        onSuggestionsClearRequested={onSuggestionsClearRequested}
        getSuggestionValue={getSuggestionValue}
        renderSuggestion={renderSuggestion}
        inputProps={{
          value: value,
          onChange: onChange,
          type: 'text',
          className: 'typeahead-input',
          placeholder: 'Type ahead...',
        }}
      />
    </div>
  );
};

export default TypeAhead;
