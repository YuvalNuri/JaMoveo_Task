import { Link, useNavigate } from 'react-router-dom';
import { useContext, useEffect, useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { ApiContext } from '../context/ApiContext';
import CreatableSelect from 'react-select/creatable';
import '../styles/forms.css';
import { useSocket } from '../context/SocketContext';

export default function MainPlayer() {
    const navigate = useNavigate();
    const { local, server } = useContext(ApiContext);
    const { connection } = useSocket();

    useEffect(() => {
        if (!connection) return;

        connection.on("SongSelected", (songName) => {
            navigate("/live", { state: { songName } });
        });

        connection.on("SessionQuit", () => {
            navigate("/");
        });

        return () => {
            connection.off("SongSelected");
            connection.off("SessionQuit");
        };
    }, [connection]);

    return (
        <div>
            <h2>player</h2>

        </div>
    );
}
