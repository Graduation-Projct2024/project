'use client'
import React from 'react'

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faFacebookF, faGithub, faGooglePlusG, faInstagram, faXTwitter } from '@fortawesome/free-brands-svg-icons'
import { faEnvelope, faHeadphones, faLocationDot } from '@fortawesome/free-solid-svg-icons'
import AdbIcon from '@mui/icons-material/Adb';
import FacebookOutlinedIcon from '@mui/icons-material/FacebookOutlined';
import { deepPurple ,purple} from '@mui/material/colors';
import './footer.css'
export default function Footer() {
  return (
    
<div className="footer">
  <div className="container">
    <div className="footer-top">
      <div className="row">
        <div className="col-xl-4 col-lg-4 col-md-6">
          <div className="footer-widget">
            <div className="footer-logo">
            <AdbIcon className='academyIcon'/>
            </div>
            <p>Sorem ipsum dolor sit amet conse ctetur adipiscing elit, sed do eiusmod incididunt ut labore et dolore magna aliqua. Utenim ad minim veniam, quis nostrud exercition ullamco laboris nisi </p>
            <div className="footer-social">
              <span>Follow Us</span>
              <div className="footer-social-icon">
                <ul className="d-flex">
                  <li className="list-unstyled"><FontAwesomeIcon icon={faFacebookF} className='iconFoter'/></li>
                  <li className="list-unstyled"><FontAwesomeIcon icon={faXTwitter} className='iconFoter'/></li>
                  <li className="list-unstyled"><FontAwesomeIcon icon={faGooglePlusG} className='iconFoter'/></li>
                  <li className="list-unstyled"><FontAwesomeIcon icon={faGithub} className='iconFoter'/></li>
                  <li className="list-unstyled"><FontAwesomeIcon icon={faInstagram} className='iconFoter'/></li>
                </ul>
              </div>
            </div>
          </div>
        </div>
        <div className="col-xl-4 col-lg-4 col-md-6">
          <div className="footer-widget">
            <div className="footer-heading">
              <h4 className='text-center'>Quick Links</h4>
            </div>
            <div className="footer-menu ">
              <ul className='text-center' >
                <li className="list-unstyled"><a href="#" className="text-decoration-none">Home</a></li>
                <li className="list-unstyled"><a href="#" className="text-decoration-none">About Us</a></li>
                <li className="list-unstyled"><a href="#" className="text-decoration-none">All Courses</a></li>
                <li className="list-unstyled"><a href="#" className="text-decoration-none">Events</a></li>
                <li className="list-unstyled"><a href="#" className="text-decoration-none">Feedback</a></li>
                <li className="list-unstyled"><a href="#" className="text-decoration-none">Contact Us</a></li>


              </ul>
            </div>
          </div>
        </div>
        
        <div className="col-xl-4 col-lg-4 col-md-6">
          <div className="footer-widget">
            <div className="footer-heading">
              <h4>Contact Us</h4>
            </div>
            <div className="footer-contact-list">
              <div className="single-footer-contact-info d-flex">
              <FontAwesomeIcon icon={faHeadphones} className='contactIconn'/>
                <span className="footer-contact-list-text">+970593925818</span>
              </div>
              <div className="single-footer-contact-info d-flex">
              <FontAwesomeIcon icon={faEnvelope} className='contactIconn'/>
                <span className="footer-contact-list-text">tala.ismail.kafa@gmail.com</span>
              </div>
              <div className="single-footer-contact-info d-flex">
              <FontAwesomeIcon icon={faLocationDot} className='contactIconn'/>
                <span className="footer-contact-list-text">Tulkarm, Palestine</span>
              </div>
            </div>
            <div className="opening-time mt-3">
              <span>Opening Hour</span>
              <span className="opening-date">
                Sun - Sat : 10:00 am - 05:00 pm
              </span>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div className="footer-bottom">
      <div className="container">
        <div className="row">
          <div className="col-xl-12">
            <div className="footer-copyright text-center">
              <span>Copyright © 2024. All rights reserved</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>

  )
}
