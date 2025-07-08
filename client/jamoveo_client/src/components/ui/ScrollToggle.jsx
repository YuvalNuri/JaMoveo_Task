export default function ScrollToggle({ isScrolling, onToggle }) {
  return (
    <button className="floating-toggle" onClick={onToggle}>
      {isScrolling ? "Pause Scroll" : "Resume Scroll"}
    </button>
  );
}
