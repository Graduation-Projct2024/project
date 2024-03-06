import React from 'react'

export default function Input(props) {
    const {type, name ,id ,title,value, onChange,onBlur, errors, touched} = props
  return (
    <div className='input-goup mp-3 inputs'>
    <label className="text-black" htmlFor={id}>{title}</label>
    <input type={type} name={name} value={value} className='form-control' id={id} onChange={onChange} onBlur={onBlur}/>
    {touched[name]&&errors[name]&&<p className='text text-danger'>{errors[name]}</p>}
</div>
  )
}
