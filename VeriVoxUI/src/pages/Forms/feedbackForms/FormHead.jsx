import React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import logo from './logo.jpg'
import productLogo from './productLogo.jpg'

const FormHead = (props) => {
  return (
    <div className="card border-1 mb-1 ">
        
             <div className='col-12'>
                    <div className='row'>
                        <img src={productLogo} className="col-sm-2 img-fluid" alt="Product Logo"/>
                        <div className='col-sm-10'>
                            <div className="title p-1">
                                <input
                                type="text"
                                className=' form-control form-control-lg fw-bold fs-1'
                                value={props.title}
                                onChange={props.handleTitleChange}
                                style={{ outline: 'none', border: 'none', backgroundColor: 'transparent' }}
                                />
                            </div>
                            {/* <img src={logo} className="img-fluid" alt="..."/> */}
                            <div className="input-with-image d-flex align-items-center mt-2">
                                <img src={logo} className="img-thumbnail" style={{borderRadius:'50%'}} alt="Company Logo" />
                                <input
                                    type="text"
                                    className="form-control fs-5 border-0 bg-transparent"
                                    value="Created By Qburst"
                                    onChange={props.handleCompanyName}
                                />
                            </div>
                        </div>
                    </div>
                
                 </div>
                <div className="d-flex p-1 pb-1 mt-2">
                        <div className='fs-5 mt-2 mx-3'>Name on Form's URL :</div>
                        <div>
                        <input
                        type="text"
                        className=' form-control form-control-lg fs-5  '
                        value={props.urlName}
                        onChange={props.handleURLName}
                        style={{backgroundColor: 'transparent' }}
                        />
                        </div>
                </div>
                <div className="p-1 pb-1 mt-2 mb-1">
                    <input
                    type="text"
                    className=' form-control form-control-lg fs-5'
                    value={props.description}
                    onChange={props.handleDescriptionChange}
                    style={{ outline: 'none', border: 'none', backgroundColor: 'transparent' }}
                    />
                </div>
        
    </div>
  )
}

export default FormHead;