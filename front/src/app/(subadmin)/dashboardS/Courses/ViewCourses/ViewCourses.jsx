'use client'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useContext, useEffect, useState } from 'react'
import { faArrowUpFromBracket, faEye, faFilter, faPen } from '@fortawesome/free-solid-svg-icons'
import Link from 'next/link';
import axios from 'axios';
import CreateCourse from '../CreateCourse/CreateCourse';
import { UserContext } from '@/context/user/User';
import EditCourse from '../EditCourse/[id]/page';
import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, Stack, useMediaQuery, useTheme } from '@mui/material';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';


export default function ViewCourses() {


  const [courses, setCourses] = useState([]);
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


  const fetchCourses = async () => {
    if(userData){
    try{
    const { data } = await axios.get(`http://localhost:5134/api/CourseContraller`);
    console.log(data.result);
    setCourses(data.result);
  }
    catch(error){
      console.log(error);
    }
  }
  };

  useEffect(() => {
    fetchCourses();
  }, [courses,userData]);

  const [searchTerm, setSearchTerm] = useState('');

  const handleSearch = (event) => {
    setSearchTerm(event.target.value);
  };

  const filteredCourses = Array.isArray(courses) ? courses.filter((course) => {
    const matchesSearchTerm = Object.values(course).some(
      (value) =>
        typeof value === "string" &&
        value.toLowerCase().includes(searchTerm.toLowerCase())
    );
    return matchesSearchTerm;
  }) : [];
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
                {/* <button type="button" className="btn btn-primary ms-2 addEmp" data-bs-toggle="modal" data-bs-target="#staticBackdrop1">
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

        {/* <div className="modal fade" id="exampleModal1" tabIndex="-1" aria-labelledby="exampleModal1Label" aria-hidden="true"> */}
        {/* <div className="modal fade" id="staticBackdrop1" data-bs-backdrop="static" data-bs-keyboard="false" tabIndex="-1" aria-labelledby="staticBackdrop1Label" aria-hidden="true">
          <div className="modal-dialog modal-dialog-centered modal-lg">
            <div className="modal-content row justify-content-center">
              <div className="modal-body text-center ">
                <h2 className='fs-1'>CREATE COURSE</h2>
                  <div className="row">
                    <CreateCourse/>
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
        
      <CreateCourse/>
        </DialogContent>
        <DialogActions>
         
         <Button onClick={handleClose} autoFocus>
           Cancle
         </Button>
       </DialogActions>
        </Dialog>
      </div>
{/* <>
      {filteredCourses.length ? filteredCourses.map((course) =>(
        <img src={course.imageUrl}/>
        
      )) : <p>no imgs</p>}
</> */}
      <table className="table">
  <thead>
    <tr>
      <th scope="col">ID</th>
      <th scope="col">Name</th>
      <th scope="col">Price</th>
      <th scope="col">Category</th>
      <th scope="col">Status</th>
      <th scope="col">Start Date</th>
      <th scope="col">Instructor</th>
      <th scope="col">Option</th>
    </tr>
  </thead>
  <tbody>
  {filteredCourses.length ? (
    filteredCourses.map((course) =>(
      
      <tr key={course.id}>
        {console.log(course.id)}
        {console.log(course.imageUrl)}
      <th scope="row">{course.id}</th>
      <td>{course.name}</td>
      <td>{course.price}</td>
      <td>{course.category}</td>
      <td>{course.status}</td>
      <td>{course.startDate}</td>
      <td>{course.instructorName}</td>
      
      <td className='d-flex gap-1'>
      <button className="border-0 bg-white " type="button" data-bs-toggle="modal" data-bs-target={`#exampleModal2-${course.id}`}>
                    <FontAwesomeIcon icon={faPen} className="edit-pen" />
                  </button>

                  <div className="modal fade" id={`exampleModal2-${course.id}`} tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div className="modal-dialog modal-dialog-centered modal-lg">
                      <div className="modal-content row justify-content-center">
                        <div className="modal-body text-center ">
                          <h2>Edit Course</h2>
                          <div className="row">
                            <EditCourse id = {course.id}  name = {course.name} price = {course.price} category ={course.category} description = {course.description} InstructorId = {course.instructorId} image = {course.imageUrl}/>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
      <Link href={'/Profile'}>
        <button  type="button" className='border-0 bg-white '>
        <FontAwesomeIcon icon={faEye}  className='edit-pen'/>
        </button>
        </Link>
        </td>

    </tr>
    
      ))): (
        <tr>
          <td colSpan="7">No Courses</td>
        </tr>
        )}
    
    
  </tbody>
</table>



      </>
  )
}
