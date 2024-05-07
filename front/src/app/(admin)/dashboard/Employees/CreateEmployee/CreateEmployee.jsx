'use client'
import Input from '@/component/input/Input';
import { createEmployee } from '@/component/validation/validation';
import axios from 'axios';
import { useFormik } from 'formik';
import { useRouter } from 'next/navigation';
import React, { useEffect, useState } from 'react'
import Swal from 'sweetalert2';

export default function CreateEmployee() {
  const router = useRouter();

    
  const [selectedGender, setSelectedGender] = useState('');
  const [selectedRole, setSelectedRole] = useState('');
  const [loading,setLoading] = useState(false);
  const [pageLoading,setPageLoading] = useState(false);

  const initialValues={
    FName: '',
    LName:'',
    email: '',
    password:'',
    phoneNumber:'',
    address:'',
    role:'',
    gender:'',
    
};


const onSubmit = async (users) => {
  setLoading(true);
    try {


      const formData = new FormData();
      formData.append('FName', users.FName);
      formData.append('LName', users.LName);
      formData.append('email', users.email);
      formData.append('password', users.password);
      formData.append('phoneNumber', users.phoneNumber);
      formData.append('address', users.address);
      // formData.append('gender', users.gender);;
      // formData.append('role', users.role);
      formData.append('gender', selectedGender); // Use selectedGender from state
    formData.append('role', selectedRole); 
  
      
  
      const { data } = await axios.post('http://localhost:5134/api/Employee/CreateEmployee', formData,
        {
        // headers: {
        //   'Content-Type': 'multipart/form-data','Content-Type': 'application/json',
        // }
        
      });
      
     if(data.isSuccess){
      //console.log(data);
      setPageLoading(true);
      //console.log('tttt');
      formik.resetForm();
      router.push('/dashboard');
      Swal.fire({
        title: "Account Created Successfully!",
        text: "Check Dashboard to see it",
        icon: "success"
      });
  }

    } catch (error) {
      // Handle the error here
      console.error('Error submitting form:', error);
      console.log('Error response:', error.response);
      // Optionally, you can show an error message to the user
    }
    finally {
      setLoading(false); // Set loading back to false regardless of success or failure
    }
  };


  useEffect(() => {
    // Check if page loading state is true, then set it back to false after a certain duration
    if (pageLoading) {
      const timer = setTimeout(() => {
        setPageLoading(false);
      }, 2000); // You can adjust the duration as needed
      return () => clearTimeout(timer);
    }
  }, [pageLoading]);


const formik = useFormik({
  initialValues : initialValues,
  onSubmit,
  validationSchema:createEmployee
})
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
        type : 'email',
        id:'email',
        name:'email',
        title:'User Email',
        value:formik.values.email,
    },
    
  {
      type : 'password',
      id:'password',
      name:'password',
      title:'User Password',
      value:formik.values.password,
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
// {
//   type : 'text',
//   id:'role',
//   name:'role',
//   title:'User role',
//   value:formik.values.role,
// },{
//   type : 'text',
//   id:'gender',
//   name:'gender',
//   title:'User gender',
//   value:formik.values.gender,
// },
]

// const handleFormSubmit = (e) => {
//   e.preventDefault();
//   onSubmit({ ...formik.values});
//   formik.resetForm();
// };


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
    {pageLoading ? ( // Show loading indicator while page is loading
        <div>Loading...</div>
      ) : (
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
      <div className="col-md-6">
        <select
          className="form-select p-3"
          aria-label="Default select example"
          value={selectedRole}
          
          onChange={(e) => {
            formik.handleChange(e);
            setSelectedRole(e.target.value);
          }}
        >
          <option value="" disabled>
            Select Role
          </option>
          <option value="subadmin">SubAdmin</option>
          <option value="instructor">Instructor</option>
        </select>
      </div> 
      <div className="col-md-12">
      <button
        type="submit"
        className="btn btn-primary createButton mt-3 fs-3 px-3 w-50"
        disabled={formik.isSubmitting || Object.keys(formik.errors).length > 0 || Object.keys(formik.touched).length === 0||!selectedGender || 
        !selectedRole }
      >
        {/* CREATE ACCOUNT */}
        {loading ? 'Creating...' : 'CREATE ACCOUNT'}
      </button>
      
      </div>
      <div className="col-md-12"><button type="button" className="btn btn-secondary createButton mt-3 fs-3 px-3 w-25" data-bs-dismiss="modal">
        Close
        </button></div>
    </form>)}
    </>
  );
}
