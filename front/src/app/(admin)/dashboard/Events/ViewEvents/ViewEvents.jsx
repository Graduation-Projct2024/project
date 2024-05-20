'use client'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useContext, useEffect, useState } from 'react'
import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons'
import axios from 'axios';
import Link from 'next/link';
import { UserContext } from '@/context/user/User';
import { FormControl, InputLabel, MenuItem, Pagination, Select, Stack } from '@mui/material';

export default function ViewEvents() {

    const [events, setEvent] = useState([]);
    const {userToken, setUserToken, userData}=useContext(UserContext);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(0);


    const fetchEvents = async (pageNum = pageNumber, pageSizeNum = pageSize)  => {
      if(userData){
      try{
      const { data } = await axios.get(`http://localhost:5134/api/EventContraller/GetAllAccreditEvents?pageNumber=${pageNum}&pageSize=${pageSize}`);
      //console.log(data);
      setEvent(data.result.items);
      setTotalPages(data.result.totalPages);
    }
      catch(error){
        console.log(error);
      }
    }
    };
  

    useEffect(() => {
      fetchEvents();
    }, [events,userData, pageNumber, pageSize]);  // Fetch courses on mount and when page or size changes
    
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
  
    const filteredEvents = events.filter((event) => {
  const matchesSearchTerm =
    Object.values(event).some(
      (value) =>
        typeof value === 'string' && value.toLowerCase().includes(searchTerm.toLowerCase())
    );
  return matchesSearchTerm ;
  });


  return (
    <>
    <div className="filter py-2 text-end">
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
                {/* <button type="button" data-bs-toggle="modal" data-bs-target="#staticBackdrop"> */}
               
               

            </div>
        </nav>

        {/* <div className="modal fade" id="exampleModal2" tabIndex="-1" aria-labelledby="exampleModal2Label" aria-hidden="true"> */}
        
        
      </div>

      <table className="table">
  <thead>
    <tr>
      <th scope="col">ID</th>
      <th scope="col">Name</th>
      <th scope="col">Content</th>
      {/* <th scope="col">Category</th> */}
      <th scope="col">Event Date</th>
      <th scope="col">SubAdmin name</th>
      <th scope="col">Option</th>
    </tr>
  </thead>
  <tbody>
  {filteredEvents.length ? (
    filteredEvents.map((event) =>(
      <tr key={event.id}>
        {console.log(event.id)}
      <th scope="row">{event.id}</th>
      <td>{event.name}</td>
      <td>{event.content}</td>
      {/* <td>{event.category}</td> */}
      <td>{event.dateOfEvent}</td>
      <td>{event.subAdminName}</td>
      <td className='d-flex gap-1'>

      <Link href={'/Profile'}>
        <button  type="button" className='border-0 bg-white' >
        <FontAwesomeIcon icon={faEye}  className='edit-pen '/>
        </button>
        </Link>
        </td>

    </tr>
      ))): (
        <tr>
          <td colSpan="7">No Events</td>
        </tr>
        )}
    
    
  </tbody>
</table>
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
