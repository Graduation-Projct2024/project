'use client'
import Input from '@/component/input/Input';
import { editCourse, editProfile } from '@/component/validation/validation';
import { UserContext } from '@/context/user/User';
import axios from 'axios';
import { useFormik } from 'formik';
import React, { useContext, useEffect, useState } from 'react'
import Swal from 'sweetalert2';

export default function EditCourse({id , name , price  , category,description,InstructorId  , image}) {
  const {userToken, setUserToken, userData,userId}=useContext(UserContext);

    const [course, setCourse] = useState({});
    
      const fetchCourseData = async () => {
        if(userData){
        try {
          const { data } = await axios.get(`http://localhost:5134/api/CourseContraller/GetCourseById?id=${id}`);
          console.log(data.result);
          setCourse(data.result);
        } catch (error) {
          console.error('Error fetching employee data:', error);
        }}
      };

useEffect(() => {
      fetchCourseData();
    }, [id]);

    const handleFieldChange = (event) => {
        formik.setFieldValue('image', event.target.files[0]); // Set the file directly to image
      };

    const onSubmit = async (updatedData) => {
      if(userData){
      try {
        const formData = new FormData();
        formData.append('name', updatedData.name);
        formData.append('price', updatedData.price);
        formData.append('category', updatedData.category);
        formData.append('description', updatedData.description);
        formData.append('InstructorId', updatedData.InstructorId);
        if (updatedData.image) {
            formData.append('image', updatedData.image);
        }
       

        const {data} = await axios.post(`http://localhost:5134/api/CourseContraller/EditCourse?id=${id}`, formData);
        if(data.isSuccess){
          console.log('Course Updated')
            formik.resetForm();
            Swal.fire({
                title: "Course updated successfully",
                text: "You can see the data updated in Dashboard",
                icon: "success"
              });
              

            
        }

      } catch (error) {
        console.error('Error updating course:', error);
      }}
    };
  
    const formik = useFormik({
      initialValues: {
        name: `${name}`,
        price: `${price}`,
        category: `${category}`,
        description: `${description}`,
        InstructorId: `${InstructorId}`,
        image : `${image}`,
        
      },
      validationSchema:editCourse,
      onSubmit,
    });
  
    useEffect(() => {
      if (course) {
        formik.setValues(course);
      }
    }, [course]);

  
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
            title:'Price',
            value:formik.values.price,
        },
       
        {
          type : 'text',
          id:'category',
          name:'category',
          title:'category',
          value:formik.values.category,
      },
      {
        type : 'text',
        id:'description',
        name:'description',
        title:'description',
        value:formik.values.description,
      },
      {
        type : 'number',
        id:'InstructorId',
        name:'InstructorId',
        title:'InstructorId',
        value:formik.values.InstructorId,
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
    
    
    <div className="col-md-12">
    <button
      type="submit"
      className="btn btn-primary createButton mt-3 fs-3 px-3 w-50"
      disabled={formik.isSubmitting || Object.keys(formik.errors).length > 0 || Object.keys(formik.touched).length === 0 }
    >
      Edit Profile
    </button>
    </div>
    <div className="col-md-12"><button type="button" className="btn btn-secondary createButton mt-3 fs-3 px-3 w-25" data-bs-dismiss="modal">Close</button></div>
  </form>
  )
}

