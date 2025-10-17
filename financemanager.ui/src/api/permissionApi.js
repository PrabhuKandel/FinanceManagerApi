import axiosInstance from './axiosInstance';
export const getPermission = async () => {
  try {
    const response = await axiosInstance.get('/permissions/get-all');
    return response.data;
  } catch (error) {
    console.error('Error fetching roles ', error);

  }
};
