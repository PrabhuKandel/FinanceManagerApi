import axiosInstance from './axiosInstance';
export const getApplicationUsers = async () => {
  try {
    const response = await axiosInstance.get('/ApplicationUser'); 
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

export const assignRolesToUser = async (userId, roleNames) => {
  try {
    const payload = {
      applicationUserId: userId,
      roleNames: roleNames
    };
    const response = await axiosInstance.post('/ApplicationUser/assign-roles', payload);
    return response.data;
  } catch (error) {
    console.error('Error assigning roles to user:', error);
    throw error;
  }
}
