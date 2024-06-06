'use client'
import axios from 'axios';
import React, { useEffect, useState } from 'react'
import Layout from '../Layout/Layout'
import { faCode} from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import './Events.css'
import Box from '@mui/material/Box';
import Tab from '@mui/material/Tab';
import TabContext from '@mui/lab/TabContext';
import TabList from '@mui/lab/TabList';
import TabPanel from '@mui/lab/TabPanel';
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import ErrorOutlineIcon from '@mui/icons-material/ErrorOutline';
import Link from '@mui/material/Link';

export default function page() {
  const [events, setEvents] = useState([]);
  const getEvents = async () => {
      try {
        const { data } = await axios.get(
          `https://localhost:7116/api/EventContraller/GetAllAccreditEvents`
        );
        console.log(data);
        setEvents(data.result.items);
      } catch (error) {
        console.log(error);
      }
    
  };

  useEffect(() => {
    getEvents();
  }, []);
  const [value, setValue] = React.useState('1');

  const handleChange = (event, newValue) => {
    setValue(newValue);
  };
  return (
    <Layout>
       <div className='container'>
      <div className="pageTitle text-center py-5">
        <h2 className='eventstitle'>All Events</h2>
        <div className="shape1">
          <FontAwesomeIcon icon={faCode} style={{color: "#4c5372",}} className='shape1a fs-3'/>
          {/* <img src="/tag.png" alt="tag" /> */}
        </div>
        {/* <div className="shape2">
          <DeveloperModeIcon className='shape1a fs-3'/>
        </div>
         */}
      </div>
      <Box sx={{ width: '100%', typography: 'body1' }}>
      <TabContext value={value}>
        <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
          <TabList onChange={handleChange} aria-label="lab API tabs example">
            <Tab label="All" value="1" />
            <Tab label="Upcoming" value="2" />
            <Tab label="Expired" value="3" />
          </TabList>
        </Box>
        <TabPanel value="1">
          <div className='events'>
            <div className='row'>
              {events?.length?(
                events.map(event=>(
                  <div className='col-lg-4 col-sm-6 col-md-6 '>
                    <div className='event-details'>
                      <div className='event-image'>
                      <img src={`https://localhost:7116/${event.imageUrl.split('https://localhost:7116/')[2]}`}/>
                      <span className='date'><CalendarMonthIcon sx={{m:1}}/>{event.dateOfEvent}</span>
                      </div>
                    
                  
                  <div className='content'>
                  <h3>
                    <Link href={`/AllEvents/${event.id}`} className='event-title'>{event.name}</Link>
                  </h3>
                  <p>{event.content.substring(0, event.content.length /5)}...{' '}
            <Link href={`/AllEvents/${event.id}`} className="see-more">See More</Link></p>
                  </div>
                  </div>
                  </div>
                ))
              ):(<h4 className='text-center'> <ErrorOutlineIcon sx={{m:1}}/>no events yet.</h4>)}
            </div>
          </div>

        </TabPanel>
        <TabPanel value="2">Item Two</TabPanel>
        <TabPanel value="3">Item Three</TabPanel>
      </TabContext>
    </Box>
      </div>
    </Layout>
  )
}
