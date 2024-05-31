'use client';
import React, { useContext, useEffect, useState } from 'react'
import ViewTask from '../../../components/View/ViewTask.jsx'
import ViewAnnouncement from '../../../components/View/ViewAnnouncement.jsx'
import ViewFile from '../../../components/View/ViewFile.jsx'
import ViewLink from '../../../components/View/ViewLink.jsx'
import { useParams } from 'next/navigation.js';
import Layout from '../../../studentLayout/Layout.jsx';
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
        try{
        const {data}= await axios.get(`https://localhost:7116/api/MaterialControllar/GetMaterialById?id=${materialId}`,
        {headers :{Authorization:`Bearer ${userToken}`}}

        )
      
        setType(data.result.type);
        setName(data.result.name);

        console.log(data)
        }catch(error){
          console.log(error);
        }
    }
    }
    useEffect(() => {
        getMaterial();
      }, [userToken,type]);
    
  return (
   
    <Layout title={name}>
       {type=='Task'&& <ViewTask  materialID={materialId} type='courseId' Id={courseId}/>} 
       {type=='Announcement'&& <ViewAnnouncement  materialID={materialId} type='courseId' Id={courseId}/>}
       {type=='File'&& <ViewFile  materialID={materialId}  type='courseId' Id={courseId}/>}
       {type=='Link'&& <ViewLink materialID={materialId}  type='courseId' Id={courseId}/>}

    </Layout>
  )
}
