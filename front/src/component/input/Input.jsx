import React from 'react'


  return (
    <div className="col-md-6">
        <div className="form-floating mb-3 ">

              <label htmlFor={id}>{title}</label>
              {touched[name] && errors[name] && <p className='text text-danger'> {errors[name]} </p>}
          
        </div>
  </div>)
}
