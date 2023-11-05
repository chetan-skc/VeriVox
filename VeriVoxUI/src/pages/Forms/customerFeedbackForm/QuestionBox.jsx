import React from 'react'
import 'bootstrap/dist/css/bootstrap.css';
import SelectedOption from './SelectedOption'

export const QuestionBox = (props) => {
  return (
    <div className='card mt-4'>
        <div className='card-body mx-5 text-left p-3 mt-4'>
            <div className='d-flex align-item-start fw-normal fs-5'>
                <div className='me-2'>{props.index + 1}.</div>
                <div className='flex-grow d-flex'>
                    <label>{props.questionText}</label>{props.isMandatory &&(
                      <div style={{color:'red'}}>*</div>
                    )}
                </div>
            </div>
            <div className='d-flex align-item-start mt-4 mb-4'>
              <div className='me-2 my-2' style={{ visibility: 'hidden' }}>1.</div>
              <div className='flex-grow-1'>
                <SelectedOption answerType={props.selectedAnswerType} option={props.option}/>
              </div>
            </div>
        </div>
    </div>
  )
}
