"use client";
import React ,{ useContext }from "react";
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
import { useRouter } from 'next/navigation'
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';
import { UserContext } from "../../../../context/user/User.jsx";
export default function page() {
  const {userToken, setUserToken, userData}=useContext(UserContext);
  const router = useRouter();
  const [open, setOpen] = React.useState(false);
  const handleClose = (event, reason) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };
  const [course, setCourse] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const { courseId } = useParams();
  const getCourses = async () => {

    const data = await axios.get(
      `http://localhost:5134/api/CourseContraller/GetCourseById?id=${courseId}`
    );
  
    console.log(data.data.result);
      setCourse(data.data.result);
  };
  const [studentId, setStudentId]=useState(null);
  const enrollCourse = async () => {
    const formData = new FormData();

    formData.append("courseId", courseId);
formData.append("studentId", userData.userId);
    const data = await axios.post(
      `http://localhost:5134/api/StudentsContraller/EnrollInCourse`,formData,
     { headers: {
        'Content-Type': 'multipart/form-data','Content-Type': 'application/json',Authorization: `Bearer ${userToken}`
      }
},
    );
    // router.push(`/MyCourses/${courseId}`);
    setOpen(true);

    console.log(data);
  };

  useEffect(() => {
    if(userData){
      setStudentId(userData.userId);

    }
    getCourses();
  }, [userData]);
  if (isLoading) {
    return (
      <Box sx={{ display: "flex", justifyContent: "center" }}>
        <CircularProgress color="primary" />
      </Box>
    );
  }
  return (
    <Layout title={course.name}>
       <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
        <Alert
          onClose={handleClose}
          severity="success"
          variant="filled"
          sx={{ width: '100%' }}
        >
          Your request to join this course done successfully, please wait for Accreditation!
        </Alert>
      </Snackbar>
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
              src={`${course.imageUrl}`}
              alt="course Image"
            />
          </Box>
        </Grid>
        <Grid item xs={6} sx={{mt:3}}>
          <Box m={3}>
            <Typography variant="h6" >Category :{course.category}</Typography>
            <Typography variant="h6">Price :{course.price}</Typography>
            <Typography variant="h6">Started At :{course.startDate}</Typography>
            <Typography variant="h6">Online</Typography>
            <Typography variant="h6">Instructor :{course.instructorId}</Typography>
            <Button onClick={enrollCourse} variant="contained" sx={{mt:2}}>Enroll Now!</Button>

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
    <Typography variant="body1" >{course.description}</Typography>

    </div>
    <div className="tab-pane fade" id="Participants-tab-pane" role="tabpanel" aria-labelledby="Participants-tab" tabIndex={0}>
    {" "}
             
   
    
    </div>
   
  </div>
</div> 
    </Layout>
  );
}
