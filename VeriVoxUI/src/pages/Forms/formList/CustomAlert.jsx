import React from 'react';
import { Icon } from 'react-icons-kit';

const CustomAlert = ({message, iconName, outlineColor, backgroundColor,iconColor, iconBackgroundColor }) => {

  return (
    <div className='d-flex align-items-center alert'  role="alert" style={{ position: 'fixed', right: 30, borderColor: outlineColor, backgroundColor: backgroundColor, borderWidth:'2px' }}>
      {iconName && (
        <div className='p-2 rounded-circle' style={{color:iconColor,backgroundColor:iconBackgroundColor }}>
          <Icon icon={iconName} size={24} />
        </div>
      )}
       <div className="mx-3" dangerouslySetInnerHTML={{ __html: message }}>
      </div>
    </div>
  );
};

export default CustomAlert;
