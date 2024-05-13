import Footer from '@/component/footer/Footer'
import Navbar from '@/component/navbar/Navbar'
import React from 'react'

export default function Layout({children}) {
  return (
    <div>
      <Navbar className=''/>
      <main className='py-5 mt-5'>{children}</main>
      <Footer/>
    </div>
  )
}
