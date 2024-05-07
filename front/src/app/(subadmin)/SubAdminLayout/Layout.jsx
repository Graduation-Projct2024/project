'use client';
import * as React from 'react';
import PropTypes from 'prop-types';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import CssBaseline from '@mui/material/CssBaseline';
import Divider from '@mui/material/Divider';
import Drawer from '@mui/material/Drawer';
import IconButton from '@mui/material/IconButton';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import MenuIcon from '@mui/icons-material/Menu';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import DashboardIcon from '@mui/icons-material/Dashboard';
import LibraryBooksIcon from '@mui/icons-material/LibraryBooks';
import LocalLibraryIcon from '@mui/icons-material/LocalLibrary';
import MenuBookIcon from '@mui/icons-material/MenuBook';
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import NotificationsIcon from '@mui/icons-material/Notifications';
import { deepPurple ,purple} from '@mui/material/colors';
import Link from '@mui/material/Link';
import { useRouter } from 'next/navigation'
import LogoutIcon from '@mui/icons-material/Logout';
import { Avatar, Stack } from '@mui/material';
import AdbIcon from '@mui/icons-material/Adb';
import './Layout.css'
import { UserContext } from '@/context/user/User';
import SubAdminRoute from '@/app/(auth)/protectedRoute/SubAdminRoute';
const drawerWidth = 240;

function Layout(props) {
  const { window } = props;
  const { children, title } = props;
  let { userToken, setUserToken ,userData,setUserData,userId} = React.useContext(UserContext);

  const [mobileOpen, setMobileOpen] = React.useState(false);
  const [isClosing, setIsClosing] = React.useState(false);
  const router = useRouter();
  
  const sidebarItems = [
  {
    name: "Dashboard",
    href: "/dashboardS",
    icon: DashboardIcon,
    
  },
  {
    name: "Profile",
    href: `/ProfileS/${userId}`,
    icon: AccountCircleIcon,
  },
    {
      name: "Notification",
      href: "/NotificationS",
      icon: NotificationsIcon,
    },
    // {
    //   name: "My Lectures",
    //   href: "/myLectures",
    //   icon: LocalLibraryIcon,
    // },
    // {
    //   name: "Book a Lecture",
    //   href: "/Lectures",
    //   icon: CalendarMonthIcon,
    // },
 
];
  const handleDrawerClose = () => {
    setIsClosing(true);
    setMobileOpen(false);
  };
  const logout = () => {
    localStorage.removeItem("userToken");
    setUserToken(null);
    setUserData(null);
    router.push("/login");
  };
  const handleDrawerTransitionEnd = () => {
    setIsClosing(false);
  };

  const handleDrawerToggle = () => {
    if (!isClosing) {
      setMobileOpen(!mobileOpen);
    }
  };

  const drawer = (
    <div className='side-drawer'>
      <Stack spacing={1} direction='row' alignItems='center'  justifyContent='center' sx={{ ml:6, my:3}}  >
            <AdbIcon sx={{/*color:deepPurple[50]*/}}/>
          <Link
            variant='h4'
            href="/"
            underline='none'
            // color={deepPurple[50]}
            flexGrow= {1}
            fontFamily= 'monospace'
            fontWeight= {700}
            mr={2}

          >
            LOGO
          </Link>
            </Stack>
      <Divider />
      <List sx={{ my:7, pt:5 }} >
              {sidebarItems.map(({ name, href, icon: Icon }) =>{
                return(
                <ListItem key={name} 
                // color={deepPurple[50]}
                >
                  <Link  className={`sidebar__link ${
                    router.pathname === href ? "sidebar__link--active" : ""
                  }`}
                  href={href}
                  // color={deepPurple[50]}
                  underline='none'>
                  <ListItemButton >
                    <ListItemIcon >
                    <Icon
                     sx={{/*color:deepPurple[50]*/}}
                     />
                    </ListItemIcon>
                    <ListItemText  primary={name} />
                  </ListItemButton>
                  </Link>
                </ListItem>
                )
                })}
            </List>
      <List sx={{my:'auto',pt:5}}>
                <ListItem >
                  <ListItemButton onClick={logout} sx={{/*color:deepPurple[50],*/ alignSelf: 'flex-end',pt:5}}>
                    <ListItemIcon>
                    <LogoutIcon sx={{/*color:deepPurple[50]*/}}/>
                    </ListItemIcon>
                    <ListItemText primary='Logout' />
                  </ListItemButton>
                </ListItem>
            </List>
    </div>
  );

  const container = window !== undefined ? () => window().document.body : undefined;

  return (
    <SubAdminRoute>
    <div className='side-drawer2'>
    <Box sx={{ display: 'flex' }}>
      <CssBaseline />
      <Stack
        position="absolute"
        sx={{
          width: { sm: `calc(100% - ${drawerWidth}px)` },
          ml: { sm: `${drawerWidth}px` },
        }}
      >
        <Toolbar sx={{ justifyContent: 'space-between' }}>
          <IconButton
            //color="inherit"
            aria-label="open drawer"
            edge="start"
            onClick={handleDrawerToggle}
            sx={{ mr: 2, display: { sm: 'none' } }}
          >
            <MenuIcon />
          </IconButton>
          <Typography variant="h4" noWrap component="div" >
          {title}
          </Typography>
          <Link  >
             <Avatar alt="Remy Sharp" sx={{ width: 60, height: 60,mr:7 ,mt:3,}} />
          </Link>
        </Toolbar>
      </Stack>
      <Box
        component="nav"
        sx={{ width: { sm: drawerWidth }, flexShrink: { sm: 0 }, /* bgcolor: purple[800],*/ }}
        aria-label="mailbox folders"
      >
        {/* The implementation can be swapped with js to avoid SEO duplication of links. */}
        <Drawer
          container={container}
          variant="temporary"
          open={mobileOpen}
          onTransitionEnd={handleDrawerTransitionEnd}
          onClose={handleDrawerClose}
          ModalProps={{
            keepMounted: true, // Better open performance on mobile.
          }}
          sx={{
            display: { xs: 'block', sm: 'none' },
            '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
            
          }}
        >
          {drawer}
        </Drawer>
        <Drawer
          variant="permanent"
          
          sx={{
            display: { xs: 'none', sm: 'block' },
            '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth /*,bgcolor: purple[800]*/, },
            
          }}
          open
        >
          {drawer}
        </Drawer>
      </Box>
      <Box
        component="main"
        sx={{ flexGrow: 1, p: 3, width: { sm: `calc(100% - ${drawerWidth}px)` } }}
      >
        <Toolbar />
        { children }
      </Box>
    </Box>
    </div>
    </SubAdminRoute>
   );
}

Layout.propTypes = {
  /**
   * Injected by the documentation to work in an iframe.
   * Remove this when copying and pasting into your project.
   */
  window: PropTypes.func,
};

export default Layout;