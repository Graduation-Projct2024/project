'use client'
import Layout from '@/app/(admin)/AdminLayout/Layout'
import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import axios from 'axios'
import Link from 'next/link'
import React, { useContext, useEffect, useState } from 'react'
import '../../dashboard/dashboard.css'
import { UserContext } from '@/context/user/User'
import { FormControl, InputLabel, MenuItem, Pagination, Select, Stack } from '@mui/material'

export default function InstructorCourses({params}) {
    // console.log(params.Instructorid)
    const {userToken, setUserToken, userData}=useContext(UserContext);
    

    const [instructorCourse,setInstructorCourse] = useState([])
    const [employeeName, setEmployeeName] = useState('');
    const [loading, setLoading] = useState(false);
  // const {id} = useParams();
  // console.log(useParams());
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(0);

    const getInstructorCourses = async (pageNum = pageNumber, pageSizeNum = pageSize) =>{
      if(userData){
    try {
      //setLoading(false)
      const {data} = await axios.get(`https://localhost:7116/api/CourseContraller/GetAllCoursesGivenByInstructor?Instructorid=${params.Instructorid}&pageNumber=${pageNum}&pageSize=${pageSize}`,{ headers: { Authorization: `Bearer ${userToken}` } });
      if(data.isSuccess){
        // console.log(data.result);
      setInstructorCourse(data.result.items);
      setTotalPages(data.result.totalPages);
      //setLoading(false)
      }}
      catch (error) {
      console.log(error)
      }
    }   
  };

  const [employees, setEmployees] = useState([]);


      const fetchEmployees = async () => {
        if(userData){
        try{
        const { data } = await axios.get(`https://localhost:7116/api/Employee/GetAllEmployee`);
        console.log(data);
        setEmployees(data.result.items);
      }
        catch(error){
          console.log(error);
        }}
      };


  useEffect(() => {
    getInstructorCourses();
    fetchEmployees();
    }, [instructorCourse,userData, pageNumber, pageSize]);  // Fetch courses on mount and when page or size changes
  
  const handlePageSizeChange = (event) => {
    setPageSize(event.target.value);
    setPageNumber(1); // Reset to the first page when page size changes
  };
  
  const handlePageChange = (event, value) => {
    setPageNumber(value);
  };

  useEffect(() => {
    const employee = employees.find(employee => employee.id == params.Instructorid);
    if(employee) {
        setEmployeeName(`${employee.fName} ${employee.lName}`);
    }
}, [employees, params.Instructorid]);
console.log(employeeName)

  const [searchTerm, setSearchTerm] = useState('');

  const handleSearch = (event) => {
    setSearchTerm(event.target.value);
  };

  const filteredICourses = instructorCourse.filter((ICourse) => {
const matchesSearchTerm =
  Object.values(ICourse).some(
    (value) =>
      typeof value === 'string' && value.toLowerCase().includes(searchTerm.toLowerCase())
  );
return matchesSearchTerm ;

 
});

  
  return (
    
    <Layout title = {`Courses for "${employeeName}"`}>
        
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
                <div className="icons d-flex gap-2 pt-2">
                    
                    <div className="dropdown">
  <button className="dropdown-toggle border-0 bg-white edit-pen" type="button" data-bs-toggle="dropdown" aria-expanded="false">
    <FontAwesomeIcon icon={faFilter} />
  </button>
  <ul className="dropdown-menu">
 
  </ul>
</div>
<FontAwesomeIcon icon={faArrowUpFromBracket} />
                    
                </div>
                </form>
               

            </div>
        </nav>
        
      </div>
      {/* {employees ? employees.map((employee) =>{
          {params.Instructorid == employee.id ? <h2>Courses for {employee.fName} {employee.lName}</h2> : <p> None </p>}
      }) : <p>None</p>} */}

      <table className="table">
  <thead>
    <tr>
      <th scope="col">ID</th>
      <th scope="col">Name</th>
      <th scope="col">Price</th>
      <th scope="col">Category</th>
      <th scope="col">Status</th>
      <th scope="col">Start Date</th>
      <th scope="col">Option</th>
    </tr>
  </thead>
  <tbody>
  {filteredICourses.length ? (
    filteredICourses.map((course) =>(
      <tr key={course.id}>
        {console.log(course.id)}
      <th scope="row">{course.id}</th>
      <td>{course.name}</td>
      <td>{course.price}</td>
      <td>{course.category}</td>
      <td>{course.status}</td>
      <td>{course.startDate}</td>
      <td className='d-flex gap-1'>

      <Link href={'/Profile'}>
        <button  type="button"className='edit-pen border-0 bg-white '>
        <FontAwesomeIcon icon={faEye}  />
        </button>
        </Link>
        </td>

    </tr>
      ))): (
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


      
    </Layout>
    
  )
}
// 'use client'
// import Layout from '@/app/(admin)/AdminLayout/Layout';
// import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons';
// import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
// import axios from 'axios';
// import Link from 'next/link';
// import React, { useContext, useEffect, useState } from 'react';
// import '../../dashboard/dashboard.css';
// import { UserContext } from '@/context/user/User';

// export default function InstructorCourses({ params }) {
//     const { userToken, userData } = useContext(UserContext);

//     const [instructorCourse, setInstructorCourse] = useState([]);
//     const [employeeName, setEmployeeName] = useState('');
//     const [employees, setEmployees] = useState([]);
//     const [searchTerm, setSearchTerm] = useState('');

//     const getInstructorCourses = async () => {
//         if (userData && userToken) {
//             try {
//                 const { data } = await axios.get(
//                     `http://localhost:5134/api/Employee/GetAllCoursesGivenByInstructor?Instructorid=${params.Instructorid}&pageNumber=1&pageSize=10`,
//                     { headers: { Authorization: `Bearer ${userToken}` } }
//                 );
//                 if (data.isSuccess) {
//                     setInstructorCourse(data.result.items);
//                 }
//             } catch (error) {
//                 console.error('Error fetching instructor courses:', error);
//                 if (error.response && error.response.status === 403) {
//                     console.error('403 Forbidden: You do not have the necessary permissions.');
//                 }
//             }
//         }
//     };

//     const fetchEmployees = async () => {
//         if (userData) {
//             try {
//                 const { data } = await axios.get(`http://localhost:5134/api/Employee/GetAllEmployee`, {
//                     headers: { Authorization: `Bearer ${userToken}` }
//                 });
//                 console.log(data);
//                 setEmployees(data);
//             } catch (error) {
//                 console.error('Error fetching employees:', error);
//                 if (error.response && error.response.status === 403) {
//                     console.error('403 Forbidden: You do not have the necessary permissions.');
//                 }
//             }
//         }
//     };

//     useEffect(() => {
//         getInstructorCourses();
//         fetchEmployees();
//     }, [userData, userToken]);

//     useEffect(() => {
//         console.log('employees:', employees);
//         if (Array.isArray(employees)) {
//             const employee = employees.find(employee => employee.id === params.Instructorid);
//             if (employee) {
//                 setEmployeeName(`${employee.fName} ${employee.lName}`);
//             }
//         } else {
//             console.error('employees is not an array', employees);
//         }
//     }, [employees, params.Instructorid]);

//     const handleSearch = (event) => {
//         setSearchTerm(event.target.value);
//     };

//     const filteredICourses = instructorCourse.filter((ICourse) => {
//         const matchesSearchTerm = Object.values(ICourse).some(
//             (value) => typeof value === 'string' && value.toLowerCase().includes(searchTerm.toLowerCase())
//         );
//         return matchesSearchTerm;
//     });

//     return (
//         <Layout title={`Courses "${employeeName}" Instructor`}>
//             <div className="filter py-2 text-end">
//                 <nav className="navbar">
//                     <div className="container justify-content-end">
//                         <form className="d-flex" role="search">
//                             <input
//                                 className="form-control me-2"
//                                 type="search"
//                                 placeholder="Search"
//                                 aria-label="Search"
//                                 value={searchTerm}
//                                 onChange={handleSearch}
//                             />
//                             <div className="icons d-flex gap-2 pt-2">
//                                 <div className="dropdown">
//                                     <button className="dropdown-toggle border-0 bg-white edit-pen" type="button" data-bs-toggle="dropdown" aria-expanded="false">
//                                         <FontAwesomeIcon icon={faFilter} />
//                                     </button>
//                                     <ul className="dropdown-menu"></ul>
//                                 </div>
//                                 <FontAwesomeIcon icon={faArrowUpFromBracket} />
//                             </div>
//                         </form>
//                     </div>
//                 </nav>
//             </div>
//             <table className="table">
//                 <thead>
//                     <tr>
//                         <th scope="col">ID</th>
//                         <th scope="col">Name</th>
//                         <th scope="col">Price</th>
//                         <th scope="col">Category</th>
//                         <th scope="col">Status</th>
//                         <th scope="col">Start Date</th>
//                         <th scope="col">Option</th>
//                     </tr>
//                 </thead>
//                 <tbody>
//                     {filteredICourses.length ? (
//                         filteredICourses.map((course) => (
//                             <tr key={course.id}>
//                                 <th scope="row">{course.id}</th>
//                                 <td>{course.name}</td>
//                                 <td>{course.price}</td>
//                                 <td>{course.category}</td>
//                                 <td>{course.status}</td>
//                                 <td>{course.startDate}</td>
//                                 <td className="d-flex gap-1">
//                                     <Link href={'/Profile'}>
//                                         <button type="button" className="edit-pen border-0 bg-white ">
//                                             <FontAwesomeIcon icon={faEye} />
//                                         </button>
//                                     </Link>
//                                 </td>
//                             </tr>
//                         ))
//                     ) : (
//                         <tr>
//                             <td colSpan="7">No Courses</td>
//                         </tr>
//                     )}
//                 </tbody>
//             </table>
//         </Layout>
//     );
// }


// 'use client'
// import Layout from '@/app/(admin)/AdminLayout/Layout';
// import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons';
// import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
// import axios from 'axios';
// import Link from 'next/link';
// import React, { useContext, useEffect, useState } from 'react';
// import '../../dashboard/dashboard.css';
// import { UserContext } from '@/context/user/User';
// import { useRouter } from 'next/router';

// export default function InstructorCourses({ params }) {
//     const router = useRouter();
//     const { fName, lName } = router.query;  // Retrieve the query parameters

//     const { userToken, userData } = useContext(UserContext);

//     const [instructorCourse, setInstructorCourse] = useState([]);
//     const [employeeName, setEmployeeName] = useState(`${fName} ${lName}`);
//     const [searchTerm, setSearchTerm] = useState('');

//     const getInstructorCourses = async () => {
//         if (userData && userToken) {
//             try {
//                 const { data } = await axios.get(
//                     `http://localhost:5134/api/Employee/GetAllCoursesGivenByInstructor?Instructorid=${params.Instructorid}&pageNumber=1&pageSize=10`,
//                     { headers: { Authorization: `Bearer ${userToken}` } }
//                 );
//                 if (data.isSuccess) {
//                     setInstructorCourse(data.result.items);
//                 }
//             } catch (error) {
//                 console.error('Error fetching instructor courses:', error);
//                 if (error.response && error.response.status === 403) {
//                     console.error('403 Forbidden: You do not have the necessary permissions.');
//                 }
//             }
//         }
//     };

//     // const fetchEmployees = async () => {
//     //     if (userData) {
//     //         try {
//     //             const { data } = await axios.get(`http://localhost:5134/api/Employee/GetAllEmployee`, {
//     //                 headers: { Authorization: `Bearer ${userToken}` }
//     //             });
//     //             console.log(data);
//     //             setEmployees(data);
//     //         } catch (error) {
//     //             console.error('Error fetching employees:', error);
//     //             if (error.response && error.response.status === 403) {
//     //                 console.error('403 Forbidden: You do not have the necessary permissions.');
//     //             }
//     //         }
//     //     }
//     // };

//     useEffect(() => {
//         getInstructorCourses();
//         // fetchEmployees();
//     }, [instructorCourse,userData, userToken]);

//     // useEffect(() => {
//     //     console.log('employees:', employees);
//     //     if (Array.isArray(employees)) {
//     //         const employee = employees.find(employee => employee.id === params.Instructorid);
//     //         if (employee) {
//     //             setEmployeeName(`${employee.fName} ${employee.lName}`);
//     //         }
//     //     } else {
//     //         console.error('employees is not an array', employees);
//     //     }
//     // }, [employees, params.Instructorid]);

//     const handleSearch = (event) => {
//         setSearchTerm(event.target.value);
//     };

//     const filteredICourses = instructorCourse.filter((ICourse) => {
//         const matchesSearchTerm = Object.values(ICourse).some(
//             (value) => typeof value === 'string' && value.toLowerCase().includes(searchTerm.toLowerCase())
//         );
//         return matchesSearchTerm;
//     });

//     return (
//         <Layout title={`Courses ${employeeName} Instructor`}>
//             <div className="filter py-2 text-end">
//                 <nav className="navbar">
//                     <div className="container justify-content-end">
//                         <form className="d-flex" role="search">
//                             <input
//                                 className="form-control me-2"
//                                 type="search"
//                                 placeholder="Search"
//                                 aria-label="Search"
//                                 value={searchTerm}
//                                 onChange={handleSearch}
//                             />
//                             <div className="icons d-flex gap-2 pt-2">
//                                 <div className="dropdown">
//                                     <button className="dropdown-toggle border-0 bg-white edit-pen" type="button" data-bs-toggle="dropdown" aria-expanded="false">
//                                         <FontAwesomeIcon icon={faFilter} />
//                                     </button>
//                                     <ul className="dropdown-menu"></ul>
//                                 </div>
//                                 <FontAwesomeIcon icon={faArrowUpFromBracket} />
//                             </div>
//                         </form>
//                     </div>
//                 </nav>
//             </div>
//             <table className="table">
//                 <thead>
//                     <tr>
//                         <th scope="col">ID</th>
//                         <th scope="col">Name</th>
//                         <th scope="col">Price</th>
//                         <th scope="col">Category</th>
//                         <th scope="col">Status</th>
//                         <th scope="col">Start Date</th>
//                         <th scope="col">Option</th>
//                     </tr>
//                 </thead>
//                 <tbody>
//                     {filteredICourses.length ? (
//                         filteredICourses.map((course) => (
//                             <tr key={course.id}>
//                                 <th scope="row">{course.id}</th>
//                                 <td>{course.name}</td>
//                                 <td>{course.price}</td>
//                                 <td>{course.category}</td>
//                                 <td>{course.status}</td>
//                                 <td>{course.startDate}</td>
//                                 <td className="d-flex gap-1">
//                                     <Link href={'/Profile'}>
//                                         <button type="button" className="edit-pen border-0 bg-white ">
//                                             <FontAwesomeIcon icon={faEye} />
//                                         </button>
//                                     </Link>
//                                 </td>
//                             </tr>
//                         ))
//                     ) : (
//                         <tr>
//                             <td colSpan="7">No Courses</td>
//                         </tr>
//                     )}
//                 </tbody>
//             </table>
//         </Layout>
//     );
// }


