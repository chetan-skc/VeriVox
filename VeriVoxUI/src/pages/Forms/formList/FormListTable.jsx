import React,{useState , useRef, useEffect} from 'react'
import 'bootstrap/dist/css/bootstrap.css';
import { v4 as uuidv4 } from 'uuid';
import { Icon } from 'react-icons-kit';
import { eye } from 'react-icons-kit/fa/eye';
import { edit } from 'react-icons-kit/fa/edit';
import { trashO } from 'react-icons-kit/fa/trashO';
import { user } from 'react-icons-kit/fa/user';
import {ic_fiber_manual_record} from 'react-icons-kit/md/ic_fiber_manual_record'
import {ic_keyboard_arrow_down} from 'react-icons-kit/md/ic_keyboard_arrow_down'
import {ic_clear_twotone} from 'react-icons-kit/md/ic_clear_twotone'
import { Link } from 'react-router-dom';
import RoleCard from './RoleCard'
import {useNavigate} from 'react-router-dom';
import './FormListTableStyle.css';
import '../feedbackForms/OverlayStyle.css'
import { AssignPage } from './AssignPage';
import { AssignPageSaveCard } from './AssignPageSaveCard';
import PreviewPage from "./PreviewPage";

const FormListTable = ({filteredForms , currentPage, itemsPerPage,setFilteredForms,setCurrentPage}) => {

    const [selectedForm,setSelectedForm]=useState(null);
    const [showActions, setShowActions]=useState(false);
    const [actionPosition , setActionPosition] =  useState({ top: 0, left: 0 });
    const [showDeleteConfirmation, setShowDeleteConfirmation] = useState(false);
    const [statusCard, setStatusCard]= useState(false);
    const [createdByData, setCreatedByData] = useState({});

    const actionRef = useRef(null);
    const deleteConfirmationRef = useRef(null);
    const statusRef = useRef(null);
    const descriptionTdRef = useRef(null);

    const startIndex = (currentPage - 1) * itemsPerPage;
    const endIndex = startIndex + itemsPerPage;
    const currentPageItems = filteredForms.slice(startIndex, endIndex);
    const navigate= useNavigate();
    const [hoveredDescription, setHoveredDescription] = useState({ id: null, content: '' });
    const [assignCard,setAssignCard]=useState(false);
    const [isOverlay,setIsOverlay]= useState(false);
    const [companyId,setCompanyId]= useState();
    const [productId,setProductId]= useState();
    const [urlFormId,setUrlFormId]= useState();
    const [urlText,setUrlText]=useState();
    const token = sessionStorage.getItem("jwtToken");
    const [saveCard,setSaveCard] = useState(false);
    const [finalFormName, setFinalFormName] = useState();
    const [finalFormId, setFinalFormId] = useState();
    const [finalProductId,setFinalProductId]= useState();
    const [finalUrlText, setFinalUrlText]= useState();
    // const [finalUrl,setFinalUrl]=useState();
    const [previewData, setPreviewData] = useState(null);
    const [showPreviewPopup, setShowPreviewPopup] = useState(false);


    const openPreviewPopup = (form) => {
      setSelectedForm(form);
      if (selectedForm) {
        fetch(`https://localhost:7199/api/Form/${selectedForm.id}`)
          .then((response) => response.json())
          .then((data) => {
            console.log(data);
            setPreviewData(data);
            console.log("preview", previewData);
          })
          .catch((error) => {
            console.error("Error fetching preview data:", error);
          });
    
          if (previewData != null) {
            setShowPreviewPopup(true);
          }      
      }
    };      
  
    // Function to close the preview popup
    const closePreviewPopup = () => {
      setShowPreviewPopup(false);
      setSelectedForm(null);
    };

    const showPreviewBox = () => {

      return (
        <div className="preview-popup-container">
          <PreviewPage
            onClose={closePreviewPopup}
            object={previewData}
          />
        </div>
      );
  
    };

    useEffect(() => {

        const fetchCreatedByData = async () => {
        const newCreatedByData = {}; 
        
    
        
        const fetchPromises = filteredForms.map(async (form) => {
          try {
            const response = await fetch(`https://localhost:7199/api/Form/GetCreatedBy/${form.id}`, {
              method: 'GET',
              headers: {
                'Content-Type': 'application/json'
              }
            });
    
            if (response.ok) {
              const data = await response.text();
              newCreatedByData[form.id] = data;
            } else {
              throw new Error('Network response was not ok');
            }
          } catch (error) {
            console.error('Error fetching form data:', error);
            alert('An error occurred while fetching the form data. Please try again later.');
          }
        });
    
        
        await Promise.all(fetchPromises);
    
        
        setCreatedByData(newCreatedByData);
      };
    

      fetchCreatedByData();


        document.addEventListener('mousedown', handleOutsideClick);

        return () => {
          document.removeEventListener('mousedown', handleOutsideClick);
        };  
       
    }, [filteredForms])

    const handleThreeDotClick = (form , event) =>{

      if(form.isActive)
      {
        setSelectedForm(form);
        setShowActions(true);

        const rect = event.target.getBoundingClientRect();
        setActionPosition({top:rect.top+35, left: rect.left - 133});

      }

        
    };

    const handleEdit = () => {
        navigate(`/formpage/${selectedForm.id}`); 

         setShowActions(false);
         setSelectedForm(null);
      };

      const handleDelete = () => {
        setShowDeleteConfirmation(true);
      };
    
      const handleCancelDelete = () => {
        setShowDeleteConfirmation(false);
        setSelectedForm(null);
      };
    
      const handleConfirmDelete = () => {
        
        fetch(`https://localhost:7199/api/Form/${selectedForm.id}`, {
          method: 'DELETE',
          headers: {
            'Content-Type': 'application/json',
          },
        })
          .then((response) => {
            if (!response.ok) {
              throw new Error('Network response was not ok');
            }
            
            const updatedForms = filteredForms.filter((form) => form.id !== selectedForm.id);
            setFilteredForms(updatedForms);
      
            
            if (currentPage > 1 && filteredForms.length % itemsPerPage === 1) {
              setCurrentPage(currentPage - 1);
            }
          })
          .catch((error) => {
            console.error('Error deleting form data:', error);
            
            alert('An error occurred while deleting the form data. Please try again later.');
          });
      
          
        setShowDeleteConfirmation(false);
        setSelectedForm(null);
      };
      
    const handleAddToProduct = () => {
        // alert(`Assign to Product clicked for form with ID ${selectedForm.id}`);
        setUrlFormId(selectedForm.id);
        setFinalFormName(selectedForm.name);
        document.body.style.overflow = 'hidden';
        setAssignCard(true);
        setShowActions(false);
        setSelectedForm(null);
      };

    const handleOutsideClick = (event) =>{
        if (actionRef.current && !actionRef.current.contains(event.target)) {
            setShowActions(false);
          }
        if (deleteConfirmationRef.current && !deleteConfirmationRef.current.contains(event.target)) {
            setShowDeleteConfirmation(false);
          }
        if (statusRef.current && !statusRef.current.contains(event.target)) {
            setShowDeleteConfirmation(false);
          }
    }

    const formatDate =(dateString)=> {
        const date = new Date(dateString);
        const options = { year: 'numeric', month: 'long', day: 'numeric' };
        return date.toLocaleDateString(undefined, options);
    }

    const handleStatus = (form ) =>{
        setSelectedForm(form);
        setStatusCard(true);
    }

    const handleCancelStatus = () => {
        setStatusCard(false);
        setSelectedForm(null);
      };

      const handleConfirmStatus = () => {
        const newStatus = !selectedForm.isActive;
      
        const requestBody = {
          isActive: newStatus,
        };
      
        fetch(`https://localhost:7199/api/Form/FormStatusChange${selectedForm.id}`, {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(requestBody),
        })
          .then((response) => {
            if (!response.ok) {
              throw new Error('Network response was not ok');
            }
            return response.json(); 
          })
          .then((data) => { 
            selectedForm.isActive=data.isActive;
          })
          .then(()=>{
            setStatusCard(false);
            setSelectedForm(null);
          })
          .catch((error) => {
            console.error('Error changing status of form:', error);
            alert('An error occurred while changing the status of the form. Please try again later.');
          });

          
      };

      const customDescription = (description) => {
        if (description.length > 30) {
          description = description.match(/.{1,30}/g).join('\n');
        }
      
        const words = description.split(' ');
        
        if (words.length > 9) {
          return words.slice(0, 9).join(' ') + ' ...';
        }
      
        return description;
      };

      const handleDescriptionHover = (event,form) => {
        // const rect = event.target.getBoundingClientRect();
        const buttonRect = event.target.getBoundingClientRect();
        const top = buttonRect.bottom + window.scrollY;
        const left = buttonRect.left + window.scrollX;
        // const width = buttonRect.width;
        

        setActionPosition({ top, left});
        setHoveredDescription({ id: form.id, content: form.description });
      };
    
      const handleDescriptionLeave = () => {
        setHoveredDescription({ id: null, content: '' });
      };

      const handleCloseAssignCard=()=>{
        document.body.style.overflow = 'auto';
        setAssignCard(false);
      }

      useEffect(()=>{
        if(assignCard===false)
        {
          setCompanyId(null);
          setProductId(null);
          setUrlText(null);
          document.body.style.overflow = 'auto';
        }
        
      },[assignCard])

      useEffect(()=>{
        if(companyId!==undefined && productId!== undefined && urlFormId!==undefined)
        {
          fetch(`https://localhost:7199/api/Form/GetUrlText?companyId=${companyId}&productId=${productId}&formId=${urlFormId}`, {
            method: 'GET',
            headers: {
              'Content-Type': 'application/json',
            },
          })
            .then((response) => {
              if (!response.ok) {
                throw new Error('Network response was not ok');
              }
              
              return response.text();
              
            })
            .then((data) => {
              setUrlText(data);
            })
            .catch((error) => {
              console.error('Error fetching options from the API:', error);
            });
        }
      },[companyId,productId,urlFormId])

      

      
  return (
    <div>
      {showPreviewPopup && showPreviewBox()}
      <table className="table table-lg   mt-4 text-center" style={{ border: '3px solid rgba(245,246,249,255)' }} >
          <thead className="thead-dark">
                <tr>
                    <th scope="col" className="bg-gradient" style={{ background: 'rgba(241,243,255,255)', borderBottomColor:'rgb(47, 52, 126)',  borderBottomWidth: '2px'}}>Form Name </th>
                    <th scope="col" className="bg-gradient" style={{ background: 'rgba(241,243,255,255)', borderBottomColor:'rgb(47, 52, 126)',  borderBottomWidth: '2px'}}>Description</th>
                    <th scope="col" className="bg-gradient" style={{ background: 'rgba(241,243,255,255)', borderBottomColor:'rgb(47, 52, 126)',  borderBottomWidth: '2px'}}>Name on Form's URL</th>
                    <th scope="col" className="bg-gradient" style={{ background: 'rgba(241,243,255,255)', borderBottomColor:'rgb(47, 52, 126)',  borderBottomWidth: '2px'}}>Created Date</th>
                    <th scope="col" className="bg-gradient" style={{ background: 'rgba(241,243,255,255)', borderBottomColor:'rgb(47, 52, 126)',  borderBottomWidth: '2px'}}>Created By</th>
                    <th scope="col" className="bg-gradient" style={{ background: 'rgba(241,243,255,255)', borderBottomColor:'rgb(47, 52, 126)',  borderBottomWidth: '2px'}}>Preview</th>
                    <th scope="col" className="bg-gradient" style={{ background: 'rgba(241,243,255,255)', borderBottomColor:'rgb(47, 52, 126)',  borderBottomWidth: '2px'}}>Status</th>
                    <th scope="col" className="bg-gradient" style={{ background: 'rgba(241,243,255,255)', borderBottomColor:'rgb(47, 52, 126)',  borderBottomWidth: '2px'}}>Actions</th>
                </tr>
          </thead>
          <tbody id="form-table-body">
                {currentPageItems.map((form) => (
                    <tr key={form.id} id={`form-row-${form.id}`}>
                        <td>
                        {form.isActive ? (
                          <Link to={`/formdetails/${form.id}`} style={{ textDecoration: 'none', color: 'inherit' }}>
                            <div style={{ cursor: 'pointer' }}>
                              {form.name}
                            </div>
                          </Link>
                        ) : (
                          <div style={{ cursor: 'pointer', color: 'gray' }}>
                            {form.name}
                          </div>
                        )}
                        </td>
                        <td ref={descriptionTdRef} style={{whiteSpace: 'pre-wrap',overflowWrap: 'break-word'}}  onMouseEnter={(e) => handleDescriptionHover(e,form)} onMouseLeave={handleDescriptionLeave}>
                        {form.isActive ? (
                          <Link to={`/formdetails/${form.id}`} style={{ textDecoration: 'none', color: 'inherit' }}>
                            <div style={{ cursor: 'pointer' }}>
                              {customDescription(form.description)}
                            </div>
                          </Link>
                        ) : (
                          <div style={{ cursor: 'pointer', color: 'gray' }}>
                            {customDescription(form.description)}
                          </div>
                        )}
                      </td>
                      <td>
                        {form.isActive ? (
                          <Link to={`/formdetails/${form.id}`} style={{ textDecoration: 'none', color: 'inherit' }}>
                            <div style={{ cursor: 'pointer' }}>
                              {form.nameOnFormURL}
                            </div>
                          </Link>
                        ) : (
                          <div style={{ cursor: 'pointer', color: 'gray' }}>
                            {form.nameOnFormURL}
                          </div>
                        )}
                      </td>
                      <td>
                        {form.isActive ? (
                          <Link to={`/formdetails/${form.id}`} style={{ textDecoration: 'none', color: 'inherit' }}>
                            <div style={{ cursor: 'pointer' }}>
                              {formatDate(form.createdDate)}
                            </div>
                          </Link>
                        ) : (
                          <div style={{ cursor: 'pointer', color: 'gray' }}>
                            {formatDate(form.createdDate)}
                          </div>
                        )}
                      </td>
                      <td>
                            <div style={{ cursor: 'pointer', display: 'flex', flexDirection: 'column' , alignItems:'center'}}>
                              <div>
                                {form.isActive ? (
                                  <Link to={`/formdetails/${form.id}`} style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <div>
                                    {createdByData[form.id]}
                                    </div>
                                  </Link>
                                ) : (
                                  <div style={{ color: 'gray' }}>
                                    {createdByData[form.id]}
                                  </div>
                                )}
                              </div>
                              
                                <div >
                                  <RoleCard role={form.scopeId} />
                                </div>
                              
                            </div>
                      </td>
                      <td> 
                            <div onClick={() => { openPreviewPopup(form) }} 
                          style={{backgroundColor:'rgba(241,243,255,255)', color:'rgb(47, 52, 126)',  cursor: 'pointer'}}>
                                <Icon icon={eye} /> Preview
                            </div>
                        </td>

                        <td>
                            
                            <div onClick={(e) => handleStatus(form, e)} style={{ cursor: 'pointer' }}>
                                {form.isActive?
                                <button className='btn btn-outline-success btn-sm' style={{color:'green', backgroundColor:'transparent'}}>
                                    <Icon icon={ic_fiber_manual_record} size={15} style={{color: 'green' }} />  Active <Icon icon={ic_keyboard_arrow_down} size={25} style={{color:'grey'}}/>
                                </button>
                                :
                                <button className='btn btn-outline-danger btn-sm' style={{color:'red', backgroundColor:'transparent'}}>
                                    <Icon icon={ic_fiber_manual_record} size={15} style={{color: 'red' }} />  Inactive <Icon icon={ic_keyboard_arrow_down} size={25} style={{color:'grey'}}/>
                                </button>
                                }
                            </div>
                            {selectedForm === form}
                        </td>
                        <td>
                            <div className='fw-bold' onClick={(e) => handleThreeDotClick(form, e)} style={{cursor:'pointer', backgroundColor: 'rgba(241,243,255,255)',borderRadius:'100%',width:'30%',marginLeft:'35%', paddingBottom:'8%'}}>
                                ...
                            </div>
                        </td>
                    </tr>
                ))}
          </tbody>
        </table>

        <div>
            {showActions && (
              <>
              <div className='overlay'></div>
              <div
                ref={actionRef}
                className='speech-bubble-2'  
                style={
                  showDeleteConfirmation
                    ? {
                        position: 'absolute',
                        top: `${actionPosition.top}px`,
                        left: `${actionPosition.left}px`,
                        zIndex: 1000,
                        backgroundColor: 'white',
                        minWidth:'180px'
                      }
                    : {
                        position: 'absolute',
                        top: `${actionPosition.top}px`,
                        left: `${actionPosition.left}px`,
                        zIndex: 20000,
                        backgroundColor: 'white',
                        minWidth:'180px'
                      }
                }
              >
                <div className='speech-bubble-2-content'>
                  <div className='mt-2 fw-bold' onClick={handleEdit} style={{ cursor: 'pointer',color:'black' }}>
                    <Icon icon={edit} style={{ color: 'rgb(47, 52, 126)' , backgroundColor:'rgba(241,243,255,255)', borderRadius:'40%',padding:'1%' }} /> Edit
                  </div>
                  <div className='mt-2 fw-bold' onClick={handleDelete} style={{ cursor: 'pointer',color:'black' }}>
                    <Icon icon={trashO} style={{ color: 'rgb(47, 52, 126)', backgroundColor:'rgba(241,243,255,255)', borderRadius:'40%',padding:'1%'}} /> Delete
                  </div>
                  <div className='mt-2 fw-bold' onClick={handleAddToProduct} style={{ cursor: 'pointer',color:'black' }}>
                    <Icon icon={user} style={{ color: 'rgb(47, 52, 126)', backgroundColor:'rgba(241,243,255,255)', borderRadius:'40%',padding:'1%'}} /> Assign to Product
                  </div>
                </div>
              </div>
              </>
            )}
          </div>

        <div>
                {showDeleteConfirmation && (
                <>
                <div className='overlay' ></div>
                <div
                className="card"
                style={{
                    height: '300px',
                    width: '500px',
                    position: 'fixed',
                    top: '50%',
                    left: '50%',
                    transform: 'translate(-50%, -50%)',
                    borderRadius: '5%',
                    zIndex:30000
                }}
                ref={deleteConfirmationRef}
                >
                <div className="card-body text-center">
                    <Icon className="mt-2" icon={trashO} size={40} style={{ color: 'red' }} />
                    <p className="fs-3 fw-bold mt-2">Delete This Form</p>
                    <p>You are about to delete {selectedForm.name}. It will be gone forever, and you won't be able to recover it.</p>
                    <button className="btn btn-secondary btn-lg mx-2 col-4" onClick={handleCancelDelete}>Cancel</button>
                    <button className="btn btn-danger btn-lg mx-2 col-4" onClick={handleConfirmDelete}>Delete</button>
                </div>
                </div>
                </>
                )}
        </div>

        <div>
            {statusCard &&(
                <>
                <div className='overlay'></div>
                <div 
                className='card'
                style={{
                    height: '300px',
                    width: '500px',
                    position: 'fixed',
                    top: '50%',
                    left: '50%',
                    transform: 'translate(-50%, -50%)',
                    borderRadius: '5%',
                    zIndex:20000
                }}
                ref={statusRef}
                >
                    <div className="card-body text-center">
                        <Icon className="mt-2" icon={ic_clear_twotone} size={60} style={{ color: 'rgb(47, 52, 126)', backgroundColor:'rgb(198, 201, 244)',borderRadius:'50%'}} />
                        <p className="fs-3 fw-bold mt-2">Change status to {selectedForm.isActive? "Inactive" : "Active"} </p>
                        <p>You're about to change status of '{selectedForm.name}'<br/> to {selectedForm.isActive? "Inactive" : "Active"} </p>
                        <button className="btn btn-secondary btn-lg mx-2 col-4" onClick={handleCancelStatus}>Cancel</button>
                        <button className="btn btn-danger btn-lg mx-2 col-4" onClick={handleConfirmStatus}>Yes</button>
                    </div>
                </div>
                </>
            )}
        </div>
        <div>
          {hoveredDescription.id && (
            <div
              className="description-bubble"
              style={{
                position: 'absolute',
                width: '250px',
                top: `${actionPosition.top}px`,
                left: `${actionPosition.left}px`,
                zIndex: 999,
                overflow:'auto',
                // backgroundColor:' rgba(52, 60, 81, 1)',
                // boxShadow:' 0 2px 10px rgba(0, 0, 0, 0.2)',
                // borderRadius:'10px',

              }}
            >
              <div className="description-content">
                {hoveredDescription.content}
              </div>
            </div>
          )}
        </div>
        <div>
            {assignCard && (
              <div className="overlay"></div>
            )}
            <div className={`card-slide ${assignCard ? "open" : ""}`}>
              {saveCard ? 
              <AssignPageSaveCard assignCard={assignCard} setAssignCard={setAssignCard} saveCard={saveCard} setSaveCard={setSaveCard} finalFormId={finalFormId} finalProductId={finalProductId} finalUrlText={finalUrlText} finalFormName={finalFormName} />
              :
              <AssignPage handleCloseAssignCard={handleCloseAssignCard} companyId={companyId} setCompanyId={setCompanyId} productId={productId} setProductId={setProductId} urlText={urlText} setUrlText={setUrlText} assignCard={assignCard} urlFormId={urlFormId} setAssignCard={setAssignCard} saveCard={saveCard} setSaveCard={setSaveCard} setFinalFormId={setFinalFormId} setFinalProductId={setFinalProductId} setFinalUrlText={setFinalUrlText}/>
              }
              
            </div>
          </div>


    </div>
  )
}

export default FormListTable
