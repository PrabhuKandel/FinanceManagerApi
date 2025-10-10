import axiosInstance from './axiosInstance';
export const getRoles = async () => {
  try {
    const response = await axiosInstance.get('/roles/get-all'); // adjust endpoint
    return response.data;
  } catch (error) {
    console.error('Error fetching roles ', error);

  }
};
