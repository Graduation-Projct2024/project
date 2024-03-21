'use client'
import axios from 'axios';
import React, { useEffect, useState } from 'react'
import '../../dashboard/dashboard.css'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons'
import Link from 'next/link';


export default function AccreditCourses() {
  const [accreditCourses, setAccreditCourses] = useState([]);
  let[loader,setLoader] = useState(false);
  const fetchCoursesForAccredit = async () => {
    try{
    const { data } = await axios.get(`http://localhost:5134/api/CourseContraller/GetAllCoursesForAccredit`);
    console.log(data);
    setAccreditCourses(data);
  }
    catch(error){
      console.log(error);
    }
  };

  const accreditCourse = async (courseId , Status) => {
    //setLoader(true);
    console.log(courseId);
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
    }
  };

  useEffect(() => {
    fetchCoursesForAccredit();
  }, []);
if(loader){
  return <p>Loading ...</p>
}
  const [searchTerm, setSearchTerm] = useState('');
  const handleSearch = (event) => {
    setSearchTerm(event.target.value);
  };

  const filteredAccreditCourses = accreditCourses.filter((course) => {
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
      {/* <>
      {filteredAccreditCourses.length ? filteredAccreditCourses.map((course) =>(
        <img src={course.imageUrl}/>
        
      )) : <p>no imgs</p>}
</> */}
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
              <tr key={course.id}>
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
                  <button type="button" className="btn accredit" onClick={()=>accreditCourse(course.id,'accredit')}>Accredit</button>  
                {/* <Link href='/dashboard' className='text-decoration-none acc'>Accredit </Link> */}
                <button type="button" className="btn accredit" onClick={()=>accreditCourse(course.id,"reject")}>Reject</button>

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
