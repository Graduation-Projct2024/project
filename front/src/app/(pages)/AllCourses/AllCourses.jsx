'use client'
import axios from 'axios';
import React, { useEffect, useState } from 'react'
import './AllCourses.css'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faStar } from '@fortawesome/free-solid-svg-icons';
import Link from 'next/link';

export default function AllCourses() {
    const [courses, setCourses] = useState([]);

    const fetchCourses = async () => {
        try {
          const { data } = await axios.get(
            `http://localhost:5134/api/CourseContraller`
          );
          console.log(data);
          setCourses(data.result);
        } catch (error) {
          console.log(error);
        }
      
    };
  
    useEffect(() => {
      fetchCourses();
    }, []);
  return (
    <>
        <div className="container">
          <div className="row">
          {courses.length ? (
            courses.map((course) => (
              <div className="col-lg-4 col-md-6 col-sm-12 pb-4" key={course.id}>
               <div className="singleCourse">
                <div className="course-image row">
                  <img src={course.imageUrl} alt="Courses" className='col-md-6'/>
                  <div className="price col-md-6">
                    <span className=''>${course.price}</span>
                  </div>
                </div>
                <div className="course-content">
                    <div className="instructor-rating">
                      <ul>
                        <li><img src="./user1.png" alt="instructor-img"/></li>
                        <li><p className='instructorName'>{course.instructorName}</p></li>
                      </ul>
                      <ul>
                        <li><FontAwesomeIcon icon={faStar} style={{color: "#FFD43B",}} /></li>
                        <li>(4.9)</li>
                      </ul>
                    </div>
                    <h4 className="title">{course.name}</h4>
                    <p>{course.description}</p>
                    <div className="category row align-items-center">
                      <p className='col-6 pt-4 pe-5'>{course.category}</p>
                      <div className="courses-button col-6 justify-content-end pt-2 ps-4">
                                <Link href={`CourseDetails/${course.id}`} className = "text-decoration-none btn btn-dark p-3">View Details</Link>
                      </div>
                    </div>
                </div>
            </div>
           
            </div>
            ))
          ) : (
            <div>No Courses Found!</div>
          )}
            
          </div>
           
        </div>
    </>
  )
}
