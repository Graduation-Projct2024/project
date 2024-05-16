
'use client';
import { useFormik } from 'formik';
import React, { useContext, useState } from 'react'
import * as yup from "yup";
import axios from 'axios';
import './style.css';
import {  Link } from '@mui/material';
import { useRouter } from 'next/navigation'
import Register from '../register/Register.jsx'
import Input from '../../../component/input/Input.jsx';
import { UserContext } from '../../../context/user/User.jsx';
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';
import Layout from '@/app/(pages)/Layout/Layout';

export default function page() {
  const [open, setOpen] = React.useState(false);

  const handleClose = (event, reason) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };
  
  const {userToken, setUserToken,userData, setUserData}=useContext(UserContext);
  const router = useRouter();
  // if(userToken){
  //   router.push('/dashboard');
  // }


    const [isActive, setIsActive] = useState(false);
  
    const handleRegisterClick = () => {
        setIsActive(true);
    };
  
    const handleLoginClick = () => {
        setIsActive(false);
    };
  
  const initialValues={
    email:'',
    password:'',
  };
  const loginSchema = yup.object({
    email: yup.string().required("email is required").email(),
    password: yup
      .string()
      .required("password is required")
      .min(3, "must be at least 3 character")
      .max(20, "must be at max 20 character"),
  });
  const onSubmit = async (users) => {
    console.log(users);

    const { data } = await axios.post(
      "http://localhost:5134/api/UserAuth/Login",
      users
    );
    console.log(data);

    if (data.isSuccess) {
      setOpen(true);
      localStorage.setItem("userToken", data.result.token);
      setUserToken(data.result.token);
      setUserData(data.result.user);
      console.log(userToken);
      if(data.result.user.role === "admin") {
        router.push('/dashboard');
        }
        if(data.result.user.role == "subadmin") {
          router.push('/dashboardS');
          }
      if(data.result.user.role === "student") {
      router.push('/MyDashboard');
      }
      if(data.result.user.role == "instructor") {
        router.push('/myDashboard');
        }
        if(data.result.user.role == "main-subadmin") {
          router.push('/dashboardM');
          }
    }
  };

  const formik=useFormik(
 {   initialValues,
    onSubmit,
    validationSchema : loginSchema,}
  )
  const inputs = [
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
      title: "password",
      value: formik.values.password,
    },
  ];
  const renderInputs = inputs.map((input, index) => (
    <Input
      type={input.type}
      id={input.id}
      name={input.name}
      value={input.value}
      title={input.title}
      onChange={formik.handleChange}
      onBlur={formik.handleBlur}
      touched={formik.touched}
      errors={formik.errors}
      key={index}
    />
  ));
  return (
    <>
    <Layout>
      <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
        <Alert
          onClose={handleClose}
          severity="success"
          variant="filled"
          sx={{ width: '100%' }}
        >
          Log in succssfully!
        </Alert>
      </Snackbar>
    <div className='Body'>
      <div className={isActive ? "logIncontainer active loginBody" : "logIncontainer loginBody"}id="container">
        <Register/>
  <div className="form-container sign-in">
    <form onSubmit={formik.handleSubmit}>
      <h1 className='pb-3'>Log In</h1>
      
        {renderInputs}
        <div className="text-center mt-3 loginActions">
              <button
                className="m-2 btn "
                type="submit"
                disabled={!formik.isValid}
              >
                Login
              </button>
              {/* <Link  to='/sendCode'>Forget Password?</Link> */}
            </div>
    </form>
  </div>
  <div className="toggle-container">
    <div className="toggle">
      <div className="toggle-panel toggle-left">
        <h1>Welcome Back !</h1>
        <p>Enter your personal details to use all of site features</p>
        <button className="hidden" id="login" onClick={handleLoginClick}>Sign In</button>
      </div>
      <div className="toggle-panel toggle-right">
        <h1>Welcome, Friend!</h1>
        <p>Enter your personal details to use all of site features</p>
        <button className="hidden" id="register"  onClick={handleRegisterClick}>Sign Up</button>
      </div>
    </div>
  </div>
</div>

 
    </div>
    </Layout>
    </>
  )
}
