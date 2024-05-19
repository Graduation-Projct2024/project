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
import CircularProgress from "@mui/material/CircularProgress";
import './style.css'
import Layout from '../instructorLayout/Layout.jsx'
import { UserContext } from '../../../context/user/User.jsx';
export default function page() {
  const [courses, setCourses] = useState([]);
  const [loading , setLoading]=useState(true);
  const {userToken, setUserToken, userData}=useContext(UserContext);
  console.log(userData);
  const getCourses = async () => {
    if(userData){
    const data = await axios.get(
      `http://localhost:5134/api/Employee/GetAllCoursesGivenByInstructor?Instructorid=${userData.userId}&pageNumber=1&pageSize=10`,{headers :{Authorization:`Bearer ${userToken}`}}
    );
    setCourses(data.data.result.items);
    setLoading(false);
    }
    
  };


  useEffect(() => {
    getCourses();
  }, [userData]);

  if (loading) {
    return (
      <Box sx={{ display: "flex", justifyContent: "center" }}>
        <CircularProgress color="primary" />
      </Box>
    );
  }
  return (
    <Layout title='Dashboard'>
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
    {console.log(userData)}
    {userData &&<Typography variant='h4' sx={{ mt: 6, ml: 3 }} color={deepPurple[50]}>Welcome {userData.userName},</Typography>}
    <Typography variant='h6' sx={{ ml: 3 }} color={deepPurple[50]}>Have a nice day!</Typography>
  </Box>
</div>

    <Typography gutterBottom variant="h5" component="div">
          Your Courses         </Typography>
        {courses?.length ? (
          courses.map((course) => (
            <Link href={`courses/${course.id}`}>
<Card sx={{ maxWidth: 200 , borderRadius: 3, height:270 , display:'inline-block', m:2}}  key={course.id}>
      <CardActionArea>
       
                  <img
                    src={course.imageUrl}
                    // className="card-img-top img-fluid "
                    alt="category"
                    width={200}
        height={150}
                  />
                   <CardContent>
          <Typography gutterBottom variant="h6" component="div">
          {course.name}          </Typography>

         
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
