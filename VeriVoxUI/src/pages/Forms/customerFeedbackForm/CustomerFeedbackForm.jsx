import React,{useState, useEffect} from 'react'
import { CustomerFeedbackFormHead } from './CustomerFeedbackFormHead'
import {CustomerFormBody} from './CustomerFormBody'
import { useParams } from "react-router-dom";

const CustomerFeedbackForm = () => {
  const { identifier } = useParams();
  const [tokenValue, setTokenValue] = useState();
  const [formId, setFormId] = useState();
  const [productId, setProductId] = useState();
  const [companyId, setCompanyId] = useState();
  const [productLogo, setProductLogo] = useState();
  const [companyLogo, setCompanyLogo] = useState();
  const [selectedForm, setSelectedForm] = useState();
  const [title, setTitle]=useState();
  const [importedQuestions, setImportedQuestions] = useState([]);
  const [companyName, setCompanyName] = useState();
  
  const token = sessionStorage.getItem("jwtToken");


  useEffect(()=>{
    if (identifier !== undefined) {
      const parts = identifier.split('.');
      if (parts.length === 2) {
        const extractedPart = parts[1];
        setTokenValue(extractedPart);
        // console.log(extractedPart); 
      }
    }
  },[identifier])

  useEffect(()=>{
    if(tokenValue!==undefined)
    {
      getFormId();
      getProductId();
    }
  },[tokenValue])

  useEffect(()=>{
    if(productId!==undefined)
    {
      getCompanyId();
      getProductLogo();
    }
  },[productId])

  useEffect(()=>{
    if(companyId!==undefined)
    {
      getCompanyLogo();
      
    }
  },[companyId])

  const getFormId=()=>{
    fetch(`https://localhost:7199/api/Form/GetFormId?token=${tokenValue}`,{
      method:'GET',
      headers:{
        'Content-Type': 'application/json'
      }
    })
    .then((res) => {
      if(res.ok) {
        return res.json();
      }
      throw new Error('Network response was not ok');
    })
    .then((data) => {
      // console.log(data);
      setFormId(data);

    })
    .catch((error) => {
      console.error('Error fetching form data:', error)
      alert('An error occurred while fetching the form data. Please try again later.')
    })
  }

  const getProductId=()=>{
    fetch(`https://localhost:7199/api/Form/GetProductId?token=${tokenValue}`,{
      method:'GET',
      headers:{
        'Content-Type': 'application/json'
      }
    })
    .then((res) => {
      if(res.ok) {
        return res.json();
      }
      throw new Error('Network response was not ok');
    })
    .then((data) => {
      // console.log(data);
      setProductId(data);

    })
    .catch((error) => {
      console.error('Error fetching form data:', error)
      alert('An error occurred while fetching the form data. Please try again later.')
    })
  }

  const getCompanyId=()=>{
    fetch(`https://localhost:7199/api/Form/GetCompanyId?productId=${productId}`,{
      method:'GET',
      headers:{
        'Content-Type': 'application/json'
      }
    })
    .then((res) => {
      if(res.ok) {
        return res.json();
      }
      throw new Error('Network response was not ok');
    })
    .then((data) => {
      // console.log("Company Id : ",data);
      setCompanyId(data);

    })
    .catch((error) => {
      console.error('Error fetching form data:', error)
      alert('An error occurred while fetching the form data. Please try again later.')
    })
  }

  const getProductLogo=()=>{
    fetch(`https://localhost:7199/api/Product/${productId}`,{
      method:'GET',
      headers:{
        'Content-Type': 'application/json'
      }
    })
    .then((res) => {
      if(res.ok) {
        return res.json();
      }
      throw new Error('Network response was not ok');
    })
    .then((data) => {
      // console.log(data);
      setProductLogo(data.product.logoImage);
    })
    .catch((error) => {
      console.error('Error fetching form data:', error)
      alert('An error occurred while fetching the form data. Please try again later.')
    })

  }

  const getCompanyLogo=()=>{
    fetch(`https://localhost:7199/api/Company/${companyId}`,{
      method:'GET',
      headers:{
        'Content-Type': 'application/json'
      }
    })
    .then((res) => {
      if(res.ok) {
        return res.json();
      }
      throw new Error('Network response was not ok');
    })
    .then((data) => {
      // console.log(data);
      setCompanyLogo(data.logoImage);
      setCompanyName(data.name);

    })
    .catch((error) => {
      console.error('Error fetching form data:', error)
      alert('An error occurred while fetching the form data. Please try again later.')
    })

  }

  useEffect(()=>{
    if(formId!==undefined)
    {
      fetch(`https://localhost:7199/api/Form/${formId}`,{
        method:'GET',
        headers:{
          'Content-Type': 'application/json'
        }
      })
      .then((res)=>{
        if(res.ok){
          return res.json();
        }
        throw new Error('Network response was not ok');
      })
      .then((data) => {
        console.log(data.formQuestion.sort((a, b) => a.questionNumber - b.questionNumber));
        setImportedQuestions(data.formQuestion.sort((a, b) => a.questionNumber - b.questionNumber));
        setSelectedForm(data);
  
      })
      .catch((error) => {
        console.error('Error fetching form data:', error)
        alert('An error occurred while fetching the form data. Please try again later.')
      })
    }
  },[formId])

  useEffect(()=>{
    if(selectedForm!==undefined)
    {
      setTitle(selectedForm.name);
    }
  },[selectedForm])

  return (
    <div  style={{backgroundColor:'rgba(236,238,243,255)',display: 'flex', flexDirection: 'column',minHeight: '100%',alignItems: 'center'  }}>
      <div className='w-50 mt-5' style={{ marginBottom: '40px' }}>
          <CustomerFeedbackFormHead formTitle={title} productLogo={productLogo} companyLogo={companyLogo} companyName={companyName} />
          <CustomerFormBody importedQuestions={importedQuestions}/>
      </div>
      {/* <div>
        <button>Clear Form</button>
        <button>Save </button>
      </div> */}
      {/* <div className='w-75 mt-5'>
          
      </div> */}
    </div>
  )
}

export default CustomerFeedbackForm;
