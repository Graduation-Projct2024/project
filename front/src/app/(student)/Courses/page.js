'use client'
import React, { useEffect, useState, useContext } from 'react'
import Layout from '../studentLayout/Layout.jsx'
import axios from 'axios';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { CardActionArea, Grid } from '@mui/material';
import Rating from "@mui/material/Rating";
import Link from '@mui/material/Link';
import { Box } from '@mui/material'
import Divider from '@mui/material/Divider';
import Button from '@mui/material/Button';
import Pagination from '@mui/material/Pagination';
import Stack from '@mui/material/Stack';
import './style.css'
import { UserContext } from '@/context/user/User';
import { useRouter } from 'next/navigation'

export default function page() {
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalPages, setTotalPages] = useState(0);
  const {userToken, setUserToken, userData,userId}=useContext(UserContext);
  const router = useRouter();
const [role, setRole]=useState('student');
    const [courses, setCourses] = useState([]);
    const getCourses = async () => {
      if(userId&&userToken){
        try{
      const data = await axios.get(
        `https://localhost:7116/api/CourseContraller/GetAllCoursesToStudent?studentId=${userId}&pageNumber=${pageNumber}&pageSize=6`,
        {headers :{Authorization:`Bearer ${userToken}`}}

      );
    
      console.log(data);
       setCourses(data.data.result.items);
    }catch(error){
      console.log(error);
    }}
    };
    const handlePageChange = (event, value) => {
      setPageNumber(value);
    };
    const handleClick = (course) => {
      router.push(`/Courses/${course.id}?isEnrolled=${course.isEnrolled}`);
    };
    
  useEffect(() => {
    getCourses();
  }, [page,userId,userToken]);
if (role=='student'){
  return (
    <Layout title='Courses'>
         <Grid container spacing={2}>
      <Grid item xs={12} sm={9} my={2}>
        <Box display="flex" flexWrap="wrap">
        {courses.length ? (
          courses.map((course, index) => (
            <Box
              key={index}
              color="primary.contrastText"
              mb={2}
              m={2}
            >
                       <Button onClick={()=>handleClick(course)}>
<Card sx={{ maxWidth: 230 , borderRadius: 3, height:200 , display:'inline-block', m:2}}  key={course.id}>
      <CardActionArea>
       
                  <img
                    src={course.imageUrl}
                    alt="category"
                    width={230}
        height={150}
                  />
                   <CardContent>
          <Typography gutterBottom variant="subtitle1" component="div">
          {course.name}          </Typography>
          <Box
                        sx={{
                          "& > legend": { mt: 2 },
                        }}
                        className="d-inline-block "
                      >
                     
                      </Box>
         
        </CardContent>
      </CardActionArea>
        
      </Card>
      </Button>
            </Box>
           ))
           ) : (
             <p>No courses yet</p>
           )}
        </Box>
      </Grid>
      <Grid item xs={12} sm={3} my={3}>
        <Box
          sx={{
            position: 'sticky',
            top: 0,
            height: 500,
            background: '#f0f0f0',
            border: '1px solid #ccc',
            borderRadius: '4px',
            zIndex: 999, 
            boxShadow: 4 
          }}
        >
           <Box
           sx={{height: 250, justifyContent: 'center', borderBottom: '1px solid grey', flexDirection: 'column' }}  display="flex"
           alignItems="center"
           >
         <Typography sx={{p:3}}> Choose specific topics to create your own course</Typography> 
         <Button variant="contained"><Link href='/requestCourse' color='inherit' underline='none'>Create Now!</Link></Button>

        </Box>
        <Box
           sx={{height: 250, justifyContent: 'center', borderBottom: '1px solid grey', flexDirection: 'column' }}  display="flex"
           alignItems="center"
           >
         <Typography sx={{p:3}}> If you have questions about a specific topic Book a lecture now!</Typography> 
         <Button variant="contained"><Link href='/calender' color='inherit' underline='none'>Book Now!</Link></Button>

        </Box>
        </Box>
      </Grid>
      <Stack spacing={2} sx={{ width: '100%', maxWidth: 500, margin: '0 auto' }} className='pt-5'>
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
      
    </Grid>
    </Layout>
  )
 }
 
}
