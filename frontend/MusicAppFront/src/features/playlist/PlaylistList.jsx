import React, { useEffect, useState } from 'react';
import PlaylistService from './PlaylistService';


const PlaylistList = () => {
  const [playlists, setPlaylists] = useState([]);
  const [selectedPlaylistId, setSelectedPlaylistId] = useState(null);
  const [musicList, setMusicList] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function fetchPlaylists() {
      try {
        const response = await fetch('http://localhost:5000/api/playlist');
        if (!response.ok) throw new Error('Failed to fetch playlists');
        const data = await response.json();
        setPlaylists(data);
      } catch (err) {
        setError(err.message);
      }
    }
    fetchPlaylists();
  }, []);

  useEffect(() => {
    if (selectedPlaylistId === null) {
      setMusicList([]);
      return;
    }
    async function fetchMusic() {
      try {
        const music = await PlaylistService.getMusicByPlaylist(selectedPlaylistId);
        setMusicList(music);
        setError(null);
      } catch (err) {
        setError(err.message);
      }
    }
    fetchMusic();
  }, [selectedPlaylistId]);

  return (
    <div>
      <h2>Your Playlists</h2>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <ul>
        {playlists.map((playlist) => (
          <li key={playlist.id}>
            <button onClick={() => setSelectedPlaylistId(playlist.id)}>
              {playlist.name}
            </button>
          </li>
        ))}
      </ul>

      {selectedPlaylistId && (
        <>
          <h3>Music in Playlist</h3>
          {musicList.length === 0 ? (
            <p>No music found in this playlist.</p>
          ) : (
            <ul>
              {musicList.map((music) => (
                <li key={music.id}>{music.title}</li>
              ))}
            </ul>
          )}
        </>
      )}
    </div>
  );
};

export default PlaylistList;
