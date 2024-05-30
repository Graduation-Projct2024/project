'use client';
import React, { useContext, useState, useEffect } from 'react';
import Link from '@mui/material/Link';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import useMediaQuery from '@mui/material/useMediaQuery';
import { useTheme } from '@mui/material/styles';
import Button from '@mui/material/Button';
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
import Snackbar from '@mui/material/Snackbar';
import HighlightOffIcon from '@mui/icons-material/HighlightOff';
import Alert from '@mui/material/Alert';
import EditLink from '../Edit/EditLink.jsx';
import { useRouter } from 'next/navigation'
import { UserContext } from '../../../../context/user/User.jsx';

export default function ViewLink({ materialID , courseId}) {
 const [material, setMaterial]=useState(null);
 const [loading ,setLoading]=useState(true);
 const [isEditing, setIsEditing] = useState(false);
 const router = useRouter();
 const {userToken, setUserToken, userData}=useContext(UserContext);

 const [open, setOpen] = React.useState(false);
 const theme = useTheme();
 const fullScreen = useMediaQuery(theme.breakpoints.down('md'));
 const [openAlert, setOpenAlert] = React.useState(false);
 const handleClickOpen = () => {
  setOpen(true);
};

const handleClose = () => {
  setOpen(false);
};

 const handleCloseAlert = (event, reason) => {
   if (reason === 'clickaway') {
     return;
   }

   setOpenAlert(false);
 };
 const getMaterial=async()=>{
  if(userToken){
    try{
  const {data}= await axios.get(`https://localhost:7116/api/MaterialControllar/GetMaterialById?id=${materialID}`,
  {headers :{Authorization:`Bearer ${userToken}`}}
)

  setMaterial(data.result);
  setLoading(false);
  console.log(data)

  }catch(error){
    console.log(error);
  }
 }}
 const deleteMaterial=async()=>{
  try{
  const {data}= await axios.delete(`https://localhost:7116/api/MaterialControllar/DeleteMaterial?id=${materialID}`,
  {headers :{Authorization:`Bearer ${userToken}`}}

  )
  setOpenAlert(true);
  setOpen(false);
  router.back();}
  catch(error){
    console.log(error);
  }
 }
 const handleEdit =()=>{
  setIsEditing(!isEditing);
 }
 useEffect(() => {
    getMaterial();
  
}, [materialID, userToken]);


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
    <Stack direction="row" alignItems="center"  justifyContent= 'end' width='89%' mt='2px'>
    <div >
    <IconButton aria-label="delete" onClick={handleClickOpen} >
      <DeleteIcon  color="error"/>
    </IconButton>
    <IconButton aria-label="Edit"  onClick={handleEdit} >
      <ModeEditIcon color="success" />
    </IconButton >
    </div>
  </Stack>
     <Snackbar open={openAlert} autoHideDuration={6000} onClose={handleCloseAlert}>
        <Alert
          onClose={handleClose}
          severity="success"
          variant="filled"
          sx={{ width: '100%' }}
        >
          The Link Deleted succssfully!
        </Alert>
      </Snackbar>
     <Dialog
        fullScreen={fullScreen}
        open={open}
        onClose={handleClose}
        aria-labelledby="responsive-dialog-title"
        sx={{p:7}}
      >
       <Stack  direction="column"
  justifyContent="center"
  alignItems="center"
  spacing={2}
  mt='5px'>
       <HighlightOffIcon sx={{fontSize:100, mt:5}} color="error"/>
       <Typography variant='h4'>Are you sure?</Typography>

       </Stack>
        <DialogContent>
          
          <DialogContentText>
           Do you really want to delete this link? This process cannot be undone.
          </DialogContentText>
        </DialogContent>
        <Stack  direction="row"
  justifyContent="center"
  alignItems="center"
  spacing={2}
  mt='5px'>
        <DialogActions>
          <Button autoFocus variant="outlined" color="success" onClick={handleClose} >
            Cancle
          </Button>
          <Button onClick={deleteMaterial} autoFocus variant="contained" color="error">
            Delete
          </Button>
        </DialogActions>
        </Stack>
      </Dialog>
  
      {isEditing?(
    <EditLink materialID={materialID} name={material.name} linkURL={material.linkUrl} courseId={courseId}/>

):(
  <div className='link mt-5 pt-5 ms-5'> 
    <List sx={{ ...style, width: '80%', maxWidth: 'none', mt:7, }} aria-label="mailbox folders">
    <ListItem sx={{p:4}} >
    <Typography bold sx={{mr:3}}>Title :</Typography>
    <Typography>{material.name}</Typography>
  </ListItem>
  <Divider component="li" />

  <ListItem sx={{p:4}} >
    <Typography bold sx={{mr:3}}>Link :</Typography>
    <Link download target='_blank'  href={`${material.linkUrl}`}>{material.name}</Link>
  </ListItem>
</List>
</div>
)}
  </>
  )
}
