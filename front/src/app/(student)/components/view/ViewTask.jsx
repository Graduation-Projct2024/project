'use client';
import React, { useState, useEffect } from 'react';
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

import axios from 'axios';
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import ModeEditIcon from '@mui/icons-material/ModeEdit';
import CircularProgress from "@mui/material/CircularProgress";
import './style.css'
import AddTaskSubmission from '../Add/AddTaskSubmission.jsx';
export default function ViewTask({ materialID }) {
 const [material, setMaterial]=useState(null);
 const [loading ,setLoading]=useState(true);
 const getMaterial=async()=>{
  const {data}= await axios.get(`http://localhost:5134/api/MaterialControllar/GetMaterialById?id=${materialID}`)

if(data.isSuccess==true){
  setMaterial(data.result);
  setLoading(false);
  console.log(data)
}

 }

 const DownloadMaterial = async () => {
  let cleanUrl = material.pdfUrl.replace("http://localhost:5134/", "");
  let fileName = material.pdfUrl.replace("http://localhost:5134/Files\\", "");

  console.log(fileName);

  const { data } = await axios.get(
    `http://localhost:5134/api/Genaric/DownloadFile?filename=${cleanUrl}`,
  {  responseType: 'blob'},
  );

  fileDownload(data, fileName);
};
 useEffect(() => {
    getMaterial();
  
}, [materialID]);


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

    <List sx={{ ...style, width: '80%', maxWidth: 'none' }} aria-label="mailbox folders">
    <ListItem sx={{p:3}} >
    <Typography bold sx={{mr:3}}>Task title :</Typography>
    <Typography>{material.name}</Typography>
  </ListItem>
  <Divider component="li" />
  <ListItem sx={{p:3}} >
  <div style={{ display: 'flex', alignItems: 'center' }}>

    <Typography bold sx={{mr:3}}>Task Description:</Typography>
    <Typography>{material.description}</Typography>
    </div>
  </ListItem>
  <Divider component="li" />
  <ListItem sx={{p:3}} >
    <Typography bold sx={{mr:3}}>DeadLine :</Typography>
    <Typography>{material.deadLine}</Typography>
  </ListItem>
  <Divider component="li" />
  <ListItem sx={{p:3}} >
    <Typography bold sx={{mr:3}}>File :</Typography>
    <Link download target='_blank'  href={`${material.pdfUrl}`}>{material.name}</Link>
    <Button sx={{px:2, mx:2}} variant="contained"  onClick={DownloadMaterial}>
  Download
</Button>
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
