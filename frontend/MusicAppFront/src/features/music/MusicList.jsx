// src/features/music/MusicList.jsx

import React, { useEffect, useState } from 'react';
import { getAllMusic } from './MusicService';

const MusicList = () => {
  const [musicList, setMusicList] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchMusic = async () => {
      try {
        const data = await getAllMusic();
        setMusicList(data);
      } catch (err) {
        setError(err.message || 'Failed to load music');
      } finally {
        setLoading(false);
      }
    };

    fetchMusic();
  }, []);

  if (loading) return <p>Loading music...</p>;
  if (error) return <p style={{ color: 'red' }}>{error}</p>;

  if (musicList.length === 0) return <p>No music found.</p>;

  return (
    <div>
      <h2>Music List</h2>
      <ul>
        {musicList.map((music) => (
          <li key={music.id} style={{ marginBottom: '10px' }}>
            <strong>{music.title}</strong> <br />
            Uploaded by: {music.uploadedByUserId} <br />
            Uploaded at: {new Date(music.uploadedAt).toLocaleString()}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default MusicList;
