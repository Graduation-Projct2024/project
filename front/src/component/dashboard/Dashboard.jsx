
import { getSession } from 'next-auth/react';
import AdminDashboard from '../../app/(admin)/(dashboard)/AdminDashboard.jsx';
import SubAdminDashboard from '../../app/(subAdmin)/(dashboard)/SubAdminDashboard.jsx';
import StudentDashboard from '../../app/(student)/(dashboard)/StudentDashboard.jsx';
import InstructorDashboard from '../../app/(instructor)/(dashboard)/InstructorDashboard.jsx';

function Dashboard({ userRole }) {

  // Render the dashboard content based on the user's role
  return (
    <div>
      {userRole === 'admin' && <AdminDashboard />}
      {userRole === 'subAdmin' && <SubAdminDashboard />}
      {userRole === 'student' && <StudentDashboard />}
      {userRole === 'instructor' && <InstructorDashboard />}
    </div>
  );
}

export default Dashboard;

export async function getServerSideProps(context) {
    console.log(context);
  // Fetch session data
  const session = await getSession(context);

  // Get the user's role from the session
  const userRole = session?.user?.role || null;

  // If user has no role, redirect to login
  if (!userRole) {
    return {
      redirect: {
        destination: '/login',
        permanent: false,
      },
    };
  }

  // Return the user's role as props
  return {
    props: {
      userRole,
    },
  };
}
