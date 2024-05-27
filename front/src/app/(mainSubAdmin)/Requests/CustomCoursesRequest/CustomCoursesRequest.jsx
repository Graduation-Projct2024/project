'use client'
import { UserContext } from '@/context/user/User';
import { faArrowUpFromBracket, faFilter } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { FormControl, InputLabel, MenuItem, Pagination, Select, Stack } from '@mui/material';
import axios from 'axios';
import React, { useContext, useEffect, useState } from 'react'

export default function CustomCoursesRequest() {

    const {userToken, setUserToken, userData}=useContext(UserContext);
    const [customCourses, setCustomCourses] = useState([]);
    // const [loading,setLoading] = useState(true);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(0);
  
  

    const fetchRequestsForCustomCourses = async (pageNum = pageNumber, pageSizeNum = pageSize) => {
      if(userData){
      try{
      const { data } = await axios.get(`http://localhost:5134/api/Employee/GetAllCustomCourses?pageNumber=${pageNum}&pageSize=${pageSize}`,{headers :{Authorization:`Bearer ${userToken}`}});
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
      <th scope="col">ID</th>
      <th scope="col">Name</th>
      <th scope="col">Start date</th>
      <th scope="col">End date</th>
      <th scope="col">Student Id</th>
      <th scope="col">Student Name</th>
      <th scope="col">Description</th>
      {/* <th scope="col">Option</th> */}
    </tr>
  </thead>
  <tbody>
  {filteredCustomCourses.length ? (
    filteredCustomCourses.map((course) =>(
      <tr key={course.id}>
      <th scope="row">{course.id}</th>
      <td>{course.name}</td>
      <td>{course.startDate}</td>
      <td>{course.endDate}</td>
      <td>{course.studentId}</td>
      <td>{course.studentFName} {course.studentLName}</td>
      <td>{course.description}</td>
      {/* <td className='d-flex gap-1'>

      <Link href={`/Profile/${course.studentId}`}>
        <button  type="button" className='edit-pen border-0 bg-white '>
        <FontAwesomeIcon icon={faEye} />
        </button>
        </Link>
        </td> */}

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
