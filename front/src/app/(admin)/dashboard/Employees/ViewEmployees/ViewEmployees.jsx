'use client'
import React, { useContext, useEffect, useState } from 'react'
import CreateEmployee from '../CreateEmployee/CreateEmployee';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowUpFromBracket, faBook, faEye, faFilter, faPen } from '@fortawesome/free-solid-svg-icons'
import Link from 'next/link';
import axios from 'axios';
import UpdateEmployee from '../UpdateEmployee/[id]/page';
import { UserContext } from '@/context/user/User';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, Pagination, Stack, useMediaQuery, useTheme, MenuItem, FormControl, Select, InputLabel, Box, Tooltip } from "@mui/material";
import '../../../dashboard/dashboard.css'
import '../../../../../../node_modules/bootstrap/dist/js/bootstrap.bundle'

export default function ViewEmployees() {

      const {userToken, setUserToken, userData}=useContext(UserContext);
      const [employees, setEmployees] = useState([]);
      // const [loading,setLoading] = useState(true);
      const [open, setOpen] = React.useState(false);
      const [openUpdate, setOpenUpdate] = React.useState(false);
      const [pageNumber, setPageNumber] = useState(1);
      const [pageSize, setPageSize] = useState(10);
      const [totalPages, setTotalPages] = useState(0);

      const theme = useTheme();
      const fullScreen = useMediaQuery(theme.breakpoints.down('md'));
      const [employeeId, setEmployeeId] = useState(null);

    const handleClickOpenUpdate = (id) => {
        setEmployeeId(id);
        console.log(id)
        setOpenUpdate(true);
    };
    const handleCloseUpdate = () => {
      setOpenUpdate(false);
    };

      const handleClickOpen = () => {
        setOpen(true);
      };
      const handleClose = () => {
        setOpen(false);
      };
      const fetchEmployees = async (pageNum = pageNumber, pageSizeNum = pageSize)  => {
        if(userData){
        try{
        const { data } = await axios.get(`http://localhost:5134/api/Employee/GetAllEmployee?pageNumber=${pageNum}&pageSize=${pageSize}`);
        // setLoading(false)
      //  console.log(data);
        setEmployees(data.result.items);
        setTotalPages(data.result.totalPages);

      }
        catch(error){
       //   console.log(error);
        }
      }
      };

      // useEffect(() => {
      //   fetchEmployees();
      // }, [employees,userData]);

      useEffect(() => {
        fetchEmployees();
      }, [employees,userData, pageNumber, pageSize]);  // Fetch courses on mount and when page or size changes
      
      const handlePageSizeChange = (event) => {
        setPageSize(event.target.value);
        setPageNumber(1); // Reset to the first page when page size changes
      };
      
      const handlePageChange = (event, value) => {
        setPageNumber(value);
      };
      const [searchTerm, setSearchTerm] = useState('');
      const [selectedRole, setSelectedRole] = useState(null);
    
      const handleSearch = (event) => {
        setSearchTerm(event.target.value);
      };
    
      const handleRoleFilter = (type) => {
        setSelectedRole(type);
      };


      const filteredEmployees = employees.filter((employee) => {
        const matchesSearchTerm =
        Object.values(employee).some(
            (value) =>
            typeof value === 'string' && value.toLowerCase().includes(searchTerm.toLowerCase())
        );

        const matchesRole = selectedRole ? employee.type.toLowerCase() === selectedRole.toLowerCase() : true;

        return matchesSearchTerm && matchesRole;
  });
// if(loading){
//   return <h2>Loading....</h2>
// }
  return (
    <>
      <div className="filter py-2 text-end">
        <nav className="navbar">
          <div className="container justify-content-end">
            <form className="d-flex gap-1" role="search">
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
                   <Tooltip title="Filter by Role" placement="top">
                  <button
                    className="dropdown-toggle border-0 bg-white edit-pen"
                    type="button"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                  >
                    <FontAwesomeIcon icon={faFilter} />
                  </button>
                  </Tooltip>
                  <ul className="dropdown-menu">
                    <li>
                      <a
                        className="dropdown-item"
                        href="#"
                        onClick={() => handleRoleFilter("")}
                      >
                        All
                      </a>
                    </li>
                    <li>
                      <a
                        className="dropdown-item"
                        href="#"
                        onClick={() => handleRoleFilter("subadmin")}
                      >
                        SubAdmin
                      </a>
                    </li>
                    <li>
                      <a
                        className="dropdown-item"
                        href="#"
                        onClick={() => handleRoleFilter("main-subadmin")}
                      >
                        Main-SubAdmin
                      </a>
                    </li>
                    <li>
                      <a
                        className="dropdown-item"
                        href="#"
                        onClick={() => handleRoleFilter("instructor")}
                      >
                        Instructor
                      </a>
                    </li>
                  </ul>
                </div>
                
                <FontAwesomeIcon icon={faArrowUpFromBracket} className='pb-1'/>
              </div>
            </form>

            {/* <button type="button" className="btn btn-primary ms-2 addEmp" data-bs-toggle="modal" data-bs-target="#staticBackdrop2">
              <span>+ Add new</span>
            </button> */}
               <Box
        sx={{
          display: 'flex',
          justifyContent: 'flex-end',
          p: 1,
          mr: 6,
        }}
      >
<Button sx={{px:2,m:0.5}} variant="contained" className='primaryBg' startIcon={<AddCircleOutlineIcon />} onClick={handleClickOpen}>
  Add New
</Button>
      </Box>


          </div>
        </nav>
        {/* <div className="modal fade" id="exampleModal" tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true"> */}
        {/* <div
          className="modal fade"
          id="staticBackdrop2"
          data-bs-backdrop="static"
          data-bs-keyboard="false"
          tabIndex="-1"
          aria-labelledby="staticBackdrop2Label"
          aria-hidden="true"
        >
          <div className="modal-dialog modal-dialog-centered modal-lg">
            <div className="modal-content row justify-content-center">
              <div className="modal-body text-center ">
                <h2 className="fs-1">CREATE ACCOUNT</h2>
                <div className="row">
                  <CreateEmployee />
                </div>
              </div>
            </div>
          </div>
        </div> */}
      <Dialog
        fullScreen={fullScreen}
        open={open}
        onClose={handleClose}
        aria-labelledby="responsive-dialog-title"
        sx={{
          "& .MuiDialog-container": {
            "& .MuiPaper-root": {
              width: "100%",
              maxWidth: "600px!important",  
              height: "500px!important",            },
          },
          
        }}
        >
          <DialogTitle id="responsive-dialog-title" className='primaryColor fw-bold' >
          {"Add New Employee"}
        </DialogTitle>

        <DialogContent>
        <Stack
   direction="row"
   spacing={1}
   sx={{ justifyContent: 'center',  alignContent: 'center'}}
    >
      <CreateEmployee setOpen={setOpen}/>
     </Stack>
        </DialogContent>
        <DialogActions>
         
         <Button onClick={handleClose} autoFocus>
           Cancle
         </Button>
       </DialogActions>
        </Dialog>

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
            <th scope="col">Email</th>
            <th scope="col">Role</th>
            <th scope="col">Gender</th>
            <th scope="col">Phone number</th>
            <th scope="col">Address</th>
            <th scope="col">Option</th>
          </tr>
        </thead>
        <tbody>
          {filteredEmployees.length ? (
            filteredEmployees.map((employee) => (
              <tr key={employee.id}>
                <th scope="row">{employee.id}</th>
                <td>
                  {employee.fName} {employee.lName}
                </td>
                <td>{employee.email}</td>
                <td>{employee.type}</td>
                <td>{employee.gender}</td>
                <td>{employee.phoneNumber}</td>
                <td>{employee.address}</td>

                <td className="d-flex gap-1">

                <Tooltip title="Edit Employee" placement="top">
                <button className="border-0 bg-white"  type="button" onClick={() => handleClickOpenUpdate(employee.id)}>
                <FontAwesomeIcon icon={faPen} className="edit-pen" />
            </button>
            </Tooltip>
                  {/* <Button sx={{px:2,m:0.5}} variant="contained" className='bg-transparent border-0 '  onClick={handleClickOpen}>
                  <FontAwesomeIcon icon={faPen} className="edit-pen" />
                  </Button> */}
                 <Dialog
        fullScreen={fullScreen}
        open={openUpdate}
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
          {"Update Employee"}
        </DialogTitle>

        <DialogContent>
        <Stack
   direction="row"
   spacing={1}
   sx={{ justifyContent: 'center',  alignContent: 'center'}}
    >
      <UpdateEmployee id = {employee.id}  fName = {employee.fName} lName = {employee.lName} email = {employee.email} gender = {employee.gender} phoneNumber = {employee.phoneNumber} address = {employee.address}/>
     </Stack>
        </DialogContent>
        <DialogActions>
         
         <Button onClick={handleCloseUpdate} autoFocus>
           Cancle
         </Button>
       </DialogActions>
        </Dialog>
                  {/* <div className="modal fade" id={`exampleModal2-${employee.id}`} tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div className="modal-dialog modal-dialog-centered modal-lg">
                      <div className="modal-content row justify-content-center">
                        <div className="modal-body text-center ">
                          <h2>UPDATE ACCOUNT</h2>
                          <div className="row">
                            <UpdateEmployee id = {employee.id}  fName = {employee.fName} lName = {employee.lName} email = {employee.email} gender = {employee.gender} phoneNumber = {employee.phoneNumber} address = {employee.address} />
                          </div>
                        </div>
                      </div>
                    </div>
                  </div> */}

                  <Link href={`/Profile/${employee.id}`}>
                  <Tooltip title="View Profile" placement="top">
                    <button type="button" className="border-0 bg-white " >
                      <FontAwesomeIcon icon={faEye} className="edit-pen" />
                    </button>
                    </Tooltip>
                  </Link>
                  {/* {userData && employee.type == "instructor" && (
                    <Link href={`/InstructorCourses/${employee.id}`}>
                      <button type="button" className="border-0 bg-white ">
                        <FontAwesomeIcon icon={faBook} className="edit-pen" />
                      </button>
                    </Link>
                  )} */}
                   {userData && employee.type == "instructor" && (
                    <Link 
                    href={{
                      pathname: `/InstructorCourses/${employee.id}`,
                      query: { fName: employee.fName, lName: employee.lName }
                    }}>
                      <Tooltip title="View Instructor Courses" placement="top">
                    <button type="button" className="border-0 bg-white ">
                      <FontAwesomeIcon icon={faBook} className="edit-pen" />
                    </button>
                    </Tooltip>
                  </Link>
                  
                  )}
                  
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="7">No employees</td>
            </tr>
          )}
        </tbody>
      </table>

      {/* <div className="modal fade" id="exampleModal3" tabIndex="-1" aria-labelledby="exampleModa3Label" aria-hidden="true">
          <div className="modal-dialog modal-dialog-centered modal-lg">
            <div className="modal-content row justify-content-center">
              <div className="modal-body text-center ">
                <h2>Courses for this Instructor:</h2>
                  <div className="row">
                    <InstructorCourses/>
                  </div>
              </div>
            </div>
          </div>
        </div> */}
    </>
  );
}
