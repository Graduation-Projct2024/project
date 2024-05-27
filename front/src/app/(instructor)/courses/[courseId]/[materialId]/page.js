'use client';
import React, { useContext, useEffect, useState } from 'react'
import ViewTask from '../../../components/View/ViewTask.jsx'
import ViewAnnouncement from '../../../components/View/ViewAnnouncement.jsx'
import ViewFile from '../../../components/View/ViewFile.jsx'
import ViewLink from '../../../components/View/ViewLink.jsx'
import { useParams } from 'next/navigation.js';
import Layout from '../../../instructorLayout/Layout.jsx';
import axios from 'axios';
import { UserContext } from '../../../../../context/user/User.jsx';

export default function page() {
  const {userToken, setUserToken, userData}=useContext(UserContext);

    const[type,setType]=useState();
    const[name,setName]=useState();
console.log(useParams())
const{materialId, courseId}=useParams();
    const getMaterial=async()=>{
      if(userToken){
        const {data}= await axios.get(`http://localhost:5134/api/MaterialControllar/GetMaterialById?id=${materialId}`,
        {headers :{Authorization:`Bearer ${userToken}`}}

        )
      
      if(data.isSuccess==true){
        setType(data.result.type);
        setName(data.result.name);

        console.log(data)
      }
    }
    }
    useEffect(() => {
        getMaterial();
      }, [userToken]);
    
  return (
   
    <Layout title={name}>
       {type=='Task'&& <ViewTask  materialID={materialId} courseId={courseId}/>} 
       {type=='Announcement'&& <ViewAnnouncement  materialID={materialId} courseId={courseId}/>}
       {type=='File'&& <ViewFile  materialID={materialId} courseId={courseId}/>}
       {type=='Link'&& <ViewLink materialID={materialId} courseId={courseId}/>}

    </Layout>
  )
}
