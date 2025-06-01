const API_BASE_URL = 'http://localhost:5000/api/musicplaylist'; 

const PlaylistService = {
  addMusicToPlaylist: async (musicId, playlistId) => {
    const response = await fetch(API_BASE_URL, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ musicId, playlistId })
    });

    if (!response.ok) {
      const error = await response.text();
      throw new Error(error || 'Failed to add music to playlist');
    }

    return response.json();
  },

  removeMusicFromPlaylist: async (musicId, playlistId) => {
    const url = `${API_BASE_URL}?musicId=${musicId}&playlistId=${playlistId}`;
    const response = await fetch(url, {
      method: 'DELETE'
    });

    if (!response.ok) {
      const error = await response.text();
      throw new Error(error || 'Failed to remove music from playlist');
    }
  },

  getMusicByPlaylist: async (playlistId) => {
    const url = `${API_BASE_URL}/${playlistId}`;
    const response = await fetch(url);

    if (!response.ok) {
      const error = await response.text();
      throw new Error(error || 'Failed to fetch music by playlist');
    }

    return response.json();
  }
};

export default PlaylistService;
