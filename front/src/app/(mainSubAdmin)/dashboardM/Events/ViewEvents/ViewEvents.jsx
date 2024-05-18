'use client'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useContext, useEffect, useState } from 'react'
import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons'
import CreateEvent from '../CreateEvent/CreateEvent';
import axios from 'axios';
import Link from 'next/link';
import { UserContext } from '@/context/user/User';
import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, useMediaQuery, useTheme } from '@mui/material';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';


export default function ViewEvents() {

    const [events, setEvent] = useState([]);
    const {userToken, setUserToken, userData}=useContext(UserContext);
    const [open, setOpen] = React.useState(false);


    const theme = useTheme();
    const fullScreen = useMediaQuery(theme.breakpoints.down('md'));
  const handleClickOpen = () => {
      setOpen(true);
    };
    const handleClose = () => {
      setOpen(false);
    };
  

    const fetchEvents = async () => {
      if(userData){
      try{
      const { data } = await axios.get(`http://localhost:5134/api/EventContraller/GetAllAccreditEvents?pageNumber=1&pageSize=10`);
      console.log(data);
      setEvent(data.result.items);
    }
      catch(error){
        console.log(error);
      }
    }
    };
  
    useEffect(() => {
      fetchEvents();
    }, [events,userData]);
  
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
                <div className="icons d-flex gap-2 pt-2">
                    
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
                {/* <button type="button" className="btn btn-primary ms-2 addEmp" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                    <span>+ Add new</span> 
                </button> */}
                <Box
        sx={{
          display: 'flex',
          justifyContent: 'flex-end',
          p: 1,
          mr: 6,
        }}
      >
<Button sx={{px:2,m:0.5}} variant="contained" className='primaryBg' startIcon={<AddCircleOutlineIcon />} onClick={handleClickOpen}>
  Add New
</Button>
      </Box>
               

            </div>
        </nav>

        {/* <div className="modal fade" id="exampleModal2" tabIndex="-1" aria-labelledby="exampleModal2Label" aria-hidden="true"> */}
        {/* <div className="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabIndex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
          <div className="modal-dialog modal-dialog-centered modal-lg">
            <div className="modal-content row justify-content-center">
              <div className="modal-body text-center ">
                <h2 className='fs-1'>CREATE  EVENT</h2>
                  <div className="row">
                    <CreateEvent/>
                  </div>
              </div>
            </div>
          </div>
        </div> */}
        <Dialog
        fullScreen={fullScreen}
        open={open}
        onClose={handleClose}
        aria-labelledby="responsive-dialog-title"
        sx={{
          "& .MuiDialog-container": {
            "& .MuiPaper-root": {
              width: "100%",
              maxWidth: "600px!important",  
              height: "500px!important",            },
          },
          
        }}
        >
          <DialogTitle id="responsive-dialog-title" className='primaryColor fw-bold' >
          {"Add New Course"}
        </DialogTitle>

        <DialogContent >
          <CreateEvent/>
        </DialogContent>
        <DialogActions>
         
         <Button onClick={handleClose} autoFocus>
           Cancle
         </Button>
       </DialogActions>
        </Dialog>
        
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


      </>
  )
}
