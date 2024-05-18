'use client'
import { UserContext } from '@/context/user/User';
import { useFormik } from 'formik';
import React, { useContext, useEffect, useState } from 'react'

const formatDate = (dateString) => {
    if (!dateString) return '';
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  };
export default function EditCourse({id}) {

    const { userData,userToken } = useContext(UserContext);
  
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
        formData.append('InstructorId', updatedData.InstructorId);
        formData.append('Deadline', updatedData.Deadline);
        formData.append('limitNumberOfStudnet', updatedData.limitNumberOfStudnet);
        formData.append('description', updatedData.description);
        if (updatedData.image) {
          formData.append('image', updatedData.image);
        }
  
        const { data } = await axios.post(`http://localhost:5134/api/CourseContraller/EditCourse?id=${id}`, formData,{headers :{Authorization:`Bearer ${userToken}`}},);
        if (data.isSuccess) {
          console.log('Course Updated');
          formik.resetForm();
          
          Swal.fire({
            title: "Course updated successfully",
            text: "You can see the data updated in this page",
            icon: "success"
          });
  // router.refresh();
        }
      } catch (error) {
        console.error('Error updating employee:', error);
      }}
    };
  
    const formik = useFormik({
      initialValues: {
        name: name || '',
        price: price || '',
        category: category || '',
        InstructorId: InstructorId || '',
        Deadline: formatDate(Deadline) || '',
        limitNumberOfStudnet: limitNumberOfStudnet || '',
        description: description || '',
        image: image || null,
      },
      // validationSchema: editProfile,
      onSubmit,
    });
  
    useEffect(() => {
      formik.setValues({
        name,
        price,
        category,
        InstructorId,
        Deadline: formatDate(Deadline),
        limitNumberOfStudnet,
        description,
        image,
      });
      setSelectedGender(gender);
    }, [name, price, category, InstructorId, Deadline,limitNumberOfStudnet, description, image]);
  
    const inputs = [
      {
        type: 'text',
        id: 'name',
        name: 'name',
        title: 'course Name',
        value: formik.values.name,
      },
      {
        type: 'number',
        id: 'price',
        name: 'price',
        title: 'price',
        value: formik.values.price,
      },
      {
        type: 'text',
        id: 'category',
        name: 'category',
        title: 'Course category',
        value: formik.values.category,
      },
      {
        type: 'number',
        id: 'InstructorId',
        name: 'InstructorId',
        title: 'InstructorId',
        value: formik.values.InstructorId,
      },
      {
        type: 'date',
        id: 'Deadline',
        name: 'Deadline',
        title: 'Deadline',
        value: formik.values.Deadline,
      },
      {
        type: 'number',
        id: 'limitNumberOfStudnet',
        name: 'limitNumberOfStudnet',
        title: 'limitNumberOfStudnet',
        value: formik.values.limitNumberOfStudnet,
      },
      
      {
        type: 'file',
        id: 'image',
        name: 'image',
        title: 'Image',
        onChange: handleFieldChange,
      },
      {
        type: 'text',
        id: 'description',
        name: 'description',
        title: 'Course description',
        value: formik.values.description,
      },
    ];
  
    const renderInputs = inputs.slice(0, -1).map((input, index) => (
      <Input
        type={input.type}
        id={input.id}
        name={input.name}
        title={input.title}
        value={input.value}
        key={index}
        errors={formik.errors}
        onChange={input.onChange || formik.handleChange}
        onBlur={formik.handleBlur}
        touched={formik.touched}
      />
      
    ));

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
      />);


  return (
    <div>page</div>
  )
}
