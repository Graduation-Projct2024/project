'use client'
import * as React from 'react';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { StaticDateTimePicker } from '@mui/x-date-pickers/StaticDateTimePicker';
import Layout from '../studentLayout/Layout.jsx'

export default function page() {
  return (
    <Layout title='Calender'>
       <LocalizationProvider dateAdapter={AdapterDayjs}>
      <StaticDateTimePicker disablePast='true'   localeText={{ dateTimePickerToolbarTitle: 'Choose the date and time' }} sx={{width:'85%',}} orientation="portrait" />
    </LocalizationProvider>
    </Layout>
  )
}
