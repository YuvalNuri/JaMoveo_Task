import React, { useContext } from "react";
import { Link } from "react-router-dom";
import "../../styles/header.css";
import { useAuth } from '../../context/AuthContext.jsx';


export default function Header() {
  const { user, logout } = useAuth();

  return (
    <div className="site-header">
      <div className="site-header__inner">
        <Link to="/" className="site-header__brand">
          JaMoveo
        </Link>
      </div>
      {!!user && 
      <div> 
        <span>Hello {user.username} | </span>
        <span onClick={logout}>Logout</span>
        </div>}
    </div>
  );
}
