// import React, { useState } from 'react';
// import './Profile.css';
// import { faCalendarPlus, faXmark } from '@fortawesome/free-solid-svg-icons';
// import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

// export default function WeeklyHours({ id }) {
//     const [days] = useState(['Sat', 'Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri']);  // Ensure abbreviations are correct here
//     const [hours, setHours] = useState({
//         Sat: [],
//         Sun: [],
//         Mon: [],
//         Tue: [],
//         Wed: [],
//         Thu: [],
//         Fri: []
//     });

//     const addHours = (day) => {
//         const updatedHours = { ...hours, [day]: [...(hours[day] || []), {}] };  // Use a fallback empty array
//         setHours(updatedHours);
//     };

//     const removeHours = (day, index) => {
//         if (hours[day]) {
//             const updatedDayHours = [...hours[day]];
//             updatedDayHours.splice(index, 1);
//             const updatedHours = { ...hours, [day]: updatedDayHours };
//             setHours(updatedHours);
//         }
//     };

//     return (
//         <div className="weeklyHours p-5">
//             <h2 className='pr pb-3'>Weekly Hours</h2>
//             {days.map(day => (
//                 <div key={day} className="day-block row gap-2">
//                     <h3 className='day col-md-1'>{day}</h3>
//                     <div className="col-md-4 mb-3 pb-3">
//                         {(hours[day] || []).length ? hours[day].map((hour, index) => (
//                             <div key={index} className='d-flex align-items-center gap-2'>
//                                 <input type="time" id={`start-${day}-${index}`} name="start" />
//                                 <p className='day pt-3'>to</p>
//                                 <input type="time" id={`end-${day}-${index}`} name="end" />
//                                 <button className="border-0 bg-white" type="button" onClick={() => removeHours(day, index)}>
//                                     <FontAwesomeIcon icon={faXmark} className='fs-4 day' />
//                                 </button>
//                             </div>
//                         )) : <p className='unA'>UNAVAILABLE</p>}
                        
//                     </div>
//                     <button className="border-0 bg-white col-md-1" type="button" onClick={() => addHours(day)}>
//                         <FontAwesomeIcon icon={faCalendarPlus} className='fs-4 day' />
//                     </button>
//                 </div>
//             ))}
//         </div>
//     );
// }

// import React, { useState } from 'react';
// import './Profile.css';
// import { faCalendarPlus, faXmark } from '@fortawesome/free-solid-svg-icons';
// import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
// import axios from 'axios'; // Assuming axios is installed, otherwise use fetch

// export default function WeeklyHours({ id }) {
//     const [days] = useState([ 'Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri','Sat']);
//     const [hours, setHours] = useState({
//         Sun: [],
//         Mon: [],
//         Tue: [],
//         Wed: [],
//         Thu: [],
//         Fri: [],
//         Sat: [],
//     });

//     const addHours = (day) => {
//         const updatedHours = { ...hours, [day]: [...(hours[day] || []), { startTime: '', endTime: '' }] };
//         setHours(updatedHours);
//     };

//     const removeHours = (day, index) => {
//         if (hours[day]) {
//             const updatedDayHours = [...hours[day]];
//             updatedDayHours.splice(index, 1);
//             const updatedHours = { ...hours, [day]: updatedDayHours };
//             setHours(updatedHours);
//         }
//     };

//     const handleTimeChange = (day, index, field, value) => {
//         const updatedHours = [...hours[day]];
//         updatedHours[index] = { ...updatedHours[index], [field]: value };
//         setHours({ ...hours, [day]: updatedHours });
//     };

//     const submitHours = async (day) => {
//         try {
//             const dayHours = hours[day].map(async (hour) => {
//                 const response = await axios.post(`http://localhost:5134/api/Employee/AddInstructorOfficeHours/${id}`, {
//                     Day: day,
//                     StartTime: hour.startTime,
//                     EndTime: hour.endTime
//                 });
//                 return response.data;
//             });
//             await Promise.all(dayHours);
//             alert('Hours updated successfully');
//         } catch (error) {
//             console.error('Error updating hours:', error);
//             alert('Failed to update hours');
//         }
//     };

//     return (
//         <div className="weeklyHours p-5">
//             <h2 className='pr pb-3'>Weekly Hours</h2>
//             {days.map(day => (
//                 <div key={day} className="day-block row gap-2">
//                     <h3 className='day col-md-1'>{day}</h3>
//                     <div className="col-md-4 mb-3 pb-3">
//                         {(hours[day] || []).length ? hours[day].map((hour, index) => (
//                             <div key={index} className='d-flex align-items-center gap-2'>
//                                 <input type="time" value={hour.startTime} onChange={(e) => handleTimeChange(day, index, 'startTime', e.target.value)} />
//                                 <p className='day pt-3'>to</p>
//                                 <input type="time" value={hour.endTime} onChange={(e) => handleTimeChange(day, index, 'endTime', e.target.value)} />
//                                 <button className="border-0 bg-white" type="button" onClick={() => removeHours(day, index)}>
//                                     <FontAwesomeIcon icon={faXmark} className='fs-4 day' />
//                                 </button>
//                             </div>
//                         )) : <p className='unA'>UNAVAILABLE</p>}
//                     </div>
//                     <button className="border-0 bg-white col-md-1" type="button" onClick={() => addHours(day)}>
//                         <FontAwesomeIcon icon={faCalendarPlus} className='fs-4 day' />
//                     </button>
//                     <button className="save-btn" onClick={() => submitHours(day)}>Save {day} Hours</button>
//                 </div>
//             ))}
//         </div>
//     );
// }
// import React, { useState } from 'react';
// import './Profile.css';
// import { faCalendarPlus, faXmark } from '@fortawesome/free-solid-svg-icons';
// import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
// import axios from 'axios'; 

// export default function WeeklyHours({ id }) {
//     const [days] = useState([
//         { name: 'Sun', number: 1 },
//         { name: 'Mon', number: 2 },
//         { name: 'Tue', number: 3 },
//         { name: 'Wed', number: 4 },
//         { name: 'Thu', number: 5 },
//         { name: 'Fri', number: 6 },
//         { name: 'Sat', number: 7 }
//     ]);
//     const [hours, setHours] = useState({
//         Sun: [],
//         Mon: [],
//         Tue: [],
//         Wed: [],
//         Thu: [],
//         Fri: [],
//         Sat: []
//     });

//     const addHours = (day) => {
//         const updatedHours = { ...hours, [day]: [...(hours[day] || []), { startTime: '', endTime: '' }] };
//         setHours(updatedHours);
//     };

//     const removeHours = (day, index) => {
//         if (hours[day]) {
//             const updatedDayHours = [...hours[day]];
//             updatedDayHours.splice(index, 1);
//             const updatedHours = { ...hours, [day]: updatedDayHours };
//             setHours(updatedHours);
//         }
//     };

//     const handleTimeChange = (day, index, field, value) => {
//         const updatedHours = [...hours[day]];
//         updatedHours[index] = { ...updatedHours[index], [field]: value };
//         setHours({ ...hours, [day]: updatedHours });
//     };

//     const submitHours = async (dayName, dayNumber) => {
//         try {
//             const dayHours = hours[dayName].map(async (hour) => {
//                 const response = await axios.post(`http://localhost:5134/api/Employee/AddInstructorOfficeHours?InstructorId=${id}`, {
//                     day: dayNumber,
//                     startTime: hour.startTime,
//                     endTime: hour.endTime
//                 });
//                 return response.data;
//             });
//             await Promise.all(dayHours);
//             alert(`${dayName} hours updated successfully`);
//         } catch (error) {
//             console.error(`Error updating ${dayName} hours:`, error);
//             alert(`Failed to update ${dayName} hours`);
//         }
//     };

//     return (
//         <div className="weeklyHours p-5">
//             <h2 className='pr pb-3'>Weekly Hours</h2>
//             {days.map(day => (
//                 <div key={day.name} className="day-block row gap-2">
//                     <h3 className='day col-md-1'>{day.name}</h3>
//                     <div className="col-md-4 mb-3 pb-3">
//                         {(hours[day.name] || []).length ? hours[day.name].map((hour, index) => (
//                             <div key={index} className='d-flex align-items-center gap-2'>
//                                 <input type="time" value={hour.startTime} onChange={(e) => handleTimeChange(day.name, index, 'startTime', e.target.value)} />
//                                 <p className='day pt-3'>to</p>
//                                 <input type="time" value={hour.endTime} onChange={(e) => handleTimeChange(day.name, index, 'endTime', e.target.value)} />
//                                 <button className="border-0 bg-white" type="button" onClick={() => removeHours(day.name, index)}>
//                                     <FontAwesomeIcon icon={faXmark} className='fs-4 day' />
//                                 </button>
//                             </div>
//                         )) : <p className='unA'>UNAVAILABLE</p>}
//                     </div>
//                     <button className="border-0 bg-white col-md-1" type="button" onClick={() => addHours(day.name)}>
//                         <FontAwesomeIcon icon={faCalendarPlus} className='fs-4 day' />
//                     </button>
//                     <button className="save-btn" onClick={() => submitHours(day.name, day.number)}>Save {day.name} Hours</button>
//                 </div>
//             ))}
//         </div>
//     );
// }

// import React, { useState } from 'react';
// import './Profile.css';
// import { faCalendarPlus, faXmark } from '@fortawesome/free-solid-svg-icons';
// import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
// import axios from 'axios'; 

// export default function WeeklyHours({ id }) {
//     const [days] = useState([
//         { name: 'Sun', number: 1 },
//         { name: 'Mon', number: 2 },
//         { name: 'Tue', number: 3 },
//         { name: 'Wed', number: 4 },
//         { name: 'Thu', number: 5 },
//         { name: 'Fri', number: 6 },
//         { name: 'Sat', number: 7 }
//     ]);
//     const [hours, setHours] = useState({
//         Sun: [],
//         Mon: [],
//         Tue: [],
//         Wed: [],
//         Thu: [],
//         Fri: [],
//         Sat: []
//     });

//     const addHours = (day) => {
//         const updatedHours = { ...hours, [day]: [...(hours[day] || []), { startTime: '', endTime: '' }] };
//         setHours(updatedHours);
//     };

//     const removeHours = (day, index) => {
//         if (hours[day]) {
//             const updatedDayHours = [...hours[day]];
//             updatedDayHours.splice(index, 1);
//             const updatedHours = { ...hours, [day]: updatedDayHours };
//             setHours(updatedHours);
//         }
//     };

//     const handleTimeChange = (day, index, field, value) => {
//         const updatedHours = [...hours[day]];
//         updatedHours[index] = { ...updatedHours[index], [field]: value };
//         setHours({ ...hours, [day]: updatedHours });
//     };

//     const submitHours = async (dayName, dayNumber) => {
//         const dayHours = hours[dayName];
//         // Validate the hours to ensure startTime and endTime are not empty
//         for (let hour of dayHours) {
//             if (!hour.startTime || !hour.endTime) {
//                 alert(`Please fill in both start and end times for ${dayName}.`);
//                 return; // Exit the function if validation fails
//             }
//         }
    
//         try {
//             // Mapping each hour to a POST request promise
//             const promises = dayHours.map(hour =>
//                 axios.post(`http://localhost:5134/api/Employee/AddInstructorOfficeHours?InstructorId=${id}`, {
//                     day: dayNumber,
//                     startTime: hour.startTime,
//                     endTime: hour.endTime
//                 })
//             );
    
//             // Wait for all promises to resolve
//             const results = await Promise.all(promises);
//             alert(`${dayName} hours updated successfully`);
//             console.log(results); // Log results for further inspection if needed
//         } catch (error) {
//             console.error(`Error updating ${dayName} hours:`, error);
//             alert(`Failed to update ${dayName} hours: ${error.response ? error.response.data : error.message}`);
//         }
//     };
    

//     return (
//         <div className="weeklyHours p-5">
//             <h2 className='pr pb-3'>Weekly Hours</h2>
//             {days.map(day => (
//                 <div key={day.name} className="day-block row gap-2">
//                     <h3 className='day col-md-1'>{day.name}</h3>
//                     <div className="col-md-4 mb-3 pb-3">
//                         {(hours[day.name] || []).length ? hours[day.name].map((hour, index) => (
//                             <div key={index} className='d-flex align-items-center gap-2'>
//                                 <input type="time" value={hour.startTime} onChange={(e) => handleTimeChange(day.name, index, 'startTime', e.target.value)} />
//                                 <p className='day pt-3'>to</p>
//                                 <input type="time" value={hour.endTime} onChange={(e) => handleTimeChange(day.name, index, 'endTime', e.target.value)} />
//                                 <button className="border-0 bg-white" type="button" onClick={() => removeHours(day.name, index)}>
//                                     <FontAwesomeIcon icon={faXmark} className='fs-4 day' />
//                                 </button>
//                             </div>
//                         )) : <p className='unA'>UNAVAILABLE</p>}
//                     </div>
//                     <button className="border-0 bg-white col-md-1" type="button" onClick={() => addHours(day.name)}>
//                         <FontAwesomeIcon icon={faCalendarPlus} className='fs-4 day' />
//                     </button>
//                     <button className="save-btn" onClick={() => submitHours(day.name, day.number)}>Save {day.name} Hours</button>
//                 </div>
//             ))}
//         </div>
//     );
// }


'use client'
import Input from '@/component/input/Input';
import InputTime from '@/component/input/InputTime';
import axios from 'axios';
import { useFormik } from 'formik';
import React, { useEffect, useState } from 'react'
import Swal from 'sweetalert2';

export default function WeeklyHours({id}) {

 const[hours,setHours] = useState([{}])
 const [selectedDay, setSelectedDay] = useState('');

  const initialValues={
   day:'',
   startTime:'',
   endTime:'',
};


const onSubmit = async (hours) => {
    try {
      const formData = new FormData();
      formData.append('startTime', hours.startTime);
      formData.append('endTime', hours.endTime);
      formData.append('day', selectedDay); // Use selectedGender from state
      const { data } = await axios.post(`http://localhost:5134/api/Employee/AddInstructorOfficeHours?InstructorId=${id}`, formData,);
      
     if(data.isSuccess){
      console.log(data);
      console.log('tttt');
      formik.resetForm();
      Swal.fire({
        title: "Hours Added Successfully",
        text: "See it above",
        icon: "success"
      });
  }

      console.log('jhbgyvftrgybuhnjimkjhb');
    } catch (error) {
      // Handle the error here
      console.error('Error submitting form:', error);
      console.log('Error response:', error.response);
      // Optionally, you can show an error message to the user
    }
  };





const formik = useFormik({
  initialValues : initialValues,
  onSubmit,
})

const inputs =[
  {
    type : 'time',
      id:'startTime',
      name:'startTime',
      title:'startTime',
      value:formik.values.startTime,
},

  {
      type : 'time',
      id:'endTime',
      name:'endTime',
      title:'endTime',
      value:formik.values.endTime,
  },
 
]



const renderInputs = inputs.map((input,index)=>
  <InputTime type={input.type} 
        id={input.id}
         name={input.name}
          title={input.title} 
          key={index} 
          errors={formik.errors} 
          onChange={formik.handleChange}
           onBlur={formik.handleBlur}
            touched={formik.touched}
            />
        
    )


  return (
    <div className='p-5'>
<h2 className='pr '>Weekly Hours</h2>
    <form onSubmit={formik.handleSubmit} className="row justify-content-center">
        <div className="col-md-6 w-25">
        <select
          className="form-select p-3"
          aria-label="Default select example"
          value={selectedDay}
         onChange={(e) => {
            formik.handleChange(e);
            setSelectedDay(e.target.value);
          }}
        >
          <option value="" disabled>
Select Day          </option>
          <option value="0">Sun</option>
          <option value="1">Mon</option>
          <option value="2">Tue</option>
          <option value="3">Wed</option>
          <option value="4">Thu</option>
          <option value="5">Fri</option>
          <option value="6">Sat</option>
        </select>
      </div>


      {renderInputs}
       

<div className="col-md-12 justify-content-center">

</div>
      <button
        type="submit"
        className="btn btn-primary createButton mt-3 fs-3 px-3 w-25"
      >
       Submit
      </button>


    </form>
    </div>
  );
}
