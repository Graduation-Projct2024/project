'use client'
import React, { useContext, useEffect, useState } from 'react'
import axios from 'axios';
import Typography from '@mui/material/Typography';
import ArrowCircleRightIcon from '@mui/icons-material/ArrowCircleRight';
import Link from '@mui/material/Link';
import './style.css'
import Layout from '../instructorLayout/Layout.jsx'
import { UserContext } from '../../../context/user/User.jsx';
import { Box, FormControl, InputLabel, MenuItem, Pagination, Select, Stack } from '@mui/material'

export default function page() {
  const {userToken, setUserToken, userData}=useContext(UserContext);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalPages, setTotalPages] = useState(0);
console.log(userToken);
  const [courses, setCourses] = useState([]);
  const getCourses = async (pageNum = pageNumber, pageSizeNum = pageSize) => {
    if(userData){
      const data = await axios.get(
        `http://localhost:5134/api/Employee/GetAllCoursesGivenByInstructor?Instructorid=${userData.userId}&pageNumber=${pageNum}&pageSize=${pageSize}`,{headers :{Authorization:`Bearer ${userToken}`}}
      );
    setCourses(data.data.result.items);
    setTotalPages(data.data.result.totalPages);
    }
    
  };
  const handlePageSizeChange = (event) => {
    setPageSize(event.target.value);
    setPageNumber(1); // Reset to the first page when page size changes
  };
  
  const handlePageChange = (event, value) => {
    setPageNumber(value);
  };

  useEffect(() => {
    getCourses();
  }, [userData, pageNumber, pageSize]);

  return (
    <Layout title='Courses'>
      <Stack direction="row" justifyContent="flex-end" alignItems="center">
       <FormControl fullWidth className="page-Size mt-5 me-5 " >
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
              </Stack>
      {courses?(
          courses?.map((course) => (
    <Box
      height={150}
      width={1000}
      my={4}
      display="flex"
      alignItems="center"
      gap={4}
    
    >
<img
        width={200}
        height={149}
          src={course.imageUrl}
          className='m-0 p-0 rounded-start border border-3 border-black'

          alt='htgkkkkkkkkkkk'
        />  
        <Typography variant='h5'>{course.name }</Typography>
        <Link href={`courses/${course.id}`}> <ArrowCircleRightIcon sx={{ fontSize: 40 }} /></Link>
         
        </Box>
        ))
        ) : (
          <p>No courses yet.</p>
        )}

<Stack spacing={2} sx={{ width: '100%', maxWidth: 500, margin: '0 auto' }}>
     
     <Pagination
     className="pb-3 "
       count={totalPages}
       page={pageNumber}
       onChange={handlePageChange}
       variant="outlined"
       color="secondary"
       showFirstButton
       showLastButton
     />
   </Stack>
</Layout>

  )
}
