import React,{useEffect, useState} from 'react'

const LinkGeneration = (props) => {
    const url="abcd";
    const token = sessionStorage.getItem("jwtToken");
    const [randomNumber,setRandomNumber]=useState();

    useEffect(()=>{
        fetch('https://localhost:7199/api/Link', {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`,
        },
      })
      .then((response) => {
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        
        return response.text();
        
      })
        .then((data) => {
            setRandomNumber(data);
        })
        .catch((error) => {
          console.error('Error fetching options from the API:', error);
        });
    },[])


  return (
    <div>
            <div>{url}/{props.urlText}.{randomNumber}</div>
    </div>
  )
}

export default LinkGeneration;