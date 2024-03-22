'use client'
import Layout from '@/app/(admin)/AdminLayout/Layout'
import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import axios from 'axios'
import Link from 'next/link'
import React, { useEffect, useState } from 'react'
import '../../dashboard/dashboard.css'

export default function InstructorCourses({params}) {
    console.log(params.Instructorid)

    const [instructorCourse,setInstructorCourse] = useState([])
    const [loading, setLoading] = useState(false);
  // const {id} = useParams();
  // console.log(useParams());
    let[ins,setIns] = useState('');
    const getInstructorCourses =async ()=>{
    try {
      //setLoading(false)
      const {data} = await axios.get(`http://localhost:5134/api/Employee/GetAllCoursesGivenByInstructor?Instructorid=${params.Instructorid}`,
      {
    }
      );
      if(data.isSuccess){
        // console.log(data.result);
      setInstructorCourse(data.result);
      //setLoading(false)
      }}
      catch (error) {
      console.log(error)
      }
      
  }
  const [employees, setEmployees] = useState([]);


      const fetchEmployees = async () => {
        try{
        const { data } = await axios.get(`http://localhost:5134/api/Employee/GetAllEmployee`);
        console.log(data);
        setEmployees(data);
      }
        catch(error){
          console.log(error);
        }
      };
  useEffect(()=>{
    getInstructorCourses();
    fetchEmployees();
  },[])


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
    <Layout title = "Courses for this Instructor">
        
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


      
    </Layout>
    
  )
}
