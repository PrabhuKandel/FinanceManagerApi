import axiosInstance from './axiosInstance';


export  const getDashboardSummary = async () => {
  try {
    const response = await axiosInstance.get('dashboard/summary');
    return response.data;
  } catch (error) {
    console.error('Error fetching dashboard summary:', error);
    throw error;
  }
};
