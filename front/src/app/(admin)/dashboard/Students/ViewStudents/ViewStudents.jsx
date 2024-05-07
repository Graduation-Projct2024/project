import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons'
import axios from 'axios';
import React, { useContext, useEffect, useState } from 'react'
import Link from 'next/link';
import { UserContext } from '@/context/user/User';

export default function ViewStudents() {
  const {userToken, setUserToken, userData}=useContext(UserContext);

  const [students, setStudents] = useState([]);

  const fetchStudents = async () => {
    if(userData){
    try{
    const { data } = await axios.get(`http://localhost:5134/api/StudentsContraller`);
    //console.log(data);
    setStudents(data);
  }
    catch(error){
      console.log(error);
    }
  }
  };

  useEffect(() => {
    fetchStudents();
  }, [userData]);

  const [searchTerm, setSearchTerm] = useState('');

  const handleSearch = (event) => {
    setSearchTerm(event.target.value);
  };

  const filteredStudents = students.filter((student) => {
const matchesSearchTerm =
  Object.values(student).some(
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
      <th scope="col">Email</th>
      {/* <th scope="col">Gender</th>
      <th scope="col">Phone number</th>
      <th scope="col">Address</th> */}
      <th scope="col">Option</th>
    </tr>
  </thead>
  <tbody>
  {filteredStudents.length ? (
    filteredStudents.map((student) =>(
      <tr key={student.studentId}>
        {console.log(student.studentId)}
      <th scope="row">{student.studentId}</th>
      <td>{student.userName}</td>
      <td>{student.email}</td>
      {/* <td>{student.gender}</td>
      <td>{student.phoneNumber}</td>
      <td>{student.address}</td> */}
      <td className='d-flex gap-1'>

      <Link href={`/Profile/${student.studentId}`}>
        <button  type="button" className='edit-pen border-0 bg-white '>
        <FontAwesomeIcon icon={faEye} />
        </button>
        </Link>
        </td>

    </tr>
      ))): (
        <tr>
          <td colSpan="7">No Students</td>
        </tr>
        )}
    
    
  </tbody>
</table>


      </>
  )
}
