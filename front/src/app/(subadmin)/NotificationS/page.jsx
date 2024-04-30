'use client'
import React from 'react'
import Layout from '../SubAdminLayout/Layout'
import '../../(admin)/dashboard/dashboard.css'
import '../../../../node_modules/bootstrap/dist/js/bootstrap.bundle'
import CustomCoursesRequest from './CustomCoursesRequest/CustomCoursesRequest'
export default function NotificationS() {
  return (
    <Layout title = "Notification">
      {/* <CustomCoursesRequest/> */}
      <div>
  <nav>
    <div className="nav nav-tabs" id="nav-tab" role="tablist">
      <button className="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home" type="button" role="tab" aria-controls="nav-home" aria-selected="true">Custom Courses</button>
      
    </div>
  </nav>


  <div className="tab-content" id="nav-tabContent">
    <div className="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab" tabIndex={0}>
      <CustomCoursesRequest/>
    </div>

  </div>
</div>
    </Layout>
    
  )
}
