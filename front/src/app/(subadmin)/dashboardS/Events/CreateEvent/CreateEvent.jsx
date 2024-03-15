import Input from '@/component/input/Input';
import { createEvent } from '@/component/validation/validation';
import axios from 'axios';
import { useFormik } from 'formik';
import React from 'react'

export default function CreateEvent() {

  const initialValues={
    name: '',
    content:'',
    category: '',
    SubAdminId:0,
    dateOfEvent:'',
    image:'',
};

const handleFieldChange = (event) => {
  formik.setFieldValue('image', event.target.files[0]); // Set the file directly to image
};

const onSubmit = async (values) => {
  try {
    const formData = new FormData();
    formData.append('name', values.name);
    formData.append('content', values.content);
    formData.append('category', values.category);
    formData.append('SubAdminId', values.SubAdminId);
    formData.append('dateOfEvent', values.dateOfEvent);
    if (values.image) {
      formData.append('image', values.image);
    }

    const { data } = await axios.post('http://localhost:5134/api/CourseContraller/CreateEvent', formData,
      {
      
    });
    
   if(data.isSuccess){
    console.log(data);
    console.log('tttt');
    formik.resetForm();
}

    console.log('jhbgyvftrgybuhnjimkjhb');
  } catch (error) {
    // Handle the error here
    console.error('Error submitting form:', error);
    console.log('Error response:', error.response);
    // Optionally, you can show an error message to the user
  }
};

const formik = useFormik({
  initialValues : initialValues,
  onSubmit,
  validationSchema:createEvent
})


const inputs =[
  {
    type : 'text',
      id:'name',
      name:'name',
      title:'Event Name',
      value:formik.values.name,
},

  {
      type : 'text',
      id:'content',
      name:'content',
      title:'Content',
      value:formik.values.content,
  },
 
    {
        type : 'category',
        id:'category',
        name:'category',
        title:'Event category',
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
    type : 'date',
    id:'dateOfEvent',
    name:'dateOfEvent',
    title:'Event Date',
    value:formik.values.dateOfEvent,
},
  {
    type:'file',
    id:'image',
    name:'image',
    title:'image',
    onChange:handleFieldChange,
},
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
        
    )
  return (
    <form onSubmit={formik.handleSubmit} className="row justify-content-center">
      {renderInputs}
<div className="col-md-12">
      <button
        type="submit"
        className="btn btn-primary createButton mt-3 fs-3 px-3 w-50"
        disabled={formik.isSubmitting || Object.keys(formik.errors).length > 0 || Object.keys(formik.touched).length === 0}
      >
        CREATE Event
      </button>
      </div>
      <div className="col-md-12"><button type="button" class="btn btn-secondary createButton mt-3 fs-3 px-3 w-25" data-bs-dismiss="modal">Close</button></div>
    </form>
  )
}
