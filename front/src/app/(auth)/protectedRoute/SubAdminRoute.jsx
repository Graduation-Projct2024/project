import React from 'react'
import { useRouter } from 'next/navigation'

export default function SubAdminRoute({children}) {
  const router = useRouter()
  if(localStorage.getItem("userToken")==null){
    return router.push('/Login')

 }
return children
}
