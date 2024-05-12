import React from 'react'
import TextField from '@mui/material/TextField';

export default function TextArea({type='text',name,id,value,title,onChange,errors,onBlur,touched,}) {
  return (
    <div className="col-md-12">
        <div className="form-floating mb-3">
        <TextField
          id={id}
          label={title}
          multiline
          defaultValue={value}
          minRows={3}
          fullWidth 
          onChange={onChange} onBlur={onBlur}
        />

              {touched[name] && errors[name] && <p className='text text-danger'> {errors[name]} </p>}
          
        </div>
  </div>
  )
}
