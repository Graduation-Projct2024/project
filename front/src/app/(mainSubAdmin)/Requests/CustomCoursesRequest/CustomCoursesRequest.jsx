'use client'
import { UserContext } from '@/context/user/User';
import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControl, InputLabel, MenuItem, Pagination, Select, Stack, useMediaQuery, useTheme } from '@mui/material';
import axios from 'axios';
import React, { useContext, useEffect, useState } from 'react'
import SingleCustomCourse from './[id]/SingleCustomCourse';

export default function CustomCoursesRequest() {

    const {userToken, setUserToken, userData}=useContext(UserContext);
    const [customCourses, setCustomCourses] = useState([]);
    // const [loading,setLoading] = useState(true);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(0);
  
    const [openDisplay, setOpenDisplay] = React.useState(false);
const theme = useTheme();
  const fullScreen = useMediaQuery(theme.breakpoints.down('md'));
  const [courseId, setCourseId] = useState(null);
const handleClickOpenDisplay = (id) => {
  setCourseId(id);
    console.log(id)
    setOpenDisplay(true);
};
const handleCloseDisplay = () => {
  setOpenDisplay(false);
};
  

    const fetchRequestsForCustomCourses = async (pageNum = pageNumber, pageSizeNum = pageSize) => {
      if(userData){
      try{
      const { data } = await axios.get(`https://localhost:7116/api/CourseContraller/GetAllCustomCourses?pageNumber=${pageNum}&pageSize=${pageSize}`,{headers :{Authorization:`Bearer ${userToken}`}});
      // setLoading(false)
      // console.log(data.result);
      setCustomCourses(data.result.items);
      setTotalPages(data.result.totalPages);
    }
      catch(error){
        console.log(error);
      }
    }
    };


    useEffect(() => {
      fetchRequestsForCustomCourses();
    }, [customCourses,userData, pageNumber, pageSize]);  // Fetch courses on mount and when page or size changes
    
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

    const filteredCustomCourses = customCourses.filter((course) => {
      const matchesSearchTerm =
      Object.values(course).some(
          (value) =>
          typeof value === 'string' && value.toLowerCase().includes(searchTerm.toLowerCase())
      );


      return matchesSearchTerm;
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
               

            </div>
        </nav>
        
      </div>

  
      <table className="table">
  <thead>
    <tr>
      <th scope="col">#</th>
      <th scope="col">Name</th>
      <th scope="col">Start date</th>
      <th scope="col">End date</th>
      <th scope="col">Student Name</th>
      <th scope="col">Action</th>
    </tr>
  </thead>
  <tbody>
  {filteredCustomCourses.length ? (
    filteredCustomCourses.map((course,index) =>(
      <tr key={course.id}>
      <th scope="row">{++index}</th>
      <td>{course.name}</td>
      <td>{course.startDate}</td>
      <td>{course.endDate}</td>
      <td>{course.studentFName} {course.studentLName}</td>
      <td className='d-flex gap-1'>
      <button className="border-0 bg-white" type="button" onClick={() => handleClickOpenDisplay(course.id)}>
                <FontAwesomeIcon icon={faEye} className="edit-pen" />
            </button>
            <Dialog
        fullScreen={fullScreen}
        open={openDisplay && courseId === course.id}
        onClose={handleCloseDisplay}
        aria-labelledby="responsive-dialog-title"
        sx={{
          "& .MuiDialog-container": {
            "& .MuiPaper-root": {
              width: "100%",
              maxWidth: "600px!important",  
              height: "400px!important",            },
          },
          
        }}
        >
          <DialogTitle id="responsive-dialog-title" className='primaryColor fw-bold' >
          {"Custom course details"}
        </DialogTitle>

        <DialogContent>
        <Stack
   direction="row"
   spacing={1}
   sx={{ justifyContent: 'center',  alignContent: 'center'}}
    >
<SingleCustomCourse id = {course.id}/>
     </Stack>
        </DialogContent>
        <DialogActions>
         
         <Button onClick={handleCloseDisplay} autoFocus>
           Cancle
         </Button>
       </DialogActions>
        </Dialog>
        </td>

    </tr>
      ))): (
        <tr>
          <td colSpan="7">No Requests for custom courses yet.</td>
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
