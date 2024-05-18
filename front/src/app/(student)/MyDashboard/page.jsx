'use client'
import React, { useContext } from 'react'
import { Box } from '@mui/material'
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import { CardActionArea } from '@mui/material';
import { deepPurple ,purple} from '@mui/material/colors';
import { useEffect, useState } from "react";
import axios from "axios";
import Link from '@mui/material/Link';
import './style.css'
import Layout from '../studentLayout/Layout.jsx';
import { UserContext } from '../../../context/user/User.jsx';
export default function page() {
  const [courses, setCourses] = useState([]);
  const {userToken, setUserToken, userData}=useContext(UserContext);
  const getCourses = async () => {
    if(userData){
      try{
        const {data} = await axios.get(
          `http://localhost:5134/api/StudentsContraller/GetAllEnrolledCoursesForAStudent?studentid=${userData.userId}&pageNumber=1&pageSize=10`
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
    <Layout>
      <div>
        <>
        <div className='d-flex justify-content-center'>
  <Box
    height={250}
    width={1000}
    my={4}
    display="flex"
    flexDirection="column"
    alignItems="flex-start" 
    gap={4}
    p={2}
    sx={{ border: '1px solid grey', borderRadius: 3, boxShadow: 3 }}
  >
    {userData &&<Typography variant='h4' sx={{ mt: 6, ml: 3 }} color={deepPurple[50]}>Welcome {userData.userName},</Typography>}
    <Typography variant='h6' sx={{ ml: 3 }} color={deepPurple[50]}>Have a nice day!</Typography>
  </Box>
</div>
    <Typography gutterBottom variant="h5" component="div">
          Your Courses         </Typography>
        {courses.length ? (
          courses.map((course) => (
            <Link href={`/MyCourses/${course.courseId}`}>
<Card sx={{ maxWidth: 200 , borderRadius: 3, height:270 , display:'inline-block', m:2}}  key={course.id}>
      <CardActionArea>
       {console.log(course.imageUrl)}
                  <img
                    src={course.course.imageUrl}
                    
                    alt="course image"
                    width={200}
        height={200}
                  />
                   <CardContent>
          <Typography gutterBottom variant="h6" component="div">
          {course.course.name}          </Typography>

         
        </CardContent>
      </CardActionArea>
        
      </Card>
      </Link>
                 
          ))
        ) : (
          <p>No Enrolled Courses Yet</p>
        )}
       
        </>
      
</div>
    </Layout>
    
  )
}
