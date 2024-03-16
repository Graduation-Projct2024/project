import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useEffect, useState } from 'react'
import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons'
import axios from 'axios';

export default function AccreditEvents() {

    const [accreditEvents, setAccreditEvents] = useState([]);
  let[loader,setLoader] = useState(false);
  const fetchEventsForAccredit = async () => {
    try{
    const { data } = await axios.get(`http://localhost:5134/api/EventContraller/GetAllUndefinedEvents`);
    console.log(data);
    setAccreditEvents(data);
  }
    catch(error){
      console.log(error);
    }
  };

  const accreditEvent = async (eventId , Status) => {
    //setLoader(true);
    console.log(eventId);
    try{
    const { data } = await axios.patch(`http://localhost:5134/api/CourseContraller/accreditEvent?eventId=${eventId}&Status=${Status}`,
  );
  if(data.isSuccess){
    console.log(data);
    //setLoader(false);
    fetchEventsForAccredit();
  }
  }
    catch(error){
      console.log(error);
    }
  };

  useEffect(() => {
    fetchEventsForAccredit();
  }, []);
if(loader){
  return <p>Loading ...</p>
}
  const [searchTerm, setSearchTerm] = useState('');
  const handleSearch = (event) => {
    setSearchTerm(event.target.value);
  };

  const filteredAccreditEvents = accreditEvents.filter((event) => {
    const matchesSearchTerm =
    Object.values(event).some(
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
     
      <table className="table">
        <thead>
          <tr>
            <th scope="col">ID</th>
            <th scope="col">Name</th>
            <th scope="col">Content</th>
            <th scope="col">Category</th>
            <th scope="col">Event Date</th>
            <th scope="col">SubAdmin name</th>
            <th scope="col">Option</th>
          </tr>
        </thead>
        <tbody>
          {filteredAccreditEvents.length ? (
            filteredAccreditEvents.map((event) => (
              <tr key={event.id}>
                <th scope="row">{event.id}</th>
                <td>{event.name}</td>
                <td>{event.content}</td>
                <td>{event.eventCategory}</td>
                <td>{event.dateOfEvent}</td>
                <td>{event.subAdminFName} {event.subAdminLName}</td>
                <td className="d-flex gap-1">
                  {/* <Link href={"/Profile"}>
                    <button type="button" className="border-0 bg-white ">
                      <FontAwesomeIcon icon={faEye} className="edit-pen" />
                    </button>
                  </Link> */}
                  <button type="button" className="btn accredit" onClick={()=>accreditEvent(event.id,'accredit')}>Accredit</button>  
                {/* <Link href='/dashboard' className='text-decoration-none acc'>Accredit </Link> */}
                <button type="button" className="btn accredit" onClick={()=>accreditEvent(event.id,"reject")}>Reject</button>

                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="7">No Events</td>
            </tr>
          )}
        </tbody>
      </table>
    </>

  )
}
