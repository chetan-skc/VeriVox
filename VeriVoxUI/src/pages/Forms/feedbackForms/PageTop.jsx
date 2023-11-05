import React,{useEffect, useRef,useState} from 'react'
import 'bootstrap/dist/css/bootstrap.css';
import { Icon } from "react-icons-kit";
import { eye } from "react-icons-kit/fa/eye";
import { download } from "react-icons-kit/fa/download";
import {useNavigate} from 'react-router-dom';
import SearchBar from '../formList/SearchBar'
import './PageTopStyle.css'
import {ic_done} from 'react-icons-kit/md/ic_done'




const PageTop = (props) => {

  const token = sessionStorage.getItem("jwtToken");
  const navigate = useNavigate();
  
  const [saveCard,setSaveCard] = useState(false)
  const [actionPosition , setActionPosition] =  useState({ top: 0, left: 0 });
  const [confirmationCard, setConfirmationCard] = useState(false);

  

  const importRef = useRef(null);
  const saveRef = useRef(null);
  const saveConfirmationRef = useRef(null);


  


  const saveForm = () => {
    console.log("Questions[]:",props.questions);
    
    const areAllQuestionsValid = props.questions.every((question) => {
      return question.questionText.trim() !== '' && question.answerType.trim() !== '';
    });

    if(areAllQuestionsValid&& props.urlName && props.title && props.description)
    {
      const answerTypeMapping = {
        'ShortText': { minimum: 0, maximum: 500, questionTypeId: 1 },
        'NumberInput': { minimum: 0, maximum: 500, questionTypeId: 2 },
        'BigText': { minimum: 100, maximum: 4000, questionTypeId: 3 },
        'Ratings': { minimum: 3, maximum: 10, questionTypeId: 4 },
        'Dropdown': { minimum: 1, maximum: 20, questionTypeId: 5 },
        'RadioButtons': { minimum: 1, maximum: 10, questionTypeId: 6 },
        'TypeAhead': { minimum: 1, maximum: 100, questionTypeId: 7 },
        'CheckBox': { minimum: 1, maximum: 10, questionTypeId: 8 },
    };

      

        const savedQuestion = props.questions.map((question, index) => {
        const selectedTypeValues = answerTypeMapping[question.answerType];
        return {
          // id: question.id,
          questionNumber: index + 1,
          questionText: question.questionText,
          isMandatory: question.isMandatory,
          minimum: selectedTypeValues.minimum,
          maximum: selectedTypeValues.maximum,
          questionTypeId: selectedTypeValues.questionTypeId,
          QuestionOption: question.option
        };
      });
      
      const formData = {
        name:props.title,
        description:props.description,
        nameOnFormUrl:props.urlName,
        formQuestion:savedQuestion
      };

      console.log(formData);
    
      const mockApiUrl = "https://localhost:7199/api/Form";

      fetch(mockApiUrl, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(formData),
      })
      .then((response) => {
        
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        setConfirmationCard(true);
        return response.json();
        
      })
      .catch((error) => {
        console.error("Error saving form:", error);
      });
     
    }
    else
    {
      alert("All questions are not saved...!");
      return;
    }

    if(saveCard)
    setSaveCard(false);
  
    
  };

  const handleCloseImportCard = () =>{
    props.setImportCard(false);
  }

  const handleSearchChange = (value) => {
    props.setSearchTerm(value);
  };

  const handleDocumentClick = (event) => {
    if (importRef.current && !importRef.current.contains(event.target)) {
      props.setImportCard(false);
    }

    if (saveRef.current && !saveRef.current.contains(event.target) && event.target !== document.getElementById('save-button')) {
      setSaveCard(false);
    }

    // if (saveConfirmationRef.current && !saveConfirmationRef.current.contains(event.target)) {
    //   setConfirmationCard(false);
    // }
  };

  useEffect(() => {
    document.addEventListener('mousedown', handleDocumentClick);

        return () => {
          document.removeEventListener('mousedown', handleDocumentClick);
    };
  }, []);

  const handleFormClick=(e,form)=>{
    props.setImportCard(false);
    props.setImportFormId(form.id);
    props.setImporting(true);
  }

  const saveBox = (event) =>{
    setSaveCard(true);

    const buttonRect = document.getElementById('save-button').getBoundingClientRect();
    const top = buttonRect.bottom + window.scrollY;
    const left = buttonRect.left + window.scrollX;
    const width = buttonRect.width;

    setActionPosition({ top, left, width });

    if(saveCard)
    setSaveCard(false);

    
  }

  const handleUpdateForm = () =>{
    // console.log("Question : ",props.questions);

    const areAllQuestionsValid = props.questions.every((question) => {
      return question.questionText.trim() !== '' && question.answerType.trim() !== '';
    });

    if(areAllQuestionsValid&& props.urlName && props.title && props.description)
    {
      const answerTypeMapping = {
        'ShortText': { minimum: 0, maximum: 500, questionTypeId: 1 },
        'NumberInput': { minimum: 0, maximum: 500, questionTypeId: 2 },
        'BigText': { minimum: 100, maximum: 4000, questionTypeId: 3 },
        'Ratings': { minimum: 3, maximum: 10, questionTypeId: 4 },
        'Dropdown': { minimum: 1, maximum: 20, questionTypeId: 5 },
        'RadioButtons': { minimum: 1, maximum: 10, questionTypeId: 6 },
        'TypeAhead': { minimum: 1, maximum: 100, questionTypeId: 7 },
        'CheckBox': { minimum: 1, maximum: 10, questionTypeId: 8 },
    };

      

        const savedQuestion = props.questions.map((question, index) => {
        const selectedTypeValues = answerTypeMapping[question.answerType];
        return {
          questionNumber: index + 1,
          questionText: question.questionText,
          isMandatory: question.isMandatory,
          minimum: selectedTypeValues.minimum,
          maximum: selectedTypeValues.maximum,
          questionTypeId: selectedTypeValues.questionTypeId,
          QuestionOption: question.option
        };
      });
      
      const newFormData = {
        name:props.title,
        description:props.description,
        nameOnFormUrl:props.urlName,
        formQuestion:savedQuestion
      };

      console.log("Updating Form:",newFormData);
      console.log(props.importFormId);
    

    fetch(`https://localhost:7199/api/Form/${props.importFormId}`, {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
          },
          body: JSON.stringify(newFormData),
        })
        .then((response) => {
        
          if (!response.ok) {
            throw new Error("Network response was not ok");
          }
          navigate("/formList");
          return response.json();
          
        })
        .catch((error) => {
          console.error("Error updating form:", error);
        });
      }
      else{
        alert("All questions are not saved...!");
        return;
      }

      
      setSaveCard(false);

  }

  const handleSkipForNow =()=>{
        navigate("/formList");
        setConfirmationCard(false);
  }

  const handleAssignToProduct=()=>{

  }

  return (
    <>
    <div className='col-12'>
        <div className='row'>
            <div className='col-sm-6 fw-bold fs-2'>Create Form</div>
            <div className="col-sm-6 text-end">
                <button type="button" className="btn btn-outline mx-2" onClick={props.handleImportForm} style={{ borderColor: 'rgb(47, 52, 126)' , color:'rgb(47, 52, 126)'}}><Icon icon={download} /> Import Form</button>
                <button type="button" className="btn btn-outline mx-2" style={{ borderColor: 'rgb(47, 52, 126)' , color:'rgb(47, 52, 126)'}}>
                   <div className='d-flex'>
                      <div> <Icon icon={eye}/></div>
                      <div className='mx-2'> Preview</div>
                    </div> 
                </button>
                {props.dualButtonMode ? 
                (
                  <button type="button" className="btn btn  mx-2" id='save-button' onClick={(e)=>saveBox(e)} style={{backgroundColor: 'rgb(47, 52, 126)', color: 'white'  }}>Save Form   &#9662;</button> 
                ):
                (
                  <button type="button" className="btn btn  mx-2" onClick={saveForm} style={{backgroundColor: 'rgb(47, 52, 126)', color: 'white'  }}>Save Form</button>
                )}
                
            </div>
            
        </div>
    </div>
    
    
    <hr className="line bg-dark" style={{height:'1px' , color:'grey'}}/>

    
      <div>
        {props.importCard && (
          <div
          className='card '
          style={{
              height: '550px',
              width: '500px',
              position: 'fixed',
              top: '15%',
              left: '38%',
              zIndex: 999,
              boxShadow: '0 2px 10px rgba(0, 0, 0, 0.2)',
              border: '1px solid #ccc',
              padding:'20px',
              overflow:'auto'
              }}
            ref={importRef}
          >
              <div className='col-12'>
                <div className='row justify-content-between'>
                  <div className='col-sm-6 fw-bold fs-3'>Select a Form</div>
                    <button type="button" className="btn-close" onClick={handleCloseImportCard} aria-label="Close"></button>
                </div>
              </div>
              <hr className="line bg-dark" style={{height:'1px' , color:'grey'}}/>
              
              <div className=''>
              <SearchBar searchTerm={props.searchTerm} onSearchChange={handleSearchChange}/>
              </div>
              <table className='table table-lg table-hover table-bordered table-responsive text-start mt-3' id='custom-table-row' >
                  
                  <tbody>
                    {props.filteredForms.map((form) => (
                      <tr key={form.id} id={`form-row-${form.id}`}>
                        <td onClick={(e)=>handleFormClick(e,form)}>
                              <div style={{cursor:'default'}}>
                              {form.name}
                              </div>
                          </td>
                      </tr>
                    ))}
                  </tbody>
            </table>

          </div>
        )}
      </div>

      <div>
        {saveCard && (
          <div
          ref={saveRef} 
          className='card align-items-start'
          style={{
              position: 'absolute',
              top: `${actionPosition.top}px`,
              left: `${actionPosition.left}px`,
              zIndex: 999,
              backgroundColor: 'transparent',
              boxShadow: '0 2px 10px rgba(0, 0, 0, 0.2)',
              border: '1px solid #ccc',
              minWidth: `${actionPosition.width}px`,
              borderRadius:'10px'
              }}
      >
                  <table className='table table-lg table-hover table-borderless table-responsive text-center' id='custom-table-row' style={{ marginBottom: '0' }} >
                  
                        <tbody >
                            <tr >
                              <td onClick={saveForm}>
                                 <div style={{cursor:'default'}}>Copy</div>   
                              </td>
                             </tr>
                             <tr >
                              <td onClick={handleUpdateForm}>
                                <div style={{cursor:'default'}}>Current</div>  
                              </td>
                             </tr>
                          
                        </tbody>
                  </table>
      </div>
        )}
      </div>

      <div>
        {confirmationCard && (
          <>
          <div
            className="overlay"
            style={{
              position: 'fixed',
              top: 0,
              left: 0,
              width: '100%',
              height: '100%',
              backgroundColor: 'rgba(0, 0, 0, 0.5)', 
              zIndex: 19999,
            }}
          ></div>
          <div
            className="card"
            style={{
              height: '300px',
              width: '550px',
              position: 'fixed',
              top: '50%',
              left: '50%',
              zIndex: 20000,
              transform: 'translate(-50%, -50%)',
              borderRadius: '2%',
              boxShadow: '0 2px 10px rgba(0, 0, 0, 0.2)',
            }}
            ref={saveConfirmationRef}
          >
            <div className="card-body text-center">
              <Icon className="mt-2" icon={ic_done} size={60} style={{ backgroundColor: 'green', color: 'white', borderRadius: '50%' }} />
              <p className="fs-3 mt-2">Successfully created <b> '{props.title}'</b> </p>
              <p style={{color:'grey'}}>Now you can Assign to a product or skip this step and do it later</p>
              <button type="button" className="btn btn-outline mt-3 mx-2" onClick={handleSkipForNow} style={{ borderColor: 'rgb(47, 52, 126)' , color:'rgb(47, 52, 126)'}}> Skip For now</button>
              <button type="button" className="btn mt-3 mx-2" onClick={handleAssignToProduct} style={{backgroundColor: 'rgb(47, 52, 126)', color: 'white'  }}>Assign to a product</button>
              
            </div>
          </div>
        </>
        )}
      </div>

    </>
    
  )
}

export default PageTop;