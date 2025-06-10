const BASE_URL = 'http://localhost:5000/api/music';

// Fetch all music entries
export const getAllMusic = async () => {
  const response = await fetch(BASE_URL);
  if (!response.ok) {
    throw new Error('Failed to fetch music list');
  }
  return await response.json();
};

// Fetch a single music entry by ID
export const getMusicById = async (id) => {
  const response = await fetch(`${BASE_URL}/${id}`);
  if (!response.ok) {
    if (response.status === 404) return null;
    throw new Error('Failed to fetch music');
  }
  return await response.json();
};

// Upload a music file + metadata (title, uploadedByUserId)
// Uses FormData and calls the /upload endpoint on backend
export const uploadMusic = async ({ file, title, uploadedByUserId }) => {
  const formData = new FormData();
  formData.append('file', file);
  formData.append('title', title);
  formData.append('uploadedByUserId', uploadedByUserId);

  const response = await fetch(`${BASE_URL}/upload`, {
    method: 'POST',
    body: formData,
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(errorText || 'Failed to upload music');
  }

  return await response.json();
};

// Create music entry without file upload (if needed)
export const createMusic = async (music) => {
  const response = await fetch(BASE_URL, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(music),
  });

  if (!response.ok) {
    throw new Error('Failed to create music');
  }
  return await response.json();
};

// Delete a music entry by ID
export const deleteMusic = async (id) => {
  const response = await fetch(`${BASE_URL}/${id}`, {
    method: 'DELETE',
  });

  if (!response.ok) {
    if (response.status === 404) return false;
    throw new Error('Failed to delete music');
  }
  return true;
};
