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
import TextArea from '../../../../component/input/TextArea.jsx';

import { UserContext } from '../../../../context/user/User.jsx';
export default function AddFile({ open, onClose,handleCloseAdd ,courseId }) {
  const {userToken, setUserToken, userData}=useContext(UserContext);

  const handelFieldChang = (event) => {
    console.log(event);
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
    pdf: "",
  };
  const onSubmit = async (tasks) => {
try{
  const formData = new FormData();
formData.append("name", tasks.name);
formData.append("description", tasks.description);
formData.append("pdf", tasks.pdf);
formData.append("courseId", courseId);
formData.append("InstructorId",userData.userId);
console.log(tasks.pdf)

const { data } = await axios.post(
  "https://localhost:7116/api/MaterialControllar/AddFile",
 
  formData,
  {headers :{Authorization:`Bearer ${userToken}`}}


);
  console.log("test");
 formik.resetForm();
 setAlertOpen(true);
 onClose(); 
 handleCloseAdd();

  }catch(error){
    console.log(error);
  }
  };
  const validationSchema = yup.object({
    name: yup
      .string()
      .required("name is required"),
      description: yup.string(),

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
      title: "name",
      value: formik.values.name,
    },
 
    {
      id: "pdf",
      type: "file",
      name: "pdf",
      title: "Upload File",
      onChange: handelFieldChang,
    },
    {
      id: "description",
      type: "text",
      name: "description",
      title: "description",
      value: formik.values.description,
    }
  ];
  const renderInputs = inputs.slice(0, -1).map((input, index) => (
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
  />
);

  return (
    <>
     <Snackbar open={Alertopen} autoHideDuration={6000} onClose={handleClose}>
        <Alert
          onClose={handleClose}
          severity="success"
          variant="filled"
          sx={{ width: '100%' }}
        >
          Link Added successfully!
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

    <DialogTitle>Add File</DialogTitle>
    <DialogContent>
          <div className="form-container sign-up">
      <form onSubmit={formik.handleSubmit} encType="multipart/form-data">        
        {renderInputs}
        {textAraeInput}
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
