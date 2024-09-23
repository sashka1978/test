import { Outlet } from 'react-router';
import './App.css';
import Navbar from './Components/Navbar/Navbar';
import { Toast, ToastContainer } from 'react-toastify/dist/components';
import "react-toastify/dist/ReactToastify.css";
import { UserProvider } from './Context/useAuth';
function App() { 
  return (
   <>
   <UserProvider>
      <Navbar/>
      <Outlet/>
      <ToastContainer/>
   </UserProvider>
   </>
  );
}

export default App;
