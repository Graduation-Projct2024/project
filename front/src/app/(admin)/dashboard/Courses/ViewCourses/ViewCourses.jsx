"use client";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { useContext, useEffect, useState } from "react";
import {
  faArrowUpFromBracket,
  faEye,
  faFileCsv,
  faFilter,
  faPen,
} from "@fortawesome/free-solid-svg-icons";
import Link from "next/link";
import axios from "axios";
import { UserContext } from "@/context/user/User";
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, Pagination, Stack, useMediaQuery, useTheme, MenuItem, FormControl, Select, InputLabel, Tooltip } from "@mui/material";
import EditCourse from "../EditCourse/[courseId]/page";

export default function ViewCourses() {
  const { userToken, setUserToken, userData } = useContext(UserContext);

  const [openUpdate, setOpenUpdate] = React.useState(false);
  const theme = useTheme();
  const fullScreen = useMediaQuery(theme.breakpoints.down('md'));
  const [courseId, setCourseId] = useState(null);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalPages, setTotalPages] = useState(0);
  

const handleClickOpenUpdate = (id) => {
  setCourseId(id);
    console.log(id)
    setOpenUpdate(true);
};
const handleCloseUpdate = () => {
  setOpenUpdate(false);
};

  const [courses, setCourses] = useState([]);
  const ExportAllDataToPdf =async()=>{
    if(userData){
      try{
        const data = JSON.stringify(courses);
        const response = await axios.get(
          `https://localhost:7116/api/Reports/export-all-Data-To-PDF?data=course`,
          {
            headers: { Authorization: `Bearer ${userToken}`, 'Content-Type': 'application/json' },
            responseType: 'blob'
          }
        );
  
        // Check the response in the console
        console.log('Response Headers:', response.headers);
        console.log('Response Data:', response.data);
        const url = window.URL.createObjectURL(new Blob([response.data], { type: 'application/pdf' }));
        const link = document.createElement('a');
        link.href = url;
        link.setAttribute('download', 'Courses.pdf');
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        console.log(response)
      }catch(error){
        console.log(error)
      }
    }
  }

  const ExportAllDataToCSV =async()=>{
    if(userData){
      try{
        const data = JSON.stringify(courses);
        const response = await axios.get(
          `https://localhost:7116/api/Reports/export-all-data-to-excel?data=course`,
          {
            headers: { Authorization: `Bearer ${userToken}`, 'Content-Type': 'application/json' },
            responseType: 'blob'
          }
        );
  
        // Check the response in the console
        console.log('Response Headers:', response.headers);
        console.log('Response Data:', response.data);
        const url = window.URL.createObjectURL(new Blob([response.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' }));
        const link = document.createElement('a');
        link.href = url;
        link.setAttribute('download', 'courses.xlsx');
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        console.log(response)
      }catch(error){
        console.log(error)
      }
    }
  }


  // const fetchCourses = async () => {
  //   if (userData) {
  //     try {
  //       const { data } = await axios.get(
  //         `http://localhost:5134/api/CourseContraller?pageNumber=1&pageSize=10`
  //       );
  //       setTotalPages(data.result.totalPages);
  //       //console.log(data);
  //       setCourses(data.result.items);
  //     } catch (error) {
  //       console.log(error);
  //     }
  //   }
  // };
  const fetchCourses = async (pageNum = pageNumber, pageSizeNum = pageSize) => {
    if (userData) {
      try {
        const {data} = await axios.get(`https://localhost:7116/api/CourseContraller/GetAllAccreditCourses?pageNumber=${pageNum}&pageSize=${pageSizeNum}`, {
          headers: {
            Authorization: `Bearer ${userData.token}`,  // Assuming you have a token for authorization
          },
        });
        setCourses(data.result.items);
        setTotalPages(data.result.totalPages);
      } catch (error) {
        console.error('Failed to fetch courses:', error);
      }
    }
  };

//   useEffect(() => {
//     fetchCourses();
//   }, [courses,pageNumber, pageSize,userData]);
// //  console.log(courses)
// const handleChange = (event, value) => {
//   setPageNumber(value);
// };
useEffect(() => {
  fetchCourses();
}, [courses,userData, pageNumber, pageSize]);  // Fetch courses on mount and when page or size changes

const handlePageSizeChange = (event) => {
  setPageSize(event.target.value);
  setPageNumber(1); // Reset to the first page when page size changes
};

const handlePageChange = (event, value) => {
  setPageNumber(value);
};
  const [searchTerm, setSearchTerm] = useState("");

  const handleSearch = (event) => {
    setSearchTerm(event.target.value);
  };

  const filteredCourses = Array.isArray(courses) ? courses.filter((course) => {
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
            <form className="d-flex gap-2" role="search">
            
              <input
                className="form-control me-1"
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
              <div className="icons d-flex gap-2 pt-2">
                <div className="dropdown">
                  <button
                    className="dropdown-toggle border-0 bg-white edit-pen"
                    type="button"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                  >
                    <FontAwesomeIcon icon={faFilter} />
                  </button>
                  <ul className="dropdown-menu"></ul>
                </div>
                
              </div>
            </form>
            <Tooltip title="Convert courses into pdf" placement="top">
            <button className='border-0 bg-transparent edit-pen' onClick={ExportAllDataToPdf}>
                <FontAwesomeIcon icon={faArrowUpFromBracket} className='pb-2'/>
                </button>
                </Tooltip>
                <Tooltip title="Convert courses into Excel file" placement="top">
        <button className='border-0 bg-transparent edit-pen' onClick={ExportAllDataToCSV}>
              <FontAwesomeIcon icon={faFileCsv} className="pb-2"/>
            </button>
            </Tooltip>
          </div>
        </nav>
      </div>



      <table className="table ">
        <thead>
          <tr>
            <th scope="col">ID</th>
            <th scope="col">Name</th>
            <th scope="col">Price</th>
            <th scope="col">Category</th>
            <th scope="col">Status</th>
            <th scope="col">Start Date</th>
            <th scope="col">Instructor</th>
            <th scope="col">Option</th>
          </tr>
        </thead>
        <tbody>
          {filteredCourses.length ? (
            filteredCourses.map((course,index) => (
              <tr key={course.id}>
                <th scope="row">{++index}</th>
                <td>{course.name}</td>
                <td>{course.price}</td>
                <td>{course.category}</td>
                <td>{course.status}</td>
                <td>{course.startDate}</td>
                <td>{course.instructorName}</td>
                <td className="d-flex gap-1">
                <Tooltip title="Edit course" placement="top">
                <button className="border-0 bg-white" type="button" onClick={() => handleClickOpenUpdate(course.id)}>
                <FontAwesomeIcon icon={faPen} className="edit-pen" />
            </button>
            </Tooltip>

            <Dialog
        fullScreen={fullScreen}
        open={openUpdate && courseId === course.id}
        onClose={handleCloseUpdate}
        aria-labelledby="responsive-dialog-title"
        sx={{
          "& .MuiDialog-container": {
            "& .MuiPaper-root": {
              width: "100%",
              maxWidth: "600px!important",  
              height: "400px!important",            },
          },
          
        }}
        >
          <DialogTitle id="responsive-dialog-title" className='primaryColor fw-bold' >
          {"Edit Course"}
        </DialogTitle>

        <DialogContent>
        <Stack
   direction="row"
   spacing={1}
   sx={{ justifyContent: 'center',  alignContent: 'center'}}
    >
      <EditCourse courseId={course.id} startDate = {course.startDate} Deadline={course.deadline} InstructorId={course.instructorId} limitNumberOfStudnet={course.limitNumberOfStudnet} description={course.description} image={course.imageUrl} setOpenUpdate={setOpenUpdate} />
     </Stack>
        </DialogContent>
        <DialogActions>
         
         <Button onClick={handleCloseUpdate} autoFocus>
           Cancle
         </Button>
       </DialogActions>
        </Dialog>
        <Tooltip title="View Course details" placement="top">
                  <Link href={`CourseDetails/${course.id}`}>
                    <button
                      type="button"
                      className="edit-pen border-0 bg-white "
                    >
                      <FontAwesomeIcon icon={faEye} />
                    </button>
                  </Link>
                  </Tooltip>
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
    </>
  );
}