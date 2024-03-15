// 'use client'
// import Input from '@/component/input/Input';
// import { createCourse } from '@/component/validation/validation';
// import axios from 'axios';
// import { useFormik } from 'formik';
// import React, { useState } from 'react'


// export default function CreateCourse() {

//     const initialValues={
//         name: '',
//         price:'',
//         category: '',
//         subAdminId:'',
//         instructorId:'',
//         description:'',
//        // imageUrl:null,
        
//     };

//     const handleFieldChange = (event) => {
//       formik.setFieldValue('imageUrl', event.target.files[0]); // Set the file directly to imageUrl
//   };
    
//     const onSubmit = async (values) => {
//         try {
    
//           const formData = new FormData();

//           formData.append('name', values.name);
//           formData.append('price', values.price);
//           formData.append('category', values.category);
//           formData.append('subAdminId', values.subAdminId);
//           formData.append('instructorId', values.instructorId);
//           formData.append('description', values.description);
//          // formData.append('imageUrl', values.imageUrl);
          
      
          
      
//           const { data } = await axios.post('http://localhost:5134/api/CourseContraller/CreateCourse', formData,
//             {
//               headers: {
//               'Content-Type': 'multipart/form-data',
//              'Content-Type': 'application/json',
//             //    accept: 'application/json',
//             //  Accept: 'application/json, text/plain, */*',
//             // 'Content-Type': 'multipart/form-data','Content-Type': 'application/json',
//                // 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8','Content-Type': 'multipart/form-data','Content-Type': 'application/json',
//                Accept: 'application/json, text/plain',
//               //  'Content-Type': 'application/json;charset=UTF-8'
//               }
//               // headers: {
//               //   'Content-Type': 'multipart/form-data','Content-Type': 'application/json',
//               //     "Access-Control-Allow-Origin": "*",
//               //     "Accept": "application/json" 
//               // }
            
//           });
          
//          if(data.isSuccess){
//           console.log(data);
//           console.log('course craeated');
//           formik.resetForm();
//       }
    
//           console.log('jhbgyvftrgybuhnjimkjhb');
//         } catch (error) {
//           // Handle the error here
//           console.error('Error submitting form:', error);
//           console.log('Error response:', error.response);
//           // Optionally, you can show an error message to the user
//         }
//       };

      
    
//     const formik = useFormik({
//       initialValues : initialValues,
//       onSubmit,
//       validationSchema:createCourse
//     })

//     const inputs =[
//       {
//         type : 'text',
//           id:'name',
//           name:'name',
//           title:'Course Name',
//           value:formik.values.name,
//     },
    
//       {
//           type : 'number',
//           id:'price',
//           name:'price',
//           title:'Price',
//           value:formik.values.price,
//       },
     
//         {
//             type : 'text',
//             id:'category',
//             name:'category',
//             title:'Course category',
//             value:formik.values.category,
//         },
        
//       {
//           type : 'text',
//           id:'subAdminId',
//           name:'subAdminId',
//           title:'SubAdmin Id',
//           value:formik.values.subAdminId,
//       },
//       {
//         type : 'text',
//         id:'instructorId',
//         name:'instructorId',
//         title:'Instructor Id',
//         value:formik.values.instructorId,
//     },
//     {
//       type : 'text',
//       id:'description',
//       name:'description',
//       title:'Description',
//       value:formik.values.description,
//     },
//     // {
//     //     type:'file',
//     //     id:'imageUrl',
//     //     name:'imageUrl',
//     //     title:'imageUrl',
//     //     onChange:handleFieldChange,
//     // }
 
//     ]
    
//     // const handleFormSubmit = (e) => {
//     //   e.preventDefault();
//     //   onSubmit({ ...formik.values});
//     //   formik.resetForm();
//     // };
    
    
//     const renderInputs = inputs.map((input,index)=>
//       <Input type={input.type} 
//             id={input.id}
//              name={input.name}
//               title={input.title} 
//               key={index} 
//               errors={formik.errors} 
//               onChange={formik.handleChange}
//                onBlur={formik.handleBlur}
//                 touched={formik.touched}
//                 />
            
//         )

//   return (
//     <form onSubmit={formik.handleSubmit} className="row justify-content-center" /*encType="multipart/form-data" */>
//       {renderInputs}
       
      
//       <button
//         type="submit"
//         className="btn btn-primary createButton mt-3 fs-3 px-3 w-50"
//         disabled={formik.isSubmitting || Object.keys(formik.errors).length > 0 || Object.keys(formik.touched).length === 0 }   
//            >
//         CREATE COURSE
//       </button>
//     </form>
//   )
// }

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
    subAdminId:0,
    instructorId:0,
    
};


const onSubmit = async (values) => {

  try {
    const formData = new FormData();
    formData.append('name', values.name);
    formData.append('description', values.description);
    formData.append('price', values.price);
    formData.append('category', values.category);
    formData.append('subAdminId', values.subAdminId);
    formData.append('instructorId', values.instructorId);
   
    const { data } = await axios.post('http://localhost:5134/api/CourseContraller/CreateCourse', formData,
      {
        headers: {
          'Content-Type': 'multipart/form-data','Content-Type': 'application/json',
        }
    });
    
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
      id:'subAdminId',
      name:'subAdminId',
      title:'subAdminId',
      value:formik.values.subAdminId,
  },
  {
    type : 'number',
    id:'instructorId',
    name:'instructorId',
    title:'instructorId',
    value:formik.values.instructorId,
},
{
  type : 'text',
  id:'description',
  name:'description',
  title:'description',
  value:formik.values.description,
},
];

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
    );
  return (
    <form onSubmit={formik.handleSubmit} className="row justify-content-center">
      {renderInputs}
      <button
        type="submit"
        className="btn btn-primary createButton mt-3 fs-3 px-3 w-50"
        disabled={formik.isSubmitting || Object.keys(formik.errors).length > 0 || Object.keys(formik.touched).length === 0}
      >
        CREATE COURSE
      </button>
    </form>
  )
}
