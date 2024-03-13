'use client'
import React, { useEffect, useState } from 'react'
import Layout from '../../studentLayout/Layout.jsx'
import axios from 'axios';
import { useParams } from 'next/navigation.js';
import AssignmentIcon from '@mui/icons-material/Assignment';
import InsertDriveFileIcon from '@mui/icons-material/InsertDriveFile';
import MessageIcon from '@mui/icons-material/Message';
import LinkIcon from '@mui/icons-material/Link';
import { deepPurple } from '@mui/material/colors';
import Typography from '@mui/material/Typography';
import WhatsAppIcon from '@mui/icons-material/WhatsApp';
import EmailIcon from '@mui/icons-material/Email';
import Link from '@mui/material/Link';
import Box from '@mui/material/Box';
import '../../../../../node_modules/bootstrap/dist/js/bootstrap.bundle.min.js'
import './style.css'

export default function page() {
    const contents=[
        {
            title:'Task 1',
            color:'#7C7E9D',
            icon:AssignmentIcon,
            description:'this task until 3/3/2024',
            type:'task'
        },
        {
            title:'Chapter 1 ',
            color:'#949AB1',
            icon:InsertDriveFileIcon,
            description:'this file upload for chapter 1',
            type:'file'


        },
          {
            title:'Announcement 1',
            color:'#7388D7',
            icon:MessageIcon,
            description:'',
            type:'announcement'


        },
        {
            title:'Lecture 1',
            color:'#9AA6D7',
            icon:LinkIcon,
            description:'Link of lecture one ',
            type:'link'

        }
];
const participants=[
    {
        name:'Hala Mallak',
        whatsApp:'+970569047593',
        email:'hala_mallak6522@hotmail.com'

    },
    {
        name:'Tala mahmoud',
        whatsApp:'+970593925818',
        email:'hala_mallak6522@hotmail.com'

    },
    {
        name:'Manar fuqha',
        whatsApp:'+970569047593',
        email:'hala_mallak6522@hotmail.com'

    },
    {
        name:'Nour Mallak',
        whatsApp:'+970569047593',
        email:'hala_mallak6522@hotmail.com'

    },
    {
      name:'Hala Mallak',
      whatsApp:'+970569047593',
      email:'hala_mallak6522@hotmail.com'

  },
  {
      name:'Tala mahmoud',
      whatsApp:'+970593925818',
      email:'hala_mallak6522@hotmail.com'

  },
  {
      name:'Manar fuqha',
      whatsApp:'+970569047593',
      email:'hala_mallak6522@hotmail.com'

  },
  {
      name:'Nour Mallak',
      whatsApp:'+970569047593',
      email:'hala_mallak6522@hotmail.com'

  },
];
    const [product, setProduct] = useState([]);
    const { courseId } = useParams();
    const getProduct = async () => {
        const { data } = await axios.get(
          `https://ecommerce-node4.vercel.app/products/${courseId}`
        );
        console.log(data.product);
        setProduct(data.product);
      };
      useEffect(() => {
        getProduct();
      }, []);
  return (
    <Layout title={product.name}>
    <div>
  <ul className="nav nav-tabs" id="myTab" role="tablist">
    <li className="nav-item" role="presentation">
      <button className="nav-link active" id="content-tab" data-bs-toggle="tab" data-bs-target="#content-tab-pane" type="button" role="tab" aria-controls="home-tab-pane" aria-selected="true">Content</button>
    </li>
    <li className="nav-item" role="presentation">
      <button className="nav-link" id="Participants-tab" data-bs-toggle="tab" data-bs-target="#Participants-tab-pane" type="button" role="tab" aria-controls="Participants-tab-pane" aria-selected="false">Participants</button>
    </li>
   
  </ul>
  <div className="tab-content" id="myTabContent">
    <div className="tab-pane fade show active" id="content-tab-pane" role="tabpanel" aria-labelledby="content-tab" tabIndex={0}>
   {contents.map(({title, color, icon: Icon, description,type})=>( 
   <Box
      height={70}
      width={1000}
      my={3}
      display="flex"
      alignItems="center"
      gap={4}
      p={2}
      sx={{ border: '1px solid grey' ,borderRadius: 3}}
      className={type}
    >
     <Icon sx={{fontSize:50, }}/>
     <Typography variant='h6'> {title}</Typography>
    </Box>))}
   
    

    </div>
    <div className="tab-pane fade" id="Participants-tab-pane" role="tabpanel" aria-labelledby="Participants-tab" tabIndex={0}>
    <div className='mt-5 ms-5'>
    {participants.map((participant, index)=>( 
    <Box
      height={50}
      width={1000}
      display="flex"
      alignItems="center"
      gap={4}
      p={2}
      sx={{ border: '1px solid grey' ,borderRadius: 3, justifyContent: 'space-between' }}
      className={index%2==0?"bg-purple1":'bg-purple2'}
    >
     <Typography variant='h6'> {participant.name}</Typography>
     <div className='m-2'>
     <Link href={`https://wa.me/${participant.whatsApp}`} className='m-2' ><WhatsAppIcon/></Link>
     <Link href={`mailto:${participant.email}`} ><EmailIcon/></Link>
     </div>
    

    </Box>))}
   
    </div>
   
    
    </div>
   
  </div>
</div>

    </Layout>
  )
}
