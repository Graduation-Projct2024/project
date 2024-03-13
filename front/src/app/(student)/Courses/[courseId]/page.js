"use client";
import React from "react";
import Layout from "../../studentLayout/Layout.jsx";
import { useEffect, useState } from "react";
import axios from "axios";
import { useParams } from "next/navigation.js";
import Link from "@mui/material/Link";
import Box from "@mui/material/Box";
import "../../../../../node_modules/bootstrap/dist/js/bootstrap.bundle.min.js";
import CircularProgress from "@mui/material/CircularProgress";
import Typography from "@mui/material/Typography";
import Grid from '@mui/system/Unstable_Grid';
import Button from '@mui/material/Button';
import Rating from "@mui/material/Rating";

import './style.css'
export default function page() {
  const [product, setProduct] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const { courseId } = useParams();
  const getProduct = async () => {
    const { data } = await axios.get(
      `https://ecommerce-node4.vercel.app/products/${courseId}`
    );
    if (data.message == "success") {
      setProduct(data.product);
      setIsLoading(false);
      console.log(data.product)
    }
  };
  useEffect(() => {
    getProduct();
  }, []);
  if (isLoading) {
    return (
      <Box sx={{ display: "flex", justifyContent: "center" }}>
        <CircularProgress color="primary" />
      </Box>
    );
  }
  return (
    <Layout title={product.name}>
      
      <Grid container spacing={2}>
        <Grid item xs={3} m={3}>
          <Box
            height={250}
            width={300}
            m={3}
       
          >
            <img
              width={300}
              height={250}
              className="rounded"
              src={product.mainImage.secure_url}
              alt="Product Image"
            />
          </Box>
        </Grid>
        <Grid item xs={6} sx={{mt:3}}>
          <Box m={3}>
            <Typography variant="h6" >Category :{product.categoryId}</Typography>
            <Typography variant="h6">Price :{product.finalPrice}</Typography>
            <Typography variant="h6">Started At :{product.createdAt}</Typography>
            <Typography variant="h6">Expected Duration :{product.createdAt}</Typography>
            <Typography variant="h6">Online</Typography>
            <Typography variant="h6">Instructor :</Typography>
            <Button variant="contained" sx={{mt:2}}><Link href={`/MyCourses/${product._id}`}  color="inherit"  underline='none' >Enroll Now!</Link></Button>

          </Box>
        </Grid>
      </Grid>
      <div >
  <ul className="nav nav-tabs d-flex justify-content-center" id="myTab" role="tablist">
    <li className="nav-item" role="presentation">
      <button className="nav-link active" id="content-tab" data-bs-toggle="tab" data-bs-target="#content-tab-pane" type="button" role="tab" aria-controls="home-tab-pane" aria-selected="true">Description</button>
    </li>
    <li className="nav-item" role="presentation">
      <button className="nav-link" id="Participants-tab" data-bs-toggle="tab" data-bs-target="#Participants-tab-pane" type="button" role="tab" aria-controls="Participants-tab-pane" aria-selected="false">Reviews</button>
    </li>
   
  </ul>
  <div className="tab-content border border-2 mx-3 p-4" id="myTabContent">
    <div className="tab-pane fade show active" id="content-tab-pane" role="tabpanel" aria-labelledby="content-tab" tabIndex={0}>
    <Typography variant="body1" >{product.description}</Typography>

    </div>
    <div className="tab-pane fade" id="Participants-tab-pane" role="tabpanel" aria-labelledby="Participants-tab" tabIndex={0}>
    {" "}
              {product.reviews.length ? (
                product.reviews.map((review, index) => (
                  <div className="" key={review._id}>
                    <div className="d-flex">
                      {/* <p className="fw-bold mt-3 me-3">
                        {" "}
                        <img
                          src={review.createdBy.image.secure_url}
                          alt="hugenerd"
                          width={30}
                          height={30}
                          className="rounded-circle me-2"
                        />
                        {review.createdBy.userName}
                      </p> */}

                      <Box
                        sx={{
                          "& > legend": { mt: 2 },
                        }}
                      >
                        <Rating
                          name="read-only"
                          value={review.rating}
                          readOnly
                          sx={{mt:2}}
                        />
                      </Box>
                    </div>

                    <p className="ms-3">{review.comment}</p>
                  </div>
                ))
              ) : (
                <p>No Product</p>
              )}
   
    
    </div>
   
  </div>
</div>
    </Layout>
  );
}
