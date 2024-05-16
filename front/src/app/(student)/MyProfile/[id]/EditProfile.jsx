// import Input from '@/component/input/Input';
// import { editProfile } from '@/component/validation/validation';
// import { UserContext } from '@/context/user/User';
// import { Button } from '@mui/material';
// import axios from 'axios';
// import { useFormik } from 'formik';
// import React, { useContext, useEffect, useState } from 'react'
// import Swal from 'sweetalert2';

// export default function EditProfile({id , FName , LName  , gender,phoneNumber,DateOfBirth  , address , image}) {
//   const {userToken, setUserToken, userData,userId}=useContext(UserContext);

//     const [employeeData, setEmployeeData] = useState('');
//     const [selectedGender, setSelectedGender] = useState('');
// console.log(id, FName,LName, gender, phoneNumber, DateOfBirth, address, image)
    
//       const fetchUserData = async () => {
//         if(userData){
//         try {
//           const { data } = await axios.get(`http://localhost:5134/api/UserAuth/GetProfileInfo?id=${id}`);
//           console.log(data.result);
//           setEmployeeData(data.result);
//         } catch (error) {
//           console.error('Error fetching employee data:', error);
//         }}
//       };

// useEffect(() => {
//       fetchUserData();
//     }, [id]);

//     const handleFieldChange = (event) => {
//         formik.setFieldValue('image', event.target.files[0]); // Set the file directly to image
//       };

//     const onSubmit = async (updatedData) => {
//       if(userData){
//       try {
//         const formData = new FormData();
//         formData.append('FName', updatedData.FName);
//         formData.append('LName', updatedData.LName);
//         formData.append('phoneNumber', updatedData.phoneNumber);
//         formData.append('address', updatedData.address);
//         formData.append('DateOfBirth', updatedData.DateOfBirth);
//         formData.append('gender', selectedGender);
//         if (updatedData.image) {
//             formData.append('image', updatedData.image);
//         }
       

//         const {data} = await axios.put(`http://localhost:5134/api/UserAuth/EditProfile?id=${id}`, formData);
//         if(data.isSuccess){
//           console.log('Profile Updated')
//             formik.resetForm();
//             Swal.fire({
//                 title: "Profile updated successfully",
//                 text: "You can see the data updated in your profile",
//                 icon: "success"
//               });
              

            
//         }

//       } catch (error) {
//         console.error('Error updating employee:', error);
//       }}
//     };
  
//     const formik = useFormik({
//       initialValues: {
//         FName: `${FName}`,
//         LName: `${LName}`,
//         phoneNumber: `${phoneNumber}`,
//         address: `${address}`,
//         DateOfBirth: `${DateOfBirth}`,
//         gender: `${gender}`,
//         image : ``,
        
//       },
//       validationSchema:editProfile,
//       onSubmit,
//     });
  
//     useEffect(() => {
//       if (employeeData) {
//         formik.setValues(employeeData);
//       }
//     }, [employeeData]);

  
//     const inputs =[
//         {
//           type : 'text',
//             id:'FName',
//             name:'FName',
//             title:'First Name',
//             value:formik.values.FName,
//       },
      
//         {
//             type : 'text',
//             id:'LName',
//             name:'LName',
//             title:'Last Name',
//             value:formik.values.LName,
//         },
       
//         {
//           type : 'text',
//           id:'phoneNumber',
//           name:'phoneNumber',
//           title:'User phoneNumber',
//           value:formik.values.phoneNumber,
//       },
//       {
//         type : 'text',
//         id:'address',
//         name:'address',
//         title:'User address',
//         value:formik.values.address,
//       },
//       {
//         type : 'date',
//         id:'DateOfBirth',
//         name:'DateOfBirth',
//         title:'Date of birth',
//         value:formik.values.DateOfBirth,
//     },
//   {
//           type:'file',
//           id:'image',
//           name:'image',
//           title:'image',
//           onChange:handleFieldChange,
//       }

//       ]

//       const renderInputs = inputs.map((input,index)=>
//   <Input type={input.type} 
//         id={input.id}
//          name={input.name}
//           title={input.title} 
//           value={input.value}
//           key={index} 
//           errors={formik.errors} 
//           onChange={input.onChange||formik.handleChange}
//            onBlur={formik.handleBlur}
//             touched={formik.touched}
//             />
        
//     )


//   return (
//     <form onSubmit={formik.handleSubmit} className="row justify-content-center">
//     {renderInputs}
//      <div className="col-md-6">
//       <select
//         className="form-select p-3"
//         aria-label="Default select example"
//         value={selectedGender}
//        onChange={(e) => {
//           formik.handleChange(e);
//           setSelectedGender(e.target.value);
//         }}
//       >
//         <option value="" disabled>
//           Select Gender
//         </option>
//         <option value="Male">Male</option>
//         <option value="Female">Female</option>
//       </select>
//     </div>
    
//     <div className='text-center mt-3'>
//         <Button
//           sx={{ px: 2 }}
//           variant="contained"
//           className="m-2 btn primaryBg"
//           type="submit"
//           disabled={formik.isSubmitting}
//         >
//           Update
//         </Button>
//       </div>
//   </form>
//   )
// }


import Input from '@/component/input/Input';
import { editProfile } from '@/component/validation/validation';
import { UserContext } from '@/context/user/User';
import { Button } from '@mui/material';
import axios from 'axios';
import { useFormik } from 'formik';
import React, { useContext, useEffect, useState } from 'react';
import Swal from 'sweetalert2';

// Helper function to format the date to YYYY-MM-DD
const formatDate = (dateString) => {
  if (!dateString) return '';
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
};

export default function EditProfile({ id, FName, LName, gender, phoneNumber, DateOfBirth, address, image }) {
  const { userData } = useContext(UserContext);

  const [selectedGender, setSelectedGender] = useState(gender);

  const handleFieldChange = (event) => {
    formik.setFieldValue('image', event.target.files[0]); // Set the file directly to image
  };

  const onSubmit = async (updatedData) => {
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

      const { data } = await axios.put(`http://localhost:5134/api/UserAuth/EditProfile?id=${id}`, formData);
      if (data.isSuccess) {
        console.log('Profile Updated');
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
      FName: FName || '',
      LName: LName || '',
      phoneNumber: phoneNumber || '',
      address: address || '',
      DateOfBirth: formatDate(DateOfBirth) || '',
      gender: gender || '',
      image: image || null,
    },
    // validationSchema: editProfile,
    onSubmit,
  });

  useEffect(() => {
    formik.setValues({
      FName,
      LName,
      phoneNumber,
      address,
      DateOfBirth: formatDate(DateOfBirth),
      gender,
      image,
    });
    setSelectedGender(gender);
  }, [FName, LName, phoneNumber, address, DateOfBirth, gender, image]);

  const inputs = [
    {
      type: 'text',
      id: 'FName',
      name: 'FName',
      title: 'First Name',
      value: formik.values.FName,
    },
    {
      type: 'text',
      id: 'LName',
      name: 'LName',
      title: 'Last Name',
      value: formik.values.LName,
    },
    {
      type: 'text',
      id: 'phoneNumber',
      name: 'phoneNumber',
      title: 'User phoneNumber',
      value: formik.values.phoneNumber,
    },
    {
      type: 'text',
      id: 'address',
      name: 'address',
      title: 'User address',
      value: formik.values.address,
    },
    {
      type: 'date',
      id: 'DateOfBirth',
      name: 'DateOfBirth',
      title: 'Date of birth',
      value: formik.values.DateOfBirth,
    },
    {
      type: 'file',
      id: 'image',
      name: 'image',
      title: 'Image',
      onChange: handleFieldChange,
    },
  ];

  const renderInputs = inputs.map((input, index) => (
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
          name="gender"
        >
          <option value="" disabled>
            Select Gender
          </option>
          <option value="Male">Male</option>
          <option value="Female">Female</option>
        </select>
      </div>
      <div className='text-center mt-3'>
        <Button
          sx={{ px: 2 }}
          variant="contained"
          className="m-2 btn primaryBg"
          type="submit"
          disabled={formik.isSubmitting}
        >
          Update
        </Button>
      </div>
    </form>
  );
}
