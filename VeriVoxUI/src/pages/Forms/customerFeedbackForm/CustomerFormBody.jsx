import React,{useEffect,  useState} from 'react'
import 'bootstrap/dist/css/bootstrap.css';
import { v4 as uuidv4 } from 'uuid';
import { QuestionBox } from './QuestionBox';


export const CustomerFormBody = (props) => {
  const [questions, setQuestions] = useState([]);
  const [options,setOptions] = useState();

  const addAnotherQuestion = () => {
    const newQuestionId = uuidv4();
    // const newOptions = {
    //   Id: newQuestionId,
    //   values: [],
    // };

    const newQuestion = {
      id: newQuestionId,
      questionText: '',
      answerType: '',
      isMandatory: props.mandatory,
      option: [],
    };

    // setOptionId(newQuestion.option.Id);
 
    // props.setOptions((prevOptions) => [
    //   ...prevOptions,
    //   newOptions,
    // ]);

    // console.log(props.options)

    setQuestions([...questions, newQuestion]);
    // props.setFormContent([...props.formContent, newQuestionId]);
  };

  useEffect(()=>{
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
    if(props.importedQuestions)
    {
      console.log(props.importedQuestion);
      const newQuestions = props.importedQuestions.map((question)=>{
        const selectedTypeValues = answerTypeMapping[question.questionTypeId];
        return {
          id: uuidv4(),
          questionText: question.questionText,
          answerType: selectedTypeValues,
          isMandatory: question.isMandatory,
          option: question.questionOption,
        };
      });
      console.log("New Question:",newQuestions);
      setQuestions(newQuestions);
    }
    // setQuestions(props.importedQuestions)
    
  },[props.importedQuestions])

  // useEffect(()=>{
  //   console.log("Questions:",questions);
  // },[questions])


  return (
    <>
      {questions.map((question,index)=>(
        <QuestionBox
        Key={question.id}
        id={question.id}
        index={index}
        questionText={question.questionText}
        selectedAnswerType={question.answerType}
        isMandatory={question.isMandatory}
        option={question.option}
        />
      ))}

      <div className='mt-5 text-end' >
        <button className='btn btn-lg btn-primary mx-4'>Clear Form</button>
        <button className='btn btn-lg btn-primary '>Save </button>
      </div>
    </>
  )
}
