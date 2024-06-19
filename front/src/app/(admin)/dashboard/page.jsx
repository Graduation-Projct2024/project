'use client'
// import React, { useEffect, useState } from 'react'
import './dashboard.css'
import '../../../../node_modules/bootstrap/dist/js/bootstrap.bundle.min.js'

import Layout from '../AdminLayout/Layout'
import ViewEmployees from './Employees/ViewEmployees/ViewEmployees'
import ViewStudents from './Students/ViewStudents/ViewStudents'
import ViewCourses from './Courses/ViewCourses/ViewCourses'
import ViewEvents from './Events/ViewEvents/ViewEvents'

export default function dashboard() {
  // const [loading, setLoading] = useState(true);

  // useEffect(() => {
  //   // Simulate data fetching
  //   const fetchData = async () => {
  //     // You can replace this with actual data fetching logic
  //     setTimeout(() => {
  //       setLoading(false);
  //     }, 5000); // Simulating a 2-second data fetching process
  //   };

  //   fetchData();
  // }, []);
 
  return (
    <Layout title = "Dashboard">
       <div>
  <nav>
    <div className="nav nav-tabs" id="nav-tab" role="tablist">
      <button className="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home" type="button" role="tab" aria-controls="nav-home" aria-selected="true">Employees</button>
      <button className="nav-link" id="nav-profile-tab" data-bs-toggle="tab" data-bs-target="#nav-profile" type="button" role="tab" aria-controls="nav-profile" aria-selected="false">Students</button>
      <button className="nav-link" id="nav-contact-tab" data-bs-toggle="tab" data-bs-target="#nav-contact" type="button" role="tab" aria-controls="nav-contact" aria-selected="false">Courses</button>
      <button className="nav-link" id="nav-event-tab" data-bs-toggle="tab" data-bs-target="#nav-event" type="button" role="tab" aria-controls="nav-event" aria-selected="false">Events</button>

    </div>
  </nav>


  <div className="tab-content" id="nav-tabContent">
    <div className="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab" tabIndex={0}>
      <ViewEmployees/>
    </div>
    <div className="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab" tabIndex={0}>
      <ViewStudents/>
    </div>
    <div className="tab-pane fade" id="nav-contact" role="tabpanel" aria-labelledby="nav-contact-tab" tabIndex={0}>
      <ViewCourses/>
    </div>
    <div className="tab-pane fade" id="nav-event" role="tabpanel" aria-labelledby="nav-event-tab" tabIndex={0}>
      <ViewEvents/>
    </div>
  </div>
</div>

    </Layout>
    
    
  )
}
