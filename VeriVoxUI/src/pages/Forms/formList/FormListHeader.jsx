import React from 'react'
import 'bootstrap/dist/css/bootstrap.css';
import { Icon } from 'react-icons-kit';
import { plus } from 'react-icons-kit/fa/plus';
import { Link } from 'react-router-dom';

const FormListHeader = () => {
  return (
    <div className="row">
            <div className="col-sm-6 fw-bold fs-3 text-start" >
                Forms
            </div>
            <div className="col-sm-6 text-end mt-1" >
                <Link to="/formpage">
                        <button
                            type="button"
                            className="btn "
                            style={{ backgroundColor: 'rgb(47, 52, 126)', color: 'white' }}
                            >
                            <Icon icon={plus} /> Form
                        </button>
                </Link>
            </div>
            <hr className="line bg-dark mt-3" style={{ height: '1px', color: 'grey' , width:'100%'}} />
    </div>
  )
}

export default FormListHeader
