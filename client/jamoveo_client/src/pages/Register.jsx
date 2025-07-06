import { Link, useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { useAuth } from '../context/AuthContext';

export default function Register() {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [form, setForm] = useState({ email: '', password: '' });

  const handleSubmit = async e => {
    e.preventDefault();
    if (await login(form)) navigate('/');   //  redirect הביתה
  };

  return (
    <section>
      <h2>הרשמה</h2>
      <form onSubmit={handleSubmit}>
        {/* אינפוטים... */}
        <button type="submit">סיימתי</button>
      </form>
    </section>
  );
}
