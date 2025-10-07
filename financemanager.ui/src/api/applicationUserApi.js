import axiosInstance from './axiosInstance';
export const getApplicationUsers = async () => {
  try {
    const response = await axiosInstance.get('/ApplicationUser'); // adjust endpoint
    console.log(response);
    return response.data;
  } catch (error) {
    console.error('Error fetching application users:', error);

  }
};
