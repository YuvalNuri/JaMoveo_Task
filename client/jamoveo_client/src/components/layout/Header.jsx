import React from "react";
import { Link } from "react-router-dom";
import "../../styles/header.css"

export default function Header() {
  return (
    <header className="site-header">
      <div className="site-header__inner">
        <Link to="/" className="site-header__brand">
          JaMoveo
        </Link>
      </div>
    </header>
  );
}
