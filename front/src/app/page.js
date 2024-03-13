'use client';
import Image from "next/image";
import styles from "./page.module.css";
import Header from "../component/header/Header.jsx";
import Navbar from "../component/navbar/Navbar.jsx";
import { UserContext } from "../context/user/User.jsx";
import { useContext, useEffect } from "react";

export default function Home() {
  let { setUserToken, userToken } = useContext(UserContext);
  useEffect(()=>{

    if(localStorage.getItem('userToken')!=null){
      setUserToken(localStorage.getItem('userToken'));
    }
  },[userToken])
  return (
    <main className={styles.main}>
      <Navbar/>
      <Header/>
    </main>
  );
}
