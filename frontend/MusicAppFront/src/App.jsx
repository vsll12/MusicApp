import { Routes, Route } from 'react-router-dom';

import Home from './pages/Home';
import Login from './features/auth/Login';
import MusicList from './features/music/MusicList';
import PlaylistList from './features/playlist/PlaylistList';

function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/login" element={<Login />} />
      <Route path="/music" element={<MusicList />} />
      <Route path="/playlist" element={<PlaylistList />} />
    </Routes>
  );
}

export default App;
