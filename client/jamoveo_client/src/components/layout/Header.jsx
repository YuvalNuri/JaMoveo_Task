import React, { useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import "../../styles/header.css";
import { useAuth } from '../../context/AuthContext.jsx';


export default function Header() {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

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
          <span onClick={handleLogout}>Logout</span>
        </div>}
    </div>
  );
}
