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
    const { connection, selectedSong } = useSocket();

    useEffect(() => {
        if (!connection) return;

        connection.on("SongSelected", (song) => {
            navigate("/live", { state: { song } });
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
            <h3>Waiting for next song</h3>
        </div>
    );
}
