import { Link, useNavigate } from 'react-router-dom';
import { useContext, useEffect, useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { ApiContext } from '../context/ApiContext';
import CreatableSelect from 'react-select/creatable';
import '../styles/forms.css';

export default function MainAdmin() {
    const navigate = useNavigate();
    const [songs, setSongs] = useState([]);
    const [pickedSong, setPickedSong] = useState(null);

    const handleSubmit = async e => {
        e.preventDefault();
    };

    return (
        <div>
            <h2>Waiting for the next song</h2>
            <form onSubmit={handleSubmit} style={{ maxWidth: 400 }}>

                <CreatableSelect
                    required
                    isClearable
                    classNamePrefix="react-select"
                    options={songs.map(song => ({ label: song, value: song }))}
                    onChange={opt => setPickedSong(opt?.value || null)}
                    placeholder="Pick a aong"
                    styles={{ container: base => ({ ...base, margin: "0.5rem 0" }) }}
                />

                <button type="submit" style={{ margin: 15 }}>Let's start</button>
            </form>
        </div>
    );
}
