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

// Create a new user
export const createApplicationUser = async (userData) => {
  try {
    const response = await axiosInstance.post('/Auth/register', userData);
    return response.data;
  } catch (error) {
    console.error('Error creating application user:', error);
    return Promise.reject(error.response.data);
  }
}
