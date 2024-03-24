import Input from '@/component/input/Input';
import { editProfile } from '@/component/validation/validation';
import axios from 'axios';
import { useFormik } from 'formik';
import React, { useEffect, useState } from 'react'

export default function EditProfile({userId , userName , lName  , gender,phoneNumber,dob  , address , image}) {

    const [employeeData, setEmployeeData] = useState(null);
    const [selectedGender, setSelectedGender] = useState('');
console.log(userId, userName,lName, gender, phoneNumber, dob, address, image)
    useEffect(() => {
      const fetchUserData = async () => {
        try {
          const { data } = await axios.get(`http://localhost:5134/api/UserAuth/GetProfileInfo?id=${userId}`);
          console.log(data.result);
          setEmployeeData(data.result);
        } catch (error) {
          console.error('Error fetching employee data:', error);
        }
      };

      fetchUserData();
    }, [userId]);

    const handleFieldChange = (event) => {
        formik.setFieldValue('image', event.target.files[0]); // Set the file directly to image
      };

    const onSubmit = async (updatedData) => {
      try {
        const formData = new FormData();
        formData.append('fName', updatedData.fName);
        formData.append('lName', updatedData.lName);
        formData.append('email', updatedData.email);
        formData.append('phoneNumber', updatedData.phoneNumber);
        formData.append('address', updatedData.address);
        formData.append('dateOfBirth', updatedData.dateOfBirth);
        formData.append('gender', selectedGender);
        if (updatedData.image) {
            formData.append('image', updatedData.image);
        }
        // formData.append('gender', users.gender);;
        // formData.append('role', users.role);
         // Use selectedGender from state

        const {data} = await axios.put(`http://localhost:5134/api/UserAuth/EditProfile?id=${userId}`, updatedData);
        if(data.isSuccess){
            formik.resetForm();
            Swal.fire({
                title: "Profile updated successfully",
                text: "You can see the data updated in your profile",
                icon: "success"
              });
            
        }

      } catch (error) {
        console.error('Error updating employee:', error);
      }
    };
  
    const formik = useFormik({
      initialValues: {
        fName: `${userName}`,
        lName: `${lName}`,
        phoneNumber: `${phoneNumber}`,
        address: `${address}`,
        dateOfBirth : `${dob}`,
        gender: `${gender}`,
        image : `${image}`,
        
      },
      validationSchema:editProfile,
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
        id:'dateOfBirth',
        name:'dateOfBirth',
        title:'Date of birth',
        value:formik.values.dateOfBirth,
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
    
    <div className="col-md-12">
    <button
      type="submit"
      className="btn btn-primary createButton mt-3 fs-3 px-3 w-50"
      disabled={formik.isSubmitting || Object.keys(formik.errors).length > 0 || Object.keys(formik.touched).length === 0 }
    >
      Edit Profile
    </button>
    </div>
    <div className="col-md-12"><button type="button" class="btn btn-secondary createButton mt-3 fs-3 px-3 w-25" data-bs-dismiss="modal">Close</button></div>
  </form>
  )
}
