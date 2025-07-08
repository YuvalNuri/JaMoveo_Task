import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import '../styles/forms.css';

export default function MainAdmin() {
    const navigate = useNavigate();
    const [query, setQuery] = useState('');

    const handleSubmit = e => {
        e.preventDefault();

        if (query.trim()) {
            navigate(`/results?query=${encodeURIComponent(query.trim())}`);
        }
    };

    return (
        <div>
            <h2>Search any song...</h2>
            <form onSubmit={handleSubmit} style={{ maxWidth: 400 }}>
                <input
                    className="form-input"
                    type="text"
                    value={query}
                    onChange={e => setQuery(e.target.value)}
                    placeholder="Type a song name"
                    required
                />
                <button type="submit" style={{ margin: 15 }}>Search</button>
            </form>
        </div>
    );
}
