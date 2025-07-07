import { Link, useNavigate } from 'react-router-dom';
import { useContext, useEffect, useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { ApiContext } from '../context/ApiContext';
import CreatableSelect from 'react-select/creatable';
import '../styles/forms.css';

export default function MainAdmin() {
    const navigate = useNavigate();
    const { local, server } = useContext(ApiContext);

    return (
        <div>
            <h2>player</h2>

        </div>
    );
}
