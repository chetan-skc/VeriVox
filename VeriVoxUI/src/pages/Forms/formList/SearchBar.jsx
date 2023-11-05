import React from 'react';
import { Icon } from 'react-icons-kit';
import { search } from 'react-icons-kit/fa/search';
import './SearchBarStyle.css';

const SearchBar = ({ searchTerm, onSearchChange }) => {
  const handleInputChange = (e) => {
    const value = e.target.value;
    onSearchChange(value);
  };

  return (
    <div id='search-input-wrapper'>
      <Icon icon={search} id='search-icon' size={20} />
      <input
        id='search-input'
        placeholder="Search Form Name"
        value={searchTerm}
        onChange={handleInputChange}
      />
    </div>
  );
};

export default SearchBar;
