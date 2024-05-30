'use client'
import React, { useContext, useEffect, useState } from 'react'
import axios from 'axios';
import Typography from '@mui/material/Typography';
import ArrowCircleRightIcon from '@mui/icons-material/ArrowCircleRight';
import Link from '@mui/material/Link';
import './style.css'
import Layout from '../studentLayout/Layout.jsx';
import { UserContext } from '../../../context/user/User.jsx';
import { Box, FormControl, InputLabel, MenuItem, Pagination, Select, Stack } from '@mui/material';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell, { tableCellClasses } from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { styled } from '@mui/material/styles';

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

export default function page() {
  const { userToken, userId } = useContext(UserContext);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalPages, setTotalPages] = useState(0);
  const [lectures, setLectures] = useState([]);
  const [loading, setLoading] = useState(true);
  const getLectures = async (pageNum = pageNumber, pageSizeNum = pageSize) => {
    if (userId) {
      try {
        const response = await axios.get(
          `https://localhost:7116/api/Lectures/GetAllConsultations?studentId=${userId}&pageNumber=${pageNum}&pageSize=${pageSizeNum}`,
          { headers: { Authorization: `Bearer ${userToken}` } }
        );
        console.log(response);
        setLectures(response.data.result.items);
        setTotalPages(response.data.result.totalPages);
      } catch (error) {
        console.error('Error fetching lectures:', error);
      } finally {
        setLoading(false);
      }
    }
  };

  useEffect(() => {
    getLectures();
  }, [userId, userToken, pageNumber, pageSize]);

  const handlePageSizeChange = (event) => {
    setPageSize(event.target.value);
    setPageNumber(1); // Reset to the first page when page size changes
  };

  const handlePageChange = (event, value) => {
    setPageNumber(value);
  };

  if (loading) {
    return <div>Loading...</div>;
  }
  
  return (
    <Layout title='My Lectures'>
      <Stack
      direction="row"
      justifyContent="flex-end"
      alignItems="center"
      spacing={2}
    >
      <FormControl fullWidth className="page-Size mt-5 me-5">
        <InputLabel id="page-size-select-label">Page Size</InputLabel>
        <Select
          className="justify-content-center"
          labelId="page-size-select-label"
          id="page-size-select"
          value={pageSize}
          label="Page Size"
          onChange={handlePageSizeChange}
        >
          <MenuItem value={5}>5</MenuItem>
          <MenuItem value={10}>10</MenuItem>
          <MenuItem value={20}>20</MenuItem>
          <MenuItem value={50}>50</MenuItem>
        </Select>
      </FormControl>
    </Stack>
      <TableContainer component={Paper} sx={{ width: '90%', mt: 5 }}>
        <Table sx={{ minWidth: 700 }} aria-label="customized table">
          <TableHead>
            <TableRow>
              <StyledTableCell>Lecture title</StyledTableCell>
              <StyledTableCell align="center">Date</StyledTableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {lectures.length ? (
              lectures.map((lecture) => (
                <StyledTableRow key={lecture.consultationId}>
                  <StyledTableCell component="th" scope="row">
                  <Link href={`myLectures/${lecture.consultationId}`}>   {lecture.name}</Link>
                  </StyledTableCell>
                  <StyledTableCell align="center">{lecture.date}</StyledTableCell>
                </StyledTableRow>
              ))
            ) : (
              <StyledTableRow>
                <StyledTableCell colSpan={2} align="center">
                  No Lectures Yet.
                </StyledTableCell>
              </StyledTableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
      <Stack spacing={2} sx={{ width: '100%', maxWidth: 500, margin: '0 auto' }}>
        <Pagination
          className="pb-3"
          count={totalPages}
          page={pageNumber}
          onChange={handlePageChange}
          variant="outlined"
          color="secondary"
          showFirstButton
          showLastButton
        />
      </Stack>
</Layout>

  )
}
