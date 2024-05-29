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
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, Tooltip, useMediaQuery, useTheme } from '@mui/material';
import { Stack } from '@mui/system';


export default function page({params}) {
  const {userToken, setUserToken, userData,userId}=useContext(UserContext);

  let [user,setUser] = useState({})
  const [loading, setLoading] = useState(false);
  const [openUpdate, setOpenUpdate] = React.useState(false);

  const theme = useTheme();
  const fullScreen = useMediaQuery(theme.breakpoints.down('md'));
  const [userIdP, setUserIdP] = useState(null);

const handleClickOpenUpdate = (id) => {
    setUserIdP(id);
    console.log(id)
    setOpenUpdate(true);
};
const handleCloseUpdate = () => {
  setOpenUpdate(false);
};
  // const {id} = useParams();
  // console.log(useParams());
// console.log(params)
  const getUser =async ()=>{
    if(userData){
    try {
      //setLoading(false)
      const {data} = await axios.get(`https://localhost:7116/api/UserAuth/GetProfileInfo?id=${userId}`,
      {headers :{Authorization:`Bearer ${userToken}`}}

      );
      console.log(data);
      setUser(data.result);
      //setLoading(false)
      }
      catch (error) {
      console.log(error)
      }}
      
  }
  console.log(user)
  useEffect(()=>{
      getUser();
  },[user,userData])
  
  return (
    <Layout>
        <div className="container">
      <div className="row">
        <div className="col-xl-4 text-center">
          <h1 className='pr'>PROFILE</h1>
        </div>
      </div>
      <div className="row">
        <div className="col-xl-4 text-center justify-content-center">
          {userData && user.imageUrl ? <img src={`${user.imageUrl}`} className='profile img-fluid'/> :<img src='/user.jpg' className='profile img-fluid'/>}
          
        </div>
        <div className="col-xl-8">
          <div className="row">
            <div className="col-xl-8 col-lg-12 pt-lg-3 pt-md-3 pt-sm-3 pt-3 d-flex gap-2">
              <p className='text-uppercase fw-bold pt-2 '><span className='name'>{user.userName} {user.lName}</span></p>
              {userData && params.id == userId && <Tooltip title="Update profile" placement="top"><button className="border-0 bg-white" type="button" onClick={() => handleClickOpenUpdate(params.id)}>
                <FontAwesomeIcon icon={faPen} className="edit-pen" />
            </button></Tooltip>} 
            </div>
            
            {/* <div className="col-xl-6 col-lg-12">
              
            </div> */}
            {/* <div className="modal fade" id={`exampleModal2-${params.id}`} tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div className="modal-dialog modal-dialog-centered modal-lg">
                      <div className="modal-content row justify-content-center">
                        <div className="modal-body text-center ">
                          <h2>Edit Profile Info</h2>
                          <div className="row">
                            {userData &&
                            <EditProfile id={params.id}  FName = {userData.userName} LName= {userData.lName}  gender = {userData.gender} phoneNumber = {userData.phoneNumber} DateOfBirth = {userData.dateOfBirth} address= {userData.address} image = {userData.imageUrl}/>
                           }</div>
                        </div>
                      </div>
                    </div>
                  </div> */}

<Dialog
        fullScreen={fullScreen}
        open={openUpdate}
        onClose={handleCloseUpdate}
        aria-labelledby="responsive-dialog-title"
        sx={{
          "& .MuiDialog-container": {
            "& .MuiPaper-root": {
              width: "100%",
              maxWidth: "600px!important",  
              height: "400px!important",            },
          },
          
        }}
        >
          <DialogTitle id="responsive-dialog-title" className='primaryColor fw-bold' >
          {"Update Profile"}
        </DialogTitle>

        <DialogContent>
        <Stack
   direction="row"
   spacing={1}
   sx={{ justifyContent: 'center',  alignContent: 'center'}}
    >
      {userData &&
    <EditProfile id={params.id}  FName = {userData.userName} LName= {userData.lName}  gender = {userData.gender} phoneNumber = {userData.phoneNumber} DateOfBirth = {userData.dateOfBirth} address= {userData.address} image = {userData.imageUrl} openUpdate={openUpdate} setOpenUpdate={setOpenUpdate}/>}
     </Stack>
        </DialogContent>
        <DialogActions>
         
         <Button onClick={handleCloseUpdate} autoFocus>
           Cancle
         </Button>
       </DialogActions>
        </Dialog>
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
                  <div className="d-flex justify-content-xl-start justify-content-lg-center justify-content-md-center justify-content-sm-center justify-content-center"><p className='fw-bold labels'>Date of birth:</p><p className=' info ps-4'>{user.dateOfBirth}</p></div>
                 
                </div>
                
              </div>

            </div>
            <div className="col-xl-6">
            <div className="row info2">
              <div className="col-xl-10">
              <div className="d-flex justify-content-xl-start justify-content-lg-center justify-content-md-center justify-content-sm-center justify-content-center">
                    <p className='fw-bold labels'>Email:</p>
                    <p className=' info ps-2'>{user.email}</p>
                  </div>
                  <div className="d-flex justify-content-xl-start justify-content-lg-center justify-content-md-center justify-content-sm-center justify-content-center">
                    <p className='fw-bold labels'>Address:</p>
                    <p className=' info ps-2'>{user.address}</p>
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
