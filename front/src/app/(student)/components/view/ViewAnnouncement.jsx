'use client';
import React, { useState } from 'react';
import Link from '@mui/material/Link';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import Button from '@mui/material/Button';
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import Divider from '@mui/material/Divider';
export default function ViewAnnouncement({ open, onClose }) {
 const taskInfo={
  title:'task1',
  description:'this is task',
  deadLine:'12/4/2024',
  file:'task1.pdf'
 }
  const [Alertopen, setAlertOpen] = React.useState(false);
  const handleClose = (event, reason) => {
    if (reason === 'clickaway') {
      return;
    }

    setAlertOpen(false);
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
  return (
    <>
    <Dialog
     open={open} 
     onClose={onClose}
     sx={{
      "& .MuiDialog-container": {
        "& .MuiPaper-root": {
          width: "100%",
          maxWidth: "700px!important",  
          // height: "800px!important", 
                    },
      },}} >

    <DialogTitle>Task</DialogTitle>
    <DialogContent>
    
    <List sx={{ ...style, width: '100%', maxWidth: 'none' }} aria-label="mailbox folders">
    <ListItem sx={{p:3}} >
    <Typography bold sx={{mr:3}}>Task title :</Typography>
    <Typography>{taskInfo.title}</Typography>
  </ListItem>
  <Divider component="li" />
  <ListItem sx={{p:3}} >
    <Typography bold sx={{mr:3}}>Task Description :</Typography>
    <Typography>{taskInfo.description}</Typography>
  </ListItem>
  <Divider component="li" />
  <ListItem sx={{p:3}} >
    <Typography bold sx={{mr:3}}>DeadLine :</Typography>
    <Typography>{taskInfo.deadLine}</Typography>
  </ListItem>
  <Divider component="li" />
  <ListItem sx={{p:3}} >
    <Typography bold sx={{mr:3}}>File :</Typography>
    <Typography>{taskInfo.file}</Typography>
  </ListItem>
</List>
    </DialogContent>
    <DialogActions>
      <Button onClick={onClose} color="primary">
        Close
      </Button>
    </DialogActions>
  </Dialog>
  </>
  )
}
