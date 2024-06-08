'use client'
import React, { useContext, useEffect, useState } from 'react'
import CreateEmployee from '../CreateEmployee/CreateEmployee';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowUpFromBracket, faBook, faEye, faFileCsv, faFilter, faPen, faPeopleArrows } from '@fortawesome/free-solid-svg-icons'
import Link from 'next/link';
import axios from 'axios';
import UpdateEmployee from '../UpdateEmployee/[id]/page';
import { UserContext } from '@/context/user/User';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, Pagination, Stack, useMediaQuery, useTheme, MenuItem, FormControl, Select, InputLabel, Box, Tooltip, CircularProgress } from "@mui/material";
import '../../../dashboard/dashboard.css'
import '../../../../../../node_modules/bootstrap/dist/js/bootstrap.bundle.min.js'
import ChangeRole from '../ChangeRole/[userId]/ChangeRole';
import CategoryIcon from '@mui/icons-material/Category';
import '../../loading.css'

export default function ViewEmployees() {

      const {userToken, setUserToken, userData}=useContext(UserContext);
      const [employees, setEmployees] = useState([]);
      const [loading,setLoading] = useState(true);
      const [open, setOpen] = React.useState(false);
      const [openUpdate, setOpenUpdate] = React.useState(false);
      const [openChange, setOpenChange] = React.useState(false);
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
    const handleClickOpenChange = (id) => {
      setEmployeeId(id);
      console.log(id)
      setOpenChange(true);
  };
  const handleCloseChange = () => {
    setOpenChange(false);
  };

      const handleClickOpen = () => {
        setOpen(true);
      };
      const handleClose = () => {
        setOpen(false);
      };
      // console.log(employees)
      const fetchEmployees = async (pageNum = pageNumber, pageSizeNum = pageSize)  => {
        if(userData){
          setLoading(true);
        try{
        const { data } = await axios.get(`https://localhost:7116/api/Employee/GetAllEmployee?pageNumber=${pageNum}&pageSize=${pageSize}`);
        // setLoading(false)
      //  console.log(data);
        setEmployees(data.result.items);
        setTotalPages(data.result.totalPages);

      }
        catch(error){
       //   console.log(error);
        }
        finally {
          setLoading(false);
        }
      }
      };

      const ExportAllDataToPdf =async()=>{
        if(userData){
          try{
            const data = JSON.stringify(employees);
            const response = await axios.get(
              `https://localhost:7116/api/Reports/export-all-Data-To-PDF?data=employee`,
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
            link.setAttribute('download', 'employees.pdf');
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
            const data = JSON.stringify(employees);
            const response = await axios.get(
              `https://localhost:7116/api/Reports/export-all-data-to-excel?data=employee`,
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
            link.setAttribute('download', 'employees.xlsx');
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
            console.log(response)
          }catch(error){
            console.log(error)
          }
        }
      }

      // useEffect(() => {
      //   fetchEmployees();
      // }, [employees,userData]);

      useEffect(() => {
        fetchEmployees();
      }, [userData, pageNumber, pageSize]);  // Fetch courses on mount and when page or size changes
      
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

  return (
    
    <>
    {loading ? (
        <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '80vh' }}>
          {/* <CircularProgress /> */}
          {/* <div className='loading bg-white position-fixed vh-100 w-100 d-flex justify-content-center align-items-center z-3'> */}
      <span class="loader"></span>
    {/* </div> */}
        </Box>
        
      ) : (

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
                
              </div>
            </form>
            <Tooltip title="Convert Employees into pdf" placement="top">
            <button className='border-0 bg-transparent edit-pen' onClick={ExportAllDataToPdf}>
                <FontAwesomeIcon icon={faArrowUpFromBracket} className=''/>
                </button>
                </Tooltip>
                <Tooltip title="Convert Employees into Excel" placement="top">
            <button className='border-0 bg-transparent edit-pen ps-2' onClick={ExportAllDataToCSV}>
                  <FontAwesomeIcon icon={faFileCsv} />
                </button>
                </Tooltip>
                

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
<Button sx={{px:2,m:0.5}} variant="contained" className='primaryBg' startIcon={<AddCircleOutlineIcon  className='addIcon'/>} onClick={handleClickOpen}>
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
              maxWidth: "700px!important",  
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


      {loading ? (
        <Box display="flex" justifyContent="center" alignItems="center" height="50vh">
          <CircularProgress />
        </Box>
      ) : (
      <table className="table">
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">Name</th>
            <th scope="col">Email</th>
            <th scope="col">Role</th>
            <th scope="col">Gender</th>
            <th scope="col">Phone No.</th>
            <th scope="col">Address</th>
            <th scope="col">Option</th>
          </tr>
        </thead>
        <tbody>
          {filteredEmployees.length ? (
            filteredEmployees.map((employee,index) => (
              <tr key={employee.id}>
                <th scope="row">{++index}</th>
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
        open={openUpdate && employeeId === employee.id}
        onClose={handleCloseUpdate}
        aria-labelledby="responsive-dialog-title"
        sx={{
          "& .MuiDialog-container": {
            "& .MuiPaper-root": {
              width: "100%",
              maxWidth: "700px!important",  
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
      <UpdateEmployee id = {employee.id}  fName = {employee.fName} lName = {employee.lName} email = {employee.email} gender = {employee.gender} phoneNumber = {employee.phoneNumber} address = {employee.address} setOpenUpdate={setOpenUpdate}/>
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
 {userData && (employee.type == "subadmin"|| employee.type == "main-subadmin")  && (
<Tooltip title="Swap roles" placement="top">
                <button className="border-0 bg-white"  type="button" onClick={() => handleClickOpenChange(employee.id)}>
                <FontAwesomeIcon icon={faPeopleArrows} className='edit-pen'/>
            </button>
            </Tooltip>)}
                  
            <Dialog
        fullScreen={fullScreen}
        open={openChange && employeeId === employee.id}
        onClose={handleCloseChange}
        aria-labelledby="responsive-dialog-title"
        sx={{
          "& .MuiDialog-container": {
            "& .MuiPaper-root": {
              width: "100%",
              maxWidth: "450px!important",  
              height: "280px!important",            },
          },
          
        }}
        >
          <DialogTitle id="responsive-dialog-title" className='primaryColor fw-bold text-center' >
          {"Change Role"}
        </DialogTitle>

        <DialogContent>
        <Stack
   direction="row"
   spacing={1}
   sx={{ justifyContent: 'center',  alignContent: 'center'}}
    >
      <ChangeRole userId = {employee.id} role={employee.type} setOpenChange={setOpenChange} />
     </Stack>
        </DialogContent>
        <DialogActions>
         
         <Button onClick={handleCloseChange} autoFocus>
           Cancle
         </Button>
       </DialogActions>
                </Dialog>
                {userData && employee.type == "instructor" && (
                    <Link 
                    href={{
                      pathname: `/InstructorSkills/${employee.id}`,
                   }}>

                      <Tooltip title="View Instructor skills" placement="top">
                    <button type="button" className="border-0 bg-white ">
                     <CategoryIcon className='edit-pen'/>
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
      </table>)}
      
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
    </>)}
    </>
  );
}

// 'use client'
// import React, { useContext, useEffect, useState } from 'react';
// import CreateEmployee from '../CreateEmployee/CreateEmployee';
// import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
// import { faArrowUpFromBracket, faBook, faEye, faFileCsv, faFilter, faPen, faPeopleArrows } from '@fortawesome/free-solid-svg-icons';
// import Link from 'next/link';
// import axios from 'axios';
// import UpdateEmployee from '../UpdateEmployee/[id]/page';
// import { UserContext } from '@/context/user/User';
// import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
// import { Button, Dialog, DialogActions, DialogContent, DialogTitle, Pagination, Stack, useMediaQuery, useTheme, MenuItem, FormControl, Select, InputLabel, Box, Tooltip, CircularProgress } from "@mui/material";
// import '../../../dashboard/dashboard.css';
// import '../../../../../../node_modules/bootstrap/dist/js/bootstrap.bundle.min.js';
// import ChangeRole from '../ChangeRole/[userId]/ChangeRole';
// import CategoryIcon from '@mui/icons-material/Category';

// export default function ViewEmployees() {
//   const { userToken, userData } = useContext(UserContext);
//   const [employees, setEmployees] = useState([]);
//   const [loading, setLoading] = useState(true);
//   const [open, setOpen] = useState(false);
//   const [openUpdate, setOpenUpdate] = useState(false);
//   const [openChange, setOpenChange] = useState(false);
//   const [pageNumber, setPageNumber] = useState(1);
//   const [pageSize, setPageSize] = useState(10);
//   const [totalPages, setTotalPages] = useState(0);
//   const theme = useTheme();
//   const fullScreen = useMediaQuery(theme.breakpoints.down('md'));
//   const [employeeId, setEmployeeId] = useState(null);
//   const [searchTerm, setSearchTerm] = useState('');
//   const [selectedRole, setSelectedRole] = useState(null);

//   const handleClickOpenUpdate = (id) => {
//     setEmployeeId(id);
//     setOpenUpdate(true);
//   };

//   const handleCloseUpdate = () => {
//     setOpenUpdate(false);
//   };

//   const handleClickOpenChange = (id) => {
//     setEmployeeId(id);
//     setOpenChange(true);
//   };

//   const handleCloseChange = () => {
//     setOpenChange(false);
//   };

//   const handleClickOpen = () => {
//     setOpen(true);
//   };

//   const handleClose = () => {
//     setOpen(false);
//   };

//   const fetchEmployees = async (pageNum = pageNumber, pageSizeNum = pageSize) => {
//     if (userData) {
//       setLoading(true);
//       try {
//         const { data } = await axios.get(`https://localhost:7116/api/Employee/GetAllEmployee?pageNumber=${pageNum}&pageSize=${pageSizeNum}`);
//         setEmployees(data.result.items);
//         setTotalPages(data.result.totalPages);
//       } catch (error) {
//         console.log(error);
//       } finally {
//         setLoading(false);
//       }
//     }
//   };

//   const ExportAllDataToPdf = async () => {
//     if (userData) {
//       try {
//         const response = await axios.get(
//           `https://localhost:7116/api/Reports/export-all-Data-To-PDF?data=employee`,
//           {
//             headers: { Authorization: `Bearer ${userToken}`, 'Content-Type': 'application/json' },
//             responseType: 'blob'
//           }
//         );

//         const url = window.URL.createObjectURL(new Blob([response.data], { type: 'application/pdf' }));
//         const link = document.createElement('a');
//         link.href = url;
//         link.setAttribute('download', 'employees.pdf');
//         document.body.appendChild(link);
//         link.click();
//         document.body.removeChild(link);
//       } catch (error) {
//         console.log(error);
//       }
//     }
//   };

//   const ExportAllDataToCSV = async () => {
//     if (userData) {
//       try {
//         const response = await axios.get(
//           `https://localhost:7116/api/Reports/export-all-data-to-excel?data=employee`,
//           {
//             headers: { Authorization: `Bearer ${userToken}`, 'Content-Type': 'application/json' },
//             responseType: 'blob'
//           }
//         );

//         const url = window.URL.createObjectURL(new Blob([response.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' }));
//         const link = document.createElement('a');
//         link.href = url;
//         link.setAttribute('download', 'employees.xlsx');
//         document.body.appendChild(link);
//         link.click();
//         document.body.removeChild(link);
//       } catch (error) {
//         console.log(error);
//       }
//     }
//   };

//   useEffect(() => {
//     fetchEmployees();
//   }, [userData, pageNumber, pageSize]);

//   const handlePageSizeChange = (event) => {
//     setPageSize(event.target.value);
//     setPageNumber(1);
//   };

//   const handlePageChange = (event, value) => {
//     setPageNumber(value);
//   };

//   const handleSearch = (event) => {
//     setSearchTerm(event.target.value);
//   };

//   const handleRoleFilter = (type) => {
//     setSelectedRole(type);
//   };

//   const filteredEmployees = employees.filter((employee) => {
//     const matchesSearchTerm = Object.values(employee).some(
//       (value) =>
//         typeof value === 'string' && value.toLowerCase().includes(searchTerm.toLowerCase())
//     );

//     const matchesRole = selectedRole ? employee.type.toLowerCase() === selectedRole.toLowerCase() : true;

//     return matchesSearchTerm && matchesRole;
//   });

//   return (
//     <>
//       <div className="filter py-2 text-end">
//         <nav className="navbar">
//           <div className="container justify-content-end">
//             <form className="d-flex gap-1" role="search">
//               <input
//                 className="form-control me-2"
//                 type="search"
//                 placeholder="Search"
//                 aria-label="Search"
//                 value={searchTerm}
//                 onChange={handleSearch}
//               />
//               <FormControl fullWidth className="w-50">
//                 <InputLabel id="page-size-select-label">Page Size</InputLabel>
//                 <Select
//                   className="justify-content-center"
//                   labelId="page-size-select-label"
//                   id="page-size-select"
//                   value={pageSize}
//                   label="Page Size"
//                   onChange={handlePageSizeChange}
//                 >
//                   <MenuItem value={5}>5</MenuItem>
//                   <MenuItem value={10}>10</MenuItem>
//                   <MenuItem value={20}>20</MenuItem>
//                   <MenuItem value={50}>50</MenuItem>
//                 </Select>
//               </FormControl>
//               <div className="icons d-flex gap-2 pt-3">
//                 <div className="dropdown">
//                   <Tooltip title="Filter by Role" placement="top">
//                     <button
//                       className="dropdown-toggle border-0 bg-white edit-pen"
//                       type="button"
//                       data-bs-toggle="dropdown"
//                       aria-expanded="false"
//                     >
//                       <FontAwesomeIcon icon={faFilter} />
//                     </button>
//                   </Tooltip>
//                   <ul className="dropdown-menu">
//                     <li>
//                       <a
//                         className="dropdown-item"
//                         href="#"
//                         onClick={() => handleRoleFilter("")}
//                       >
//                         All
//                       </a>
//                     </li>
//                     <li>
//                       <a
//                         className="dropdown-item"
//                         href="#"
//                         onClick={() => handleRoleFilter("subadmin")}
//                       >
//                         SubAdmin
//                       </a>
//                     </li>
//                     <li>
//                       <a
//                         className="dropdown-item"
//                         href="#"
//                         onClick={() => handleRoleFilter("main-subadmin")}
//                       >
//                         Main-SubAdmin
//                       </a>
//                     </li>
//                     <li>
//                       <a
//                         className="dropdown-item"
//                         href="#"
//                         onClick={() => handleRoleFilter("instructor")}
//                       >
//                         Instructor
//                       </a>
//                     </li>
//                   </ul>
//                 </div>
//               </div>
//             </form>
//             <Tooltip title="Convert Employees into pdf" placement="top">
//               <button className='border-0 bg-transparent edit-pen' onClick={ExportAllDataToPdf}>
//                 <FontAwesomeIcon icon={faArrowUpFromBracket} />
//               </button>
//             </Tooltip>
//             <Tooltip title="Convert Employees into Excel" placement="top">
//               <button className='border-0 bg-transparent edit-pen ps-2' onClick={ExportAllDataToCSV}>
//                 <FontAwesomeIcon icon={faFileCsv} />
//               </button>
//             </Tooltip>
//             <Box
//               sx={{
//                 display: 'flex',
//                 justifyContent: 'flex-end',
//                 p: 1,
//                 mr: 6,
//               }}
//             >
//               <Button sx={{ px: 2, m: 0.5 }} variant="contained" className='primaryBg' startIcon={<AddCircleOutlineIcon className='addIcon' />} onClick={handleClickOpen}>
//                 Add New
//               </Button>
//             </Box>
//           </div>
//         </nav>
//         <Dialog
//           fullScreen={fullScreen}
//           open={open}
//           onClose={handleClose}
//           aria-labelledby="responsive-dialog-title"
//           sx={{
//             "& .MuiDialog-container": {
//               "& .MuiPaper-root": {
//                 width: "100%",
//                 maxWidth: "900px",
//                 height: "100%",
//                 maxHeight: "700px",
//               },
//             },
//           }}
//         >
//           <DialogTitle id="responsive-dialog-title">
//             {"Create Employee"}
//           </DialogTitle>
//           <DialogContent>
//             <CreateEmployee />
//           </DialogContent>
//           <DialogActions>
//             <Button autoFocus onClick={handleClose}>
//               Close
//             </Button>
//           </DialogActions>
//         </Dialog>
//       </div>
//       {loading ? (
//         <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '80vh' }}>
//           <CircularProgress />
//         </Box>
//       ) : (
//         <div className="overflow-scroll">
//           <table className="table table-borderless">
//             <thead>
//               <tr>
//                 <th className="fw-bold text-secondary">ID</th>
//                 <th className="fw-bold text-secondary">First Name</th>
//                 <th className="fw-bold text-secondary">Last Name</th>
//                 <th className="fw-bold text-secondary">Email</th>
//                 <th className="fw-bold text-secondary">Phone Number</th>
//                 <th className="fw-bold text-secondary">Role</th>
//                 <th className="fw-bold text-secondary">Type</th>
//                 <th className="fw-bold text-secondary">Position</th>
//                 <th className="fw-bold text-secondary">Date Of Birth</th>
//                 <th className="fw-bold text-secondary">Address</th>
//                 <th className="fw-bold text-secondary">City</th>
//                 <th className="fw-bold text-secondary">Country</th>
//                 <th className="fw-bold text-secondary">Zip Code</th>
//                 <th className="fw-bold text-secondary">Gender</th>
//                 <th className="fw-bold text-secondary">Change Role</th>
//                 <th className="fw-bold text-secondary">Update</th>
//               </tr>
//             </thead>
//             <tbody>
//               {filteredEmployees.length > 0 ? (
//                 filteredEmployees.map((employee, index) => (
//                   <tr key={employee.id}>
//                     <td>{(pageNumber - 1) * pageSize + index + 1}</td>
//                     <td>{employee.firstName}</td>
//                     <td>{employee.lastName}</td>
//                     <td>{employee.email}</td>
//                     <td>{employee.phoneNumber}</td>
//                     <td>{employee.role}</td>
//                     <td>{employee.type}</td>
//                     <td>{employee.position}</td>
//                     <td>{employee.dateOfBirth}</td>
//                     <td>{employee.address}</td>
//                     <td>{employee.city}</td>
//                     <td>{employee.country}</td>
//                     <td>{employee.zipCode}</td>
//                     <td>{employee.gender}</td>
//                     <td>
//                       <Tooltip title="Change Role" placement="top">
//                         <button
//                           className="border-0 bg-transparent edit-pen"
//                           onClick={() => handleClickOpenChange(employee.id)}
//                         >
//                           <FontAwesomeIcon icon={faPeopleArrows} />
//                         </button>
//                       </Tooltip>
//                     </td>
//                     <td>
//                       <Tooltip title="Update" placement="top">
//                         <button
//                           className="border-0 bg-transparent edit-pen"
//                           onClick={() => handleClickOpenUpdate(employee.id)}
//                         >
//                           <FontAwesomeIcon icon={faPen} />
//                         </button>
//                       </Tooltip>
//                     </td>
//                   </tr>
//                 ))
//               ) : (
//                 <tr>
//                   <td colSpan="16" className="text-center text-danger">No data found</td>
//                 </tr>
//               )}
//             </tbody>
//           </table>
//         </div>
//       )}
//       <div className="mt-4 mb-4">
//         <Stack spacing={2} className="d-flex align-items-center">
//           <Pagination
//             count={totalPages}
//             page={pageNumber}
//             onChange={handlePageChange}
//             color="primary"
//             size="large"
//           />
//         </Stack>
//       </div>
//       <Dialog
//         fullScreen={fullScreen}
//         open={openUpdate}
//         onClose={handleCloseUpdate}
//         aria-labelledby="responsive-dialog-title"
//         sx={{
//           "& .MuiDialog-container": {
//             "& .MuiPaper-root": {
//               width: "100%",
//               maxWidth: "900px",
//               height: "100%",
//               maxHeight: "700px",
//             },
//           },
//         }}
//       >
//         <DialogTitle id="responsive-dialog-title">
//           {"Update Employee"}
//         </DialogTitle>
//         <DialogContent>
//           <UpdateEmployee id={employeeId} />
//         </DialogContent>
//         <DialogActions>
//           <Button autoFocus onClick={handleCloseUpdate}>
//             Close
//           </Button>
//         </DialogActions>
//       </Dialog>
//       <Dialog
//         fullScreen={fullScreen}
//         open={openChange}
//         onClose={handleCloseChange}
//         aria-labelledby="responsive-dialog-title"
//         sx={{
//           "& .MuiDialog-container": {
//             "& .MuiPaper-root": {
//               width: "100%",
//               maxWidth: "900px",
//               height: "100%",
//               maxHeight: "700px",
//             },
//           },
//         }}
//       >
//         <DialogTitle id="responsive-dialog-title">
//           {"Change Employee Role"}
//         </DialogTitle>
//         <DialogContent>
//           <ChangeRole userId={employeeId} />
//         </DialogContent>
//         <DialogActions>
//           <Button autoFocus onClick={handleCloseChange}>
//             Close
//           </Button>
//         </DialogActions>
//       </Dialog>
//     </>
//   );
// }
