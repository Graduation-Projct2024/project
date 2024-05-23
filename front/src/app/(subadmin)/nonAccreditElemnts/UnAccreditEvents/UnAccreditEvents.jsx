import { useTheme } from '@emotion/react';
import { useMediaQuery } from '@mui/material';
import React, { useState } from 'react'

export default function UnAccreditEvents() {

    const { userToken, setUserToken, userData,userId } = useContext(UserContext);

    const [nonAccreditEvents, setNonAccreditEvents] = useState([]);
    const [openUpdate, setOpenUpdate] = React.useState(false);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(0);
  
    const theme = useTheme();
    const fullScreen = useMediaQuery(theme.breakpoints.down('md'));
    const [courseId, setCourseId] = useState(null);
  
  const handleClickOpenUpdate = (id) => {
    setCourseId(id);
      console.log(id)
      setOpenUpdate(true);
  };
  const handleCloseUpdate = () => {
    setOpenUpdate(false);
  };
  
    const fetchEventsBeforeAccredittion =  async (pageNum = pageNumber, pageSizeNum = pageSize) => {
      if (userData) {
        try {
          const { data } = await axios.get(
            `http://localhost:5134/api/CourseContraller/GetallUndefinedCoursesToSubAdmin?subAdminId=${userId}&pageNumber=${pageNum}&pageSize=${pageSize}`,{
              headers: {
                  Authorization: `Bearer ${userToken}`,
              },
          }
          );
          console.log(data);
          nonAccreditEvents(data.result.items);
          setTotalPages(data.result.totalPages);
        } catch (error) {
          console.log(error);
        }
      }
    };
  
    (() => {
      fetchEventsBeforeAccredittion();
    }, [nonAccreditEvents,userData, pageNumber, pageSize]);  // Fetch courses on mount and when page or size changes
    
    const handlePageSizeChange = (event) => {
      setPageSize(event.target.value);
      setPageNumber(1); // Reset to the first page when page size changes
    };
    
    const handlePageChange = (event, value) => {
      setPageNumber(value);
    };
  //  console.log(courses)
  
    const [searchTerm, setSearchTerm] = useState("");
  
    const handleSearch = (event) => {
      setSearchTerm(event.target.value);
    };
  
    const filteredEventsBeforeAccreditation = Array.isArray(nonAccreditEvents) ? nonAccreditEvents.filter((event) => {
      const matchesSearchTerm = Object.values(event).some(
        (value) =>
          typeof value === "string" &&
          value.toLowerCase().includes(searchTerm.toLowerCase())
      );
      return matchesSearchTerm;
    }) : [];
  
  
  return (
    <div>UnAccreditEvents</div>
  )
}
