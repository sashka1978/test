import { Outlet } from 'react-router';
import './App.css';
import Navbar from './Components/Navbar/Navbar';
import { Toast, ToastContainer } from 'react-toastify/dist/components';
import "react-toastify/dist/ReactToastify.css";
function App() { 
  return (
   <>
   <Navbar/>
   <Outlet/>
   <ToastContainer/>
   </>
  );
}

export default App;
