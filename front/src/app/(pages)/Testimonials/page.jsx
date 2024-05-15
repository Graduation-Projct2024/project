'use client'
import React, { useEffect, useState } from 'react'
import { Navigation, Pagination, Scrollbar, A11y } from 'swiper/modules';

import { Swiper, SwiperSlide } from 'swiper/react';
import './Testimonials.css'
// Import Swiper styles
import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import 'swiper/css/scrollbar';
import axios from 'axios';
import Link from 'next/link';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEnvelope, faPhone } from '@fortawesome/free-solid-svg-icons';
import '../../(admin)/dashboard/dashboard.css'

export default function Testimonials() {

  let [contacts,setContact] = useState([]);
  const fetchContacts = async () => {
    try{
    const { data } = await axios.get(`http://localhost:5134/api/Employee/GetAllEmployeeForContact`);
    console.log(data);
    setContact(data);
  }
    catch(error){
      console.log(error);
    }
  
  };

  useEffect(() => {
    fetchContacts();
  }, []);
  return (
    <div className="testimonials py-5">
      <div className="container">
        <div className="title text-center">
          <h2> Our Experience Instructors</h2>
        </div>
        <Swiper 
           modules={[Navigation, Pagination, Scrollbar, A11y]}
           spaceBetween={50}
           slidesPerView={3}
          //  navigation
           pagination={{ clickable: true }}
           onSwiper={(swiper) => console.log(swiper)}
           onSlideChange={() => console.log('slide change')}
           loop = {true}
        >
           {contacts ? contacts.map((contact)=>(
          <SwiperSlide className='py-5'>
         
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

          </SwiperSlide>
                )) : <h1>No Data</h1>}
         
        </Swiper>
      </div>
    </div>
  )
}
