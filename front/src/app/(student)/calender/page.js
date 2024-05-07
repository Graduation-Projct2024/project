'use client';
import React, { useRef, useEffect, uesState ,useContext} from 'react';
import FullCalendar from '@fullcalendar/react';
import resourceTimelinePlugin from '@fullcalendar/resource-timeline';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction'; // needed for dateClick
import timeGridPlugin from '@fullcalendar/timegrid';
import listWeek from '@fullcalendar/list';
import listPlugin from '@fullcalendar/list';
import * as yup from "yup";
import axios from "axios";
import { useFormik } from "formik";
import timeGridDay  from '@fullcalendar/timegrid'
import Layout from '../studentLayout/Layout.jsx'
import './style.css'
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import useMediaQuery from '@mui/material/useMediaQuery';
import { useTheme } from '@mui/material/styles';
import Input from '../../../component/input/Input.jsx';
import { UserContext } from '../../../context/user/User.jsx';
import TextArea from '../../../component/input/TextArea.jsx';
import Switch from '@mui/material/Switch';
import { styled } from '@mui/material/styles';
import Tooltip, { tooltipClasses } from '@mui/material/Tooltip';
import FormControlLabel from '@mui/material/FormControlLabel';
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';
export default function CalendarPage() {
const {userToken, setUserToken, userData,userId}=useContext(UserContext);
const [date, setDate]=React.useState();
const [lecture, setLecture]=React.useState();

const [status, setStatus] = React.useState('private');
const [selectedInstructor,setSelectedInstructor]=React.useState();
const [open, setOpen] = React.useState(false);
const [alertOpen, setAlertOpen] = React.useState(false);

  const initialValues = {
    name: "",
    description:"",
    startTime:"",
    endTime:""
  };
  const[instructors, setInstructors]=React.useState();
  const getInstructors = async () => {
    const data = await axios.get(
      `http://localhost:5134/api/Employee/GetAllInstructorsList
      `
    );
  
    console.log(data);
    setInstructors(data.data.result);
  };
  const getLectures = async () => {
    if(userId){
    const {data} = await axios.post(
      `http://localhost:5134/api/StudentsContraller/GetAllConsultations?studentId=${userId}
      `
    );
    if(data.isSuccess){
    console.log(data.result);

    setLecture(data.result);
    }
    }
  
  };
  console.log(lecture);

  const onSubmit = async (lectures) => {
    const [startHour, startMinute] = lectures.startTime.split(':').map(Number);
    const [endHour, endMinute] = lectures.endTime.split(':').map(Number);

    let durationMinutes = (endHour * 60 + endMinute) - (startHour * 60 + startMinute);

    if (durationMinutes < 0) {
        durationMinutes += 24 * 60; 
    }

    const hours = Math.floor(durationMinutes / 60);
    const minutes = durationMinutes % 60;

    const formattedHours = String(hours).padStart(2, '0');
    const formattedMinutes = String(minutes).padStart(2, '0');

    const duration=`${formattedHours}:${formattedMinutes}`;
    const formData = new FormData();
formData.append("name", lectures.name);
formData.append("description", lectures.description);
formData.append("type", status);
formData.append("InstructorId", selectedInstructor);
formData.append("Duration",duration);


const { data } = await axios.post(
  `http://localhost:5134/api/StudentsContraller/BookALecture?studentId=${userId}&date=${date}&startTime=${lectures.startTime}&endTime=${lectures.endTime}`,
  formData,
  {headers: {
    'Content-Type': 'application/problem+json; charset=utf-8'
  }}

);
 if(data.isSuccess){
  setAlertOpen(true);
  setOpen(false);
  console.log(data);
 formik.resetForm();
 //setAlertOpen(true);


  }
  };
  const validationSchema = yup.object({
    name: yup
      .string()
      .required("title is required"),
      description: yup.string(),

  });

  const formik = useFormik({
    initialValues,
    onSubmit,
    validationSchema: validationSchema,
  });

  const inputs = [
    {
      id: "name",
      type: "text",
      name: "name",
      title: "Title",
      value: formik.values.name,
    },
    {
      id: "startTime",
      type: "time",
      name: "startTime",
      title: "startTime",
      value: formik.values.startTime,
    }
    ,
    {
      id: "endTime",
      type: "time",
      name: "endTime",
      title: "endTime",
      value: formik.values.endTime,
    }
    ,
    {
      id: "description",
      type: "text",
      name: "description",
      title: "Description",
      value: formik.values.description,
    }
  ];
  const renderInputs = inputs.slice(0, -1).map((input, index) => (
    <Input
      type={input.type}
      id={input.id}
      name={input.name}
      value={input.value}
      title={input.title}
      onChange={input.onChange || formik.handleChange}
      onBlur={formik.handleBlur}
      touched={formik.touched}
      errors={formik.errors}
      key={index}
    />
  ));
  const lastInput = inputs[inputs.length - 1];

const textAraeInput = (
  <TextArea
    type={lastInput.type}
    id={lastInput.id}
    name={lastInput.name}
    value={lastInput.value}
    title={lastInput.title}
    onChange={lastInput.onChange || formik.handleChange}
    onBlur={formik.handleBlur}
    touched={formik.touched}
    errors={formik.errors}
    key={inputs.length - 1}
  />
);

 

  const theme = useTheme();
  const fullScreen = useMediaQuery(theme.breakpoints.down('md'));
  const [isChecked, setIsChecked] = React.useState(false);
  const longText = `
  When "Public" is activated, anyone can book the lecture with you.
  `;

  const handleChange = (event) => {
    setIsChecked(event.target.checked);
    if(event.target.checked==true){
      setStatus('public');
    
    }else{
      setStatus('private');
    }
  };
  const IOSSwitch = styled((props) => (
    <Switch focusVisibleClassName=".Mui-focusVisible" disableRipple {...props} />
  ))(({ theme }) => ({
    width: 42,
    height: 26,
    padding: 0,
    '& .MuiSwitch-switchBase': {
      padding: 0,
      margin: 2,
      transitionDuration: '300ms',
      '&.Mui-checked': {
        transform: 'translateX(16px)',
        color: '#fff',
        '& + .MuiSwitch-track': {
          backgroundColor: theme.palette.mode === 'dark' ? '#2ECA45' : '#65C466',
          opacity: 1,
          border: 0,
        },
        '&.Mui-disabled + .MuiSwitch-track': {
          opacity: 0.5,
        },
      },
      '&.Mui-focusVisible .MuiSwitch-thumb': {
        color: '#33cf4d',
        border: '6px solid #fff',
      },
      '&.Mui-disabled .MuiSwitch-thumb': {
        color:
          theme.palette.mode === 'light'
            ? theme.palette.grey[100]
            : theme.palette.grey[600],
      },
      '&.Mui-disabled + .MuiSwitch-track': {
        opacity: theme.palette.mode === 'light' ? 0.7 : 0.3,
      },
    },
    '& .MuiSwitch-thumb': {
      boxSizing: 'border-box',
      width: 22,
      height: 22,
    },
    '& .MuiSwitch-track': {
      borderRadius: 26 / 2,
      backgroundColor: theme.palette.mode === 'light' ? '#E9E9EA' : '#39393D',
      opacity: 1,
      transition: theme.transitions.create(['background-color'], {
        duration: 500,
      }),
    },
  }));
  const handleClickOpen = () => {
    setOpen(true);
  };
  const handleDateClick = (arg) => {
    let today=new Date();
    let date2 = new Date(arg.dateStr);

    if(date2.getDate()>today.getDate()){
      setOpen(true);
      setDate(arg.dateStr);

    }
  };
  console.log(date);
  const handleClose = () => {
    setOpen(false);
  };
  const handleCloseAlert = () => {
    setAlertOpen(false);
  };
  const calendarRef = useRef(null);

  useEffect(() => {
    getInstructors();
    getLectures();
    if (calendarRef.current) {
      calendarRef.current.getApi().setOption('dayMaxEventRows', 3);
    }
  }, [status, date, selectedInstructor]);
  return (
    <Layout title='Book a Lecture'>
       <Snackbar open={alertOpen} autoHideDuration={6000} onClose={handleCloseAlert}>
        <Alert
          onClose={handleClose}
          severity="success"
          variant="filled"
          sx={{ width: '100%' }}
        >
          Lecture Booked succssfully!
        </Alert>
      </Snackbar>
      <div className='calendar-container '>
      <FullCalendar
      height={'100vh'}
      schedulerLicenseKey='GPL-My-Project-Is-Open-Source'
        plugins={[
          resourceTimelinePlugin,
          dayGridPlugin,
          interactionPlugin,
          timeGridPlugin,
          listPlugin
        ]}
        headerToolbar={{
          left: 'prev,next today',
          center: 'title',
          right: 'dayGridMonth,timeGridWeek,listWeek',
        }}
        initialView="dayGridMonth" // Initial view can be customized as needed
        nowIndicator={true}
        //editable= {true}
        //events={{}}
        //eventContent={handleClickOpen}
        selectable={true}
        selectMirror={true}
        eventLimit= {true}
        ref={calendarRef}
        // eventDidMount ={(info)=>{
        // }}


        //views= {views}
        //initialDate={new Date()}
        events={[{ title: 'nice event', start: new Date(), resourceId: 'a' }, { title: 'nice event', start: new Date(), resourceId: 'a' }, { title: 'nice event', start: new Date(), resourceId: 'a' }, { title: 'nice event', start: new Date(), resourceId: 'a' },  { title: 'event 1', start: '2024-04-30T12:30:00Z', resourceId: 'a' },
        { title: 'event 2', start: '2024-04-02T12:30:00Z', resourceId: 'a' }]}
        

        dateClick={handleDateClick} // Bind handleDateClick function to dateClick event
        timeZone="Asia/Hebron"
      />
           <Dialog
        fullScreen={fullScreen}
        open={open}
        onClose={handleClose}
        aria-labelledby="responsive-dialog-title"
        sx={{
          "& .MuiDialog-container": {
            "& .MuiPaper-root": {
              width: "100%",
              maxWidth: "500px!important",  
              minHeight: "300px!important",            },
          },
          
        }}
        >
        <DialogTitle id="responsive-dialog-title" sx={{ display: 'flex', alignItems: 'center' }}>
  <span className='mt-4 mb-2'>Lecture Information</span>
  <Tooltip title={longText} arrow placement="bottom-start">
    <div style={{ marginLeft: 'auto' }}>
      <FormControlLabel
      
        value="Public"
        control={<IOSSwitch checked={isChecked} onChange={handleChange} inputProps={{ 'aria-label': 'controlled' }} />}
        label="Public"
        labelPlacement="top"
      />
    </div>
  </Tooltip>
</DialogTitle>


        <DialogContent>
        <form onSubmit={formik.handleSubmit} encType="multipart/form-data">        
        {renderInputs}
        <div className="col-md-12">
       <select
        className="form-select p-3"
        aria-label="Default select example"
        value={selectedInstructor}
        onChange={(e) => {
          formik.handleChange(e);
          console.log(e.target.value);
          setSelectedInstructor(e.target.value);
          
        }}
      >
        <option value="" >
          Select instructor
        </option>
        {instructors?.length ? (
          instructors.map((instructor, index) => (
            <option key={instructor.id} value={instructor.id}>
              {instructor.name}
            </option>
          ))
        ) : (
          <option disabled>No data</option>
        )}
      </select>
      </div>
      
        {textAraeInput}
        <div className="text-center mt-3">
        <Button sx={{px:2}} variant="contained"
              className="m-2 btn "
              type="submit"
              disabled={!formik.isValid}
            >
              Add
            </Button>
        </div>
      </form>
      
        </DialogContent>
        <DialogActions>
         
          <Button onClick={handleClose} autoFocus>
            Cancle
          </Button>
        </DialogActions>
      </Dialog>
      </div>
     
    </Layout>
  )
}