import React from 'react'

export default function Input({type='text',name,id,title,onChange,errors,onBlur,touched}) {
  return (
    <div className="col-md-6">
        <div className="form-floating mb-3 ">
            
              <input type={type} className="form-control" name={name} id={id} placeholder={title}  onChange={onChange} onBlur={onBlur} />
              <label htmlFor={id}>{title}</label>
              {touched[name] && errors[name] && <p className='text text-danger'> {errors[name]} </p>}
          
        </div>
  </div>)
}
