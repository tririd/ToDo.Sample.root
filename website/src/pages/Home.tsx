import React from 'react';
import { useNavigate  } from "react-router-dom";
import logo from '../logo.svg';
import '../App.css';
import { Button } from '@mui/material';


function Home() {
  const navigate = useNavigate();
  function handleGoToToDoList() {
    navigate("/ToDo");
  }
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Welcome to TO DO Sample Application.
        </p>
        <div className="App-subheader">- by TRIRID</div>
        <p>
          <Button variant="contained" onClick={() => handleGoToToDoList()} >TO DO List</Button>
        </p>
      </header>
    </div>
  );
}

export default Home;
