import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import { v4 as uuidv4 } from 'uuid';
import { QuestionBox } from './QuestionBox';
import { Icon } from 'react-icons-kit';
import { circle_plus } from 'react-icons-kit/ikons/circle_plus';

const FormBody = (props) => {
  
  useEffect(() => {
    if (props.questions.length === 0) {
      addAnotherQuestion();
    }
  }, []);

  useEffect(()=>{
    if(props.importing)
    {
      importBody();
    }
    props.setImporting(false);
  },[props.importing])

  const addAnotherQuestion = () => {
    const newQuestionId = uuidv4();

    const newQuestion = {
      id: newQuestionId,
      questionText: '',
      answerType: '',
      isMandatory: true,
      option: [],
    };
    props.setQuestions([...props.questions, newQuestion]);
    props.setFormContent([...props.formContent, newQuestionId]);
  };
  

  const duplicateQuestion = (id) => {
    const questionToDuplicate = props.questions.find((question) => question.id === id);
  console.log(questionToDuplicate);
    if (questionToDuplicate) {
  
      const newQuestion = {
        id: uuidv4(),
        questionText: questionToDuplicate.questionText,
        answerType: questionToDuplicate.answerType,
        isMandatory: questionToDuplicate.isMandatory,
        option: questionToDuplicate.option,
      };
  
      const questionIndex = props.questions.findIndex((question) => question.id === id);
      const updatedQuestions = [...props.questions];
      updatedQuestions.splice(questionIndex + 1, 0, newQuestion);
  
      props.setQuestions(updatedQuestions);
  
      props.setFormContent((prevFormContent) => {
        const index = prevFormContent.indexOf(id);
        if (index !== -1) {
          return [
            ...prevFormContent.slice(0, index + 1),
            newQuestion.id,
            ...prevFormContent.slice(index + 1),
          ];
        }
        return prevFormContent;
      });
    }
  };
  
  
  
  
  const importBody = () => {
    fetch(`https://localhost:7199/api/Form/${props.importFormId}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        // 'Authorization': `Bearer ${token}`
      },
    })
      .then((res) => {
        if (res.ok) {
          return res.json();
        }
        throw new Error('Network response was not ok');
      })
      .then((data) => {
        console.log("imported Q:", data);

        const answerTypeMapping = {
          1: 'ShortText',
          2: 'NumberInput',
          3: 'BigText',
          4: 'Ratings',
          5: 'Dropdown',
          6: 'RadioButtons',
          7: 'TypeAhead',
          8: 'CheckBox',
        };
  
        if (data) {
          data.formQuestion.sort((a, b) => a.questionNumber - b.questionNumber);
          console.log("Imported Data:",data);
          const importedQuestions = data.formQuestion.map((question) => {
          const selectedTypeValues = answerTypeMapping[question.questionTypeId];

  
            return {
              id: uuidv4(),
              questionText: question.questionText,
              answerType: selectedTypeValues,
              isMandatory: question.isMandatory,
              option: question.questionOption,
            };
          });
          console.log("lets see the new Q:" , importedQuestions);

          
          
          props.setQuestions(importedQuestions);
        }
      })
      .catch((error) => {
        console.error('Error fetching form data:', error);
        alert('An error occurred while fetching the form data. Please try again later.');
      });
  };
  
  
  
  
  

const deleteQuestion = (id) => {
  const updatedQuestions = props.questions.filter((question) => question.id !== id);
  props.setQuestions(updatedQuestions);

  const updatedContent = props.formContent.filter((itemId) => itemId !== id);
  props.setFormContent(updatedContent);
};
  


  

  const handleAnswerTypeChange = (questionId, newAnswerType) => {
    const questionToUpdate = props.questions.find((question) => question.id === questionId);

    questionToUpdate.option=[];


    const updatedQuestions = props.questions.map((question) => {
      if (question.id === questionId) {
        return { ...question, answerType: newAnswerType };
      }
      return question;
    });
    props.setQuestions(updatedQuestions);
  };

  const handleOptionChange = (questionId, newOption) => {
    const updatedQuestions = props.questions.map((question) => {
      if (question.id === questionId) {
        return { ...question, option: newOption };
      }
      return question;
    });
    props.setQuestions(updatedQuestions);
  };

  

  

  return (
    <>
      <div className="text-center">
        {props.questions.map((question, index) => (
          <QuestionBox
            key={question.id}
            id={question.id}
            index={index}
            onDuplicate={duplicateQuestion}
            onDelete={deleteQuestion}
            formContent={props.formContent}
            selectedAnswerType={question.answerType}
            setSelectedAnswerType={(newAnswerType) => handleAnswerTypeChange(question.id, newAnswerType)}
            questionText={question.questionText} 
            isMandatory={question.isMandatory}
            setMandatory= {(mandatory)=>{
              const updatedMandatory = props.questions.map((q)=>{
                if(q.id === question.id){
                  return{...q, isMandatory: mandatory};
                }
                return q;
              });
              props.setQuestions(updatedMandatory);
            }}
            setQuestionText={(newText) => {
              const updatedQuestions = [...props.questions];
              const questionIndex = updatedQuestions.findIndex((q) => q.id === question.id);
              if (questionIndex !== -1) {
                updatedQuestions[questionIndex].questionText = newText;
                props.setQuestions(updatedQuestions);
              }
            }}
            questions={props.questions}
            option={question.option}
            setOption={(newOption)=>{
              const updatedQuestions = [...props.questions];
              const questionIndex = updatedQuestions.findIndex((q)=>q.id=== question.id);
              if(questionIndex !== -1)
              {
                updatedQuestions[questionIndex].option = newOption;
                props.setQuestions(updatedQuestions);
              }
            }}
            handleOptionChange={handleOptionChange}
          />
        ))}

        <div style={{ display: 'flex', alignItems: 'center' }} className="mt-3">
          <hr style={{ flexGrow: 1, borderTop: '2px solid #808080', margin: '0 10px' }} />
          <button
            type="button"
            className="btn btn-primary mb-2"
            onClick={addAnotherQuestion}
            style={{ backgroundColor: 'rgb(47, 52, 126)', outline: 'rgb(47, 52, 126)' }}
          >
            <Icon icon={circle_plus} size={30} />
          </button>
          <hr style={{ flexGrow: 1, borderTop: '2px solid #808080', margin: '0 10px' }} />
        </div>
      </div>
    </>
  );
};

export default FormBody;
