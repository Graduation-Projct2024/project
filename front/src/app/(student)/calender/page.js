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

export default function CalendarPage() {
  const {userToken, setUserToken, userData,userId}=useContext(UserContext);

  const initialValues = {
    name: "",
    InstructorId: "",
    Duration:"",
    type:"",
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
  const onSubmit = async (lectures) => {
console.log("test")
const formData = new FormData();
formData.append("name", tasks.name);
formData.append("linkUrl", tasks.linkUrl);
formData.append("courseId", courseId);
formData.append("InstructorId",userData.userId);

const { data } = await axios.post(
  `http://localhost:5134/api/StudentsContraller/BookALecture?studentId=${userId}&date=2024%2F04%2F28&startTime=15%3A00%3A00%20GMT&endTime=16%3A00%3A00%20GMT`,
  formData,
  // {headers: {
  //   'Content-Type': 'multipart/form-data','Content-Type': 'application/json',
  // }}

);
 if(data.isSuccess){
  console.log("test");
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

const [selectedInstructor,setSelectedInstructor]=React.useState();
  const [open, setOpen] = React.useState(false);
  const theme = useTheme();
  const fullScreen = useMediaQuery(theme.breakpoints.down('md'));
  const [isChecked, setIsChecked] = React.useState(false);
  const [status, setStatus] = React.useState('private');
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

    }
  };
  const handleClose = () => {
    setOpen(false);
  };
  const calendarRef = useRef(null);

  useEffect(() => {
    getInstructors();
    if (calendarRef.current) {
      calendarRef.current.getApi().setOption('dayMaxEventRows', 3);
    }
  }, [status]);
  return (
    <Layout title='Book a Lecture'>
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
       {renderInputs}
       <div className="col-md-12">
       <select
        className="form-select p-3"
        aria-label="Default select example"
        value={selectedInstructor}
        onChange={(e) => {
          formik.handleChange(e);
          setSelectedInstructor(e.target.value);
        }}
      >
        <option value="" disabled>
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