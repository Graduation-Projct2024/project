'use client';
import axios from "axios";
import {useEffect, useState } from "react";
const { createContext } = require("react");
import CircularProgress from "@mui/material/CircularProgress";
import Box from "@mui/material/Box";


export let UserContext= createContext({});
export default function UserContextProvider({children}){
    const [userToken, setUserToken]= useState(null)
    const [userId, setUserId]= useState(null)
    const [userData, setUserData]= useState(null)
    const [loading, setLoading]= useState(false)
   console.log(userToken);
    const getUserID= async() =>{
        if(userToken){
            const {data} = await axios.get(`http://localhost:5134/api/UserAuth/GetUserIdFromToken`,
            {headers :{Authorization:`Bearer ${userToken}`}})
            if(data!=null){
            setUserId(data);
            // setLoading(false)
            console.log(userId);}            // setLoading(false);
            //  setUserData(data);
            // setLoading(false);
            
        }
    }
    if (loading) {
        return (
          <Box sx={{ display: "flex", justifyContent: "center" }}>
            <CircularProgress color="primary" />
          </Box>
        );
      }

<<<<<<< HEAD
    const getUserInfo= async() =>{
        if(userId){ 
=======
      const getUserInfo= async() =>{
        if(userId!=null){
>>>>>>> 10e28681990b17396ae7c9731d0953f8932308bf
            const {data} = await axios.get(`http://localhost:5134/api/UserAuth/GetProfileInfo?id=${userId}`,
            {headers :{Authorization:`Bearer ${userToken}`}}
            )
            setUserData(data.result);
          }}
            console.log(userData);

    
    useEffect(()=>{
        
            if(localStorage.getItem('userToken')!=null){
              setUserToken(localStorage.getItem('userToken'));
            }

        getUserID();
        getUserInfo();
    },[userToken,userId])
   
   
return <UserContext.Provider value={{userToken, setUserToken, userData, setUserData, userId , setUserId}}>
    {children}
</UserContext.Provider>

}