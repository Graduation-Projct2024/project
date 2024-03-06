import Image from "next/image";
import styles from "./page.module.css";
import Header from "../component/header/Header.jsx";
import Navbar from "../component/navbar/Navbar.jsx";

export default function Home() {
  return (
    <main className={styles.main}>
      <Navbar/>
      <Header/>
    </main>
  );
}
