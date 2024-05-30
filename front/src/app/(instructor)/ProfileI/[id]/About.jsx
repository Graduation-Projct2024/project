'use client';
import React, { useEffect, useState, useContext } from 'react'
import TextArea from '@/component/input/TextArea';
import { UserContext } from '@/context/user/User';
import axios from 'axios';
import { useFormik } from 'formik';
import * as yup from "yup";
import { Button } from '@mui/material';
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';

export default function About() {
    const {userToken, setUserToken, userData, userId}=useContext(UserContext);
    const [bio, setBio] = React.useState(false);
    const [Alertopen, setAlertOpen] = React.useState(false);
    const getBio = async () => {
        if (userId) {
          try {
            const { data } = await axios.get(`https://localhost:7116/api/Employee/GetEmployeeById?id=${userId}`);
            setBio(data.result.skillDescription);
        } catch (error) {
         console.log(error);
        }
     }
    };
    const handleClose = (event, reason) => {
      if (reason === 'clickaway') {
        return;
      }
  
      setAlertOpen(false);
    };
    const initialValues = {
        skillDescription: "",
    };
    const onSubmit = async (description) => {
      try{
  const formData = new FormData();
  formData.append("skillDescription", description.skillDescription);

  
  const { data } = await axios.put(
    `https://localhost:7116/api/Instructor/AddASkillDescription?instructorId=${userId}`,
    formData,
    {headers: {
    'Authorization':`Bearer ${userToken}`,
  
      'Content-Type': 'multipart/form-data','Content-Type': 'application/json',
    }}
  
  );
   formik.resetForm();
   setAlertOpen(true);
   onClose(); 
   handleCloseAdd();
      }catch(error){
        console.log(error);
      }
    
    };
    const validationSchema = yup.object({
        skillDescription: yup
        .string()
        .required("Description is required"),
  
    });
  
    const formik = useFormik({
      initialValues,
      onSubmit,
      validationSchema: validationSchema,
    });
    const inputs = [
      {
        id: "skillDescription",
        type: "text",
        name: "skillDescription",
        title: "About me",
        value: formik.values.name,
      },

   
    ];
    const renderInputs = inputs.map((input, index) => (
      <TextArea
        type={input.type}
        id={input.id}
        name={input.name}
        value={input.value}
        title={input.title}
        onChange={input.onChange || formik.handleChange}
        onBlur={formik.handleBlur}
        touched={formik.touched}
        errors={formik.errors}
        key={index}
      />
    ));
    useEffect(()=>{
        getBio();
    },[userId, bio])
    
  return (
    <>
     <Snackbar open={Alertopen} autoHideDuration={6000} onClose={handleClose}>
        <Alert
          onClose={handleClose}
          severity="success"
          variant="filled"
          sx={{ width: '100%' }}
        >
          Bio Added successfully!
        </Alert>
      </Snackbar>
      <div className='p-5'>
        <h2 className='pr '>Bio</h2>
          {bio&&<p>{bio}</p>}
    
    
     </div>
    <div className='p-5'>
    <h2 className='pr '>Update Bio</h2>
        <form onSubmit={formik.handleSubmit} className="row justify-content-center">
            
          {renderInputs}
           
          <div className='text-center mt-3'>
          <Button sx={{px:2}} variant="contained"
                  className="m-2 btn primaryBg"
                  type="submit"
                  disabled={!formik.isValid}

                >
                  Add
                </Button>
          </div>
    
    
        </form>
        </div>
        </>
  )
}
