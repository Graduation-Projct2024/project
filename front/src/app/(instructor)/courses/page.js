'use client'
import React, { useContext, useEffect, useState } from 'react'
import Box from '@mui/system/Box';
import axios from 'axios';
import Typography from '@mui/material/Typography';
import ArrowCircleRightIcon from '@mui/icons-material/ArrowCircleRight';
import Link from '@mui/material/Link';
import './style.css'
import Layout from '../instructorLayout/Layout.jsx'
import { UserContext } from '../../../context/user/User.jsx';

export default function page() {
  const {userToken, setUserToken, userData}=useContext(UserContext);
console.log(userToken);
  const [courses, setCourses] = useState([]);
  const getCourses = async () => {
    if(userData){
    const data = await axios.get(
      `http://localhost:5134/api/Employee/GetAllCoursesGivenByInstructor?Instructorid=${userData.userId}pageNumber=1&pageSize=10`,{headers :{Authorization:`Bearer ${userToken}`}}
    );
    setCourses(data.data.result.items);
    console.log(data.result.items)
    }
    
  };


  useEffect(() => {
    getCourses();
  }, [userData]);

  return (
    <Layout title='Courses'>
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
</Layout>

  )
}
