'use client'
import './Contact.css'
import Layout from '../AdminLayout/Layout'
import '../dashboard/dashboard.css'
import '../Profile/[id]/Profile.css'
import '../../../../node_modules/bootstrap/dist/js/bootstrap.bundle.min.js'

import React from 'react'
import EmployeeContacts from './EmployeeContacts'
import StudentContacts from './StudentContacts'

export default function page() {
  return (
    <Layout title = "Contscts">
       <div>
  <nav>
    <div className="nav nav-tabs" id="nav-tab" role="tablist">
      <button className="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home" type="button" role="tab" aria-controls="nav-home" aria-selected="true">Employees</button>
      <button className="nav-link" id="nav-profile-tab" data-bs-toggle="tab" data-bs-target="#nav-profile" type="button" role="tab" aria-controls="nav-profile" aria-selected="false">Students</button>

    </div>
  </nav>


  <div className="tab-content" id="nav-tabContent">
    <div className="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab" tabIndex={0}>
      <EmployeeContacts/>
    </div>
    <div className="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab" tabIndex={0}>
      <StudentContacts/>
    </div>
  </div>
</div>

    </Layout>
    
  )
}
