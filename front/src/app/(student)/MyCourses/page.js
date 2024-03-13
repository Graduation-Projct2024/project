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

export default function page() {
  const {userToken, setUserToken, userData}=useContext(UserContext);
console.log(userToken);
  const [courses, setCourses] = useState([]);
  const getCourses = async () => {
    const { data } = await axios.get(
      `https://ecommerce-node4.vercel.app/products?page=1&limit=5`
    );
  
    console.log(data);
    setCourses(data.products);
  };


  useEffect(() => {
    getCourses();
  }, []);

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
          src={course.mainImage.secure_url}
          className='m-0 p-0 rounded-start border border-3 border-black'

          alt='htgkkkkkkkkkkk'
        />  
        <Typography variant='h5'>{course.name }</Typography>
        <Link href={`MyCourses/${course._id}`}> <ArrowCircleRightIcon sx={{ fontSize: 40 }} /></Link>
         
        </Box>
        ))
        ) : (
          <p>No Product</p>
        )}
</Layout>

  )
}
