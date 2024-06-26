'use client'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useContext, useEffect, useState } from 'react'
import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons'
import axios from 'axios';
import Swal from 'sweetalert2';
import { UserContext } from '@/context/user/User';
import { FormControl, InputLabel, MenuItem, Pagination, Select, Stack } from '@mui/material';

export default function AccreditEvents() {

    const [accreditEvents, setAccreditEvents] = useState([]);
  let[loader,setLoader] = useState(false);
  // const [processingEvent, setProcessingEvent] = useState(null); // State to track which event is being processed
  const {userToken, setUserToken, userData}=useContext(UserContext);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalPages, setTotalPages] = useState(0);

  const fetchEventsForAccredit =async (pageNum = pageNumber, pageSizeNum = pageSize) => {
    if(userData){
    
    try{
    const { data } = await axios.get(`${process.env.NEXT_PUBLIC_EDUCODING_API}EventContraller/GetAllEventsToAdmin?pageNumber=${pageNum}&pageSize=${pageSize}`,{
      headers: {
          Authorization: `Bearer ${userToken}`,
      },
  });
    console.log(data);
    // setAccreditEvents(data);
    setTotalPages(data.result.totalPages);
    setAccreditEvents(data.result.items);
  }
    catch(error){
      console.log(error);
    }}
  };



  // const accreditEvent = async (eventId , Status) => {
  //   //setLoader(true);
  //   console.log(eventId);
  //   if(userData){
  //   try{
  //   const { data } = await axios.patch(`http://localhost:5134/api/CourseContraller/accreditEvent?eventId=${eventId}&Status=${Status}`,
  // );
  // console.log(data);
  // if(Status === 'accredit'){
  // Swal.fire({
  //   title: `Event Accredited Successfully`,
  //   text: "Check View Events page",
  //   icon: "success"
  // });}
  // else if(Status === 'reject'){
  //   Swal.fire({
  //     icon: "error",
  //     title: "Event Rejected ):",
  //     text: "Opsss...",
      
  //   });

  // }

  // }
  //   catch(error){
  //     console.log(error);
  //   }

  // }
  // };
  const accreditEvent = async (eventId , Status) => {
    if (userData) {
      Swal.fire({
        title: `Are you sure?`,
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes!"
      }).then(async (result) => {
        if (result.isConfirmed) {
          try {
            const { data } = await axios.patch(`${process.env.NEXT_PUBLIC_EDUCODING_API}EventContraller/accreditEvent?eventId=${eventId}`, {Status},
              {
                headers: {
                  Authorization: `Bearer ${userToken}`,
                },
              });
  
            console.log(data);
            if (Status == "accredit") {
              Swal.fire({
                title: `Event Accredit Successully`,
                text: "Request Accepted",
                icon: "success"
              });
            } else if (Status == 'reject') {
              Swal.fire({
                icon: "error",
                title: "Request Rejected ):",
                text: "Opsss...",
              });
            }
  
          } catch (error) {
            console.log(error);
          }
        }
      });
    }
  };


  useEffect(() => {
    fetchEventsForAccredit();
  }, [accreditEvent,userData, pageNumber, pageSize]);

  const handlePageSizeChange = (event) => {
    setPageSize(event.target.value);
    setPageNumber(1); // Reset to the first page when page size changes
  };
  
  const handlePageChange = (event, value) => {
    setPageNumber(value);
  };


if(loader){
  return <p>Loading ...</p>
}
// console.log(accreditEvents);


  const [searchTerm, setSearchTerm] = useState('');
  const handleSearch = (event) => {
    setSearchTerm(event.target.value);
  };

const  filteredAccreditEvents= Array.isArray(accreditEvents) ? accreditEvents.filter((event) => {
  const matchesSearchTerm = Object.values(event).some(
    (value) =>
      typeof value === "string" &&
      value.toLowerCase().includes(searchTerm.toLowerCase())
  );
  return matchesSearchTerm;
}) : [];

const getStatusStyle = (status) => {
  switch (status) {
    case 'accredit':
      return { borderRadius:'6px', color: 'green',backgroundColor:'rgba(0, 128, 0, 0.1)'};
    case 'reject':
      return {  borderRadius:'6px', color: 'red' ,backgroundColor:'rgba(255, 0, 0, 0.1)'};
    default:
      return {  borderRadius:'6px', color: 'rgb(101, 101, 101)' ,backgroundColor:'rgba(128, 128, 128, 0.07)'};
  }
};
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
          <MenuItem value={15}>15</MenuItem>
        </Select>
      </FormControl>
              {/* <div className="icons d-flex gap-2 pt-3">
                <div className="dropdown">
                  <button
                    className="dropdown-toggle border-0 bg-white edit-pen"
                    type="button"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                  >
                    <FontAwesomeIcon icon={faFilter} />
                  </button>
                  <ul className="dropdown-menu">
                    <li>uuu</li>
                  </ul>
                </div>
                <FontAwesomeIcon icon={faArrowUpFromBracket} />
              </div> */}
            </form>
          </div>
        </nav>
      </div>
      
     
      <table className="table">
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">Name</th>
            <th scope="col">Category</th>
            <th scope="col">Event Date</th>
            <th className='text-center' scope="col">SubAdmin</th>
            <th className='text-center' scope="col">Status</th>
            <th className='text-center' scope="col">Option</th>
          </tr>
        </thead>
        <tbody>
          {filteredAccreditEvents.length ? (
            filteredAccreditEvents.map((event,index) => (
               <tr key={event.id} /*className={event.accredited ? "accredited-row" : ""}*/>
              {/* <tr key={event.id} style={{ backgroundColor: accreditRow(event) ? 'green' : (rejectRow(event) ? 'red' : 'white') }}> */}
                <th scope="row">{++index}</th>
                <td >{event.name}</td>
                <td>{event.category}</td>
                <td>{event.dateOfEvent}</td>
                <td className='text-center'>{event.subAdminFName} {event.subAdminLName}</td>
                <td className='text-center align-content-center'><span className='p-2 border-2' style={getStatusStyle(event.status)}>{event.status}</span></td>
                {/* {console.log (event.status)} */}
                <td className="d-flex gap-1">
                  {/* <Link href={"/Profile"}>
                    <button type="button" className="border-0 bg-white ">
                      <FontAwesomeIcon icon={faEye} className="edit-pen" />
                    </button>
                  </Link> */}
                  <button type="button" className="btn accredit" onClick={()=>accreditEvent(event.id,'accredit')} disabled = {event.status == 'accredit' || event.status == 'reject'} >Accredit</button>  
                {/* <Link href='/dashboard' className='text-decoration-none acc'>Accredit </Link> */}
                <button type="button" className="btn accredit" onClick={()=>accreditEvent(event.id,"reject")} disabled = {event.status == 'accredit' || event.status == 'reject'}>Reject</button>

                </td>
              </tr>
            ))
          ) : (
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
