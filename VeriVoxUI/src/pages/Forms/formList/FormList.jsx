import React,{useState, useEffect} from 'react'
import 'bootstrap/dist/css/bootstrap.css';
import FormListHeader from './FormListHeader'
import SearchBar from './SearchBar'
import FormListTable from './FormListTable'
import FormListPagination from './FormListPagination'
import Navbar from '../../../components/NavBar/NavBar';
import SideBar from '../../../components/SideBar/SideBar';


const FormList = () => {

  const [searchTerm, setSearchTerm] = useState('');
  const [filteredForms, setFilteredForms] = useState([]);
  const [currentPage, setCurrentPage]= useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(3);
  const [loading, setLoading] = useState(true);
  // const [url,setUrl] = useState('')
  // const currentURL = window.location.href;
  

  const token = sessionStorage.getItem("jwtToken");

  useEffect(()=>{
    updateTable();
  },[])

  // useEffect(()=>{
  //   setUrl(window.location.href);
  // })

  useEffect(()=>{
    updateTable();
  },[currentPage, itemsPerPage, searchTerm,token])

  const updateTable = () => {
    setLoading(true);

    const startIndex = (currentPage - 1) * itemsPerPage;

    fetch(`https://localhost:7199/api/Form?startIndex=${startIndex}&itemsPerPage=${itemsPerPage}`, {
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
      setLoading(false);
    })
    .catch((error) => {
      console.error('Error fetching form data:', error)
      alert('An error occurred while fetching the form data. Please try again later.')
      setLoading(false);
    })
  }

  const handleSearchChange = (value) => {
    setSearchTerm(value);
  };

  return (
        <div style={{backgroundColor:'rgba(236,238,243,255)',  minHeight: '100vh', font: 'inherit' , letterSpacing:'inherit' , overflow:'auto'}}>
          <div className='row'>
            <div className='col-12'>
                <div className='shadow-lg col-md-4 '>
                  <SideBar/>
                </div>
                <div className='col-md-8'>
                  <div className='row'>
                    <div className='col-12'><Navbar/></div>
                  </div>
                  <div className='col-12' >
                      <div className=' p-4 overflow-auto' 
                      style=
                      {{ 
                        marginTop:'18vh', 
                        marginLeft:'20vw', 
                        width:'76vw', 
                        backgroundColor:'white', 
                        height:'75vh',
                        // '@media (min-width: 76vw)': {
                        //   height: '80vh', // Adjust this value as needed
                        // },
                        }}>
                        <div className='row'>
                          <div className='col-md-12'>
                              <FormListHeader/> 
                          </div>
                          <div className='col-md-5'>
                              <SearchBar searchTerm={searchTerm} onSearchChange={handleSearchChange}/>
                          </div>
                          <div className='col-md-12'>
                              <FormListTable filteredForms={filteredForms} currentPage={currentPage} itemsPerPage={itemsPerPage} setFilteredForms={setFilteredForms} setCurrentPage={setCurrentPage} />
                          </div>
                        </div>
                        <div className='row'>
                          <FormListPagination
                          currentPage={currentPage}
                          itemsPerPage={itemsPerPage}
                          totalItems={filteredForms.length}
                          onPageChange={setCurrentPage}
                          />
                        </div>
                      </div>
                  </div>
              </div>
            </div>
          </div>      
      </div>
  )
}

export default FormList
