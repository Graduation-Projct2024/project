'use client'
import React, { useContext, useEffect, useState } from 'react'
import Box from '@mui/system/Box';
import axios from 'axios';
import Typography from '@mui/material/Typography';
import ArrowCircleRightIcon from '@mui/icons-material/ArrowCircleRight';
import Link from '@mui/material/Link';
import './style.css'
import Layout from '../studentLayout/Layout.jsx';
import { UserContext } from '../../../context/user/User.jsx';
import { Button } from '@mui/material';
import { useRouter } from 'next/navigation';
export default function page() {
  const {userToken, setUserToken, userData}=useContext(UserContext);
console.log(userToken);
  const [courses, setCourses] = useState([]);
  const router = useRouter();
  const getCourses = async () => {
    if(userData){
      try{
        const {data} = await axios.get(
          `${process.env.NEXT_PUBLIC_EDUCODING_API}CourseContraller/GetAllEnrolledCoursesForAStudent?studentid=${userData.userId}&pageNumber=1&pageSize=10`,{headers :{Authorization:`Bearer ${userToken}`}}
        );
        console.log(data.result);
         setCourses(data.result.items);
        console.log(courses);

      }catch(error){
        console.log(error);
      }
    }
  

  };


  useEffect(() => {
    getCourses();
  }, [userData]);
  return (
    <Layout title='My Courses'>
      {courses.length ? (
          courses.map((course) => (
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
          src={course.course.imageUrl}
          className='m-0 p-0 rounded-start border border-3 border-black'

          alt='Course Image'
        />  
        <Typography variant='h5'>{course.course.name }</Typography>
      {  console.log(course.courseId)}
        {/* <Link href={`MyCourses/${course.courseId}`}> <ArrowCircleRightIcon sx={{ fontSize: 40 }} /></Link> */}
         <Button onClick={()=>router.push(`MyCourses/${course.courseId}`)}><ArrowCircleRightIcon sx={{ fontSize: 40 }} /></Button>
        </Box>
        ))
        ) : (
          <p>No Courses Yet</p>
        )}
</Layout>

  )
}
