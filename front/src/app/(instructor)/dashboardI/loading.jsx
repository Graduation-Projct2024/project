import React from 'react'
import '../../(admin)/dashboard/loading.css'
export default function loading() {
  return (
    <div className='loading bg-white position-fixed vh-100 w-100 d-flex justify-content-center align-items-center z-3'>
      <span class="loader"></span>
    </div>
  )
}
