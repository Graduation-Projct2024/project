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
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import Divider from '@mui/material/Divider';
import axios from 'axios';
import Button from '@mui/material/Button';
import HighlightOffIcon from '@mui/icons-material/HighlightOff';
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import ModeEditIcon from '@mui/icons-material/ModeEdit';
import CircularProgress from "@mui/material/CircularProgress";
import './style.css'
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';
import { useRouter } from 'next/navigation'
import EditTask from '../Edit/EditTask.jsx';
import { styled } from '@mui/material/styles';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell, { tableCellClasses } from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { UserContext } from '../../../../context/user/User.jsx';

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: '#4c5372', // Change the background color here
    color: theme.palette.common.white,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 14,
  },
}));

const StyledTableRow = styled(TableRow)(({ theme }) => ({
  '&:nth-of-type(odd)': {
    backgroundColor: theme.palette.action.hover,
  },
  // hide last border
  '&:last-child td, &:last-child th': {
    border: 0,
  },
}));


export default function ViewTask({ materialID , type, Id}) {

  const router = useRouter();
 const [material, setMaterial]=useState(null);
 const [submission, setSubmission]=useState(null);
 const {userToken, setUserToken, userData}=useContext(UserContext);

 const [loading ,setLoading]=useState(true);
 const [isEditing, setIsEditing] = useState(false);
  const [open, setOpen] = React.useState(false);
 const theme = useTheme();
 const fullScreen = useMediaQuery(theme.breakpoints.down('md'));
 const [openAlert, setOpenAlert] = React.useState(false);
 const handleCloseAlert = (event, reason) => {
   if (reason === 'clickaway') {
     return;
   }

   setOpenAlert(false);
 };
 const handleClickOpen = () => {
   setOpen(true);
 };

 const handleClose = () => {
   setOpen(false);
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
    }
    catch(error){
      console.log(error);
    }
  }
 }
 const getSubmission=async()=>{
  if(userToken){
  const {data}= await axios.get(`https://localhost:7116/api/Submissions/GetAllSubmissionForTask?taskId=${materialID}`,
  {headers :{Authorization:`Bearer ${userToken}`}}

  )

if(data.isSuccess==true){
   setSubmission(data.result);
  console.log(data)
}
 }
 }
 const deleteMaterial=async()=>{
  const {data}= await axios.delete(`https://localhost:7116/api/MaterialControllar/DeleteMaterial?id=${materialID}`,
  {headers :{Authorization:`Bearer ${userToken}`}}

  )
  setOpenAlert(true);
  setOpen(false);
  router.back();

 }
 const handleEdit =()=>{
  setIsEditing(!isEditing);
 }
 useEffect(() => {
    getMaterial();
    getSubmission();
  
}, [materialID, isEditing, userToken]);


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
    <IconButton aria-label="delete" onClick={handleClickOpen}>
      <DeleteIcon  color="error"/>
    </IconButton>
    <IconButton aria-label="Edit" onClick={handleEdit}>
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
          The Task Deleted succssfully!
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
           Do you really want to delete this task? This process cannot be undone.
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
    <EditTask materialID={materialID} name={material.name} description={material.description}  deadLine={material.deadLine} pdf={material.pdfUrl} type={type} Id={Id}/>

):(
<div className='mt-5 pt-5 ms-5 task'>

    <List sx={{ ...style, width: '80%', maxWidth: 'none', mt:7, mb:5 }} aria-label="mailbox folders">
    <ListItem sx={{p:3}} >
    <Typography bold sx={{mr:3}}>Task title :</Typography>
    <Typography>{material.name}</Typography>
  </ListItem>
  <Divider component="li" />
  <ListItem sx={{p:3}} >
  <div style={{ display: 'flex', alignItems: 'center' }}>
  <Typography bold sx={{ mr: 3 }}>Task Description:</Typography>
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
  </ListItem>
</List>
<TableContainer component={Paper} sx={{width:'80%'}}>
      <Table sx={{ minWidth:700 }} aria-label="customized table">
        <TableHead>
          <TableRow>
            <StyledTableCell>Student Name</StyledTableCell>
            <StyledTableCell align="center">Submissions</StyledTableCell>
        
          </TableRow>
        </TableHead>
        <TableBody>

          {submission?.length ?(
          submission.map((subm) => (
            <StyledTableRow key={subm.taskId}>
              <StyledTableCell component="th" scope="row">
                {subm.userName}
              </StyledTableCell>
              <StyledTableCell align="center"><Link target='_blank' href={subm.pdfUrl}>File</Link></StyledTableCell>
            
            </StyledTableRow>
          ))):(
            <p className='ps-3 pt-3'>No Submissions Yet.</p>
          )}
        </TableBody>
      </Table>
    </TableContainer>
</div>
)}
      
   
  </>
  )
}
