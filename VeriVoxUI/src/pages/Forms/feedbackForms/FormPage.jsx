import React,{useState, useEffect} from 'react';
import { v4 as uuidv4 } from 'uuid';
import PageTop from './PageTop';
import FormHead from './FormHead';
import FormBody from './FormBody'
import Navbar from '../../../components/NavBar/NavBar';
import SideBar from '../../../components/SideBar/SideBar';
import { useParams } from "react-router-dom";

const FormPage = () => {

  const [formContent, setFormContent] = useState([]);
  const [title, setTitle] = useState("Untitled Form");
  const [description, setDescription] = useState("Breif description about the form");
  const [urlName, setUrlName]= useState();
  const [questions, setQuestions] = useState([]);


  
  const { selectedFormId } = useParams();

  const [importCard, setImportCard] = useState(false);
  const token = sessionStorage.getItem("jwtToken");

  const [searchTerm, setSearchTerm] = useState("");
  const [filteredForms, setFilteredForms] = useState([]);
  const [importFormId,setImportFormId] = useState();
  const [importing, setImporting]=useState(false);
  const [dualButtonMode, setDualButtonMode] = useState(false);

 

  useEffect(() => {
    fetchData();
    
  }, [searchTerm]);

  useEffect(()=>{
    if(importing)
    {
      importHead();
    }
    setImporting(false);
  },[importing])

  const handleTitleChange = (event) => {
    setTitle(event.target.value);
  };

  const handleDescriptionChange = (event) => {
    setDescription(event.target.value);
  };

  const handleURLName = (event) => {
    setUrlName(event.target.value);
  };

  const handleImportForm = () =>{
    setImportCard(true);
    fetchData();


  }

  const fetchData=()=>{
    fetch('https://localhost:7199/api/Form', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
    })
    .then((res) => {
      if(res.ok) {
        return res.json();
      }
      throw new Error('Network response was not ok');
    })
    .then((data) => {
      let filteredData = data;

      if(searchTerm) {
        filteredData = data.filter(
          (form) => form.name.toLowerCase().includes(searchTerm.toLowerCase())
        );
      }

      filteredData.sort((a, b) => a.name.localeCompare(b.name));
      setFilteredForms(filteredData);
    })
    .catch((error) => {
      console.error('Error fetching form data:', error)
      alert('An error occurred while fetching the form data. Please try again later.')
    })

  }

  useEffect(() => {
    if (selectedFormId !== undefined) {
      setImportFormId(selectedFormId);
      setImporting(true);
    }
  }, [selectedFormId]);
  

  const importHead=()=>{
    fetch(`https://localhost:7199/api/Form/${importFormId}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
    })
    .then((res) => {
      if(res.ok) {
        return res.json();
      }
      throw new Error('Network response was not ok');
    })
    .then((data) => {
      setTitle(data.name);
      setUrlName(data.nameOnFormURL);
      setDescription(data.description);

    })
    .catch((error) => {
      console.error('Error fetching form data:', error)
      alert('An error occurred while fetching the form data. Please try again later.')
    })
  }

  useEffect(()=>{
    if(importFormId)
    GetAccess();
  },[importFormId]);

  const GetAccess = ()=>{
    fetch(`https://localhost:7199/api/Form/GetAccess/${importFormId}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
    })
    .then((res) => {
      if(res.ok) {
        return res.json();
      }
      throw new Error('Network response was not ok');
    })
    .then((data) => {
      // console.log(data);
      if(data)
     setDualButtonMode(true);

    })
    .catch((error) => {
      console.error('Error fetching form data:', error)
      alert('An error occurred while fetching the form data. Please try again later.')
    })
  }
  
  // style={ active ?
  //   {
  //     position: 'fixed',
  //     top: 0,
  //     left: 0,
  //     width: '100%',
  //     height: '100%',
  //     backgroundColor: 'rgba(0, 0, 0, 0.5)', // You can adjust the background color and opacity
  //     zIndex: 999,
  //   }
  //   :{backgroundColor:'rgb(221, 223, 223)',  minHeight: '100vh'} }
  

  return (
     <div  style={{backgroundColor:'rgba(236,238,243,255)',  minHeight: '100vh'} }>
        <div className='row' >
            <div className='col-12' >
                  <div className='col-4' ><SideBar/></div>
                  <div className='col-8'>
                    <div className='row'>
                      <div className='col-12' ><Navbar/></div>
                    </div>  
                          <div className='  p-5 ' style={{ marginTop:'18vh', marginLeft:'20vw', width:'75vw', backgroundColor:'white'}}>
                            <div className='row'>
                              <div className='col-12'>
                                <PageTop  title={title} description={description} urlName={urlName} questions={questions} handleImportForm={handleImportForm} importCard={importCard} setImportCard={setImportCard} filteredForms={filteredForms} searchTerm={searchTerm} setSearchTerm={setSearchTerm} importFormId={importFormId} setImportFormId={setImportFormId} setImporting={setImporting} dualButtonMode={dualButtonMode} setDualButtonMode={setDualButtonMode}/>
                                <div className='col-12'>
                                    <div className='row'>
                                        <div className='col-9'>
                                            <FormHead title={title} description={description} urlName={urlName} handleTitleChange={handleTitleChange} handleDescriptionChange={handleDescriptionChange} handleURLName={handleURLName} />
                                        </div>
                                        <div >
                                        <FormBody formContent={formContent} setFormContent={setFormContent} questions={questions} setQuestions={setQuestions} importFormId={importFormId} importing={importing} setImporting={setImporting} />
                                        </div>
                                    </div>
                                </div>
                              </div>
                           </div>
                        </div>
                  </div>
            </div>
        </div>
     </div>
  )
}

export default FormPage;