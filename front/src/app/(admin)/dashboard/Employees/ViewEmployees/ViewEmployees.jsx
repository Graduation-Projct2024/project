import React, { useEffect, useState } from 'react'
import CreateEmployee from '../CreateEmployee/CreateEmployee';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowUpFromBracket, faEye, faFilter, faPen } from '@fortawesome/free-solid-svg-icons'
import Link from 'next/link';
import axios from 'axios';
import UpdateEmployee from '../UpdateEmployee/UpdateEmployee';
export default function ViewEmployees() {

    // const initialEmployees = [
    //     { id:'1',
    //       fName:'Ismail',
    //         lName: 'Tala',
    //         email: 'tala@gmail.com',
    //         role: 'subadmin',
    //         gender: 'Female',
    //         phoneNumber: '(+970)59-392-5818',
    //         Address: 'Tulkarm-Kafa street'
    //     },
    //     { id:'2',
    //       fName:'Ismail',
    //       lName: 'Tala',
    //         email: 'hala@gmail.com',
    //         role: 'Instructor',
    //         gender: 'Male',
    //         phoneNumber: '(+970)59-392-5819',
    //         Address: 'Tulkarm-Ateel-street'
    //     },
    //     { id:'3',
    //       fName:'Ismail',
    //       lName: 'Tala',
    //         email: 'tasneem@gmail.com',
    //         role: 'subadmin',
    //         gender: 'male',
    //         phoneNumber: '(+970)59-392-5818',
    //         Address: 'Nablus'
    //     },
    //   ];
    
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

      useEffect(() => {
        fetchEmployees();
      }, []);

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
  <li>
          <a className="dropdown-item" href="#" onClick={() => handleRoleFilter('')}>
            All
          </a>
        </li>
  <li>
          <a className="dropdown-item" href="#" onClick={() => handleRoleFilter('subadmin')}>
            SubAdmin
          </a>
        </li>
        <li>
          <a className="dropdown-item" href="#" onClick={() => handleRoleFilter('instructor')}>
            Instructor
          </a>
        </li>
  </ul>
</div>
<FontAwesomeIcon icon={faArrowUpFromBracket} />
                    
                </div>
                </form>
                <button type="button" className="btn btn-primary ms-2 addEmp" data-bs-toggle="modal" data-bs-target="#staticBackdrop2">
                    <span>+ Add new</span> 
                </button>

            </div>
        </nav>
        {/* <div className="modal fade" id="exampleModal" tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true"> */}
        <div class="modal fade" id="staticBackdrop2" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdrop2Label" aria-hidden="true">
          <div className="modal-dialog modal-dialog-centered modal-lg">
            <div className="modal-content row justify-content-center">
              <div className="modal-body text-center ">
                <h2 className='fs-1'>CREATE ACCOUNT</h2>
                  <div className="row">
                    <CreateEmployee/>
                  </div>
              </div>
            </div>
          </div>
        </div>
      </div>

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
    filteredEmployees.map((employee) =>(
      <tr key={employee.id}>
        {console.log(employee.type)}
      <th scope="row">{employee.id}</th>
      <td>{employee.fName} {employee.lName}</td>
      <td>{employee.email}</td>
      <td>{employee.type}</td>
      <td>{employee.gender}</td>
      <td>{employee.phoneNumber}</td>
      <td>{employee.address}</td>
      <td className='d-flex gap-1'><button className='border-0 bg-white ' type="button" data-bs-toggle="modal" data-bs-target="#exampleModal2">
        <FontAwesomeIcon icon={faPen} className='edit-pen'/>
        </button>
      <Link href={'/Profile'}>
        <button  type="button" className='border-0 bg-white '>
        <FontAwesomeIcon icon={faEye}  className='edit-pen'/>
        </button>
        </Link>
        </td>

    </tr>
      ))): (
        <tr>
          <td colSpan="7">No employees</td>
        </tr>
        )}
    
    
  </tbody>
</table>

<div className="modal fade" id="exampleModal2" tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div className="modal-dialog modal-dialog-centered modal-lg">
            <div className="modal-content row justify-content-center">
              <div className="modal-body text-center ">
                <h2>UPDATE ACCOUNT</h2>
                  <div className="row">
                    <UpdateEmployee/>
                  </div>
              </div>
            </div>
          </div>
        </div>
      </>
  )
}
