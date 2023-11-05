import React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import { Icon } from 'react-icons-kit';
import { ic_keyboard_arrow_right_twotone } from 'react-icons-kit/md/ic_keyboard_arrow_right_twotone';
import { ic_keyboard_arrow_left_twotone } from 'react-icons-kit/md/ic_keyboard_arrow_left_twotone';

const FormListPagination = ({ currentPage, itemsPerPage, totalItems, onPageChange }) => {
  const totalPages = Math.ceil(totalItems / itemsPerPage);

  const handlePageChange = (page) => {
    onPageChange(page);
  };

  const displayRangeStart = totalItems === 0 ? 0 : (currentPage - 1) * itemsPerPage + 1;
  const displayRangeEnd = totalItems === 0 ? 0 : Math.min(currentPage * itemsPerPage, totalItems);
  const totalItemsText = totalItems === 0 ? '0' : totalItems;

  return (
    <div className='d-flex justify-content-between'>
      <div>
        Showing {displayRangeStart}-{displayRangeEnd} of {totalItemsText} items
      </div>
      <div className='d-flex text-center' style={{ width: '5vw' }}>
        {currentPage > 1 ? (
          <Icon size={25} icon={ic_keyboard_arrow_left_twotone} onClick={() => handlePageChange(currentPage - 1)} />
        ) : (
          <Icon aria-disabled='true' style={{ visibility: 'hidden' }} size={25} icon={ic_keyboard_arrow_left_twotone} />
        )}
        <div
          key={currentPage}
          className={`fw-bold`}
          onClick={() => handlePageChange(currentPage)}
          style={{ backgroundColor: 'rgba(241,243,255,255)', width: '100%' }}
        >
          {currentPage}
        </div>
        {currentPage < totalPages ? (
          <Icon size={25} icon={ic_keyboard_arrow_right_twotone} onClick={() => handlePageChange(currentPage + 1)} />
        ) : (
          <Icon
            aria-disabled='true'
            style={{ visibility: 'hidden' }}
            size={25}
            icon={ic_keyboard_arrow_right_twotone}
          />
        )}
      </div>
    </div>
  );
};

export default FormListPagination;
