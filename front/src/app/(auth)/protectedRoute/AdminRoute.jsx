import React, { useContext, useEffect } from 'react';
import { useRouter } from 'next/navigation'
import { UserContext } from '../../../context/user/User.jsx';

export default function AdminRoute({children}) {
  const {userToken, setUserToken, userData}=useContext(UserContext);

  const router = useRouter()
  const AdminAuth=()=>{
    if(userData){
     
  if(localStorage.getItem("userToken")==null||userData.role!='Admin'){
    return router.push('/Login')
    
 }}
}

 useEffect(() => {

  AdminAuth();
}, [userData]);
return children
}
