// 'use client';
// import React, { useContext, useEffect, useState } from 'react'
// import ViewTask from '../../../components/View/ViewTask.jsx'
// import ViewAnnouncement from '../../../components/View/ViewAnnouncement.jsx'
// import ViewFile from '../../../components/View/ViewFile.jsx'
// import ViewLink from '../../../components/View/ViewLink.jsx'
// import { useParams } from 'next/navigation.js';
// import Layout from '../../../instructorLayout/Layout.jsx';
// import axios from 'axios';
// import { UserContext } from '../../../../../context/user/User.jsx';
// import '../style.css'

// export default function page() {
//   const {userToken, setUserToken, userData}=useContext(UserContext);

//     const[type,setType]=useState();
//     const[name,setName]=useState();
// console.log(useParams())
// const{materialId, courseId}=useParams();
//     const getMaterial=async()=>{
//       if(userData){
//         try{
//         const {data}= await axios.get(`${process.env.NEXT_PUBLIC_EDUCODING_API}MaterialControllar/GetMaterialById?id=${materialId}`,
//           { headers: {
//             'Content-Type': 'application/json',
//             'Authorization': `Bearer ${userToken}`
//           }}

//         )
      
//         setType(data.result.type);
//         setName(data.result.name);

//         console.log(data)
//         }catch(error){
//           console.log(error);
//         }
//     }
//     }
//     console.log(type)
//     useEffect(() => {
//         getMaterial();
//       }, [userData,type]);
    
//   return (
   
//     <Layout title={name}>
//        {type=='Task'&& <ViewTask  materialID={materialId} type='courseId' Id={courseId}/>} 
//        {type=='Announcement'&& <ViewAnnouncement  materialID={materialId} type='courseId' Id={courseId}/>}
//        {type=='File'&& <ViewFile  materialID={materialId}  type='courseId' Id={courseId}/>}
//        {type=='Link'&& <ViewLink materialID={materialId}  type='courseId' Id={courseId}/>}

//     </Layout>
//   )
// }

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
import '../style.css'
import dynamic from 'next/dynamic';
// const ViewTask = dynamic(() => import('../../../components/View/ViewTask.jsx'), { ssr: false });
// const ViewFile = dynamic(() => import('../../../components/View/ViewFile.jsx'), { ssr: false });
// const ViewLink = dynamic(() => import('../../../components/View/ViewLink.jsx'), { ssr: false });
// const ViewAnnouncement = dynamic(() => import('../../../components/View/ViewAnnouncement.jsx'), { ssr: false });

export default function page() {
  const {userToken, setUserToken, userData}=useContext(UserContext);

    const[type,setType]=useState();
    const[name,setName]=useState();
console.log(useParams())
const{materialId, courseId}=useParams();
    const getMaterial=async()=>{
      if(userData){
        try{
        const {data}= await axios.get(`${process.env.NEXT_PUBLIC_EDUCODING_API}MaterialControllar/GetMaterialById?id=${materialId}`,
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
      }, [userData,type]);
    
  return (
   
    <Layout title={name}>
       {type=='Task'&& <ViewTask  materialID={materialId} type='courseId' Id={courseId}/>} 
       {type=='Announcement'&& <ViewAnnouncement  materialID={materialId} type='courseId' Id={courseId}/>}
       {type=='File'&& <ViewFile  materialID={materialId}  type='courseId' Id={courseId}/>}
       {type=='Link'&& <ViewLink materialID={materialId}  type='courseId' Id={courseId}/>}

    </Layout>
  )
}