import { Link } from 'react-router-dom';

const Home = () => {
  return (
    <div style={styles.container}>
      <h1 style={styles.title}>Welcome to MyMusicApp 🎵</h1>
      <p style={styles.subtitle}>
        Discover your favorite tunes, create playlists, and enjoy your music anywhere.
      </p>

      <div style={styles.buttons}>
        <Link to="/music" style={{ ...styles.button, backgroundColor: '#1DB954' }}>
          Browse Music
        </Link>
        <Link to="/playlist" style={{ ...styles.button, backgroundColor: '#535353' }}>
          Your Playlists
        </Link>
        <Link to="/login" style={{ ...styles.button, backgroundColor: '#007AFF' }}>
          Sign In
        </Link>
      </div>
    </div>
  );
};

const styles = {
  container: {
    maxWidth: 700,
    margin: '60px auto',
    padding: 20,
    textAlign: 'center',
    fontFamily: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif",
  },
  title: {
    fontSize: '3rem',
    marginBottom: 10,
  },
  subtitle: {
    fontSize: '1.25rem',
    marginBottom: 40,
    color: '#555',
  },
  buttons: {
    display: 'flex',
    justifyContent: 'center',
    gap: '1rem',
  },
  button: {
    padding: '12px 24px',
    color: '#fff',
    textDecoration: 'none',
    fontWeight: '600',
    borderRadius: '6px',
    minWidth: 140,
    transition: 'background-color 0.3s ease',
  },
};

export default Home;
