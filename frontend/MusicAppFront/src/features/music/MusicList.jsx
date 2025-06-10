import React, { useEffect, useState } from 'react';
import { getAllMusic, deleteMusic, uploadMusic } from './MusicService';

const MusicList = () => {
  const [musicList, setMusicList] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  // Upload form state
  const [title, setTitle] = useState('');
  const [file, setFile] = useState(null);
  const [uploadedByUserId, setUploadedByUserId] = useState('');
  const [uploading, setUploading] = useState(false);
  const [uploadError, setUploadError] = useState('');

  // Base URL for music files
  const musicFileBaseUrl = 'http://localhost:5000/api/music/files';

  useEffect(() => {
    fetchMusicList();
  }, []);

  const fetchMusicList = async () => {
    setLoading(true);
    try {
      const data = await getAllMusic();
      setMusicList(data);
      setError('');
    } catch (err) {
      setError(err.message || 'Failed to load music');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    if (!window.confirm('Are you sure you want to delete this music?')) return;

    try {
      const success = await deleteMusic(id);
      if (success) {
        setMusicList(musicList.filter(m => m.id !== id));
      } else {
        alert('Music not found or already deleted.');
      }
    } catch (err) {
      alert('Failed to delete music: ' + err.message);
    }
  };

  const handleFileChange = (e) => {
    setFile(e.target.files[0]);
  };

  const handleUpload = async (e) => {
    e.preventDefault();
    if (!file || !title || !uploadedByUserId) {
      setUploadError('Please fill all fields and select a file.');
      return;
    }

    setUploading(true);
    setUploadError('');

    try {
      await uploadMusic({ file, title, uploadedByUserId });
      setTitle('');
      setFile(null);
      setUploadedByUserId('');
      await fetchMusicList();
    } catch (err) {
      setUploadError(err.message || 'Upload failed');
    } finally {
      setUploading(false);
    }
  };

  if (loading) return <p>Loading music...</p>;
  if (error) return <p style={{ color: 'red' }}>{error}</p>;

  return (
    <div>
      <h2>Upload New Music</h2>
      <form onSubmit={handleUpload} style={{ marginBottom: '30px' }}>
        <div>
          <label>
            Title: <br />
            <input 
              type="text" 
              value={title} 
              onChange={e => setTitle(e.target.value)} 
              required 
            />
          </label>
        </div>
        <div>
          <label>
            Uploaded By User ID: <br />
            <input 
              type="text" 
              value={uploadedByUserId} 
              onChange={e => setUploadedByUserId(e.target.value)} 
              required 
            />
          </label>
        </div>
        <div>
          <label>
            Select Audio File: <br />
            <input 
              type="file" 
              accept="audio/*" 
              onChange={handleFileChange} 
              required 
            />
          </label>
        </div>
        <button type="submit" disabled={uploading}>
          {uploading ? 'Uploading...' : 'Upload'}
        </button>
        {uploadError && <p style={{ color: 'red' }}>{uploadError}</p>}
      </form>

      <h2>Music List</h2>
      {musicList.length === 0 && <p>No music found.</p>}

      <ul>
        {musicList.map(music => (
          <li key={music.id} style={{ marginBottom: '30px' }}>
            <strong>{music.title}</strong><br />
            Uploaded by: {music.uploadedByUserId}<br />
            Uploaded at: {new Date(music.uploadedAt).toLocaleString()}<br />
            <audio controls style={{ marginTop: '8px', width: '300px' }}>
              <source src={`${musicFileBaseUrl}/${music.filePath}`} type="audio/mpeg" />
              Your browser does not support the audio element.
            </audio><br />
            <button 
              onClick={() => handleDelete(music.id)} 
              style={{ marginTop: '10px', backgroundColor: 'red', color: 'white' }}
            >
              Delete
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default MusicList;
