import Input from '@/component/input/Input';
import { updateEmployee } from '@/component/validation/validation';
import { UserContext } from '@/context/user/User';
import axios from 'axios';
import { useFormik } from 'formik';
import React, { useContext, useEffect, useState } from 'react'
import Swal from 'sweetalert2';

export default function UpdateEmployee({id , fName , lName , email, gender, phoneNumber , address}) {

    const [employeeData, setEmployeeData] = useState(null);
    const [selectedGender, setSelectedGender] = useState('');
    const {userToken, setUserToken, userData,userId}=useContext(UserContext);

    useEffect(() => {
      const fetchEmployeeData = async () => {
        if(userData){
        try {
          const { data } = await axios.get(`http://localhost:5134/api/Employee/GetEmployeeById?id=${id}`);
          console.log(data.result);
          setEmployeeData(data.result);
        } catch (error) {
          console.error('Error fetching employee data:', error);
        }}
      };

      fetchEmployeeData();
    }, [id]);
    console.log(employeeData)

    const onSubmit = async (updatedData) => {
      if(userData){
      try {
        const formData = new FormData();
        formData.append('fName', updatedData.fName);
        formData.append('lName', updatedData.lName);
        formData.append('email', updatedData.email);
        formData.append('phoneNumber', updatedData.phoneNumber);
        formData.append('address', updatedData.address);
        // formData.append('gender', users.gender);;
        // formData.append('role', users.role);
        formData.append('gender', selectedGender); // Use selectedGender from state

        const {data} = await axios.put(`http://localhost:5134/api/Employee/UpdateEmployeeFromAdmin?id=${id}`, updatedData);
        if(data.isSuccess){
            formik.resetForm();
            Swal.fire({
                title: "Account updated successfully",
                text: "You can see the modified account in dashboard",
                icon: "success"
              });
            
        }

      } catch (error) {
        console.error('Error updating employee:', error);
      }}
    };
  
    const formik = useFormik({
      initialValues: {
        fName: `${fName}`,
        lName: `${lName}`,
        email: `${email}`,
        phoneNumber: `${phoneNumber}`,
        gender: `${gender}`,
        address: `${address}`,
      },
      validationSchema:updateEmployee,
      onSubmit,
    });
  
    useEffect(() => {
      if (employeeData) {
        formik.setValues(employeeData);
      }
    }, [employeeData]);

  
    const inputs =[
        {
          type : 'text',
            id:'fName',
            name:'fName',
            title:'First Name',
            value:formik.values.fName,
      },
      
        {
            type : 'text',
            id:'lName',
            name:'lName',
            title:'Last Name',
            value:formik.values.lName,
        },
       
          {
              type : 'email',
              id:'email',
              name:'email',
              title:'User Email',
              value:formik.values.email,
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

      const renderInputs = inputs.map((input,index)=>
  <Input type={input.type} 
        id={input.id}
        
         name={input.name}
          title={input.title} 
          value={input.value || ''}
          key={index} 
          errors={formik.errors} 
          onChange={formik.handleChange}
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
      
      <div className="col-md-12">
      <button
        type="submit"
        className="btn btn-primary createButton mt-3 fs-3 px-3 w-50"
        disabled={formik.isSubmitting || Object.keys(formik.errors).length > 0 || Object.keys(formik.touched).length === 0||!selectedGender }
      >
        UPDATE ACCOUNT
      </button>
      </div>
      <div className="col-md-12"><button type="button" class="btn btn-secondary createButton mt-3 fs-3 px-3 w-25" data-bs-dismiss="modal">Close</button></div>
    </form>
  )
}
