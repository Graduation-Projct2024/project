'use client'
import React, { useContext, useEffect, useState } from 'react';
import axios from 'axios';
import { UserContext } from '@/context/user/User';


export default function Feedback() {
  const { userToken, userId } = useContext(UserContext);
  const [feedbacks, setFeedbacks] = useState([]);

  const getFeedbacks = async () => {
    if (userId) {
      try {
        const { data } = await axios.get(`https://localhost:7116/api/Feedback/GetAllInstructorFeedback?instructorId=${userId}`);
        console.log(data);
        setFeedbacks(data.result.items || []);
      } catch (error) {
        console.log(error);
      }
    }
  };
  useEffect(() => {
    getFeedbacks();
  }, [userId, feedbacks ]);

  return (
    <div>Feedback</div>
  )
}
