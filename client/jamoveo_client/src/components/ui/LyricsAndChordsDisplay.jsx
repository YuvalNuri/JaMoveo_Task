import '../../styles/lyrics.css';

export default function LyricsAndChordsDisplay({
  lyricsData,
  role,
  isScrolling,
  isHebrew = false,
  containerRef
}) {
      if (!lyricsData) return <p>Loading lyrics...</p>;

  return (
    <div
      ref={containerRef}
      className={`lyrics-container ${isScrolling ? "scrolling" : ""}`}
      style={{
        direction: isHebrew ? "rtl" : "ltr",
        textAlign: isHebrew ? "right" : "left"
      }}
    >
      {lyricsData.map((line, idx) => (
        <div key={idx} className="line">
          {line.map((word, wIdx) => (
            <span key={wIdx} className="word-block">
              {role !== "singer" && (
                <div className="chord">{word.chords || ""}</div>
              )}
              <div className="lyrics">{word.lyrics}</div>
            </span>
          ))}
        </div>
      ))}
    </div>
  );
}
