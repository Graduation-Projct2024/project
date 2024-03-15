'use client'
import Input from '@/component/input/Input';
import { createCourse } from '@/component/validation/validation';
import axios from 'axios';
import { useFormik } from 'formik';
import React from 'react'

export default function CreateCourse() {

  const initialValues={
    name: '',
    description:'', 
    price:0,
    category: '',
    SubAdminId:0,
    InstructorId:0,
    image:'',
    
};

const handleFieldChange = (event) => {
  formik.setFieldValue('image', event.target.files[0]); // Set the file directly to image
};

const onSubmit = async (values) => {

  try {
    const formData = new FormData();
    formData.append('name', values.name);
    formData.append('description', values.description);
    formData.append('price', values.price);
    formData.append('category', values.category);
    formData.append('SubAdminId', values.SubAdminId);
    formData.append('InstructorId', values.InstructorId);
    //formData.append('image', values.image,values.image.name);
    if (values.image) {
      formData.append('image', values.image);
    }
   
    const { data } = await axios.post('http://localhost:5134/api/CourseContraller/CreateCourse', formData,
      );
    
   if(data.isSuccess){
    console.log(data);
    console.log('course created');
    formik.resetForm();
}
  } catch (error) {
    if (error.isAxiosError) {
      const requestConfig = error.config;
      console.log("Request Configuration:", requestConfig);
    } else {
      console.error("Non-Axios error occurred:", error);
    }
  };
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
      title:'Course Name',
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
        title:'Course Category',
        value:formik.values.category,
    },
    
  {
      type : 'number',
      id:'SubAdminId',
      name:'SubAdminId',
      title:'SubAdmin Id',
      value:formik.values.SubAdminId,
  },
  {
    type : 'number',
    id:'InstructorId',
    name:'InstructorId',
    title:'Instructor Id',
    value:formik.values.InstructorId,
},
{
  type : 'text',
  id:'description',
  name:'description',
  title:'description',
  value:formik.values.description,
},
    {
        type:'file',
        id:'image',
        name:'image',
        title:'image',
        onChange:handleFieldChange,
    }
];

const renderInputs = inputs.map((input,index)=>
  <Input type={input.type} 
        id={input.id}
         name={input.name}
          title={input.title} 
          key={index} 
          errors={formik.errors} 
          onChange={input.onChange||formik.handleChange}
           onBlur={formik.handleBlur}
            touched={formik.touched}
            />  
    );
  return (
    <form onSubmit={formik.handleSubmit} className="row justify-content-center">
      {renderInputs}
      <div className="col-md-12">
      <button
        type="submit"
        className="btn btn-primary createButton mt-3 fs-3 px-3 w-50"
        disabled={formik.isSubmitting || Object.keys(formik.errors).length > 0 || Object.keys(formik.touched).length === 0}
      >
        CREATE COURSE
      </button>
      
      </div>
      <div className="col-md-12"><button type="button" class="btn btn-secondary createButton mt-3 fs-3 px-3 w-25" data-bs-dismiss="modal">Close</button></div>
    </form>
  )
}
