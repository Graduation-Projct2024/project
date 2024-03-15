import { Inter } from "next/font/google";
import "./globals.css";
import 'bootstrap/dist/css/bootstrap.min.css';
import UserContextProvider from "../context/user/User.jsx";

const inter = Inter({ subsets: ["latin"] });

export const metadata = {
  title: "Academy",
  description: "Generated by create next app",
};

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body className={inter.className}>
 
<UserContextProvider>
{children}
</UserContextProvider></body>
    </html>
  );
}
