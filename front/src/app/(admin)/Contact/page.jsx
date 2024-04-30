'use client'
import React, { useContext, useEffect, useState } from 'react'
import './Contact.css'
import Layout from '../AdminLayout/Layout'
import '../dashboard/dashboard.css'
import axios from 'axios';
import Link from 'next/link'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPhone } from '@fortawesome/free-solid-svg-icons'
import { faEnvelope } from '@fortawesome/free-regular-svg-icons'
import '../Profile/[id]/Profile.css'
import { faArrowUpFromBracket, faEye, faFilter } from '@fortawesome/free-solid-svg-icons'
import { UserContext } from '@/context/user/User'

export default function page() {
  const {userToken, setUserToken, userData}=useContext(UserContext);

  let [contacts,setContact] = useState([]);
  const fetchContacts = async () => {
    if(userData){
    try{
    const { data } = await axios.get(`http://localhost:5134/api/Employee/GetAllEmployeeForContact`);
    console.log(data);
    setContact(data);
  }
    catch(error){
      console.log(error);
    }
  }
  };

  useEffect(() => {
    fetchContacts();
  }, [userData]);

  const [searchTerm, setSearchTerm] = useState('');

  const handleSearch = (event) => {
    setSearchTerm(event.target.value);
  };

  const filteredContacts = contacts.filter((contact) => {
const matchesSearchTerm =
  Object.values(contact).some(
    (value) =>
      typeof value === 'string' && value.toLowerCase().includes(searchTerm.toLowerCase())
  );

return matchesSearchTerm ;
});

  return (
    <Layout title="Contacts">
       <div className="filter py-2 text-end border-top mt-4">
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

      <div className="row">
        {filteredContacts ? filteredContacts.map((contact)=>(
          <div key={contact.id} className="col-md-4">
                    <div className="card text-center mb-3" style={{ width: "18rem" }}>
                      <div className="card-body m-3">
                      <img src={contact.imageUrl ? contact.imageUrl : "./user1.png"} 
           className="pho pb-3 img-fluid" 
           alt="Profile" 
           onError={(e) => { 
             console.error("Error loading image:", contact.imageUrl); 
             e.target.onerror = null; // prevents looping
             e.target.src = "./user1.png"; // default image if error
           }} />                    
                      <h4 className="card-title contactName">{contact.userName} {contact.lName}</h4>
                        
                        <div className="d-flex justify-content-center gap-3 pt-3">
                          <Link className='social' href={`tel:${contact.phone}`}><FontAwesomeIcon icon={faPhone} /></Link>
                          <Link className='social' href={`mailto:${contact.email}`}><FontAwesomeIcon icon={faEnvelope} /></Link>
                        </div>
                      </div>
                    </div>
                  </div>
        )) : <h1>No Data</h1>}
       
      </div>
    </Layout>
  );
}
