import React,{useEffect,useState} from 'react'
import { Icon } from 'react-icons-kit';
import {copy} from 'react-icons-kit/icomoon/copy'
import CustomAlert from './CustomAlert';
import {intact} from 'react-icons-kit/iconic/intact'
import './AssignPageSaveCardStyle.css'

export const AssignPageSaveCard = (props) => {

    const token = sessionStorage.getItem("jwtToken");
    const [links,setLinks]=useState();
    const [alertVisible, setAlertVisible] = useState(false);

    const handleCloseSaveCard=()=>{
        props.setAssignCard(false);
        props.setSaveCard(false);
    }

    const url='http://localhost:3000/customerForms';

    useEffect(()=>{
        if(props.finalFormId!==undefined && props.finalProductId!==undefined )
        fetchLinks();
    },[])

    const fetchLinks=()=>{
        console.log("url:",url);
        fetch(`https://localhost:7199/api/Link/${props.finalFormId}?id2=${props.finalProductId}`, {
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
            setLinks(filteredData);
        })
        .catch((error) => {
            console.error('Error fetching form data:', error)
            alert('An error occurred while fetching the form data. Please try again later.')
        })
    }

    const handleCopyLink = (value) => {
        const linkToCopy = `${url}/${props.finalUrlText}.${value}`;
        navigator.clipboard.writeText(linkToCopy)
          .then(() => {
            setAlertVisible(true);
          })
          .catch((err) => {
            console.error('Copy failed:', err);
          });
      };

    useEffect(() => {
        if (alertVisible) {
          const timeout = setTimeout(() => {
            setAlertVisible(false);
          }, 3000);
          return () => clearTimeout(timeout);
        }
      }, [alertVisible]);
    

  return (
    <>
        <div className='card-body' >
            
            <div className="col-12">
                    <div className="row justify-content-between">
                        <div className="col-sm-6 fw-bold fs-4">Links Created</div>
                        <div type="button" className="btn-close mt-1" onClick={handleCloseSaveCard} aria-label="Close"></div>
                    </div>
                    <div>
                        {/* <div className={`alert-slide ${alertVisible ? "open":""}`}>
                            <CustomAlert
                                message={`<b>Link(s) Copied</b><br/>Link copied of ${props.finalFormName} form`}
                                outlineColor={'rgba(172,204,165,255)'}
                                backgroundColor={'rgba(242,255,245,255)'}
                                iconName={intact}
                                iconColor={'white'}
                                iconBackgroundColor={'rgba(71,192,99,255)'}
                            />
                        </div> */}
                    {alertVisible && (
                        <CustomAlert
                            message={`<b>Link(s) Copied</b><br/>Link copied of ${props.finalFormName} form`}
                            outlineColor={'rgba(172,204,165,255)'}
                            backgroundColor={'rgba(242,255,245,255)'}
                            iconName={intact}
                            iconColor={'white'}
                            iconBackgroundColor={'rgba(71,192,99,255)'}
                        />
                        )}
                    </div>
            </div>
            <hr className="line bg-dark" style={{ height: '1px', color: 'grey' }} />
            <div style={{color: 'grey' }}> You can copy the links of {props.finalFormName} form</div>
            <div className='table table-lg mt-4 text-center' style={{overflowY:'auto'}}>
                {links && links.map((link) => (
                    <div key={link.id} id={`link-row-${link.id}`} style={{ display: 'flex', marginBottom: '20px', backgroundColor: 'rgba(236, 238, 243, 255)', borderRadius: '15px', alignItems: 'center', justifyContent: 'center' }}>
                        <div style={{ flex: 1, padding: '1%', borderTopLeftRadius: '15px', borderBottomLeftRadius: '15px' }}>{`${url}/${props.finalUrlText}.${link.value}`}</div>
                        <div style={{ flex: 1, padding: '1%' }}>{link.description}</div>
                        <div style={{ flex: 1, padding: '1%', borderTopRightRadius: '15px', borderBottomRightRadius: '15px' }}>
                            <div style={{backgroundColor: 'white',display: 'inline-block', padding:'3%', borderRadius:'50%', cursor:'pointer'}}  onClick={() => handleCopyLink(link.value)}><Icon icon={copy}/> Copy Link</div>
                        </div>
                    </div>
                ))}
            </div>
            {/* <div>
                <table className='table table-lg mt-4 '  style={{overflowY:'auto'}}>
                    <tbody>
                        {links && links.map((link) => (
                            <tr key={link.id} id={`link-row-${link.id}`} >
                                <td style={{ backgroundColor: 'rgba(236, 238, 243, 255)', borderBottom: '20px solid white' }}>
                                {`${url}/${props.finalUrlText}.${link.value}`}
                                </td>
                                <td style={{ backgroundColor: 'rgba(236, 238, 243, 255)', borderBottom: '20px solid white' }}>
                                    {link.description}
                                </td>
                                <td style={{ backgroundColor: 'rgba(236, 238, 243, 255)', borderBottom: '20px solid white'}}>
                                        <div style={{backgroundColor: 'white',display: 'inline-block', padding:'1%', borderRadius:'50%', cursor:'pointer'}}  onClick={() => handleCopyLink(link.value)}><Icon icon={copy}/> Copy Link</div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>

            </div> */}
            


        </div>
    </>
  )
}
