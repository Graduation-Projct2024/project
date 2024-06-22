'use client'
import React, { useContext, useEffect, useState } from 'react'
import Layout from '../AdminLayout/Layout'
import '../dashboard/dashboard.css'
import { UserContext } from '@/context/user/User';
import axios from 'axios';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowUpFromBracket, faFilter } from '@fortawesome/free-solid-svg-icons';
import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControl, InputLabel, MenuItem, Pagination, Select, Stack, useMediaQuery, useTheme } from '@mui/material';
import AddSkill from './AddSkill/AddSkill';
import '../dashboard/loading.css'

export default function AcademySkills() {

  useEffect(() => {
    if (typeof window !== 'undefined' && typeof document !== 'undefined') {
      require('bootstrap/dist/js/bootstrap.bundle.min.js');
    }
  }, []);
    const {userToken, setUserToken, userData}=useContext(UserContext);
    const [skills, setSkills] = useState([]);
    const [open, setOpen] = React.useState(false);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(0);
    // const [loading, setLoading] = useState(true);

    const theme = useTheme();
    const fullScreen = useMediaQuery(theme.breakpoints.down('md'));
    const handleClickOpen = () => {
           setOpen(true);
         };
    const handleClose = () => {
           setOpen(false);
         };

    const fetchSkills = async (pageNum = pageNumber, pageSizeNum = pageSize) => {
      if(userData){
        // setLoading(true);
      try{
      const { data } = await axios.get(`${process.env.NEXT_PUBLIC_EDUCODING_API}Skill/GetAllSkillOptions?pageNumber=${pageNum}&pageSize=${pageSize}`,{ headers: { Authorization: `Bearer ${userToken}` } });
      // setLoading(false)
      console.log(data);
      setSkills(data.result.items);
      setTotalPages(data.result.totalPages);
    }
      catch(error){
        console.log(error);
      }
      // finally{
      //   setLoading(false)
      // }
    }
    };

    useEffect(() => {
        fetchSkills();
    }, [skills,userData, pageNumber, pageSize]);
    const handlePageSizeChange = (event) => {
      setPageSize(event.target.value);
      setPageNumber(1); // Reset to the first page when page size changes
    };
    
    const handlePageChange = (event, value) => {
      setPageNumber(value);
    };

    const [searchTerm, setSearchTerm] = useState('');
  
    const handleSearch = (event) => {
      setSearchTerm(event.target.value);
    };


const filteredSkills = Array.isArray(skills) ? skills.filter((skill) => {
  const matchesSearchTerm = Object.values(skill).some(
    (value) =>
      typeof value === "string" &&
      value.toLowerCase().includes(searchTerm.toLowerCase())
  );
  return matchesSearchTerm;
}) : [];


  return (
    <Layout title="Skills in academy">
      {/* {loading ? (
        <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '80vh' }}>
          <CircularProgress />
          <div className='loading bg-white position-fixed vh-100 w-100 d-flex justify-content-center align-items-center z-3'>
      <span className="loader"></span>
    </div>
          {/* <CircularProgress /> */}
          {/* <div className='loading bg-white position-fixed vh-100 w-100 d-flex justify-content-center align-items-center z-3'> */}
      {/* <span className="loader"></span> */}
    {/* </div> */}
        {/* </Box> */}
        
      {/* ) : ( */} 

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
              <FormControl fullWidth className="w-50">
        <InputLabel id="page-size-select-label">Page Size</InputLabel>
        <Select
        className="justify-content-center"
          labelId="page-size-select-label"
          id="page-size-select"
          value={pageSize}
          label="Page Size"
          onChange={handlePageSizeChange}
        >
          <MenuItem value={5}>5</MenuItem>
          <MenuItem value={10}>10</MenuItem>
          <MenuItem value={15}>15</MenuItem>
        </Select>
      </FormControl>
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
                  <li className=''>
                      <a
                        className="dropdown-item"
                        href="#"
                       
                      >
                        All
                      </a>
                    </li>
                  </ul>
                </div>
                <FontAwesomeIcon icon={faArrowUpFromBracket} />
              </div>
            </form>

            {/* <button type="button" className="btn btn-primary ms-2 addEmp" data-bs-toggle="modal" data-bs-target="#staticBackdrop2">
              <span>+ Add new</span>
            </button> */}
               <Box
        sx={{
          display: 'flex',
          justifyContent: 'flex-end',
          p: 1,
          mr: 6,
        }}
      >
<Button sx={{px:2,m:0.5}} variant="contained" className='primaryBg' startIcon={<AddCircleOutlineIcon />} onClick={handleClickOpen}>
  Add New
</Button>
      </Box>


          </div>
        </nav>

      <Dialog
        fullScreen={fullScreen}
        open={open}
        onClose={handleClose}
        aria-labelledby="responsive-dialog-title"
        sx={{
          "& .MuiDialog-container": {
            "& .MuiPaper-root": {
              width: "100%",
              maxWidth: "500px!important",  
              height: "300px!important",
                        },
          },
          
        }}
        >
          <DialogTitle id="responsive-dialog-title" className='primaryColor text-center fw-bold' >
          {"Add New Skill"}
        </DialogTitle>

        <DialogContent>
        <Stack
   direction="row"
   spacing={1}
   sx={{ justifyContent: 'center',  alignContent: 'center'}}
    >
        <AddSkill setOpen={setOpen}/>
             </Stack>
        </DialogContent>
        <DialogActions>
         
         <Button onClick={handleClose} autoFocus>
           Cancle
         </Button>
       </DialogActions>
        </Dialog>

      </div>


      <table className="table">
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">Skill Name</th>
            
          </tr>
        </thead>
        <tbody>
          {filteredSkills.length ? (
            filteredSkills.map((skill,index) => (
              <tr key={skill.id}>
                <th scope="row">{++index}</th>
                <td>{skill.name}</td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="7">No Skills</td>
            </tr>
          )}
        </tbody>
      </table>
      <Stack spacing={2} sx={{ width: '100%', maxWidth: 500, margin: '0 auto' }}>
     
      <Pagination
      className="pb-3"
        count={totalPages}
        page={pageNumber}
        onChange={handlePageChange}
        variant="outlined"
        color="secondary"
        showFirstButton
        showLastButton
      />
    </Stack>
      </>
    
      {/* )}  */}
    </Layout>
  )
}