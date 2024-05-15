import Image from "next/image";
import styles from "./page.module.css";
import Header from "../component/header/Header.jsx";
import Navbar from "../component/navbar/Navbar.jsx";
import AllCourses from "./(pages)/AllCourses/AllCourses";
import Footer from "@/component/footer/Footer";
import Testimonials from "./(pages)/Testimonials/page";
import Layout from "./(pages)/Layout/Layout";

export default function Home() {
  return (
    
      <Layout>
      <Header/>
      <Testimonials/>
      </Layout>   
  );
}
