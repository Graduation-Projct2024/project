'use client';
import React, { useState } from 'react';
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

export default function AddTask({ open, onClose }) {
  const handelFieldChang = (event) => {
    console.log(event);
    formik.setFieldValue("file", event.target.files[0]);
  };
  const [Alertopen, setAlertOpen] = React.useState(false);
  const handleClose = (event, reason) => {
    if (reason === 'clickaway') {
      return;
    }

    setAlertOpen(false);
  };
  const initialValues = {
    title: "",
    Description: "",
    DeadLine: "",
    file: "",
  };
  const onSubmit = async (tasks) => {
console.log("test")
const formData = new FormData();
formData.append("title", tasks.title);
formData.append("Description", tasks.Description);
formData.append("DeadLine", tasks.DeadLine);
formData.append("file", tasks.file);

const { data } = await axios.post(
  "http://localhost:5134/api/UserAuth/Register",
 
  formData,

);
 if(data.isSuccess){
  console.log("test");
 formik.resetForm();
setOpen(true);

  }
  };
  const validationSchema = yup.object({
    title: yup
      .string()
      .required("title is required"),
      Description: yup.string(),
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
      id: "title",
      type: "text",
      name: "title",
      title: "Title",
      value: formik.values.title,
    },
    {
      id: "Description",
      type: "text",
      name: "Description",
      title: "Description",
      value: formik.values.Description,
    },
    {
      id: "DeadLine",
      type: "date",
      name: "DeadLine",
      title: "DeadLine",
      value: formik.values.DeadLine,
    },
    {
      id: "file",
      type: "file",
      name: "file",
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
     <Snackbar open={Alertopen} autoHideDuration={6000} onClose={handleClose}>
        <Alert
          onClose={handleClose}
          severity="success"
          variant="filled"
          sx={{ width: '100%' }}
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
