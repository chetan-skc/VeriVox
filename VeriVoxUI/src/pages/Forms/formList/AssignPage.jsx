import React,{useState,useEffect} from 'react'
import CompanyList from './CompanyList';
import ProductList from './ProductList';
import { v4 as uuidv4 } from 'uuid';
import { LinkBox } from './LinkBox';

export const AssignPage = (props) => {
    const [linkRows, setLinkRows] = useState([]);
    // const [saveCard,setSaveCard] = useState(false);
    const token = sessionStorage.getItem("jwtToken");

    const addLinkRow = () => {
        if(props.urlText!==null)
        {
            
            const newRow = {
                Id : uuidv4(),
                lText:'',
                description:''

            }
            setLinkRows([...linkRows, newRow]);
        }
    };
    
    const handleLinkRemoveRow = (Id) => {
        const updatedRows = linkRows.filter((row) => row.Id !== Id);
        setLinkRows(updatedRows);

        
      };

    useEffect(()=>{
        if(props.urlText!==null)
        addLinkRow();

        if(props.urlText==null)
        setLinkRows([]);

    },[props.urlText])

    const handleSaveLink = (e, formId, productId) => {
        if (linkRows.length >= 1) {
            const savedLinks = linkRows.map((links) => {
                return {
                    productId: productId,
                    formId: formId,
                    description: links.description,
                    value: links.lText,
                    responseLimit: 0
                };
            });
            // console.log("saved links: ",savedLinks);

            props.setFinalProductId(productId);
            props.setFinalFormId(formId);
            props.setFinalUrlText(props.urlText);


            fetch('https://localhost:7199/api/Form/createLinks', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify(savedLinks),
            })
                .then((response) => {
                    if (!response.ok) {
                        throw new Error("Network response was not ok");
                    }
                    props.setSaveCard(true);
                    return response.json();
                })
                .catch((error) => {
                    console.error("Error saving form:", error);
                });
        } 
        else {
            alert("At least add one link...!!!");
            return;
        }
    };
    




  return (
    <>
    <div className='card-body'>
        <div className="col-12">
            <div className="row justify-content-between">
            <div className="col-sm-6 fw-bold fs-4">Assign to a Product</div>
            <div type="button" className="btn-close mt-1" onClick={props.handleCloseAssignCard} aria-label="Close" ></div>
            </div>
        </div>
        <hr className="line bg-dark" style={{ height: '1px', color: 'grey' }} />
        <div className='col-12'>
            <div className="row">
                <div className="col-6">
                    <CompanyList companyId={props.companyId} setCompanyId={props.setCompanyId} setProductId={props.setProductId} setUrlText={props.setUrlText} setLinkRows={setLinkRows} assignCard={props.assignCard}/>
                </div>
                <div className="col-6">
                    <ProductList companyId={props.companyId} setCompanyId={props.setCompanyId} productId={props.productId} setProductId={props.setProductId} setUrlText={props.setUrlText}/>
                </div>
            </div>
            <div className='row mt-3'>
                    <label className="form-label">FormURLtext</label>
                    <div  style={{color:'grey'}}>{props.urlText}</div>
            </div>
        </div>
        <hr className="line bg-dark mt-4" style={{ height: '1px', color: 'grey' }} />
        <div className='col-12'>
            <div className='row'>
            <div className="col-sm-6 fw-bold fs-4">Links Generated</div>
            </div>
        </div>
        
        {linkRows.map((link) => (
            <LinkBox
                key={link.Id}
                Id={link.Id}
                linkText={link.lText} 
                setLinkText={(newLinkText) => {
                    const updatedLinkRows = linkRows.map((row) => {
                        if (row.Id === link.Id) {
                            return { ...row, lText: newLinkText }; 
                        }
                        return row;
                    });
                    setLinkRows(updatedLinkRows);
                }}
                description={link.description}
                urlText={props.urlText}
                setDescription={(newDescription) => {
                    const updatedLinkRows = linkRows.map((row) => {
                        if (row.Id === link.Id) {
                            return { ...row, description: newDescription };
                        }
                        return row;
                    });
                    setLinkRows(updatedLinkRows);
                }}
                onDelete={handleLinkRemoveRow}
                length={linkRows.length}
            />        
        ))}

            
        <div className='fs-5 mt-4' onClick={addLinkRow} style={{color:'rgb(47, 52, 126)', fontWeight: 'bold', cursor:'pointer'}}> + Create another link</div>
        <div className='text-end'>
        <button className='btn btn-lg mt-5' onClick={(e)=>handleSaveLink(e,props.urlFormId, props.productId)} style={{backgroundColor:'rgb(47, 52, 126)', color:'white',right:'100%'}}>Save Link</button>
        </div>
        
    </div>
    </>
  )
}

