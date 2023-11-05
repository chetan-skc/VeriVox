import React, {useState, useEffect} from 'react'
import 'bootstrap/dist/css/bootstrap.css';
import SelectedOption from './SelectedOption'
import { Icon } from "react-icons-kit";
import {copy} from 'react-icons-kit/fa/copy'
import {bin} from 'react-icons-kit/icomoon/bin'



export const QuestionBox = (props) => {
    const [isChecked, setIsChecked] = useState(props.isMandatory);
  
    const handleAnswerTypeChange = (e) => {
      props.setSelectedAnswerType(e.target.value);
    };
  
    const handleCheckboxChange = () => {
      if (isChecked) {
        setIsChecked(false);
        props.setMandatory(false);
      } else {
        setIsChecked(true);
        props.setMandatory(true);
      };
    }
  
    const handleDuplicate = () => {
      props.onDuplicate(props.id, props.selectedAnswerType);
    };
  
    const handleDelete = () => {
      props.onDelete(props.id);
    };
  
    return (
      <div className='row'>
        <div className='col-9'>
          <div className='card border-1 mt-1 mb-1'>
            <div className='card-body'>
              <div className='d-flex align-item-start'>
                <div className='me-2 my-2'>{props.index + 1}.</div>
                <div className='flex-grow-1'>
                  <input
                    type="text"
                    className='form-control form-control-lg'
                    value={props.questionText}
                    onChange={(e) => props.setQuestionText(e.target.value)}
                  />
                </div>
              </div>
              <div className='d-flex align-item-start mt-2'>
                <div className='me-2 my-2' style={{ visibility: 'hidden' }}>1.</div>
                <div className='flex-grow-1'>
                  <SelectedOption
                    answerType={props.selectedAnswerType}
                    option={props.option}
                    setOption={(newOption) => {
                      props.setOption(newOption);
                      props.handleOptionChange(props.id, newOption);
                    }}

                  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <div className='col-3'>
          <div className='card border-1 mt-1 mb-1 text-justify'>
            <div className='card-body'>
              <div>
                <select className='form-select form-select-lg' value={props.selectedAnswerType} onChange={handleAnswerTypeChange}>
                  <option value="" disabled>{props.selectedAnswerType ? props.selectedAnswerType : 'Select answer type'}</option>
                  <option value="ShortText">Text Box</option>
                  <option value="NumberInput">Text Box for Numbers</option>
                  <option value="BigText">Big Text Area</option>
                  <option value="Ratings">Ratings</option>
                  <option value="Dropdown">Dropdown</option>
                  <option value="RadioButtons">Radio Buttons</option>
                  <option value="TypeAhead">TypeAhead</option>
                  <option value="CheckBox">Checkbox</option>
                </select>
              </div>
              <div className="form-check form-switch d-flex justify-content-between text-start mt-3">
                <label className="form-check-label fw-Normal fs-5 my-2">Required</label>
                <input
                  className="form-check-input fw-bold fs-2"
                  type="checkbox"
                  id="flexSwitchCheckChecked"
                  onChange={handleCheckboxChange}
                  defaultChecked={props.isMandatory}
                  style={{
                    backgroundColor: isChecked ? 'rgb(47, 52, 126)' : 'transparent',
                    outline: isChecked ? '1px solid white' : '1px rgb(47, 52, 126)',
                  }}
                />
              </div>
              <div>
                <button type="button" className="btn btn-xlg btn-outline mx-2 mt-3 fs-6 col-12" onClick={handleDuplicate} style={{ borderColor: 'rgb(47, 52, 126)', color: 'rgb(47, 52, 126)', backgroundColor: 'white' }}>
                  <Icon icon={copy} /> Duplicate Question
                </button>
                {props.questions.length > 1 && (
                  <button type="button" className="btn btn-outline-danger btn-lg mx-2 mt-3" style={{ backgroundColor: '#FFCCCB' }} onClick={handleDelete}>
                    <Icon icon={bin} /> Delete Question
                  </button>
                )}
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  };
  