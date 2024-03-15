'use client'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useEffect, useState } from 'react'
import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons'
import Link from 'next/link';
import axios from 'axios';

export default function ViewCourses() {


  const [courses, setCourses] = useState([]);


  const fetchCourses = async () => {
    try{
    const { data } = await axios.get(`http://localhost:5134/api/CourseContraller`);
    console.log(data);
    setCourses(data);
  }
    catch(error){
      console.log(error);
    }
  };

  useEffect(() => {
    fetchCourses();
  }, []);

  const [searchTerm, setSearchTerm] = useState('');

  const handleSearch = (event) => {
    setSearchTerm(event.target.value);
  };

  const filteredCourses = courses.filter((course) => {
const matchesSearchTerm =
  Object.values(course).some(
    (value) =>
      typeof value === 'string' && value.toLowerCase().includes(searchTerm.toLowerCase())
  );


return matchesSearchTerm ;
});
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
  {filteredCourses.length ? (
    filteredCourses.map((course) =>(
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
        <button  type="button">
        <FontAwesomeIcon icon={faEye}  className='edit-pen border-0 bg-white '/>
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


      </>
  )
}
