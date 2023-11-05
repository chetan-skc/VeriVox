import React from 'react';
import productLogo from '../feedbackForms/productLogo.jpg';
import logo from '../feedbackForms/logo.jpg';

export const CustomerFeedbackFormHead = (props) => {
  return (
    <div className="card overflow-auto">
      <div className="card-body">
        <div className="col-12">
          <div className="row mx-4 text-left p-3">
          <div className="col-sm-2 mx-3 mt-3" style={{ border: '2px solid grey', width: '100px', height: '85px', background: `url(${props.productLogo}) center center / cover no-repeat`, }}></div>
            <div className="col-sm-8">
              <div className="form-title fw-bold fs-1" style={{font: 'inherit' , letterSpacing:'inherit'}}>{props.formTitle}</div>
              <div className="input-with-image d-flex align-items-center mt-2" style={{ color: 'grey' }}>
                <img src={props.companyLogo} className="img-thumbnail" style={{ borderRadius: '50%', marginRight:'10px' }} alt="Company Logo" height={20} width={35} />
                {props.companyName}
              </div>
            </div>
            <div className="col-sm-12 fs-4 mt-4" style={{ color: 'grey', font: 'inherit' , letterSpacing:'inherit'}}>
              Please provide honest and detailed feedback to help us understand your experiences better. Fields marked with an asterisk (*) are mandatory, but we encourage you to complete the entire form. If you have multiple products to provide feedback on, please submit separate forms for each one. All information shared will be kept confidential and used solely for product improvement purposes.
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
