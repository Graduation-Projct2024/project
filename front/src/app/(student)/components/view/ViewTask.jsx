'use client';
import React, { useState, useEffect , useContext} from 'react';
import Link from '@mui/material/Link';
import Button from '@mui/material/Button';
import { useQuery } from "react-query";
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import Divider from '@mui/material/Divider';
import fileDownload from 'js-file-download'
import { UserContext } from '../../../../context/user/User.jsx';
import axios from 'axios';
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import ModeEditIcon from '@mui/icons-material/ModeEdit';
import CircularProgress from "@mui/material/CircularProgress";
import './style.css'
import AddTaskSubmission from '../Add/AddTaskSubmission.jsx';
import PictureAsPdfIcon from '@mui/icons-material/PictureAsPdf';
import FileDownloadIcon from '@mui/icons-material/FileDownload';

export default function ViewTask({ materialID }) {
 const [material, setMaterial]=useState(null);
 const {userToken, setUserToken, userData}=useContext(UserContext);
 const [loading ,setLoading]=useState(true);
 const getMaterial=async()=>{
  if(userToken){
    try{
  const {data}= await axios.get(`${process.env.NEXT_PUBLIC_EDUCODING_API}MaterialControllar/GetMaterialById?id=${materialID}`,
  {headers :{Authorization:`Bearer ${userToken}`}}

  )

  setMaterial(data.result);
  setLoading(false);
  console.log(data)

  }
catch(error){
  console.log(error);
}}
 }
 useEffect(() => {
    getMaterial();
  
}, [materialID, userToken]);


const DownloadMaterial = async (url) => {
  let cleanUrl = url.replace("https://localhost:7116/", "");
  let fileName = url.replace("https://localhost:7116/Files\\", "");

  console.log(fileName);

  const { data } = await axios.get(
    `${process.env.NEXT_PUBLIC_EDUCODING_API}Files/DownloadFile?filename=${cleanUrl}`,
    {
      responseType: 'blob',
      headers: {
        'Authorization': `Bearer ${userToken}`
      }
    }
  );

  fileDownload(data, fileName);
};


  const style = {
    p: 0,
    width: '100%',
    maxWidth: 360,
    borderRadius: 2,
    border: '1px solid',
    borderColor: 'divider',
    backgroundColor: 'background.paper',
  };



if (loading) {
  return (
    <Box sx={{ display: "flex", justifyContent: "center" }}>
      <CircularProgress color="primary" />
    </Box>
  );
}
  return (
    <>
        <div className='studentTask'>
        <List sx={{ ...style, width: '80%', maxWidth: 'none', mt: 7, mb: 5 }} aria-label="mailbox folders">
      <ListItem sx={{ p: 3 }}>
        <Typography sx={{ mr: 3, fontWeight: 'bold' }}>Task title :</Typography>
        <Typography>{material.name}</Typography>
      </ListItem>
      <Divider component="li" />
      <ListItem sx={{ p: 3 }}>
        <div style={{ display: 'flex', alignItems: 'center' }}>
          <Typography sx={{ mr: 3, fontWeight: 'bold' }}>Task Description:</Typography>
          <Typography>{material.description}</Typography>
        </div>
      </ListItem>
      <Divider component="li" />
      <ListItem sx={{ p: 3 }}>
        <Typography sx={{ mr: 3, fontWeight: 'bold' }}>DeadLine :</Typography>
        <Typography>{material.deadLine}</Typography>
      </ListItem>
      <Divider component="li" />
      <ListItem sx={{ p: 3 }}>
        <Typography sx={{ mr: 3, fontWeight: 'bold' }}>File :</Typography>
        {material.materialFiles?.length ? (
             material.materialFiles.map((file, index) => (
              <Box key={index} sx={{ display: 'flex', alignItems: 'center', border: '1px solid', p: 1, mb: 1 }}>
                <PictureAsPdfIcon sx={{ mr: 1, color:'#4c5372' }} />
                <Link target='_blank' href={`${file.pdfUrl}`}>
                  File {index + 1}
                </Link>
                <IconButton aria-label="download" onClick={()=>DownloadMaterial(file.pdfUrl)}>
        <FileDownloadIcon sx={{color:'#4c5372' }} />
      </IconButton>
              </Box>
         ))
        ) : (
          <>
           <Link download target='_blank'  href={`${material.pdfUrl}`}>{material.name}</Link>
           <IconButton aria-label="download" onClick={()=>DownloadMaterial(material.pdfUrl)} >
        <FileDownloadIcon sx={{color:'#4c5372' }} />
      </IconButton>
        
          
          </>
         
          )
        }
      </ListItem>
    </List>
    

</div>
<Box
      width='85%'
      my={4}
      ml={7}
      display="flex"
      alignItems="center"
      gap={4}
      p={2}
      
      sx={{ border: '2px solid grey', borderRadius: 3 }}
    >
      <Stack  direction="column"
  justifyContent="center"
  alignItems="center"
  spacing={2}
  mt='5px'>
          <AddTaskSubmission materialID={materialID}/>

  </Stack>
    </Box>   
   
  </>
  )
}
