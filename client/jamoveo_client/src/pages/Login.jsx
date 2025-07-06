import { Link, useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { useAuth } from '../context/AuthContext';

export default function Login() {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [form, setForm] = useState({ email: '', password: '' });

  const handleSubmit = async e => {
    e.preventDefault();
    if (await login(form)) navigate('/');   //  redirect הביתה
  };

  return (
    <section>
      <h2>התחברות</h2>
      <form onSubmit={handleSubmit}>
        {/* אינפוטים... */}
        <button type="submit">התחבר</button>
      </form>
      <p>
        אין לך חשבון? <Link to="/register">להירשם</Link>
      </p>
    </section>
  );
}
