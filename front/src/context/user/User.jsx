'use client';
import axios from "axios";
import {useEffect, useState } from "react";
const { createContext } = require("react");


export let UserContext= createContext({});
export default function UserContextProvider({children}){
    const [userToken, setUserToken]= useState(null)
    const [userData, setUserData]= useState(null)
    const [Loading, setLoading]= useState(true)
   
    const getUserData= async() =>{
        if(userToken){
            const {data} = await axios.get(`http://localhost:5134/api/StudentsContraller`)
            console.log(data);
            // setUserData(data.user);
            // setLoading(false);

        }
    }
    useEffect(()=>{
        
            if(localStorage.getItem('userToken')!=null){
              setUserToken(localStorage.getItem('userToken'));
            }

        getUserData();
    },[userToken])
   
   
return <UserContext.Provider value={{userToken, setUserToken, userData, setUserData, Loading }}>
    {children}
</UserContext.Provider>

}