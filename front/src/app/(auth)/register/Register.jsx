
'use client';
import React, { useState } from 'react';
import * as yup from "yup";
import axios from "axios";
import { useFormik } from "formik";
import './style.css'
import Link from '@mui/material/Link';
import Input from '../../../component/input/Input.jsx';
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';

export default function Register() {
  const [open, setOpen] = React.useState(false);
  const handleClose = (event, reason) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };
  const initialValues = {
    userName: "",
    email: "",
    password: "",
    confirmPassword: "",
    role:""
  };
  const onSubmit = async (users) => {
console.log("test")
const formData = new FormData();
formData.append("userName", users.userName);
formData.append("email", users.email);
formData.append("password", users.password);
formData.append("confirmPassword", users.confirmPassword);
formData.append("role", 'student');

const { data } = await axios.post(
  "http://localhost:5134/api/UserAuth/Register",
 formData,
 {headers: {
  'Content-Type': 'multipart/form-data','Content-Type': 'application/json',}}
);
 if(data.isSuccess){
  console.log("test");
//   formik.resetForm();
setOpen(true);

  }
  };
  const validationSchema = yup.object({
    userName: yup
      .string()
      .required("user name is required")
      .min(3, "must be at least 3 character")
      .max(15, "must be at max 15 character"),
    email: yup.string().required("email is required").email(),
    password: yup
      .string()
      .required("password is required")
      .min(3, "must be at least 3 character")
      .max(15, "must be at max 15 character"),
      confirmPassword: yup
      .string()
      .required("password is required")
      .min(3, "must be at least 3 character")
      .max(15, "must be at max 15 character"),
   
  });

  const formik = useFormik({
    initialValues,
    onSubmit,
    validationSchema: validationSchema,
  });
  const inputs = [
    {
      id: "username",
      type: "text",
      name: "userName",
      title: "User Name",
      value: formik.values.userName,
    },
    {
      id: "email",
      type: "email",
      name: "email",
      title: "Email",
      value: formik.values.email,
    },
    {
      id: "password",
      type: "password",
      name: "password",
      title: "Password",
      value: formik.values.password,
    },
    {
      id: "confirmPassword",
      type: "password",
      name: "confirmPassword",
      title: "confirm Password",
      value: formik.values.confirmPassword,
        },
    
  ];
  const renderInputs = inputs.map((input, index) => (
    <Input
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

  return (
    <>
    <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
        <Alert
          onClose={handleClose}
          severity="success"
          variant="filled"
          sx={{ width: '100%' }}
        >
          You have registered successfully!
        </Alert>
      </Snackbar>
      <div className="form-container sign-up">
      <form onSubmit={formik.handleSubmit} encType="multipart/form-data">
        <h2>Create Account</h2>
        
        {renderInputs}
        <div className="text-center mt-3">
        <button
              className="m-2 btn "
              type="submit"
              disabled={!formik.isValid}
            >
              Register
            </button>
        </div>
      </form>
    </div>
    </>
  );
}
