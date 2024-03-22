import React, { useContext, useEffect } from 'react';
import { useRouter } from 'next/navigation'
import { UserContext } from '../../../context/user/User.jsx';

export default function SubAdminRoute({children}) {
  const {userToken, setUserToken, userData}=useContext(UserContext);

  const router = useRouter()
  const SubAdminAuth=()=>{
    if(userData){
     
  if(localStorage.getItem("userToken")==null||userData.role!='SubAdmin'){
    return router.push('/Login')
    
 }}
}

 useEffect(() => {

  SubAdminAuth();
}, [userData]);
return children
}
