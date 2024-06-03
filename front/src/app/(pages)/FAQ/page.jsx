'use client'
import React, { useState } from 'react';
import Layout from '../Layout/Layout';
import './FAQ.css';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import SchoolIcon from '@mui/icons-material/School';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import PlayCircleFilledWhiteIcon from '@mui/icons-material/PlayCircleFilledWhite';
import ReviewsIcon from '@mui/icons-material/Reviews';
import TabContext from '@mui/lab/TabContext';
import TabList from '@mui/lab/TabList';
import TabPanel from '@mui/lab/TabPanel';
import Box from '@mui/material/Box';
import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import AccordionDetails from '@mui/material/AccordionDetails';
import Typography from '@mui/material/Typography';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import Fade from '@mui/material/Fade';

export default function Page() {
  const [expanded, setExpanded] = useState(false);

  const handleExpansion = () => {
    setExpanded((prevExpanded) => !prevExpanded);
  };

  // Set the initial state value to "0" to open the first tab by default
  const [value, setValue] = useState('0');

  const handleChange = (event, newValue) => {
    setValue(newValue);
  };

  return (
    <Layout>
      <div className='container'>
        <div className='FAQ-title'>
          <h2 className='text-center pt-4'>FAQ's</h2>
        </div>

        <Box sx={{ width: '100%', typography: 'body1', mt: 5 }}>
          <TabContext value={value}>
            <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
              <TabList onChange={handleChange} aria-label="lab API tabs example" centered>
                <Tab icon={<PlayCircleFilledWhiteIcon />} label="Getting Started" value="0" />
                <Tab icon={<SchoolIcon />} label="Education" value="1" />
                <Tab icon={<AccessTimeIcon />} label="Availability" value="2" />
                <Tab icon={<ReviewsIcon />} label="Reviews" value="3" />
              </TabList>
            </Box>
            <TabPanel value="0">
              <div className='Accordion-body'>
                <Accordion
                  expanded={expanded}
                  onChange={handleExpansion}
                  TransitionComponent={Fade}
                  TransitionProps={{ timeout: 400 }}
                  sx={{
                    '& .MuiAccordion-region': { height: expanded ? 'auto' : 0 },
                    '& .MuiAccordionDetails-root': { display: expanded ? 'block' : 'none' },
                  }}
                >
                  <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel1-content"
                    id="panel1-header"
                  >
                    <Typography sx={{ fontWeight: 'bold' }}>How can I contact a school directly?</Typography>
                  </AccordionSummary>
                  <AccordionDetails>
                    <Typography sx={{ color: '#606060' }}>
                      You can contact a school by filling out a “Contact Us” form. This form can be found to the right of both the institute and education program profiles and also at the bottom of these profiles.
                    </Typography>
                  </AccordionDetails>
                </Accordion>
                <Accordion>
                  <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel2-content"
                    id="panel2-header"
                  >
                    <Typography sx={{ fontWeight: 'bold' }}>How can I contact a school directly?</Typography>
                  </AccordionSummary>
                  <AccordionDetails>
                    <Typography sx={{ color: '#606060' }}>
                      You can contact a school by filling out a “Contact Us” form. This form can be found to the right of both the institute and education program profiles and also at the bottom of these profiles.
                    </Typography>
                  </AccordionDetails>
                </Accordion>
              </div>
            </TabPanel>
            <TabPanel value="1">Education Content</TabPanel>
            <TabPanel value="2">Availability Content</TabPanel>
            <TabPanel value="3">Reviews Content</TabPanel>
          </TabContext>
        </Box>
      </div>
    </Layout>
  );
}
