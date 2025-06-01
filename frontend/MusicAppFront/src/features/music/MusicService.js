const BASE_URL = 'http://localhost:5000/api/music';

export const getAllMusic = async () => {
  const response = await fetch(BASE_URL);
  if (!response.ok) {
    throw new Error('Failed to fetch music list');
  }
  return await response.json(); 
};

export const getMusicById = async (id) => {
  const response = await fetch(`${BASE_URL}/${id}`);
  if (!response.ok) {
    if (response.status === 404) return null; 
    throw new Error('Failed to fetch music');
  }
  return await response.json(); 
};

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
