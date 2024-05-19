'use client'
import axios from 'axios';
import React, { useContext, useEffect, useState } from 'react'
import '../../dashboard/dashboard.css'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons'
import Link from 'next/link';
import { UserContext } from '@/context/user/User';
import { FormControl, InputLabel, MenuItem, Pagination, Select, Stack } from '@mui/material';


export default function AccreditCourses() {
  const {userToken, setUserToken, userData}=useContext(UserContext);

  const [accreditCourses, setAccreditCourses] = useState([]);
  let[loader,setLoader] = useState(false);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalPages, setTotalPages] = useState(0);

  const fetchCoursesForAccredit = async (pageNum = pageNumber, pageSizeNum = pageSize)=> {
    if(userData){
    try{
    const { data } = await axios.get(`http://localhost:5134/api/CourseContraller/GetAllCoursesForAccredit?pageNumber=${pageNum}&pageSize=${pageSize}`);
    console.log(data);
    setAccreditCourses(data.result.items);
    setTotalPages(data.result.totalPages);
  }
    catch(error){
      console.log(error);
    }
  }
  };

  const accreditCourse = async (courseId , Status) => {
    //setLoader(true);
    console.log(courseId);
    if(userData){
    try{
    const { data } = await axios.patch(`http://localhost:5134/api/CourseContraller/accreditCourse?courseId=${courseId}&Status=${Status}`,
  );
  if(data.isSuccess){
    console.log(data);
    //setLoader(false);
    fetchCoursesForAccredit();
  }
  }
    catch(error){
      console.log(error);
    }}
  };

  useEffect(() => {
    fetchCoursesForAccredit();
  }, [accreditCourse,userData, pageNumber, pageSize]);

  const handlePageSizeChange = (event) => {
    setPageSize(event.target.value);
    setPageNumber(1); // Reset to the first page when page size changes
  };
  
  const handlePageChange = (event, value) => {
    setPageNumber(value);
  };

if(loader){
  return <p>Loading ...</p>
}
  const [searchTerm, setSearchTerm] = useState('');
  const handleSearch = (event) => {
    setSearchTerm(event.target.value);
  };

const filteredAccreditCourses = Array.isArray(accreditCourses) ? accreditCourses.filter((course) => {
  const matchesSearchTerm = Object.values(course).some(
    (value) =>
      typeof value === "string" &&
      value.toLowerCase().includes(searchTerm.toLowerCase())
  );
  return matchesSearchTerm;
}) : [];


  return (
    <>
      <div className="filter py-2 text-end">
        <nav className="navbar">
          <div className="container justify-content-end">
            <form className="d-flex" role="search">
              <input
                className="form-control me-2"
                type="search"
                placeholder="Search"
                aria-label="Search"
                value={searchTerm}
                onChange={handleSearch}
              />
              <FormControl fullWidth className="w-50">
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
              <div className="icons d-flex gap-2 pt-3">
                <div className="dropdown">
                  <button
                    className="dropdown-toggle border-0 bg-white edit-pen"
                    type="button"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                  >
                    <FontAwesomeIcon icon={faFilter} />
                  </button>
                  <ul className="dropdown-menu">
                    <li>uuu</li>
                  </ul>
                </div>
                <FontAwesomeIcon icon={faArrowUpFromBracket} />
              </div>
            </form>
          </div>
        </nav>
      </div>
     

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
      <table className="table">
        <thead>
          <tr>
            <th scope="col">ID</th>
            <th scope="col">Name</th>
            <th scope="col">Price</th>
            <th scope="col">Category</th>
            <th scope="col">Start Date</th>
            <th scope="col">End Date</th>
            <th scope="col">Option</th>
          </tr>
        </thead>
        <tbody>
          {filteredAccreditCourses.length ? (
            filteredAccreditCourses.map((course) => (
              <tr key={course.id} /*className={course.status === 'accredit' ? 'accredit-row' : course.status === 'reject' ? 'reject-row' : ''}*/>
                {console.log(course.id)}
                {console.log(course.imageUrl)}
                <th scope="row">{course.id}</th>
                <td>{course.name}</td>
                <td>{course.price}</td>
                <td>{course.category}</td>
                <td>{course.startDate}</td>
                <td>{course.endDate}</td>
                <td className="d-flex gap-1">
                  {/* <Link href={"/Profile"}>
                    <button type="button" className="border-0 bg-white ">
                      <FontAwesomeIcon icon={faEye} className="edit-pen" />
                    </button>
                  </Link> */}
                  <button type="button" className="btn accredit" onClick={()=>accreditCourse(course.id,'accredit')} disabled = {course.status == 'accredit' || course.status == 'reject'} >Accredit</button>  
                {/* <Link href='/dashboard' className='text-decoration-none acc'>Accredit </Link> */}
                <button type="button" className="btn accredit" onClick={()=>accreditCourse(course.id,"reject")} disabled = {course.status == 'accredit' || course.status == 'reject'} >Reject</button>

                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="7">No Courses</td>
            </tr>
          )}
        </tbody>
      </table>
    </>
  );
}
