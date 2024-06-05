'use client'
import Layout from '@/app/(pages)/Layout/Layout'
import Input from '@/component/input/Input';
import { resetPass } from '@/component/validation/validation';
import { Button } from '@mui/material';
import axios from 'axios';
import { useFormik } from 'formik';
import { useRouter } from 'next/navigation';
import React, { useEffect, useState } from 'react'

export default function page() {
    
    const router = useRouter();
    const [email, setEmail] = useState(null);
    const initialValues={
        password: '',
        confirmPassword: '',
    };
    useEffect(() => {
        const params = new URLSearchParams(window.location.search);
        const email = params.get('email');
        setEmail(email);
      }, []);
      console.log(email)


const onSubmit = async (values) => {
    try {
      const formData = new FormData();
      formData.append('password', values.password);
      formData.append('confirmPassword', values.confirmPassword);
      const { data } = await axios.patch(`https://localhost:7116/api/UserAuth/ForgetPassword?email=${email}`, formData,{ headers: {
        'Content-Type': 'application/json',
      }});
      console.log(data);
      formik.resetForm();

    router.push(`/login`,undefined, { shallow: true })

    } catch (error) {
      console.error('Error submitting form:', error);
      console.log('Error response:', error.response);
    }
  };



const formik = useFormik({
  initialValues : initialValues,
  onSubmit,
  validationSchema:resetPass
})
const inputs =[
 
    {
        type : 'password',
        id:'password',
        name:'password',
        title:'New password',
        value:formik.values.password,
    },
    {
        type : 'password',
        id:'confirmPassword',
        name:'confirmPassword',
        title:'Confirm New password',
        value:formik.values.confirmPassword,
    },
]



const renderInputs = inputs.map((input,index)=>
  <Input type={input.type} 
        id={input.id}
         name={input.name}
          title={input.title} 
          key={index} 
          errors={formik.errors} 
          onChange={formik.handleChange}
           onBlur={formik.handleBlur}
            touched={formik.touched}
            />
        
    )

  return (
    <Layout>
        <div className="forgetPass">
            <div className="container">
                <div className="forgetPassSection">
                    <div className="title text-center">
                        <h2>Password Reset</h2>
                    </div>
                    <div className="forgetPassForm">
                    <form
                    onSubmit={formik.handleSubmit}
                    className="row justify-content-center align-items-center flex-column w-100"
                  >
                        {renderInputs}
                    
                    <div className="text-center mt-3">
                      <Button
                        sx={{ px: 2 }}
                        variant="contained"
                        className="m-2 btn primaryBg"
                        type="submit"
                        disabled={
                          formik.isSubmitting ||
                          Object.keys(formik.errors).length > 0 ||
                          Object.keys(formik.touched).length === 0
                        }
                      >
                        Reset
                      </Button>
                      </div>
                      </form>
                    </div>
                </div>
            </div>
        </div>
    </Layout>
    )
}
