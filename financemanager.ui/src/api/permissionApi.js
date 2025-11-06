import axiosInstance from './axiosInstance';
export const getPermission = async () => {
  try {
    const response = await axiosInstance.get('/permissions/get-all');
    return response.data;
  } catch (error) {
    console.error('Error fetching roles ', error);

  }
};

export const assignRolePermissions = async (roleId, permissions) => {
  try {
    const response = await axiosInstance.post(`/roles/${roleId}/assign-permissions`, {
      roleId,
      permissions
    });
    return response.data;
  } catch (error) {
    console.error('Error assigning permissions:', error);
    throw error;
  }
};


export const getPermissionsByRole = async (roleId) => {

  try {
    const response = await axiosInstance.get(`/permissions/${roleId}/get-by-role`)
    return response.data;
  }
  catch (error) {
    console.error( error);
  }
}
