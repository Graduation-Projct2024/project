'use client'
import  { useContext, useEffect, useState } from 'react'
import './Contact.css'
import Layout from '../AdminLayout/Layout'
import '../dashboard/dashboard.css'
import axios from 'axios';
import Link from 'next/link'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPhone } from '@fortawesome/free-solid-svg-icons'
import { faEnvelope } from '@fortawesome/free-regular-svg-icons'
import '../Profile/[id]/Profile.css'
import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons'
import { UserContext } from '@/context/user/User'
import { FormControl, InputLabel, MenuItem, Pagination, Select, Stack, Tooltip } from '@mui/material'

export default function EmployeeContacts() {
  const {userToken, setUserToken, userData}=useContext(UserContext);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalPages, setTotalPages] = useState(0);
  // const [loading,setLoading]= useState(false);
  
  let [contacts,setContact] = useState([]);
  const fetchContacts = async (pageNum = pageNumber, pageSizeNum = pageSize) => {
    if(userData){
    try{
    // setLoading(true)
    const { data } = await axios.get(`https://localhost:7116/api/Employee/GetAllEmployee?pageNumber=${pageNum}&pageSize=${pageSize}`,
    {
        headers: {
            Authorization: `Bearer ${userToken}`,
        },
    });
    console.log(data);
    setContact(data.result.items);
    setTotalPages(data.result.totalPages);
    
  }
    catch(error){
      console.log(error);
    }
    // finally{
    //   setLoading(false)
    // }
  
  }
  };
  // if (loading) {
  //   return <div>Loading...</div>;
  // }


  useEffect(() => {
    fetchContacts();
  }, [userData, pageNumber, pageSize]);

  const handlePageSizeChange = (event) => {
    setPageSize(event.target.value);
    setPageNumber(1); // Reset to the first page when page size changes
  };
  
  const handlePageChange = (event, value) => {
    setPageNumber(value);
  };

  const [searchTerm, setSearchTerm] = useState('');

  const handleSearch = (event) => {
    setSearchTerm(event.target.value);
  };

  const filteredContacts = contacts.filter((contact) => {
const matchesSearchTerm =
  Object.values(contact).some(
    (value) =>
      typeof value === 'string' && value.toLowerCase().includes(searchTerm.toLowerCase())
  );

return matchesSearchTerm ;
});


  return (
    <>
               <div className="filter py-2 text-end ">
                     <nav className="navbar">
          <div className="container justify-content-end">
                 <form className="d-flex" role="search">
                <input
                    className="form-control me-2"
                    type="search"
                    placeholder="Search"
                    aria-label="Search"
                    value={searchTerm}
                    onChange={handleSearch}
                />
                 <FormControl fullWidth className="w-50">
        <InputLabel id="page-size-select-label">Page Size</InputLabel>
        <Select
        className="justify-content-center"
          labelId="page-size-select-label"
          id="page-size-select"
          value={pageSize}
          label="Page Size"
          onChange={handlePageSizeChange}
        >
          <MenuItem value={5}>5</MenuItem>
          <MenuItem value={10}>10</MenuItem>
          <MenuItem value={20}>20</MenuItem>
          <MenuItem value={50}>50</MenuItem>
        </Select>
      </FormControl>
                <div className="icons d-flex gap-2 pt-3">
                    
                    <div className="dropdown">
  <button className="dropdown-toggle border-0 bg-white edit-pen" type="button" data-bs-toggle="dropdown" aria-expanded="false">
    <FontAwesomeIcon icon={faFilter} />
  </button>
  <ul className="dropdown-menu">
 
  </ul>
</div>
<FontAwesomeIcon icon={faArrowUpFromBracket} />
                    
                </div>
                </form>
               

            </div>
        </nav>
        
      </div>
     

      <div className="row">
        {filteredContacts ? filteredContacts.map((contact)=>(
          <div key={contact.email} className="col-md-4">
                    <div className="card text-center mb-3" style={{ width: "18rem" }}>
                      <div className="card-body m-3">
                      <img src={contact.imageUrl ? contact.imageUrl : "./user1.png"} 
           className="pho pb-3 img-fluid" 
           alt="Profile" 
           onError={(e) => { 
             console.error("Error loading image:", contact.imageUrl); 
             e.target.onerror = null; // prevents looping
             e.target.src = "./user1.png"; // default image if error
           }} />                    
                      <h4 className="card-title contactName">{contact.fName} {contact.lName}</h4>
                        
                        <div className="d-flex justify-content-center gap-3 pt-3">
                        <Tooltip title="phone" placement="top">
                          <Link className='social' href={`tel:${contact.phoneNumber}`}><FontAwesomeIcon icon={faPhone} /></Link></Tooltip>
                        <Tooltip title="Email" placement="top">
                          <Link className='social' href={`mailto:${contact.email}`}><FontAwesomeIcon icon={faEnvelope} /></Link></Tooltip>
                        </div>
                      </div>
                    </div>
                  </div>
        )) : <h1>No Data</h1>}
       
      </div>
       <Stack spacing={2} sx={{ width: '100%', maxWidth: 500, margin: '0 auto' }}>
     
      <Pagination
      className="pb-3"
        count={totalPages}
        page={pageNumber}
        onChange={handlePageChange}
        variant="outlined"
        color="secondary"
        showFirstButton
        showLastButton
      />
    </Stack>
    </>
  )
}
