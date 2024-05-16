'use client'
import Input from '@/component/input/Input';
import { editProfile } from '@/component/validation/validation';
import { UserContext } from '@/context/user/User';
import { Button } from '@mui/material';
import axios from 'axios';
import { useFormik } from 'formik';
import React, { useContext, useEffect, useState } from 'react'
import Swal from 'sweetalert2';

export default function EditProfile({id , FName , LName  , gender,phoneNumber,DateOfBirth  , address , image}) {
  const {userToken, setUserToken, userData,userId}=useContext(UserContext);
    const [employeeData, setEmployeeData] = useState({});
    const [selectedGender, setSelectedGender] = useState('');
    // const info = useState({
    //   UId : id,
    //   UFName : `${FName}`,
    //   ULName : LName,
    //   UGender : gender,
    //   UPhoneNumber : phoneNumber,
    //   UDOB : DateOfBirth,
    //   UAddress : address,
    // })
      

    // console.log(info);
console.log(id, FName,LName, gender, phoneNumber, DateOfBirth, address, image)
    console.log(userData);
      const fetchUserData = async () => {
        if(userData){
        try {
          const { data } = await axios.get(`http://localhost:5134/api/UserAuth/GetProfileInfo?id=${id}`);
          console.log(data.result);
          setEmployeeData(data.result);
        } catch (error) {
          console.error('Error fetching employee data:', error);
        }}
      };

useEffect(() => {
      fetchUserData();
    }, []);

    const handleFieldChange = (event) => {
        formik.setFieldValue('image', event.target.files[0]); // Set the file directly to image
      };

     const onSubmit = async (updatedData) => {
      if(userData){
      try {
        const formData = new FormData();
        formData.append('FName', updatedData.FName);
        formData.append('LName', updatedData.LName);
        formData.append('phoneNumber', updatedData.phoneNumber);
        formData.append('address', updatedData.address);
        formData.append('DateOfBirth', updatedData.DateOfBirth);
        formData.append('gender', selectedGender);
        if (updatedData.image) {
            formData.append('image', updatedData.image);
        }
       

        const {data} = await axios.put(`http://localhost:5134/api/UserAuth/EditProfile?id=${id}`, formData);
        if(data.isSuccess){
          console.log('Profile Updated')
            formik.resetForm();
            Swal.fire({
                title: "Profile updated successfully",
                text: "You can see the data updated in your profile",
                icon: "success"
              });
            
        }

      } catch (error) {
        console.error('Error updating employee:', error);
      }}
    };
    //  useEffect(() => {
    //   if (employeeData) {
    //     formik.setValues(employeeData);
    //   }
    // }, [employeeData]);

    // console.log(employeeData)
  
    const formik = useFormik({
      initialValues: {
        FName: `${FName}`,
        LName: `${LName}`,
        phoneNumber: `${phoneNumber}`,
        address: `${address}`,
        DateOfBirth: `${DateOfBirth}`,
        gender: `${gender}`,
        image : `${image}`,
        
      },
      validationSchema:editProfile,
      onSubmit,
    });
  
   
  
    const inputs =[
        {
          type : 'text',
            id:'FName',
            name:'FName',
            title:'First Name',
            value:formik.values.FName,
      },
      
        {
            type : 'text',
            id:'LName',
            name:'LName',
            title:'Last Name',
            value:formik.values.LName,
        },
       
        {
          type : 'text',
          id:'phoneNumber',
          name:'phoneNumber',
          title:'User phoneNumber',
          value:formik.values.phoneNumber,
      },
      {
        type : 'text',
        id:'address',
        name:'address',
        title:'User address',
        value:formik.values.address,
      },
      {
        type : 'date',
        id:'DateOfBirth',
        name:'DateOfBirth',
        title:'Date of birth',
        value:formik.values.DateOfBirth,
    },
  {
          type:'file',
          id:'image',
          name:'image',
          title:'image',
          onChange:handleFieldChange,
      }

      ]

      const renderInputs = inputs.map((input,index)=>
  <Input type={input.type} 
        id={input.id}
         name={input.name}
          title={input.title} 
          value={input.value}
          key={index} 
          errors={formik.errors} 
          onChange={input.onChange||formik.handleChange}
           onBlur={formik.handleBlur}
            touched={formik.touched}
            />
        
    )


  return (
    <form onSubmit={formik.handleSubmit} className="row justify-content-center">
    {renderInputs}
     <div className="col-md-6">
      <select
        className="form-select p-3"
        aria-label="Default select example"
        value={selectedGender}
       onChange={(e) => {
          formik.handleChange(e);
          setSelectedGender(e.target.value);
        }}
      >
        <option value="" disabled>
          Select Gender
        </option>
        <option value="Male">Male</option>
        <option value="Female">Female</option>
      </select>
    </div>
    
    <div className='text-center mt-3'>
      <Button sx={{px:2}} variant="contained"
              className="m-2 btn primaryBg"
              type="submit"
              disabled={formik.isSubmitting || Object.keys(formik.errors).length > 0 || Object.keys(formik.touched).length === 0 }
            >
              Update
            </Button>
      </div>
  </form>
  )
}
