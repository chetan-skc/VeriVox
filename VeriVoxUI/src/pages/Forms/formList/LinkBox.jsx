import React,{useEffect, useState} from 'react'
import { Icon } from 'react-icons-kit';
import { trashO } from 'react-icons-kit/fa/trashO';
import './DescriptionInputStyle.css';

export const LinkBox = (props) => {
    const url='http://localhost:3000/customerForms';
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
            // props.linkText=data;
        })
        .catch((error) => {
          console.error('Error fetching options from the API:', error);
        });
    },[])

    const handleDelete = () => {
        props.onDelete(props.Id);
      };

      useEffect(() => {
        if(props.urlText!=null)
        props.setLinkText(`${randomNumber}`);
      }, [randomNumber]);
      

  return (
    <>
        <div className='card-body'>
            <div className='col-12 mt-4'>
                <div className='row'>
                <div className='col-6 mt-4' >{url}/{props.urlText}.{randomNumber}</div>
                <div className='col-6'>
                <div id='description-input-wrapper'>
                    <input
                      id='description-input'
                      placeholder="Add link description"
                      value={props.description}
                      onChange={(e) => props.setDescription(e.target.value)}
                    />
                    <Icon icon={trashO} onClick={handleDelete} id='trash-icon' size={20} />
                </div>
                </div>
                </div>
            </div>
        </div>
    </>
  )
}
