'use client';
import React, { useEffect, useState } from 'react'
import ViewTask from '../../../components/View/ViewTask.jsx'
import ViewAnnouncement from '../../../components/View/ViewAnnouncement.jsx'
import ViewFile from '../../../components/View/ViewFile.jsx'
import ViewLink from '../../../components/View/ViewLink.jsx'
import { useParams } from 'next/navigation.js';
import Layout from '../../../instructorLayout/Layout.jsx';
import axios from 'axios';

export default function page() {
    const[type,setType]=useState();
    const[name,setName]=useState();
console.log(useParams())
const{materialId, courseId}=useParams();
    const getMaterial=async()=>{
        const {data}= await axios.get(`http://localhost:5134/api/MaterialControllar/GetMaterialById?id=${materialId}`)
      
      if(data.isSuccess==true){
        setType(data.result.type);
        setName(data.result.name);

        console.log(data)
      }
    }
    useEffect(() => {
        getMaterial();
      }, []);
    
  return (
   
    <Layout title={name}>
       {type=='Task'&& <ViewTask  materialID={materialId} courseId={courseId}/>} 
       {type=='Announcement'&& <ViewAnnouncement  materialID={materialId} courseId={courseId}/>}
       {type=='File'&& <ViewFile  materialID={materialId} courseId={courseId}/>}
       {type=='Link'&& <ViewLink materialID={materialId} courseId={courseId}/>}

    </Layout>
  )
}
