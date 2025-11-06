import axiosInstance from './axiosInstance';
export const getRoles = async () => {
  try {
    const response = await axiosInstance.get('/roles/get-all'); 
    return response.data;
  } catch (error) {
    console.error('Error fetching roles ', error);

  }
};
export const createRole = async (roleName, permissions = []) => {
  try {
    const payload = {
      roleName,
      permissions
    };
    const response = await axiosInstance.post('/roles', payload);
    return response.data;
  } catch (error) {
    console.error('Error creating role', error);
    throw error; // propagate error to handle it in UI
  }
}
