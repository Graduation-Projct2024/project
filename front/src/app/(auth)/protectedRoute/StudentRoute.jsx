import React from 'react'
import { useRouter } from 'next/navigation'

export default function StudentRoute({children}) {
  const router = useRouter()
  if(localStorage.getItem("userToken")==null){
    return router.push('/Login')

 }
return children
}
