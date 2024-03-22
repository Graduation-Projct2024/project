'use client';
import React, { useContext, useState } from 'react';
import * as yup from "yup";
import axios from "axios";
import { useFormik } from "formik";
import Link from '@mui/material/Link';
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import Button from '@mui/material/Button';
import Input from '../../../../component/input/Input.jsx';
import { UserContext } from '../../../../context/user/User.jsx';
import './style.css'
export default function AddTask({ open, onClose , courseId}) {
  const {userToken, setUserToken, userData}=useContext(UserContext);

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
 
  const initialValues = {
    name: "",
    description: "",
    DeadLine: "",
    pdf: "",
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


const { data } = await axios.post(
  "http://localhost:5134/api/MaterialControllar/AddTask",
  formData,

);
 if(data.isSuccess){
  console.log("test");
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
  const inputs = [
    {
      id: "name",
      type: "text",
      name: "name",
      title: "Title",
      value: formik.values.name,
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
          Task Added successfully!
        </Alert>
      </Snackbar>
    <Dialog
     open={open} 
     onClose={onClose}
     sx={{
      "& .MuiDialog-container": {
        "& .MuiPaper-root": {
          width: "100%",
          maxWidth: "500px!important",  
                    },
      },}} >

    <DialogTitle>Add Task</DialogTitle>
    <DialogContent>
          <div className="form-container sign-up">
      <form onSubmit={formik.handleSubmit} encType="multipart/form-data">        
        {renderInputs}
        <div className="text-center mt-3">
        <Button sx={{px:2}} variant="contained"
              className="m-2 btn "
              type="submit"
              disabled={!formik.isValid}
            >
              Add
            </Button>
        </div>
      </form>
    </div>
    </DialogContent>
    <DialogActions>
      <Button onClick={onClose} color="primary">
        Close
      </Button>
    </DialogActions>
  </Dialog>
  </>
  )
}
