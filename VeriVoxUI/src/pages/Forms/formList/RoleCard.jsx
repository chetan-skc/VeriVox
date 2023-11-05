import React from 'react';

const RoleCard = ({ role }) => {
  let roleText = null;
  let color='white'

  switch (role) {
    case 3:
      roleText = "CompanyAdmin";
      color = 'green';
      break;
    case 5:
        roleText = "ProductAdmin";
        color = 'blue';
        break;
    default:
      break;
  }

  return (
    <div>
        <div>
            {(roleText!==null) ? 
            <div 
            className='card align-items-start'
            style={{
                position: 'relative',
                zIndex: 999,
                backgroundColor: color,
                boxShadow: '0 2px 10px rgba(0, 0, 0, 0.2)',
                border: '1px solid #ccc',
                borderRadius: '25px',
                padding:'1px',
                color:'white'
                }}
            >
          {roleText}
          </div>:null
            }
        </div>
    </div>
  );
};

export default RoleCard;
