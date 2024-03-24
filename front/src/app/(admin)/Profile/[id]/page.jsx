'use client'
import React, { useContext, useEffect, useState } from 'react'
import { useParams } from 'next/navigation.js';
import axios from 'axios';
import { faFacebookF, faGithub, faLinkedinIn } from '@fortawesome/free-brands-svg-icons'
import { faUser } from '@fortawesome/free-regular-svg-icons';
import { faEnvelope, faPen } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import './Profile.css'
import Layout from '../../AdminLayout/Layout';
import '../../dashboard/dashboard.css'
import { UserContext } from '@/context/user/User';
import EditProfile from './EditProfile';
import '../../../../../node_modules/bootstrap/dist/js/bootstrap.bundle'


export default function page({params}) {
  const {userToken, setUserToken, userData,userId}=useContext(UserContext);

  let [user,setUser] = useState({})
  const [loading, setLoading] = useState(false);
  // const {id} = useParams();
  // console.log(useParams());
// console.log(params)
  const getUser =async ()=>{
    if(userData){
    try {
      //setLoading(false)
      const {data} = await axios.get(`http://localhost:5134/api/UserAuth/GetProfileInfo?id=${params.id}`,
     
      );
      if(data.isSuccess){
        console.log(data.result);
      setUser(data.result);
      //setLoading(false)
      }}
      catch (error) {
      console.log(error)
      }}
      
  }
  console.log(user)
  useEffect(()=>{
      getUser();
  },[userData])
  
  return (
    <Layout>
        <div className="container pt-5">
      <div className="row">
        <div className="col-xl-4 text-center">
          <h1 className='pr'>PROFILE</h1>
        </div>
      </div>
      <div className="row">
        <div className="col-xl-4 text-center justify-content-center">
          <img src='/user.jpg' className='profile img-fluid'/>
        </div>
        <div className="col-xl-8">
          <div className="row">
            <div className="col-xl-6 col-lg-12 pt-lg-3 pt-md-3 pt-sm-3 pt-3">
              <p className='text-uppercase fw-bold  '><span className='name'>{user.userName} {user.lName}</span></p>
            
            </div>
            {userData && params.id == userId && 
            <div className="col-xl-6 col-lg-12">
              <button className="border-0 bg-white pt-3" type="button" data-bs-toggle="modal" data-bs-target={`#exampleModal2-${params.id}`}>
                    <FontAwesomeIcon icon={faPen} className="edit-pen" />
              </button>
            </div>} 
            <div className="modal fade" id={`exampleModal2-${params.id}`} tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div className="modal-dialog modal-dialog-centered modal-lg">
                      <div className="modal-content row justify-content-center">
                        <div className="modal-body text-center ">
                          <h2>Edit Profile Info</h2>
                          <div className="row">
                            <EditProfile userId={params.id}  userName = {user.userName} lName= {user.lName}  gender = {user.gender} phoneNumber = {user.phoneNumber} dob = {user.dateOfBirth} address= {user.address} image = {user.imageUrl}/>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
             <div className="d-flex ps-xl-4 pt-3 gap-2 role justify-content-xl-start fs-5 fw-bold justify-content-lg-center justify-content-md-center justify-content-sm-center justify-content-center">
                <FontAwesomeIcon icon={faUser} className='pt-1'/>
                <p className='text-uppercase'>{user.role}</p>
              </div>
          </div>
          <div className="row ps-1 pt-3">
            <div className="col-xl-6 ps-4">
              <div className="row info1 border-">
                <div className="col-xl-10 ">
                  <div className="d-flex justify-content-xl-start justify-content-lg-center justify-content-md-center justify-content-sm-center justify-content-center">
                    <p className='fw-bold labels'>Gender:</p>
                    <p className=' info ps-4'>{user.gender}</p>
                  </div>
                  <div className="d-flex justify-content-xl-start justify-content-lg-center justify-content-md-center justify-content-sm-center justify-content-center"><p className='fw-bold labels'>Phone:</p><p className=' info ps-4'>{user.phoneNumber}</p></div>
                  <div className="d-flex justify-content-xl-start justify-content-lg-center justify-content-md-center justify-content-sm-center justify-content-center"><p className='fw-bold labels'>Date of birth:</p><p className=' info ps-4'>18/10/2002</p></div>
                 
                </div>
                
              </div>

            </div>
            <div className="col-xl-6">
            <div className="row info2">
              <div className="col-xl-10">
              <div className="d-flex justify-content-xl-start justify-content-lg-center justify-content-md-center justify-content-sm-center justify-content-center">
                    <p className='fw-bold labels'>Email:</p>
                    <p className=' info ps-4'>{user.email}</p>
                  </div>
                  <div className="d-flex justify-content-xl-start justify-content-lg-center justify-content-md-center justify-content-sm-center justify-content-center">
                    <p className='fw-bold labels'>Address:</p>
                    <p className=' info ps-4'>{user.address}</p>
                  </div>
              </div>
                
                <ul className='d-flex gap-4 justify-content-center'>
                  <li className=' social'><FontAwesomeIcon icon={faLinkedinIn} /></li>
                  <li className=' social'><FontAwesomeIcon icon={faGithub} /></li>
                  <li className=' social'><FontAwesomeIcon icon={faFacebookF} /></li>
                  <li className=' social'><FontAwesomeIcon icon={faEnvelope} /></li>
                </ul>
                
              </div>
            </div>
          </div>
          
        </div>
      </div>
    </div>
    </Layout>
  )
}
