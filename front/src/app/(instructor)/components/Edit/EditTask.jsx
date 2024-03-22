'use client';
import React, { useContext, useState, useEffect } from 'react';
import * as yup from "yup";
import axios from "axios";
import { useFormik } from "formik";
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';
import Button from '@mui/material/Button';
import Input from '../../../../component/input/Input.jsx';
import './style.css'
import { UserContext } from '../../../../context/user/User.jsx';
export default function EditTask({materialID, name, description, deadLine, pdf, courseId }) {
  const {userData}=useContext(UserContext);
console.log(courseId)
  const handelFieldChang = (event) => {
    formik.setFieldValue("pdf", event.target.files[0]);

  };
  const [Alertopen, setAlertOpen] = React.useState(false);
  const handleClose = (event, reason) => {
    if (reason === 'clickaway') {
      return;
    }

    setAlertOpen(false);
  };

  let initialValues = {
   name:` ${name}`,
    description: ` ${description}`,
    DeadLine: `${deadLine}`,
    pdf: `${pdf}`,
  };

  const onSubmit = async (tasks) => {
    try {
console.log("test")
const formData = new FormData();
formData.append("name", tasks.name);
formData.append("description", tasks.description);
formData.append("DeadLine", tasks.DeadLine);
formData.append("pdf", tasks.pdf);
console.log(tasks.pdf)
formData.append("courseId", courseId);
formData.append("instructorId", userData.userId);

const { data } = await axios.put(
 `http://localhost:5134/api/MaterialControllar/EditTask?id=${materialID}`,
  formData,


);
 if(data.isSuccess){
  console.log(data);
 formik.resetForm();
 setAlertOpen(true);

  }}
  catch (error) {
    if (error.isAxiosError) {
      const requestConfig = error.config;
  
      console.log("Request Configuration:", requestConfig);
    } else {
      console.error("Non-Axios error occurred:", error);
    }
  }};
  const validationSchema = yup.object({
    name: yup
      .string()
      .required("title is required"),
      description: yup.string(),
      DeadLine: yup
      .date().required("Deadline is required").min(new Date(), 'Date must be in the future'), 
  });

  const formik = useFormik({
    initialValues,
    onSubmit,
    validationSchema: validationSchema,
  });
  console.log(name);
  const inputs = [
    {
      id: "name",
      type: "text",
      name: "name",
      title: "Title",
      value:formik.values.name,
    },
    {
      id: "description",
      type: "text",
      name: "description",
      title: "Description",
      value: formik.values.description,
    },
    {
      id: "DeadLine",
      type: "date",
      name: "DeadLine",
      title: "DeadLine",
      value: formik.values.DeadLine,
    },
    {
      id: "pdf",
      type: "file",
      name: "pdf",
      title: "Upload File",
      onChange: handelFieldChang,
    },
  ];
  const renderInputs = inputs.map((input, index) => (
    <Input
      type={input.type}
      id={input.id}
      name={input.name}
      value={input.value}
      title={input.title}
      onChange={input.onChange || formik.handleChange}
      onBlur={formik.handleBlur}
      touched={formik.touched}
      errors={formik.errors}
      key={index}
    />
  ));

  return (
    <>
     <Snackbar open={Alertopen} autoHideDuration={10000} onClose={handleClose} >
        <Alert
          onClose={handleClose}
          severity="success"
          variant="filled"
          sx={{ width: '100%'}}
        >
          Task Edited successfully!
        </Alert>
      </Snackbar>
          <div className="form-container EditTask">
      <form onSubmit={formik.handleSubmit} encType="multipart/form-data">        
        {renderInputs}
        <div className="text-center mt-3">
        <Button sx={{px:2}} variant="contained"
              className="m-2  "
              type="submit"
              disabled={!formik.isValid}
            >
              Save
            </Button>
        </div>
      </form>
    </div>

  </>
  )
}
