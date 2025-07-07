import { Link, useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { useAuth } from '../context/AuthContext';
import '../styles/forms.css';

export default function Login() {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [form, setForm] = useState({
    username: "",
    password: "",
  });

  const handleChange = (e) =>
    setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = async e => {
    e.preventDefault();
    if (await login(form)) navigate('/');
  };

  return (
    <div>
      <h2>Login</h2>
      <form onSubmit={handleSubmit} style={{ maxWidth: 400 }}>
        {/* username */}
        <input
          className="form-input"
          type="text"
          name="username"
          placeholder="Username"
          value={form.username}
          onChange={handleChange}
          required
          maxLength={50}
        />

        {/* password */}
        <input
          className="form-input"
          type="password"
          name="password"
          placeholder="Password (min 6)"
          value={form.password}
          onChange={handleChange}
          required
          minLength={6}
        />

        <button type="submit" style={{ margin: 15 }}>Finish</button>
      </form>
    </div>
  );
}
