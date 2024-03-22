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
import axios from 'axios';
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import ModeEditIcon from '@mui/icons-material/ModeEdit';
import CircularProgress from "@mui/material/CircularProgress";
import './style.css'
export default function ViewLink({ materialID }) {
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
    <List sx={{ ...style, width: '80%', maxWidth: 'none' }} aria-label="mailbox folders">
    <ListItem sx={{p:4}} >
    <Typography bold sx={{mr:3}}>Title :</Typography>
    <Typography>{material.name}</Typography>
  </ListItem>
  <Divider component="li" />
  <ListItem sx={{p:4}} >
    <Typography bold sx={{mr:3}}>Description :</Typography>
    <Typography>{material.description}</Typography>
  </ListItem>
  <Divider component="li" />
  <ListItem sx={{p:4}} >
    <Typography bold sx={{mr:3}}>Link :</Typography>
    <Link download target='_blank'  href={`${material.linkUrl}`}>{material.name}</Link>
  </ListItem>
</List>
   
  </>
  )
}
