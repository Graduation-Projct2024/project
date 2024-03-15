'use client'
import React, { useEffect, useState } from 'react'
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
export default function page() {
  const [page, setPage] = useState(1);
const [role, setRole]=useState('student');
    const [products, setProducts] = useState([]);
  const getProducts = async () => {
    const { data } = await axios.get(
      `https://ecommerce-node4.vercel.app/products?page=${page}&limit=6`
    );
  
    console.log(data);
    setProducts(data.products);
  };


  useEffect(() => {
    getProducts();
  }, [page]);
if (role=='student'){
  return (
    <Layout title='Courses'>
         <Grid container spacing={2}>
      {/* Dynamic Boxes on the Left */}
      <Grid item xs={12} sm={9} my={2}>
        <Box display="flex" flexWrap="wrap">
        {products.length ? (
          products.map((product, index) => (
            <Box
              key={index}
              // width="30%"
              // height="100px"
              // bgcolor="primary.main"
              color="primary.contrastText"
              mb={2}
              m={2}
            >
                       <Link href={`Courses/${product._id}`}>
<Card sx={{ maxWidth: 230 , borderRadius: 3, height:200 , display:'inline-block', m:2}}  key={product._id}>
      <CardActionArea>
       
                  <img
                    src={product.mainImage.secure_url}
                    // className="card-img-top img-fluid "
                    alt="category"
                    width={230}
        height={150}
                  />
                   <CardContent>
          <Typography gutterBottom variant="subtitle1" component="div">
          {product.name}          </Typography>
          <Box
                        sx={{
                          "& > legend": { mt: 2 },
                        }}
                        className="d-inline-block "
                      >
                        {/* <Rating
                          name="read-only"
                          value={product.avgRating}
                          readOnly
                        /> */}
                      </Box>
         
        </CardContent>
      </CardActionArea>
        
      </Card>
      </Link>
            </Box>
           ))
           ) : (
             <p>No Product</p>
           )}
        </Box>
      </Grid>
      {/* Fixed Box on the Right */}
      <Grid item xs={12} sm={3} my={3}>
        <Box
          sx={{
            position: 'sticky',
            top: 0,
            height: 500,
            background: '#f0f0f0',
            border: '1px solid #ccc',
            borderRadius: '4px',
            zIndex: 999, // Adjust the z-index as per your layout
            boxShadow: 4 
          }}
        >
           <Box
           sx={{height: 250, justifyContent: 'center', borderBottom: '1px solid grey', flexDirection: 'column' }}  display="flex"
           alignItems="center"
           >
         <Typography sx={{p:3}}> Choose specific topics to create your own course</Typography> 
         <Button variant="contained">Create now!</Button>

        </Box>
        <Box
           sx={{height: 250, justifyContent: 'center', borderBottom: '1px solid grey', flexDirection: 'column' }}  display="flex"
           alignItems="center"
           >
         <Typography sx={{p:3}}> If you have questions about a specific topic Book a lecture now!</Typography> 
         <Button variant="contained">Book Now!</Button>

        </Box>
        </Box>
      </Grid>
      <div className="pagenation">
        <Stack spacing={2}>
          <Pagination
            count={5}
            defaultPage={page}
            onChange={(event, value) => setPage(value)}
          />
        </Stack>
      </div>
      
    </Grid>
    </Layout>
  )
 }
 
}
