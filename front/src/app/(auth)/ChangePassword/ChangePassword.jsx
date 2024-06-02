import Input from '@/component/input/Input';
import { changePss } from '@/component/validation/validation';
import { UserContext } from '@/context/user/User';
import { Button } from '@mui/material';
import axios from 'axios';
import { useFormik } from 'formik';
import React, { useContext } from 'react'
import Swal from 'sweetalert2';

export default function ChangePassword({setOpenChange}) {

  const { userData,userToken,userId } = useContext(UserContext);
  // console.log(userToken)
  const initialValues={
    password: '',
    newpassword: '',
  };

  const onSubmit = async (values) => {
      if(userData){
            try {
              const formData = new FormData();
              formData.append(`userId`, userId)
              formData.append('password', values.password);
              formData.append('newpassword', values.newpassword);
              const { data } = await axios.patch(`https://localhost:7116/api/UserAuth/changePassword`,
              formData,
                {
                  headers: {
                    Authorization: `Bearer ${userToken}`,
                  },
                });
    
              console.log(data);
              formik.resetForm();
              setOpenChange(false);
              
                Swal.fire({
                  title: `Password Changed successfully`,
                  text: "Request Accepted",
                  icon: "success"
                });
              
    
            } catch (error) {
              console.log(error);
            }
          }
        
      
      };

      const formik = useFormik({
          initialValues : initialValues,
          onSubmit,
           validationSchema:changePss
        })

        const inputs =[            
          {
              type : 'password',
              id:'password',
              name:'password',
              title:'old Password',
              value:formik.values.password,
          },
          {
            type : 'password',
            id:'newpassword',
            name:'newpassword',
            title:'New Password',
            value:formik.values.newpassword,
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
    <>
      <form onSubmit={formik.handleSubmit} className="row justify-content-center">

      {renderInputs}
       
      
      <div className='text-center mt-3'>
      <Button sx={{px:2}} variant="contained"
              className="m-2 btn primaryBg"
              type="submit"
              disabled={formik.isSubmitting || Object.keys(formik.errors).length > 0 || Object.keys(formik.touched).length === 0}
            >
              Change!
            </Button>
      </div>
    </form>
    </>
  )
}
