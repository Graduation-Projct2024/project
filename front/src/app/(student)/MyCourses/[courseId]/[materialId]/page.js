'use client';
import React, { useEffect, useState } from 'react'
import { useParams } from 'next/navigation.js';
import axios from 'axios';
import Layout from '../../../studentLayout/Layout.jsx';
import ViewTask from '../../../components/view/ViewTask.jsx';
import ViewAnnouncement from '../../../components/view/ViewAnnouncement.jsx';
import ViewFile from '../../../components/view/ViewFile.jsx';
import ViewLink from '../../../components/view/ViewLink.jsx';
import './style.css'
export default function page() {
    const[type,setType]=useState();
    const[name,setName]=useState();

const{materialId}=useParams();
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
       {type=='Task'&& <ViewTask  materialID={materialId}/>} 
       {type=='Announcement'&& <ViewAnnouncement  materialID={materialId}/>}
       {type=='File'&& <ViewFile  materialID={materialId}/>}
       {type=='Link'&& <ViewLink materialID={materialId}/>}

    </Layout>
  )
}
