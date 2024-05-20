'use client'
import Input from '@/component/input/Input';
import TextArea from '@/component/input/TextArea';
import { createCourse } from '@/component/validation/validation';
import { UserContext } from '@/context/user/User';
import { Button } from '@mui/material';
import axios from 'axios';
import { useFormik } from 'formik';
import React, { useContext } from 'react'
import Swal from 'sweetalert2';

export default function CreateCourse({setOpen}) {
  const {userToken, setUserToken, userData,userId}=useContext(UserContext);


  const initialValues={
    name: '',
    description:'', 
    price:0,
    category: '',
    SubAdminId:userId,
    InstructorId:0,
    startDate:'',
    Deadline:'',
    limitNumberOfStudnet:'',
    image:'',
    
};

const handleFieldChange = (event) => {
  formik.setFieldValue('image', event.target.files[0]); // Set the file directly to image
};

const onSubmit = async (values) => {
  if(userData){

  try {
    const formData = new FormData();
    formData.append('name', values.name);
    formData.append('description', values.description);
    formData.append('price', values.price);
    formData.append('category', values.category);
    formData.append('SubAdminId', values.SubAdminId);
    formData.append('InstructorId', values.InstructorId);
    formData.append('startDate', values.startDate);
    formData.append('Deadline', values.Deadline);
    formData.append('limitNumberOfStudnet', values.limitNumberOfStudnet);
    //formData.append('image', values.image,values.image.name);
    if (values.image) {
      formData.append('image', values.image);
    }
   
    const { data } = await axios.post('http://localhost:5134/api/CourseContraller/CreateCourse',formData,{headers :{Authorization:`Bearer ${userToken}`}});
    
   if(data.isSuccess){
    console.log(data);
    console.log('course created');
    formik.resetForm();
    setOpen(false);
    Swal.fire({
      title: "Course Created Successfully",
      text: "Wait for Admin accredit this Course",
      icon: "success"
    });

    
}
  } catch (error) {
    if (error.isAxiosError) {
      const requestConfig = error.config;
      console.log("Request Configuration:", requestConfig);
    } else {
      console.error("Non-Axios error occurred:", error);
    }
  };
}
};

const formik = useFormik({
  initialValues : initialValues,
  onSubmit,
  validationSchema:createCourse
});


const inputs =[
  {
    type : 'text',
      id:'name',
      name:'name',
      title:'Course name',
      value:formik.values.name,
},

  {
      type : 'number',
      id:'price',
      name:'price',
      title:'Course price',
      value:formik.values.price,
  },
 
    {
        type : 'text',
        id:'category',
        name:'category',
        title:'Course category',
        value:formik.values.category,
    },
    {
      type : 'number',
      id:'limitNumberOfStudnet',
      name:'limitNumberOfStudnet',
      title:'limit number of studnets',
      value:formik.values.limitNumberOfStudnet,
  },
    
  
  {
      type : 'number',
      id:'SubAdminId',
      name:'SubAdminId',
      title:`SubAdmin Id: ${userId}`,
      value:formik.values.SubAdminId,
      disabled: true,
  },
  {
    type : 'number',
    id:'InstructorId',
    name:'InstructorId',
    title:'Instructor Id',
    value:formik.values.InstructorId,
},
{
  type : 'date',
  id:'startDate',
  name:'startDate',
  title:'Course start date',
  value:formik.values.startDate,
},
{
  type : 'date',
  id:'Deadline',
  name:'Deadline',
  title:'Course Deadline',
  value:formik.values.Deadline,
},

    {
        type:'file',
        id:'image',
        name:'image',
        title:'image',
        onChange:handleFieldChange,
    },
    {
      type : 'text',
      id:'description',
      name:'description',
      title:'Description',
      value:formik.values.description,
    },
];

const renderInputs =  inputs.slice(0, -1).map((input,index)=>
  <Input type={input.type} 
        id={input.id}
         name={input.name}
          title={input.title} 
          key={index} 
          errors={formik.errors} 
          onChange={input.onChange||formik.handleChange}
           onBlur={formik.handleBlur}
            touched={formik.touched}
            disabled={input.disabled}
            />  
    );
    const lastInput = inputs[inputs.length - 1];

    const textAraeInput = (
      <TextArea
        type={lastInput.type}
        id={lastInput.id}
        name={lastInput.name}
        value={lastInput.value}
        title={lastInput.title}
        onChange={lastInput.onChange || formik.handleChange}
        onBlur={formik.handleBlur}
        touched={formik.touched}
        errors={formik.errors}
        key={inputs.length - 1}
      />
    );
  return (
    <form onSubmit={formik.handleSubmit} encType="multipart/form-data" >
      <div className="row justify-content-center">
          {renderInputs}
        {textAraeInput}
        
      </div>
      

              
              
      

      {/* <button
        type="submit"
        className="btn btn-primary createButton mt-3 fs-3 px-3 w-50"
        disabled={formik.isSubmitting || Object.keys(formik.errors).length > 0 || Object.keys(formik.touched).length === 0}
      >
        CREATE COURSE
      </button> */}
      <div className='text-center mt-3'>
      <Button sx={{px:2}} variant="contained"
              className="m-2 btn primaryBg"
              type="submit"
              disabled={formik.isSubmitting || Object.keys(formik.errors).length > 0 || Object.keys(formik.touched).length === 0 }
            >
              Add
            </Button>
      </div>
      
    </form>
  )
}
