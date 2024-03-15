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
import Layout from '../instructorLayout/Layout.jsx'
import { UserContext } from '../../../context/user/User.jsx';
export default function page() {
  const [courses, setCourses] = useState([]);
  const {userToken, setUserToken, userData}=useContext(UserContext);
  console.log(userToken);
  const getCourses = async () => {
    const data = await axios.get(
      `http://localhost:5134/api/CourseContraller`
    );
  
    console.log(data);
    // setCourses(data.products);
  };


  useEffect(() => {
    getCourses();
  }, []);

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
      alignItems="center"
      gap={4}
      p={2}
      
      
      sx={{ border: '1px solid grey' , borderRadius: 3, boxShadow: 3 
      // ,   backgroundImage: "url('./image/bg11.jpg')", backgroundRepeat: "no-repeat",
    }}
    >
      <Typography variant='h4' sx={{ display: 'block', mb:5, ml:3}} color={deepPurple[50]}>Welcome Hala,</Typography>
     
    </Box>
   
    </div>
    <Typography gutterBottom variant="h5" component="div">
          Your Courses         </Typography>
        {/* {products.length ? (
          products.map((product) => (
            <Link href={`MyCourses/${product._id}`}>
<Card sx={{ maxWidth: 200 , borderRadius: 3, height:270 , display:'inline-block', m:2}}  key={product._id}>
      <CardActionArea>
       
                  <img
                    src={product.mainImage.secure_url}
                    // className="card-img-top img-fluid "
                    alt="category"
                    width={200}
        height={150}
                  />
                   <CardContent>
          <Typography gutterBottom variant="h6" component="div">
          {product.name}          </Typography>

         
        </CardContent>
      </CardActionArea>
        
      </Card>
      </Link>
                 
          ))
        ) : (
          <p>No Enrolled Courses Yet</p>
        )} */}
       
        </>
      
</div>
    </Layout>
    
  )
}
