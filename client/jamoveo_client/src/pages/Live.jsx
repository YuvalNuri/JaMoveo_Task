import { useContext, useEffect, useState, useRef } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { useSocket } from "../context/SocketContext";
import LyricsAndChordsDisplay from "../components/ui/LyricsAndChordsDisplay";
import ScrollToggle from "../components/ui/ScrollToggle";
import QuitButton from "../components/ui/QuitButton";
import { ApiContext } from "../context/ApiContext";

export default function Live() {
    const { local } = useContext(ApiContext);
    const { state } = useLocation();
    const navigate = useNavigate();
    const { user } = useAuth();
    const { connection } = useSocket();
    const { song } = state;
    const lyricsContainerRef = useRef(null);

    const [lyricsData, setLyricsData] = useState(null);
    const [isScrolling, setIsScrolling] = useState(true);


    useEffect(() => {
        if (!lyricsContainerRef.current) return;

        let intervalId;

        if (isScrolling) {
            intervalId = setInterval(() => {
                lyricsContainerRef.current.scrollBy({
                    top: 1,
                    behavior: "smooth"
                });
            }, 30); // מהירות הגלילה
        }

        return () => {
            clearInterval(intervalId);
        };
    }, [isScrolling]);

    // fetch lyrics JSON
    useEffect(() => {
        if (!song?.fileName) return;

        fetch(`${local}api/Admin/song?fileName=${encodeURIComponent(song.fileName)}`)
            .then(res => res.json())
            .then(data => setLyricsData(data))
            .catch(err => console.error(err));
    }, [song?.fileName]);

    // listen for SessionQuit
    useEffect(() => {
        if (!connection) return;

        const handler = () => {
            if (user.role === "admin") {
                navigate("/mainadmin");
            } else {
                navigate("/mainplayer");
            }
        };

        connection.on("SessionQuit", handler);

        return () => {
            connection.off("SessionQuit", handler);
        };
    }, [connection, navigate, user.role]);

    function detectHebrew(lyricsData) {
        for (const line of lyricsData) {
            for (const word of line) {
                if (/[\u0590-\u05FF]/.test(word.lyrics)) {
                    return true;
                }
            }
        }
        return false;
    }

    // render after hooks
    if (!song || !lyricsData) return <p>Loading...</p>;

    return (
        <div className="live-container">
            <h1>{song.name}</h1>
            <h3>{song.artist}</h3>
            {song.img && <img src={song.img} alt={song.name} />}

            <LyricsAndChordsDisplay
                lyricsData={lyricsData}
                role={user.instrument}
                isScrolling={isScrolling}
                isHebrew={detectHebrew(lyricsData)}
                containerRef={lyricsContainerRef}
            />

            <ScrollToggle
                isScrolling={isScrolling}
                onToggle={() => setIsScrolling((prev) => !prev)}
            />

            {user.role === "admin" && <QuitButton />}
        </div>
    );
}
